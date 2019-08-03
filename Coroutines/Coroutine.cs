using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace Coroutines {
    public delegate CoResult<T> WrappedCoroutine<T>(object args = null);

    public abstract partial class Coroutine : IDisposable {
        [ThreadStatic]
        private static Stack<Coroutine> _running;
        private static Stack<Coroutine> running {
            get {
                if (_running != null) {
                    return _running;
                }
                _running = new Stack<Coroutine>();
                return _running;
            }
        }

        public static WrappedCoroutine<T> Wrap<T>(IEnumerable<T> f, bool throwErrors = false) {
            var co = new Coroutine<T>(f) { ThrowErrors = throwErrors };
            var wrapped = new WrappedCoroutine<T>((args) => {
                if (co.status == CoStatus.Dead) {
                    co.Reset();
                }
                return co.Resume(args);
            });
            return wrapped;
        }

        public static Coroutine<T> Create<T>(IEnumerable<T> f, bool throwErrors = false) {
            return new Coroutine<T>(f) { ThrowErrors = throwErrors };
        }

        public static Func<TRet> MakeSynchronous<TRet>(Func<IEnumerable<TRet>> f) {
            Func<TRet> wrapped = () => {
                var co = new Coroutine<TRet>(f()) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }

        public static Func<object, TRet> MakeSyncWithArgs<TRet>(Func<IEnumerable<TRet>> f) {
            Func<object, TRet> wrapped = (args) => {
                var co = new Coroutine<TRet>(f()) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }

        protected CoStatus status;
        public CoStatus Status { get { return status; } }

        public abstract TArgs GetArgs<TArgs>(TArgs type, bool isDefaultValue = false);
        public abstract TArgs GetArgs<TArgs>(ref TArgs args, bool keepValueIfNull = false);
        public abstract void Wait(TimeSpan duration);
        public abstract void Wait(Func<bool> predicate, CoWaitType type);
        public abstract void Reset();
        public abstract object ReturnValue { get; }
        public abstract Exception Exception { get; }
        protected abstract void _Resume(object args = null);

        protected static void Push(Coroutine co) {
            running.Push(co);
        }
        
        protected static void Pop() {
            running.Pop();
        }

        public static TArgs Args<TArgs>(TArgs type, bool isDefaultValue = false) {
            var current = Current;
            if (current == null) {
                throw new CoroutineException("Can't retrieve coroutine arguments, no coroutine currently running");
            }
            return current.GetArgs(type, isDefaultValue);
        }

        public static TArgs Args<TArgs>(ref TArgs args, bool keepValueIfNull = false) {
            var current = Current;
            if (current == null) {
                throw new CoroutineException("Can't retrieve coroutine arguments, no coroutine currently running");
            }
            return current.GetArgs(ref args, keepValueIfNull);
        }

        public static void WaitFor(double seconds) {
            WaitFor(TimeSpan.FromSeconds(seconds));
        }

        public static void WaitFor(TimeSpan duration) {
            var current = Current;
            if (current == null) {
                throw new CoroutineException("Can't wait for duration, no coroutine currently running");
            }
            current.Wait(duration);
        }

        public static void WaitUntil(Func<bool> predicate) {
            var current = Current;
            if (current == null) {
                throw new CoroutineException("Can't wait until predicate, no coroutine currently running");
            }
            current.Wait(predicate, CoWaitType.Until);
        }

        public static void WaitWhile(Func<bool> predicate) {
            var current = Current;
            if (current == null) {
                throw new CoroutineException("Can't wait while predicate, no coroutine currently running");
            }
            current.Wait(predicate, CoWaitType.While);
        }

        public static Coroutine Current {
            get {
                return running.Count > 0 ? running.Peek() : null;
            }
        }

        public static bool Resume(Coroutine co, object args = null) {
            co._Resume(args);
            return co.status != CoStatus.Dead;
        }

        #region IDisposable
        private bool disposed = false;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposed)
                return;

            if (disposing) {
                // Free any other managed objects here.
            }

            // Free any unmanaged objects here.
            disposed = true;
        }

#if DOTNET
        ~Coroutine() {
            Dispose(false);
        }
#endif
        #endregion
    }

    public class Coroutine<T> : Coroutine {
        private IEnumerable<T> f;
        private IEnumerator<T> enumerator;
        private CoResult<T> result;
        private object args;

        private CoWaitType waitType;
        private Func<bool> waitPredicate;
        private TimeSpan waitDuration;
        private Stopwatch _timer;
        private Stopwatch timer { get { if (_timer != null) { return _timer; } _timer = new Stopwatch(); return _timer; } }

        public Coroutine(IEnumerable<T> f) {
            this.f = f;
            status = CoStatus.Suspended;
        }

        public Coroutine(Func<IEnumerable<T>> del) {
            this.f = del();
            status = CoStatus.Suspended;
        }

        private CoResult<T> MakeResult(T value, Exception e = null) {
            result = new CoResult<T>(value, e, status);
            return result;
        }

        protected override void _Resume(object args = null) {
            Resume(args);
        }

        public CoResult<T> Resume(object args = null) {
            this.args = args;

            if (status == CoStatus.Dead) {
                if (ThrowErrors) {
                    var deadEx = new CoroutineException("Cannot Resume dead Coroutine");
                    MakeResult(default(T), deadEx);
                    throw deadEx;
                }
                return MakeResult(default(T), new CoroutineException("Cannot Resume dead Coroutine"));
            }

            if (waitType != CoWaitType.None) {
                switch (waitType) {
                    default:
                        throw new CoroutineException("Invalid wait type in coroutine");
                    case CoWaitType.Time:
                        if (timer.Elapsed < waitDuration) {
                            status = CoStatus.Waiting;
                            return MakeResult(default(T));
                        }
                        break;
                    case CoWaitType.Until:
                    case CoWaitType.While:
                        var predicateResult = waitPredicate();
                        if ((predicateResult && waitType == CoWaitType.While) || (!predicateResult && waitType == CoWaitType.Until)) {
                            status = CoStatus.Waiting;
                            return MakeResult(default(T));
                        }
                        break;
                }

                waitType = CoWaitType.None;
                waitPredicate = null;
            }

            if (enumerator == null) {
                enumerator = f.GetEnumerator();
            }

            status = CoStatus.Running;
            
            bool running = false;
            Exception exception = null;

            Coroutine.Push(this);
            try {
                running = enumerator.MoveNext();
            }
            catch (Exception e) {
                if (ThrowErrors) {
                    status = CoStatus.Dead;
                    MakeResult(default(T), exception);
                    throw;
                }
                else {
                    exception = e;
                }
            }
            finally {
                Coroutine.Pop();
            }

            if (exception != null) {
                status = CoStatus.Dead;
                return MakeResult(default(T), exception);
            }
            else if (running) {
                status = CoStatus.Suspended;
                return MakeResult(enumerator.Current);
            }
            else {
                status = CoStatus.Dead;
                MakeResult(enumerator.Current, null);
                enumerator.Dispose();
                enumerator = null;
                return result;
            }
        }

        public override void Reset() {
            if (enumerator != null) {
                enumerator.Dispose();
                enumerator = null;
            }
            status = CoStatus.Suspended;
        }

        public override TArgs GetArgs<TArgs>(TArgs type, bool isDefaultValue = false) {
            if (args == null && isDefaultValue) {
                return type;
            }
            return (TArgs)args;
        }

        public override TArgs GetArgs<TArgs>(ref TArgs args, bool keepValueIfNull = false) {
            if (this.args == null && keepValueIfNull) {
                return args;
            }
            args = (TArgs)this.args;
            return args;
        }

        public override void Wait(TimeSpan duration) {
            timer.Restart();
            waitDuration = duration;
            waitType = CoWaitType.Time;
        }

        public override void Wait(Func<bool> predicate, CoWaitType type) {
            if (type != CoWaitType.Until && type != CoWaitType.While) {
                throw new CoroutineException("Invalid wait type for coroutine with predicate");
            }
            waitPredicate = predicate;
            waitType = type;
        }

        #region IDisposable
        private bool disposed = false;
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (disposed)
                return;

            if (disposing) {
                // Free any other managed objects here.
                if (enumerator != null) {
                    enumerator.Dispose();
                    enumerator = null;
                }
            }

            // Free any unmanaged objects here.
            disposed = true;
        }
        #endregion

        public CoResult<T> Result { get { return result; } }
        public bool ThrowErrors { get; set; }
        public override object ReturnValue { get { return result.ReturnValue; } }
        public override Exception Exception { get { return result.Exception; } }
    }
}

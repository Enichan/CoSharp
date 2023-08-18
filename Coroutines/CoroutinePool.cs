using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coroutines { 
    public class CoroutinePool : IEnumerable<Coroutine>, IDisposable {
        public delegate void OnErrorHandler(Coroutine co);
        public delegate void OnEndHandler(Coroutine co);

        private HashSet<Coroutine> coroutines;
        private List<Coroutine> deadList;
        private List<Coroutine> addList;
        private List<Coroutine> remList;
        private bool iterating;

        public event OnErrorHandler OnError;
        public event OnErrorHandler OnEnd;

        public CoroutinePool() {
            coroutines = new HashSet<Coroutine>();
            deadList = new List<Coroutine>();
            addList = new List<Coroutine>();
            remList = new List<Coroutine>();
        }

        public void Add(Coroutine co) {
            if (co == null) {
                throw new ArgumentNullException("co", "Coroutine added to CoroutinePool must not be null");
            }
            if (iterating) {
                if (!addList.Contains(co)) {
                    addList.Add(co);
                }
                if (remList.Contains(co)) {
                    remList.Remove(co);
                }
            }
            else {
                coroutines.Add(co);
            }
        }

        public void Remove(Coroutine co) {
            if (co == null) {
                return;
            }
            if (iterating) {
                if (addList.Contains(co)) {
                    addList.Remove(co);
                }
                if (!remList.Contains(co)) {
                    remList.Add(co);
                }
            }
            else {
                coroutines.Remove(co);
                co.Dispose();
            }
        }

        public void ResumeAll() {
            if (iterating) {
                throw new CoroutineException("Cannot call ResumeAll on CoroutinePool recursively");
            }

            for (int i = 0; i < addList.Count; i++) {
                coroutines.Add(addList[i]);
            }

            deadList.Clear();
            addList.Clear();
            remList.Clear();

            iterating = true;
            foreach (var co in coroutines) {
                if (!Coroutine.Resume(co)) {
                    if (co.Exception != null) {
                        var onError = OnError;
                        if (onError != null) {
                            onError(co);
                        }
                    }
                    var onEnd = OnEnd;
                    if (onEnd != null) {
                        onEnd(co);
                    }
                    // if user resets the coroutine in an event don't remove it
                    if (co.Status == CoStatus.Dead) {
                        deadList.Add(co);
                    }
                }
            }
            iterating = false;

            for (int i = 0; i < deadList.Count; i++) {
                var co = deadList[i];
                if (!addList.Contains(co)) {
                    coroutines.Remove(co);
                    co.Dispose();
                }
            }
            for (int i = 0; i < remList.Count; i++) {
                coroutines.Remove(remList[i]);
            }
        }

        public IEnumerator<Coroutine> GetEnumerator() {
            return coroutines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Clear() {
            foreach (var co in coroutines) {
                co.Dispose();
            }
            coroutines.Clear();
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
                Clear();
            }

            // Free any unmanaged objects here.
            disposed = true;
        }

#if DOTNET
        ~CoroutinePool() {
            Dispose(false);
        }
#endif
        #endregion

        public bool IsEmpty { get { return coroutines.Count == 0; } }
    }
}

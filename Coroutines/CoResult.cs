using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coroutines {
    public struct CoResult<T> : IEquatable<CoResult<T>> {
        public readonly T ReturnValue;
        public readonly Exception Exception;
        public readonly CoStatus Status;

        public CoResult(T returnValue, Exception e = null, CoStatus status = CoStatus.Running) {
            ReturnValue = returnValue;
            Exception = e;
            Status = status;
        }

        public static implicit operator bool(CoResult<T> ret) {
            return ret.Status != CoStatus.Dead;
        }

        public override string ToString() {
            return Exception != null ? Exception.ToString() : (ReturnValue != null ? ReturnValue.ToString() : "");
        }

        #region Equality
        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }

            CoResult<T>? b = obj as CoResult<T>?;
            if ((System.Object)b == null) {
                return false;
            }

            bool excEqual, valEqual;

            if (!Object.ReferenceEquals(Exception, null)) {
                if (!Object.ReferenceEquals(b.Value.Exception, null)) {
                    excEqual = b.Value.Exception.GetType() == Exception.GetType();
                }
                excEqual = false;
            }
            else {
                excEqual = Object.ReferenceEquals(b.Value.Exception, null);
            }
            if (!Object.ReferenceEquals(ReturnValue, null)) {
                valEqual = ReturnValue.Equals(b.Value.ReturnValue);
            }
            else {
                valEqual = Object.ReferenceEquals(b.Value.ReturnValue, null);
            }
            return excEqual && valEqual && b.Value.Status == Status;
        }

        public bool Equals(CoResult<T> b) {
            bool excEqual, valEqual;

            if (!Object.ReferenceEquals(Exception, null)) {
                if (!Object.ReferenceEquals(b.Exception, null)) {
                    excEqual = b.Exception.GetType() == Exception.GetType();
                }
                excEqual = false;
            }
            else {
                excEqual = Object.ReferenceEquals(b.Exception, null);
            }
            if (!Object.ReferenceEquals(ReturnValue, null)) {
                valEqual = ReturnValue.Equals(b.ReturnValue);
            }
            else {
                valEqual = Object.ReferenceEquals(b.ReturnValue, null);
            }
            return excEqual && valEqual && b.Status == Status;
        }

        public override int GetHashCode() {
            return
                (ReturnValue != null ? ReturnValue.GetHashCode() : 0) ^
                (Exception != null ? Exception.GetType().GetHashCode() : 0) ^
                Status.GetHashCode();
        }
        #endregion

        public bool Succeeded { get { return Exception == null; } }
    }
}

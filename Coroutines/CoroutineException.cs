using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coroutines {
#if DOTNET
    [Serializable]
#endif
    public class CoroutineException : Exception {
        public CoroutineException() { }
        public CoroutineException(string message) : base(message) { }
        public CoroutineException(string message, Exception inner) : base(message, inner) { }
#if DOTNET
        protected CoroutineException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }
}

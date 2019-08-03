using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coroutines {
    public enum CoStatus {
        Suspended,
        Waiting,
        Running,
        Dead
    }
}

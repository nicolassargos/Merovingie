using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class NoWorkerAvailableException: Exception
    {
        public NoWorkerAvailableException() { }
        public NoWorkerAvailableException(string message) : base(message) { }
        public NoWorkerAvailableException(string message, Exception inner) : base(message, inner) { }
        protected NoWorkerAvailableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

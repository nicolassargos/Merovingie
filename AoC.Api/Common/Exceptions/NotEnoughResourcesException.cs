using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class NotEnoughResourcesException : Exception
    {
        public NotEnoughResourcesException() { }
        public NotEnoughResourcesException(string message) : base(message) { }
        public NotEnoughResourcesException(string message, Exception inner) : base(message, inner) { }
        protected NotEnoughResourcesException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

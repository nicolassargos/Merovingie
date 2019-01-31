using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class NotEnoughUnitSlotsAvailableException : Exception
    {
        public NotEnoughUnitSlotsAvailableException() { }
        public NotEnoughUnitSlotsAvailableException(string message) : base(message) { }
        public NotEnoughUnitSlotsAvailableException(string message, Exception inner) : base(message, inner) { }
        protected NotEnoughUnitSlotsAvailableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

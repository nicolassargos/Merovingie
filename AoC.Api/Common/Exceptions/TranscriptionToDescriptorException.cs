using System;
using System.Runtime.Serialization;

namespace AoC.Api.Domain.UseCases
{
    [Serializable]
    public class TranscriptionToDescriptorException : Exception
    {
        public TranscriptionToDescriptorException()
        {
        }

        public TranscriptionToDescriptorException(string message) : base(message)
        {
        }

        public TranscriptionToDescriptorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TranscriptionToDescriptorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
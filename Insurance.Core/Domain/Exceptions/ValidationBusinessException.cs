using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Insurance.Core.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class ValidationBusinessException : Exception

    {
        public ValidationBusinessException()
        {
        }

        public ValidationBusinessException(string message)
            : base(message)
        {
        }

        public ValidationBusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ValidationBusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
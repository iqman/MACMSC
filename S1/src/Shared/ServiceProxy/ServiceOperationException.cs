using System;
using System.Runtime.Serialization;

namespace Shared.ServiceProxy
{
    /// <summary>
    /// Exception thrown when an error occurs while working with a service proxy.
    /// </summary>
    [Serializable]
    public class ServiceOperationException : Exception
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ServiceOperationException()
        {
        }

        /// <summary>
        /// Constructor taking a message.
        /// </summary>
        public ServiceOperationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor taking a message and an inner exception.
        /// </summary>
        public ServiceOperationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Constructor used for serializations.
        /// </summary>
        protected ServiceOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

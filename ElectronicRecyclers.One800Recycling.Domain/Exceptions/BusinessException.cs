using System;
using System.Runtime.Serialization;

namespace ElectronicRecyclers.One800Recycling.Domain.Exceptions
{
    /// <summary>
    /// An exception that was caught during the execution of the code.
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The exception message</param>
        public BusinessException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception</param>
        public BusinessException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected BusinessException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
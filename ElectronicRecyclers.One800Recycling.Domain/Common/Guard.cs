using System;

namespace ElectronicRecyclers.One800Recycling.Domain.Common
{
    /// <summary>
    /// Helper class for guard statements
    /// </summary>
    public class Guard
    {
        /// <summary>
        /// Will throw a <see cref="InvalidOperationException"/> if the assertion
        /// is true, with the specified message.
        /// </summary>
        /// <param name="assertion">If set to <c>true</c> [assertion].</param>
        /// <param name="message">The message of the exception.</param>
        /// <example>
        /// Sample usage:
        /// <code>
        /// Guard.Against(string.IsNullOrEmpty(name), "Name is required.");
        /// </code>
        /// </example>
        public static void Against(bool assertion, string message)
        {
            if (assertion == false)
                return;

            throw new InvalidOperationException(message);
        }

        /// <summary>
        /// Will throw an exception of type <typeparamref name="TException"/> if the
        /// assertion is true, with the specified message.
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="assertion">If set to <c>true</c> [assertion].</param>
        /// <param name="message"> The message of the exception.</param>
        /// <example>
        /// Sample usage:
        /// <code>
        /// <![CDATA[
        /// Guard.Against<ArgumentException>(string.IsNullOrEmpty(name), "Name is required.");
        /// ]]>
        /// </code>
        /// </example>
        public static void Against<TException>(bool assertion, string message) where TException : Exception
        {
            if (assertion == false)
                return;

            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }
}
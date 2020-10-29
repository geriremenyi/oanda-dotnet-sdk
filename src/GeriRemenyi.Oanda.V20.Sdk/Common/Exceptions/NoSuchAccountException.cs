namespace GeriRemenyi.Oanda.V20.Sdk.Common.Exceptions
{
    using System;

    /// <summary>
    /// The account is not found exception
    /// </summary>
    public class NoSuchAccountException : Exception
    {
        /// <summary>
        /// Constructor with a message parameter
        /// </summary>
        /// <param name="message">The message</param>
        public NoSuchAccountException(string message) : base(message) {}
    }
}

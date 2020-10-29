namespace GeriRemenyi.Oanda.V20.Sdk.Common.Exceptions
{
    using System;

    /// <summary>
    /// Connection initialization exception
    /// </summary>
    class ConnectionInitializationException : Exception
    {
        /// <summary>
        /// Constructor with message parameter
        /// </summary>
        /// <param name="message">The exception message</param>
        public ConnectionInitializationException(string message): base(message) {}
    }
}

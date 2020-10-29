namespace GeriRemenyi.Oanda.V20.Sdk
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Common;
    using GeriRemenyi.Oanda.V20.Sdk.Common.Types;

    /// <summary>
    /// Oanda api connection factory interface
    /// </summary>
    public interface IOandaApiConnectionFactory
    {
        /// <summary>
        /// Create a new Oanda API connection
        /// </summary>
        /// <param name="connectionType">Type of the connection to create</param>
        /// <param name="token">Token to use for the connection</param>
        /// <param name="dateTimeFormat">Date time format to use for the connection</param>
        /// <returns></returns>
        public IOandaApiConnection CreateConnection(OandaConnectionType connectionType, string token, DateTimeFormat dateTimeFormat = DateTimeFormat.RFC3339);
    }
}

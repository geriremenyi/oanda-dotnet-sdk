namespace GeriRemenyi.Oanda.V20.Sdk
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Common.Types;

    /// <summary>
    /// Implementation of the oanda API factory
    /// </summary>
    public class OandaApiConnectionFactory : IOandaApiConnectionFactory
    {
        /// <summary>
        /// Create a new Oanda API connection
        /// </summary>
        /// <param name="connectionType">Type of the connection to create</param>
        /// <param name="token">Token to use for the connection</param>
        /// <param name="dateTimeFormat">Date time format to use for the connection</param>
        public IOandaApiConnection CreateConnection(OandaConnectionType connectionType, string token, DateTimeFormat dateTimeFormat = DateTimeFormat.RFC3339)
        {
            return new OandaApiConnection(connectionType, token, dateTimeFormat);
        }
    }
}

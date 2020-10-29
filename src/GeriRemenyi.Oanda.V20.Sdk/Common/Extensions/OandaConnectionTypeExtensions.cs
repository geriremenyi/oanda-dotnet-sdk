namespace GeriRemenyi.Oanda.V20.Sdk.Common.Extensions
{
    using GeriRemenyi.Oanda.V20.Sdk.Common.Types;

    /// <summary>
    /// Extensions for the Oanda connection type enum
    /// </summary>
    public static class OandaConnectionTypeExtensions
    {
        /// <summary>
        /// Translate the enum value to an actual server base path
        /// </summary>
        /// <param name="connectionType">The connection type enum to translate</param>
        /// <returns>The server URL for the connection type</returns>
        public static string ToBasePath(this OandaConnectionType connectionType)
        {
            return connectionType switch
            {
                OandaConnectionType.FxTrade => "https://api-fxtrade.oanda.com/v3",
                _ => "https://api-fxpractice.oanda.com/v3",
            };
        }

    }
}

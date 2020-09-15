namespace GeriRemenyi.Oanda.V20.Sdk.Utilities
{
    public static class OandaServerExtensions
    {

        public static string ToBasePath(this OandaServer server)
        {
            switch (server) 
            {
                case OandaServer.FxTrade:
                    return "https://api-fxtrade.oanda.com/v3";
                default:
                case OandaServer.FxPractice:
                    return "https://api-fxpractice.oanda.com/v3";

            }
        }

    }
}

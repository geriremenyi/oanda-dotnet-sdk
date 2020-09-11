namespace GeriRemenyi.Oanda.V20.Sdk.Utilities
{
    public class OandaConnection
    {
        private readonly OandaServer _server;

        private readonly string _accessToken;

        public OandaConnection(OandaServer server, string accessToken)
        {
            _server = server;
            _accessToken = accessToken;
        }
    }
}

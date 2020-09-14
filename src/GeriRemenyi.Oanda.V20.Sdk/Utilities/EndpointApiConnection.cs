namespace GeriRemenyi.Oanda.V20.Sdk.Utilities
{
    using GeriRemenyi.Oanda.V20.Client.Client;
    using GeriRemenyi.Oanda.V20.Client.Model;

    public abstract class EndpointApiConnection
    {
        protected Configuration Configuration { get; }

        protected DateTimeFormat DateTimeFormat { get; }

        public EndpointApiConnection(Configuration configuration, DateTimeFormat dateTimeFormat)
        {
            Configuration = configuration;
            DateTimeFormat = dateTimeFormat;
        }
    }
}

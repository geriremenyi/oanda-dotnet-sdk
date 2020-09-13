namespace GeriRemenyi.Oanda.V20.Sdk.Instrument
{
    using GeriRemenyi.Oanda.V20.Client.Api;
    using GeriRemenyi.Oanda.V20.Client.Client;
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Utilities;

    public class InstrumentApiConnection : EndpointApiConnection
    {
        private readonly InstrumentApi _instrumentApi;

        public InstrumentApiConnection(Configuration configuration, DateTimeFormat dateTimeFormat) : base(configuration, dateTimeFormat)
        {
            _instrumentApi = new InstrumentApi(configuration);
        }

        public Instrument GetInstrument(InstrumentName instrumentName) 
        {
            return new Instrument(_instrumentApi, instrumentName, DateTimeFormat);
        }

    }
}

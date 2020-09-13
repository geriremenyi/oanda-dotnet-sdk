namespace GeriRemenyi.Oanda.V20.Sdk
{
    using GeriRemenyi.Oanda.V20.Client.Client;
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Account;
    using GeriRemenyi.Oanda.V20.Sdk.Exceptions;
    using GeriRemenyi.Oanda.V20.Sdk.Instrument;
    using GeriRemenyi.Oanda.V20.Sdk.Utilities;

    public class ApiConnection
    {
        private readonly Configuration _configuration;

        private readonly AccountApiConnection _accountApiConnection;

        private readonly InstrumentApiConnection _instrumentApiConnection;

        public ApiConnection(OandaServer server, string accessToken, DateTimeFormat dateTimeFormat = DateTimeFormat.RFC3339)
        {
            // Init configuration
            _configuration = new Configuration()
            {
                UserAgent = "OandaDotnetSdk",
                BasePath = server.ToBasePath(),
                AccessToken = accessToken
            };

            // Init account api connection
            _accountApiConnection = new AccountApiConnection(_configuration, dateTimeFormat);

            // Init instrument historical data connection
            _instrumentApiConnection = new InstrumentApiConnection(_configuration, dateTimeFormat);

            // Try to fetch accounts as a connection test
            try
            {
                _ = _accountApiConnection.Accounts;
            }
            catch 
            {
                throw new ApiConnectionException();
            }
        }

        public void GetAccounts()
        { 
        
        }

        public Instrument.Instrument GetInstrument(InstrumentName instrumentName)
        {
            return _instrumentApiConnection.GetInstrument(instrumentName);
        }
    }
}

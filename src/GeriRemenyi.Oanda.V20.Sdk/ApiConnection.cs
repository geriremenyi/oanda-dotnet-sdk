namespace GeriRemenyi.Oanda.V20.Sdk
{
    using GeriRemenyi.Oanda.V20.Client.Client;
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Account;
    using GeriRemenyi.Oanda.V20.Sdk.Instrument;
    using GeriRemenyi.Oanda.V20.Sdk.Utilities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
        }

        public async Task Test()
        {
            // Fire a reasonable small request as a test
            // It will throw an exception if the given credentials don't work
            await GetAccounts();
        }

        public async Task<List<AccountProperties>> GetAccounts()
        {
            return await _accountApiConnection.GetAccounts();
        }

        public Account.Account GetAccount(string accountId)
        {
            return _accountApiConnection.GetAccount(accountId);
        }

        public Instrument.Instrument GetInstrument(InstrumentName instrumentName)
        {
            return _instrumentApiConnection.GetInstrument(instrumentName);
        }
    }
}

namespace GeriRemenyi.Oanda.V20.Sdk.Account
{
    using GeriRemenyi.Oanda.V20.Sdk.Trade;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ClientModel = Client.Model;

    /// <summary>
    /// Account implementation
    /// </summary>
    public class Account : IAccount
    {
        /// <summary>
        /// Oanda API connection
        /// </summary>
        private readonly IOandaApiConnection _connection;

        /// <summary>
        /// Account id
        /// </summary>
        private readonly string _accountId;

        /// <summary>
        /// Account implementation constructor
        /// Sets up the connection.
        /// </summary>
        /// <param name="connection">The Oanda API connection</param>
        /// <param name="accountId">The account ID</param>
        public Account(IOandaApiConnection connection, string accountId)
        {
            _connection = connection;
            _accountId = accountId;
            Trades = new Trades(_connection, _accountId);
        }

        /// <summary>
        /// Get the full details of an account
        /// </summary>
        /// <returns>Full details of the account</returns>
        public ClientModel.Account GetDetails()
        {
            return _connection.AccountApi.GetAccount(_accountId).Account;
        }

        /// <summary>
        /// Get the full details of an account asynchronously
        /// </summary>
        /// <returns>Full details of the account</returns>
        public async Task<ClientModel.Account> GetDetailsAsync()
        {
            var accountResponse = await _connection.AccountApi.GetAccountAsync(_accountId);
            return accountResponse.Account;
        }

        /// <summary>
        /// Get summary of an account
        /// </summary>
        /// <returns>The summary of the account</returns>
        public ClientModel.AccountSummary GetSummary()
        {
            return _connection.AccountApi.GetAccountSummary(_accountId).Account;
        }

        /// <summary>
        /// Get summary of an account asynchronously
        /// </summary>
        /// <returns>The summary of the account</returns>
        public async Task<ClientModel.AccountSummary> GetSummaryAsync()
        {
            var accountSummaryResponse = await _connection.AccountApi.GetAccountSummaryAsync(_accountId);
            return accountSummaryResponse.Account;
        }

        /// <summary>
        /// Get the tradeable instruments for the account
        /// </summary>
        /// <returns>The enumerable collection of tradeable instruments for the account</returns>
        public IEnumerable<ClientModel.Instrument> GetTradeableInstruments()
        {
            return _connection.AccountApi.GetAccountInstruments(_accountId).Instruments;
        }

        /// <summary>
        /// Get the tradeable instruments for the account asynchronously
        /// </summary>
        /// <returns>The enumerable collection of tradeable instruments for the account</returns>
        public async Task<IEnumerable<ClientModel.Instrument>> GetTradeableInstrumentsAsync()
        {
            var accountInstrumentsResponse = await _connection.AccountApi.GetAccountInstrumentsAsync(_accountId);
            return accountInstrumentsResponse.Instruments;
        }

        /// <summary>
        /// Get changes for the account
        /// </summary>
        /// <returns>The different enumerable collections for the changes done in the account</returns>
        public ClientModel.AccountChanges GetChanges()
        {
            return _connection.AccountApi.GetAccountChanges(_accountId).Changes;
        }

        /// <summary>
        /// Get changes for the account asynchronously
        /// </summary>
        /// <returns>The different enumerable collections for the changes done in the account</returns>
        public async Task<ClientModel.AccountChanges> GetChangesAsync()
        {
            var accountChangesResponse = await _connection.AccountApi.GetAccountChangesAsync(_accountId);
            return accountChangesResponse.Changes;
        }

        /// <summary>
        /// Trade actions asssociated with this account
        /// </summary>
        public ITrades Trades { get; }
    }
}

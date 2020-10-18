namespace GeriRemenyi.Oanda.V20.Sdk.Account
{
    using GeriRemenyi.Oanda.V20.Client.Api;
    using GeriRemenyi.Oanda.V20.Client.Client;
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Utilities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AccountApiConnection : EndpointApiConnection
    {
        private readonly AccountApi _accountApi;

        public AccountApiConnection(Configuration configuration, DateTimeFormat dateTimeFormat) : base(configuration, dateTimeFormat)
        {
            _accountApi = new AccountApi(configuration);
        }

        public async Task<List<AccountProperties>> GetAccounts()
        {
            return (await _accountApi.GetAccountsAsync()).Accounts;
        }

        public Account GetAccount(string accountId)
        {
            return new Account(_accountApi, Configuration, accountId, DateTimeFormat);
        }
    }
}

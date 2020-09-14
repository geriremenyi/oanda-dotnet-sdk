namespace GeriRemenyi.Oanda.V20.Sdk.Account
{
    using GeriRemenyi.Oanda.V20.Client.Api;
    using GeriRemenyi.Oanda.V20.Client.Model;
    using System.Threading.Tasks;

    public class Account
    {
        private readonly AccountApi _accountApi;
        private readonly string _accountId;
        private readonly DateTimeFormat _dateTimeFormat;

        public Account(AccountApi accountApi, string accountId, DateTimeFormat dateTimeFormat)
        {
            _accountApi = accountApi;
            _accountId = accountId;
            _dateTimeFormat = dateTimeFormat;
        }

        public async Task<Client.Model.Account> GetDetails() 
        {
            return (await _accountApi.GetAccountAsync(_accountId, _dateTimeFormat)).Account;
        }

    }
}

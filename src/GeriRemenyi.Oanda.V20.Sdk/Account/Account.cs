namespace GeriRemenyi.Oanda.V20.Sdk.Account
{
    using GeriRemenyi.Oanda.V20.Client.Api;
    using GeriRemenyi.Oanda.V20.Client.Client;
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Client.Trades;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Account
    {
        private readonly AccountApi _accountApi;
        private readonly string _accountId;
        private readonly DateTimeFormat _dateTimeFormat;
        private readonly TradeApiConnection _tradingApiConnection;

        public Account(AccountApi accountApi, Configuration configuration, string accountId, DateTimeFormat dateTimeFormat)
        {
            _accountApi = accountApi;
            _accountId = accountId;
            _dateTimeFormat = dateTimeFormat;
            _tradingApiConnection = new TradeApiConnection(configuration, _dateTimeFormat, _accountId);
        }

        public async Task<Client.Model.Account> GetDetails() 
        {
            return (await _accountApi.GetAccountAsync(_accountId, _dateTimeFormat)).Account;
        }

        public async Task<IEnumerable<Trade>> GetOpenTrades()
        {
            return await _tradingApiConnection.GetOpenTrades();
        }

        public async Task<CreateOrderResponse> OpenTrade(InstrumentName instrument, long units, double trailingStopLossDistance)
        {
            return await _tradingApiConnection.OpenTrade(instrument, units, trailingStopLossDistance);
        }

    }
}

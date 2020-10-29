namespace GeriRemenyi.Oanda.V20.Client.Trades
{
    using GeriRemenyi.Oanda.V20.Client.Api;
    using GeriRemenyi.Oanda.V20.Client.Client;
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Utilities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TradeApiConnection : EndpointApiConnection
    {
        private readonly string _accountId;
        private readonly TradeApi _tradeApi;
        private readonly OrderApi _orderApi;

        public TradeApiConnection(Configuration configuration, DateTimeFormat dateTimeFormat, string accountId) : base(configuration, dateTimeFormat)
        {
            _accountId = accountId;
            _tradeApi = new TradeApi(configuration);
            _orderApi = new OrderApi(configuration);

        }

        public async Task<IEnumerable<Trade>> GetOpenTrades()
        {
            return (await _tradeApi.GetOpenTradesAsync(_accountId)).Trades;
        }

        public async Task<CreateOrderResponse> OpenTrade(InstrumentName instrument, long units, double trailingStopLossDistance)
        {
            // TODO: check if market is open
            // TODO: add more possibilities here to create different kinds of MarketOrders (take the profit, stop loss etc)
            // TODO: type is missing from orders
            var trailingStopLoss = new TrailingStopLossDetails(trailingStopLossDistance);
            var order = new MarketOrder(instrument, units, trailingStopLossOnFill: trailingStopLoss);
            return await _orderApi.CreateOrderAsync(_accountId, new CreateOrderRequest(new 
            { 
                Instrument = order.Instrument,
                Units = order.Units, 
                TrailingStopLossOnFill = order.TrailingStopLossOnFill,
                Type = "MARKET"
            }));
        }
    }
}

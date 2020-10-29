namespace GeriRemenyi.Oanda.V20.Sdk.Trade
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ClientModel = Client.Model;

    /// <summary>
    /// Trades object
    /// </summary>
    public class Trades : ITrades
    {
        /// <summary>
        /// The Oanda API connection
        /// </summary>
        private readonly IOandaApiConnection _connection;

        /// <summary>
        /// The account id within the connection
        /// </summary>
        private readonly string _accountId;

        /// <summary>
        /// Trades constructor to initialize the connection and accountId
        /// </summary>
        /// <param name="connection">The Oanda API connection</param>
        /// <param name="accountId">The account id within the connection</param>
        public Trades(IOandaApiConnection connection, string accountId)
        {
            _connection = connection;
            _accountId = accountId;
        }

        /// <summary>
        /// Get the open trades
        /// </summary>
        /// <returns>The enumerable collection of open trades for the account</returns>
        public IEnumerable<ClientModel.Trade> GetOpenTrades()
        {
            return _connection.TradeApi.GetOpenTrades(_accountId).Trades;
        }

        /// <summary>
        /// Get the open trades asynchronously
        /// </summary>
        /// <returns>The enumerable collection of open trades for the account</returns>
        public async Task<IEnumerable<ClientModel.Trade>> GetOpenTradesAsync()
        {
            var openTradesResponse = await _connection.TradeApi.GetOpenTradesAsync(_accountId);
            return openTradesResponse.Trades;
        }

        /// <summary>
        /// Open a new trade for the account
        /// </summary>
        /// <param name="instrument">The name of the instrument</param>
        /// <param name="direction">The direction of the trade</param>
        /// <param name="units">Units to buy</param>
        /// <param name="trailingStopLossInPips">The trailing stoploss in pips</param>
        /// <returns>The created trade order</returns>
        public ClientModel.CreateOrderResponse OpenTrade(ClientModel.InstrumentName instrument, TradeDirection direction, long units, int trailingStopLossInPips)
        {
            // TODO: check if market is open
            // TODO: add more possibilities here to create different kinds of MarketOrders (take the profit, stop loss etc)
            // TODO: type is missing from orders should be fixed on yaml level
            var trailingStopLoss = new TrailingStopLossDetails(ResolvePipToUnits(instrument, trailingStopLossInPips));
            var order = new MarketOrder(instrument, units * ResolveDiretionToMultiplier(direction), trailingStopLossOnFill: trailingStopLoss);
            return _connection.OrderApi.CreateOrder(_accountId, new CreateOrderRequest(new
            {
                Instrument = order.Instrument,
                Units = order.Units,
                TrailingStopLossOnFill = order.TrailingStopLossOnFill,
                Type = "MARKET"
            }));
        }

        /// <summary>
        /// Open a new trade for the account asynchronously
        /// </summary>
        /// <param name="instrument">The name of the instrument</param>
        /// <param name="direction">The direction of the trade</param>
        /// <param name="units">Units to buy</param>
        /// <param name="trailingStopLossInPips">The trailing stoploss in pips</param>
        /// <returns>The created trade order</returns>
        public async Task<ClientModel.CreateOrderResponse> OpenTradeAsync(ClientModel.InstrumentName instrument, TradeDirection direction, long units, int trailingStopLossInPips)
        {
            // TODO: check if market is open
            // TODO: add more possibilities here to create different kinds of MarketOrders (take the profit, stop loss etc)
            // TODO: type is missing from orders should be fixed on yaml level
            var trailingStopLoss = new TrailingStopLossDetails(ResolvePipToUnits(instrument, trailingStopLossInPips));
            var order = new MarketOrder(instrument, units * ResolveDiretionToMultiplier(direction), trailingStopLossOnFill: trailingStopLoss);
            return await _connection.OrderApi.CreateOrderAsync(_accountId, new CreateOrderRequest(new
            {
                Instrument = order.Instrument,
                Units = order.Units,
                TrailingStopLossOnFill = order.TrailingStopLossOnFill,
                Type = "MARKET"
            }));
        }

        /// <summary>
        /// Resolve the trade direction to Oanda friendly -/+ units multiplier for trade open
        /// </summary>
        /// <param name="direction">The direction of the trade</param>
        /// <returns>+/-1</returns>
        private int ResolveDiretionToMultiplier(TradeDirection direction)
        {
            return direction switch
            {
                TradeDirection.Short => -1,
                _ => 1
            };
        }

        /// <summary>
        /// Resolve a pip integer value to units partial (double) value
        /// </summary>
        /// <param name="instrument">The name of the instrument</param>
        /// <param name="pips">Number of pips</param>
        /// <returns></returns>
        private double ResolvePipToUnits(ClientModel.InstrumentName instrument, int pips)
        {
            // TODO resolver for other exotic instruments
            return instrument switch
            {
                InstrumentName.USD_JPY => pips * 0.01,
                _ => pips * 0.0001
            };
        }
    }
}

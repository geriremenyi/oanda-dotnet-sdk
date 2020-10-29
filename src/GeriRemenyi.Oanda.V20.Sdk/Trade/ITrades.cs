namespace GeriRemenyi.Oanda.V20.Sdk.Trade
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ClientModel = Client.Model;

    /// <summary>
    /// Interface for trades in the account
    /// </summary>
    public interface ITrades
    {
        /// <summary>
        /// Get the open trades
        /// </summary>
        /// <returns>The enumerable collection of open trades for the account</returns>
        public IEnumerable<ClientModel.Trade> GetOpenTrades();

        /// <summary>
        /// Get the open trades asynchronously
        /// </summary>
        /// <returns>The enumerable collection of open trades for the account</returns>
        public Task<IEnumerable<ClientModel.Trade>> GetOpenTradesAsync();

        /// <summary>
        /// Open a new trade for the account
        /// </summary>
        /// <param name="instrument">The name of the instrument</param>
        /// <param name="direction">The direction of the trade</param>
        /// <param name="units">Units to buy</param>
        /// <param name="trailingStopLossInPips">The trailing stoploss in pips</param>
        /// <returns>The created trade order</returns>
        public ClientModel.CreateOrderResponse OpenTrade(ClientModel.InstrumentName instrument, TradeDirection direction, long units, int trailingStopLossInPips);

        /// <summary>
        /// Open a new trade for the account asynchronously
        /// </summary>
        /// <param name="instrument">The name of the instrument</param>
        /// <param name="direction">The direction of the trade</param>
        /// <param name="units">Units to buy</param>
        /// <param name="trailingStopLossInPips">The trailing stoploss in pips</param>
        /// <returns>The created trade order</returns>
        public Task<ClientModel.CreateOrderResponse> OpenTradeAsync(ClientModel.InstrumentName instrument, TradeDirection direction, long units, int trailingStopLossInPips);
    }
}

namespace GeriRemenyi.Oanda.V20.Sdk.Instrument
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Common.Types;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Instrument interface
    /// </summary>
    public interface IInstrument
    {
        /// <summary>
        /// Get the candles for the instrument for the timeperiod passed
        /// </summary>
        /// <param name="granularity">Granularity of the candles</param>
        /// <param name="from">The time the candles should start with</param>
        /// <param name="to">The time the candles should end with</param>
        /// <param name="pricingComponents">The pricing components to consider (bid, mid, ask)</param>
        /// <returns>Enumerable collection of candles</returns>
        public IEnumerable<Candlestick> GetCandlesByTime(CandlestickGranularity granularity, DateTime from, DateTime to, IEnumerable<PricingComponent>? pricingComponents = null);

        /// <summary>
        /// Get the candles for the instrument for the timeperiod passed asynchronously
        /// </summary>
        /// <param name="granularity">Granularity of the candles</param>
        /// <param name="from">The time the candles should start with</param>
        /// <param name="to">The time the candles should end with</param>
        /// <param name="pricingComponents">The pricing components to consider (bid, mid, ask)</param>
        /// <returns>Enumerable collection of candles</returns>
        public Task<IEnumerable<Candlestick>> GetCandlesByTimeAsync(CandlestickGranularity granularity, DateTime from, DateTime to, IEnumerable<PricingComponent>? pricingComponents = null);

        /// <summary>
        /// Get the last N candles for the instrument
        /// </summary>
        /// <param name="granularity">Granularity of the candles</param>
        /// <param name="n">How many candles to get</param>
        /// <param name="pricingComponents">The pricing components to consider (bid, mid, ask)</param>
        /// <returns>Enumerable collection of candles</returns>
        public IEnumerable<Candlestick> GetLastNCandles(CandlestickGranularity granularity, int n, IEnumerable<PricingComponent>? pricingComponents = null);

        /// <summary>
        /// Get the last N candles for the instrument asynchronously
        /// </summary>
        /// <param name="granularity">Granularity of the candles</param>
        /// <param name="n">How many candles to get</param>
        /// <param name="pricingComponents">The pricing components to consider (bid, mid, ask)</param>
        /// <returns>Enumerable collection of candles</returns>
        public Task<IEnumerable<Candlestick>> GetLastNCandlesAsync(CandlestickGranularity granularity, int n, IEnumerable<PricingComponent>? pricingComponents = null);
    }
}

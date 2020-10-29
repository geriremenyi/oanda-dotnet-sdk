namespace GeriRemenyi.Oanda.V20.Sdk.Instrument
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Common.Extensions;
    using GeriRemenyi.Oanda.V20.Sdk.Common.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Instrument object
    /// </summary>
    public class Instrument : IInstrument
    {
        /// <summary>
        /// The Oanda API connection
        /// </summary>
        private readonly IOandaApiConnection _connection;

        /// <summary>
        /// The instrument name
        /// </summary>
        private readonly InstrumentName _instrument;
        
        /// <summary>
        /// Instrument constructor to setup the connection and instrument name
        /// </summary>
        /// <param name="connection">The Oanda API connection</param>
        /// <param name="instrument">The instrument name</param>
        public Instrument(IOandaApiConnection connection, InstrumentName instrument)
        {
            _connection = connection;
            _instrument = instrument;
        }

        /// <summary>
        /// Get the candles for the instrument for the timeperiod passed
        /// </summary>
        /// <param name="granularity">Granularity of the candles</param>
        /// <param name="from">The time the candles should start with</param>
        /// <param name="to">The time the candles should end with</param>
        /// <param name="pricingComponents">The pricing components to consider (bid, mid, ask)</param>
        /// <returns>Enumerable collection of candles</returns>
        public IEnumerable<Candlestick> GetCandlesByTime(CandlestickGranularity granularity, DateTime from, DateTime to, IEnumerable<PricingComponent>? pricingComponents = null)
        {
            // Always convert dates to UTC when working with Oanda
            from = from.ToUniversalTime();
            to = to.ToUniversalTime();

            if (granularity.AreMultipleQueriesRequired(from, to))
            {
                var candlesRange = granularity.ExplodeToMultipleCandleRanges(from, to);
                var candleResponses = new List<CandlesResponse>();

                foreach (var candleFromToRange in candlesRange)
                {
                    candleResponses.Add(
                        _connection.InstrumentApi.GetInstrumentCandles(
                            instrument: _instrument,
                            granularity: granularity,
                            price: ResolvePricingComponents(pricingComponents),
                            from: candleFromToRange.From.ToOandaDateTime(_connection.DateTimeFormat),
                            to: candleFromToRange.To.ToOandaDateTime(_connection.DateTimeFormat),
                            acceptDatetimeFormat: _connection.DateTimeFormat
                        )
                    );
                }

                return candleResponses.SelectMany(cr => cr.Candles);
            }
            else
            {
                var candlesResponse = _connection.InstrumentApi.GetInstrumentCandles(
                    instrument: _instrument,
                    granularity: granularity,
                    price: ResolvePricingComponents(pricingComponents),
                    from: from.ToOandaDateTime(_connection.DateTimeFormat),
                    to: to.ToOandaDateTime(_connection.DateTimeFormat),
                    acceptDatetimeFormat: _connection.DateTimeFormat
                );

                return candlesResponse.Candles;
            }
        }

        /// <summary>
        /// Get the candles for the instrument for the timeperiod passed asynchronously
        /// </summary>
        /// <param name="granularity">Granularity of the candles</param>
        /// <param name="from">The time the candles should start with</param>
        /// <param name="to">The time the candles should end with</param>
        /// <param name="pricingComponents">The pricing components to consider (bid, mid, ask)</param>
        /// <returns>Enumerable collection of candles</returns>
        public async Task<IEnumerable<Candlestick>> GetCandlesByTimeAsync(CandlestickGranularity granularity, DateTime from, DateTime to, IEnumerable<PricingComponent>? pricingComponents = null)
        {
            // Always convert dates to UTC when working with Oanda
            from = from.ToUniversalTime();
            to = to.ToUniversalTime();

            if (granularity.AreMultipleQueriesRequired(from, to))
            {
                var candlesRange = granularity.ExplodeToMultipleCandleRanges(from, to);
                var candleResponses = new List<CandlesResponse>();

                foreach (var candleFromToRange in candlesRange)
                {
                    candleResponses.Add(
                        await _connection.InstrumentApi.GetInstrumentCandlesAsync(
                            instrument: _instrument,
                            granularity: granularity,
                            price: ResolvePricingComponents(pricingComponents),
                            from: candleFromToRange.From.ToOandaDateTime(_connection.DateTimeFormat),
                            to: candleFromToRange.To.ToOandaDateTime(_connection.DateTimeFormat),
                            acceptDatetimeFormat: _connection.DateTimeFormat
                        )
                    );
                }

                return candleResponses.SelectMany(cr => cr.Candles);
            }
            else
            {
                var candlesResponse = await _connection.InstrumentApi.GetInstrumentCandlesAsync(
                    instrument: _instrument,
                    granularity: granularity,
                    price: ResolvePricingComponents(pricingComponents),
                    from: from.ToOandaDateTime(_connection.DateTimeFormat),
                    to: to.ToOandaDateTime(_connection.DateTimeFormat),
                    acceptDatetimeFormat: _connection.DateTimeFormat
                );

                return candlesResponse.Candles;
            }
        }

        /// <summary>
        /// Get the last N candles for the instrument
        /// </summary>
        /// <param name="granularity">Granularity of the candles</param>
        /// <param name="n">How many candles to get</param>
        /// <param name="pricingComponents">The pricing components to consider (bid, mid, ask)</param>
        /// <returns>Enumerable collection of candles</returns>
        public IEnumerable<Candlestick> GetLastNCandles(CandlestickGranularity granularity, int n, IEnumerable<PricingComponent>? pricingComponents = null)
        {
            if (n > 5000)
            {
                throw new Exception("Maximum 5000 candles are allowed");
            }

            var candlesResponse = _connection.InstrumentApi.GetInstrumentCandles(
                instrument: _instrument,
                granularity: granularity,
                count: n,
                price: ResolvePricingComponents(pricingComponents)
            );

            return candlesResponse.Candles;
        }

        /// <summary>
        /// Get the last N candles for the instrument asynchronously
        /// </summary>
        /// <param name="granularity">Granularity of the candles</param>
        /// <param name="n">How many candles to get</param>
        /// <param name="pricingComponents">The pricing components to consider (bid, mid, ask)</param>
        /// <returns>Enumerable collection of candles</returns>
        public async Task<IEnumerable<Candlestick>> GetLastNCandlesAsync(CandlestickGranularity granularity, int n, IEnumerable<PricingComponent>? pricingComponents = null)
        {
            if (n > 5000)
            {
                throw new Exception("Maximum 5000 candles are allowed");
            }

            var candlesResponse = await _connection.InstrumentApi.GetInstrumentCandlesAsync(
                instrument: _instrument,
                granularity: granularity,
                count: n,
                price: ResolvePricingComponents(pricingComponents)
            );

            return candlesResponse.Candles;
        }

        /// <summary>
        /// Resolve to Oanda string based pricing component based on the pricing component list
        /// </summary>
        /// <param name="pricingComponents">Pricing components to get</param>
        /// <returns>The string representation of the pricing components</returns>
        private string ResolvePricingComponents(IEnumerable<PricingComponent>? pricingComponents)
        {
            if (pricingComponents == null)
            {
                pricingComponents = new List<PricingComponent>()
                {
                    PricingComponent.Bid,
                    PricingComponent.Mid,
                    PricingComponent.Ask
                };
            }

            return string.Join("", pricingComponents.Select(pc => pc.ToPrice()));
        }
    }
}

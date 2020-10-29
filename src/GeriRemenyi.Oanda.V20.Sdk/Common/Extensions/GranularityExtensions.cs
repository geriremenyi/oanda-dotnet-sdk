namespace GeriRemenyi.Oanda.V20.Sdk.Common.Extensions
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Common.Types;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for granularities
    /// </summary>
    public static class GranularityExtensions
    {
        /// <summary>
        /// Current limitation on candles on Oanda side
        /// </summary>
        private const int OANDA_MAX_CANDLES = 5000;

        /// <summary>
        /// Is the timerange and granularity specifies more candles than Oanda can handle in one request?
        /// </summary>
        /// <param name="granularity">The granularity to check</param>
        /// <param name="from">From date time</param>
        /// <param name="to">To date time</param>
        /// <returns></returns>
        public static bool AreMultipleQueriesRequired(this CandlestickGranularity granularity, DateTime from, DateTime to)
        {
            // Always convert dates to UTC when working with Oanda
            from = from.ToUniversalTime();
            to = to.ToUniversalTime();

            // Check the number of candles
            return (granularity.GetNumberOfCandlesForTimeRange(from, to) > OANDA_MAX_CANDLES);
        }

        /// <summary>
        /// Explode a datetime range to multiple to fit in to Oanda API limitation
        /// </summary>
        /// <param name="granularity"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<DateTimeRange> ExplodeToMultipleCandleRanges(this CandlestickGranularity granularity, DateTime from, DateTime to)
        {
            // Always convert dates to UTC when working with Oanda
            from = from.ToUniversalTime();
            to = to.ToUniversalTime();

            // Make the timerange calulation
            var numberOfCandles = granularity.GetNumberOfCandlesForTimeRange(from, to);
            var numberOfQueries = Math.Ceiling(numberOfCandles / OANDA_MAX_CANDLES);
            var candleRanges = new List<DateTimeRange>();
            for (var i = 0; i <= (numberOfQueries - 1); i++)
            {
                candleRanges.Add(new DateTimeRange() {
                    From = from.AddSeconds(i * OANDA_MAX_CANDLES * granularity.GetInSeconds()),
                    To = (i == (numberOfQueries - 1)) ? to  : from.AddSeconds(((i + 1) * OANDA_MAX_CANDLES * granularity.GetInSeconds()) - 1)
                });
            }

            // Return the tim ranges created
            return candleRanges;
        }

        /// <summary>
        /// Calculate number of candles in a time range
        /// </summary>
        /// <param name="granularity">Granularity of the candles</param>
        /// <param name="from">The from date</param>
        /// <param name="to">The to date</param>
        /// <returns></returns>
        public static double GetNumberOfCandlesForTimeRange(this CandlestickGranularity granularity, DateTime from, DateTime to)
        {
            // Always convert dates to UTC when working with Oanda
            from = from.ToUniversalTime();
            to = to.ToUniversalTime();

            // Check not to enter endless loops
            if (from > to)
            {
                throw new ArgumentException("The 'to' time cannot be before the 'to' time.");
            }

            // Return the number of candles
            return ((to - from).TotalSeconds / granularity.GetInSeconds());
        }


        /// <summary>
        /// Get a granularity in seconds
        /// </summary>
        /// <param name="granularity">The granularity</param>
        /// <returns>How many seconds that granularity represents</returns>
        public static int GetInSeconds(this CandlestickGranularity granularity)
        {
            return granularity switch
            {
                CandlestickGranularity.S5 => 5,
                CandlestickGranularity.S10 => 10,
                CandlestickGranularity.S15 => 15,
                CandlestickGranularity.S30 => 30,
                CandlestickGranularity.M1 => 60,
                CandlestickGranularity.M2 => (60 * 2),
                CandlestickGranularity.M4 => (60 * 4),
                CandlestickGranularity.M5 => (60 * 5),
                CandlestickGranularity.M10 => (60 * 10),
                CandlestickGranularity.M15 => (60 * 15),
                CandlestickGranularity.M30 => (60 * 30),
                CandlestickGranularity.H1 => (60 * 60),
                CandlestickGranularity.H2 => (60 * 60 * 2),
                CandlestickGranularity.H3 => (60 * 60 * 3),
                CandlestickGranularity.H4 => (60 * 60 * 4),
                CandlestickGranularity.H6 => (60 * 60 * 6),
                CandlestickGranularity.H8 => (60 * 60 * 8),
                CandlestickGranularity.H12 => (60 * 60 * 12),
                CandlestickGranularity.D => (60 * 60 * 24),
                CandlestickGranularity.W => (60 * 60 * 24 * 7),
                CandlestickGranularity.M => (60 * 60 * 24 * 31),
                _ => 5,
            };
        }

    }
}

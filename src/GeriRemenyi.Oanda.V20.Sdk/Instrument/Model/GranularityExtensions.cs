using GeriRemenyi.Oanda.V20.Client.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GeriRemenyi.Oanda.V20.Client.Instrument.Model
{
    public static class GranularityExtensions
    {
        private const int OANDA_MAX_CANDLES = 5000;

        public static bool AreMultipleQueriesRequired(this CandlestickGranularity granularity, DateTime utcFrom, DateTime utcTo)
        {
            return (granularity.GetNumberOfCandlesForTimeRange(utcFrom, utcTo) > OANDA_MAX_CANDLES);
        }

        public static IEnumerable<CandlesRange> ExplodeToMultipleCandleRanges(this CandlestickGranularity granularity, DateTime utcFrom, DateTime utcTo)
        {
            var numberOfCandles = granularity.GetNumberOfCandlesForTimeRange(utcFrom, utcTo);
            var numberOfQueries = Math.Ceiling(numberOfCandles / OANDA_MAX_CANDLES);
            var candleRanges = new List<CandlesRange>();

            for (var i = 0; i <= (numberOfQueries - 1); i++)
            {
                candleRanges.Add(new CandlesRange() {
                    UtcFrom = utcFrom.AddSeconds(i * OANDA_MAX_CANDLES * granularity.GetInSeconds()),
                    UtcTo = (i == (numberOfQueries - 1)) ? utcTo  : utcFrom.AddSeconds(((i + 1) * OANDA_MAX_CANDLES * granularity.GetInSeconds()) - 1)
                });
            }

            return candleRanges;
        }

        public static double GetNumberOfCandlesForTimeRange(this CandlestickGranularity granularity, DateTime from, DateTime to)
        {
            if (from > to)
            {
                throw new ArgumentException("The 'to' time cannot be before the 'to' time.");
            }

            return ((to - from).TotalSeconds / granularity.GetInSeconds());
        }

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

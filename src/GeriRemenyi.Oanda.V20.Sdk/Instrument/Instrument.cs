namespace GeriRemenyi.Oanda.V20.Sdk.Instrument
{
    using GeriRemenyi.Oanda.V20.Client.Api;
    using GeriRemenyi.Oanda.V20.Client.Instrument.Model;
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Instrument
    {
        private readonly InstrumentApi _instrumentApi;
        private readonly InstrumentName _instrumentName;
        private readonly DateTimeFormat _dateTimeFormat;


        public Instrument(InstrumentApi instrumentApi, InstrumentName instrumentName, DateTimeFormat dateTimeFormat)
        {
            _instrumentApi = instrumentApi;
            _instrumentName = instrumentName;
            _dateTimeFormat = dateTimeFormat;
        }

        public async Task<IEnumerable<Candlestick>> GetCandles(
            CandlestickGranularity granularity, 
            DateTimeOffset from, 
            DateTimeOffset to, 
            IEnumerable<PricingComponent>? pricingComponents = null
        )
        {
            if (granularity.AreMultipleQueriesRequired(from, to))
            {
                var candlesRange = granularity.ExplodeToMultipleCandleRanges(from, to);
                var candleResponses = new List<CandlesResponse>();

                foreach (var candleFromToRange in candlesRange)
                {
                    candleResponses.Add(
                        await _instrumentApi.GetInstrumentCandlesAsync(
                            instrument: _instrumentName,
                            granularity: granularity,
                            price: ResolvePricingComponents(pricingComponents),
                            from: candleFromToRange.From.ToOandaDateTime(_dateTimeFormat),
                            to: candleFromToRange.To.ToOandaDateTime(_dateTimeFormat)
                        )
                    );
                }

                return candleResponses.SelectMany(cr => cr.Candles);
            }
            else
            {
                var candlesResponse = await _instrumentApi.GetInstrumentCandlesAsync(
                    instrument: _instrumentName,
                    granularity: granularity,
                    price: ResolvePricingComponents(pricingComponents),
                    from: from.ToOandaDateTime(_dateTimeFormat),
                    to: to.ToOandaDateTime(_dateTimeFormat)
                );

                return candlesResponse.Candles;
            }
        }

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

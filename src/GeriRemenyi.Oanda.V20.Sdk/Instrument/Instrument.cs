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

        public async Task<CandlesResponse> GetCandles(
            CandlestickGranularity granularity, 
            DateTime from, 
            DateTime to, 
            IEnumerable<PricingComponent>? pricingComponents = null
        )
        {
            // TODO replace granularity to enum when fix is in
            return await _instrumentApi.GetInstrumentCandlesAsync(
                instrument: _instrumentName,
                granularity: granularity,
                price: ResolvePricingComponents(pricingComponents),
                from: from.ToOandaDateTime(_dateTimeFormat),
                to: to.ToOandaDateTime(_dateTimeFormat)
            );
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

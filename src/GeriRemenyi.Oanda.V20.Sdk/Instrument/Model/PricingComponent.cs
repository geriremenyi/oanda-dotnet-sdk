namespace GeriRemenyi.Oanda.V20.Client.Instrument.Model
{
    using System.ComponentModel;

    public enum PricingComponent
    {
        [Description("B")]
        Bid,

        [Description("A")]
        Ask,

        [Description("M")]
        Mid
    }
}

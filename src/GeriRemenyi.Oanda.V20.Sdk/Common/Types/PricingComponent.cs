namespace GeriRemenyi.Oanda.V20.Sdk.Common.Types
{
    using System.ComponentModel;

    /// <summary>
    /// Different pricing components
    /// </summary>
    public enum PricingComponent
    {
        /// <summary>
        /// The bid pricing component
        /// </summary>
        [Description("B")]
        Bid,

        /// <summary>
        /// The mid pricing component
        /// </summary>
        [Description("M")]
        Mid,

        /// <summary>
        /// The ask pricing component
        /// </summary>
        [Description("A")]
        Ask
    }
}

namespace GeriRemenyi.Oanda.V20.Sdk.Common.Extensions
{
    using GeriRemenyi.Oanda.V20.Sdk.Common.Types;
    using System.ComponentModel;

    /// <summary>
    /// Pricing component extension methods
    /// </summary>
    public static class PricingComponentExtensions
    {
        /// <summary>
        /// Pricing component enum to Oanda pricing component based on their Description attribute
        /// </summary>
        /// <param name="pricingComponent">The pricing component to convert</param>
        /// <returns></returns>
        public static string ToPrice(this PricingComponent pricingComponent)
        {
            var type = pricingComponent.GetType();
            var field = type.GetField(pricingComponent.ToString());
            DescriptionAttribute[]? attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attributes == null || attributes.Length == 0 ? string.Empty : attributes[0].Description;
        }
    }
}

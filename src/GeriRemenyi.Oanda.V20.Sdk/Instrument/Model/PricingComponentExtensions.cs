namespace GeriRemenyi.Oanda.V20.Client.Instrument.Model
{
    using System.ComponentModel;

    public static class PricingComponentExtensions
    {
        public static string ToPrice(this PricingComponent pricingComponent)
        {
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[]) pricingComponent
                    .GetType()
                    .GetField(pricingComponent.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}

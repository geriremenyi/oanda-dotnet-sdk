namespace GeriRemenyi.Oanda.V20.Sdk.Common.Extensions
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using System;
    using System.Xml;

    /// <summary>
    /// DateTime extension methods
    /// </summary>
    public static class DateTimeExtensions
    {

        /// <summary>
        /// Convert a time to Oanda time string
        /// </summary>
        /// <param name="time">The time to convert</param>
        /// <param name="dateTimeFormat">The format to convert to</param>
        /// <returns></returns>
        public static string ToOandaDateTime(this DateTime time, DateTimeFormat dateTimeFormat) 
        {
            // Always convert dates to UTC when working with Oanda
            time = time.ToUniversalTime();

            // Convert
            switch (dateTimeFormat)
            {
                case DateTimeFormat.UNIX:
                    var beginningOfTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
                    var unixTimestamp = Convert.ToInt64((time - beginningOfTime).TotalSeconds);
                    return unixTimestamp.ToString();
                default:
                case DateTimeFormat.RFC3339:
                    return XmlConvert.ToString(time, XmlDateTimeSerializationMode.RoundtripKind);
            }
        }

    }
}

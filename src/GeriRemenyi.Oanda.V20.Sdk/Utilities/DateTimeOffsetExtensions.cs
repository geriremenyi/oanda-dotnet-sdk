namespace GeriRemenyi.Oanda.V20.Sdk.Utilities
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using System;
    using System.Xml;

    public static class DateTimeOffsetExtensions
    {

        public static string ToOandaDateTime(this DateTimeOffset dateTimeOffset, DateTimeFormat dateTimeFormat) 
        {
            switch (dateTimeFormat)
            {
                case DateTimeFormat.UNIX:
                    var beginningOfTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
                    var unixTimestamp = Convert.ToInt64((dateTimeOffset - beginningOfTime).TotalSeconds);
                    return unixTimestamp.ToString();
                default:
                case DateTimeFormat.RFC3339:
                    return XmlConvert.ToString(dateTimeOffset.DateTime, XmlDateTimeSerializationMode.RoundtripKind);
            }
        }

    }
}

namespace GeriRemenyi.Oanda.V20.Sdk.Utilities
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using System;
    using System.Xml;

    public static class DateTimeExtensions
    {

        public static string ToOandaDateTime(this DateTime dateTime, DateTimeFormat dateTimeFormat) 
        {
            switch (dateTimeFormat)
            {
                case DateTimeFormat.UNIX:
                    var beginningOfTime = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
                    var unixTimestamp = Convert.ToInt64((dateTime - beginningOfTime).TotalSeconds);
                    return unixTimestamp.ToString();
                default:
                case DateTimeFormat.RFC3339:
                    return XmlConvert.ToString(dateTime, XmlDateTimeSerializationMode.RoundtripKind);
            }
        }

    }
}

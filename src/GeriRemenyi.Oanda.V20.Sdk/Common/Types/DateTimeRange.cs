namespace GeriRemenyi.Oanda.V20.Sdk.Common.Types
{
    using System;

    /// <summary>
    /// Represents a timerange with start and end date time
    /// </summary>
    public class DateTimeRange
    {
        /// <summary>
        /// The start time of the range
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// The end time of the range
        /// </summary>
        public DateTime To { get; set; }
    }
}

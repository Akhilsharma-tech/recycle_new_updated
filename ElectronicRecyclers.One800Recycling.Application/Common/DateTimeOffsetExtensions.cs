using System;

namespace ElectronicRecyclers.One800Recycling.Application.Common
{
    public static class DateTimeOffsetExtensions
    {
        public static string ToRecyclerDateString(this DateTimeOffset date)
        {
            return date.ToString("MMM d, yyyy");
        }

        public static string ToRecyclerDateTimeString(this DateTimeOffset date)
        {
            return date.ToString("MMM d, yyyy h:mm tt");
        }
    }
}
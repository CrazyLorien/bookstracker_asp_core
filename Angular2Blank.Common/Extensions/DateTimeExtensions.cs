using System;

namespace Angular2Blank.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUnixTimeSeconds(this long unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var unixTimeSeconds = dateTime.Subtract(dtDateTime).TotalSeconds;
            return (long)unixTimeSeconds;
        }
    }
}

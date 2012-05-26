using System;

namespace Currency
{
    public static class DateTimeExtensions
    {
        public static DateTime UnixTimeStampToDateTime(this double timeStamp)
        {
            var baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return baseDateTime.AddSeconds(timeStamp).ToLocalTime();
        }
    }
}


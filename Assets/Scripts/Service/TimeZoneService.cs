using System;

namespace ClockApp
{
    public static class TimeZoneService
    {
        public static DateTime ConvertDateTime(TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }

        public static string StandardName(TimeZoneInfo timeZoneInfo)
        {
            return timeZoneInfo.StandardName;
        }
    }
}
using System;
using NUnit.Framework;

namespace ClockApp.Test.Editor
{
    public class ClockTest
    {
        static System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> _timeZoneInfoList = TimeZoneInfo.GetSystemTimeZones();
        
        [TestCaseSource(nameof(_timeZoneInfoList))]
        public void ConvertDateTime_Match(TimeZoneInfo timeZoneInfo)
        {
            var timeZoneServiceDateTime = TimeZoneService.ConvertDateTime(timeZoneInfo);
            var timeZoneInfoConvertTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            TimeSpan serviceTimeSpan = new TimeSpan(timeZoneServiceDateTime.Day, timeZoneServiceDateTime.Hour, timeZoneServiceDateTime.Minute, timeZoneServiceDateTime.Second);
            TimeSpan infoTimeSpan = new TimeSpan(timeZoneInfoConvertTime.Day, timeZoneInfoConvertTime.Hour, timeZoneInfoConvertTime.Minute, timeZoneInfoConvertTime.Second);

            Assert.That(serviceTimeSpan, Is.EqualTo(infoTimeSpan));
        }
        
        [TestCaseSource(nameof(_timeZoneInfoList))]
        public void StandardName_Match(TimeZoneInfo timeZoneInfo)
        {
            var serviceStandardName = TimeZoneService.StandardName(timeZoneInfo);
            var infoStandardName = timeZoneInfo.StandardName;

            Assert.That(serviceStandardName, Is.EqualTo(infoStandardName));
        }
    }   
}
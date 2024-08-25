using System;
using NUnit.Framework;

namespace ClockApp.Test.Editor
{
    public class TimerTest
    {
        static int[] _timeSecondList = { 0, 1, 59, 60, 61, 60 * 59, 60 * 60, 60 * 61};
        [TestCaseSource(nameof(_timeSecondList))]
        public void ConvertTimeSpan_Match(int timeSecond)
        {
            TimeCountService service = new TimeCountService();
            service.SetTimer(timeSecond);
            TimeSpan convertTimeSpan = service.ConvertTimeSpan();
            convertTimeSpan = new TimeSpan(convertTimeSpan.Hours, convertTimeSpan.Minutes, convertTimeSpan.Seconds);
            TimeSpan timeSpan = new TimeSpan(0, 0, timeSecond);
            Assert.That(convertTimeSpan.TotalSeconds, Is.EqualTo(timeSpan.TotalSeconds));
        }
    }   
}
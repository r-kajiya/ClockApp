using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace ClockApp.Test
{
    public class StopwatchTest
    {
        [UnityTest]
        public IEnumerator TickTack_CountUp()
        {
            TimeCountService service = new TimeCountService();
            
            service.SetTimer(0);

            while (service.Timer < 5.0f)
            {
                service.TickTack(Time.deltaTime);
                yield return null;
            }

            bool actual = service.Timer >= 5.0f;
            Assert.That(actual, Is.True);
        }
    }   
}
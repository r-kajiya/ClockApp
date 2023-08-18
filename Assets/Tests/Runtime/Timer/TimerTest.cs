using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace ClockApp.Test
{
    public class TimerTest
    {
        [UnityTest]
        public IEnumerator TickTack_CountDown()
        {
            TimeCountService service = new TimeCountService();
            
            service.SetTimer(5.0f);

            while (service.Timer >= 0.0f)
            {
                service.TickTack(-Time.deltaTime);
                yield return null;
            }

            bool actual = service.Timer <= 0.0f;
            Assert.That(actual, Is.True);
        }
    }   
}
using System;
using UnityEngine;

namespace ClockApp
{
    public class TimeCountService
    {
        double _timerCounter;
        public double Timer => _timerCounter;

        public void SetTimer(double setTimer)
        {
            _timerCounter = setTimer;
        }
        
        public void TickTack(double deltaTime)
        {
            _timerCounter += deltaTime;
        }

        public TimeSpan ConvertTimeSpan()
        {
            int timerCounterSecond = Mathf.FloorToInt((float)_timerCounter);
            double timerCounterMilliSecond = _timerCounter - Mathf.FloorToInt((float)_timerCounter);
            TimeSpan timeSpan = new TimeSpan(0, 0, timerCounterSecond);
            int hour = timeSpan.Hours;
            int minute = timeSpan.Minutes;
            int second = timeSpan.Seconds;
            const double floatingDigitCoefficient = 100.0f;
            int millisecond = Mathf.FloorToInt((float)(timerCounterMilliSecond * floatingDigitCoefficient));
            return new TimeSpan(0, hour, minute, second, millisecond);
        }
    }
}
using System;
using UniRx;
using UnityEngine;

namespace ClockApp
{
    public class TimeCountService
    {
        readonly ReactiveProperty<double> _timerCounter = new();
        public double Timer => _timerCounter.Value;

        public void Subscribe(Action<double> onTimeChanged, CompositeDisposable disposable)
        {
            _timerCounter.Subscribe(onTimeChanged).AddTo(disposable);
        }

        public void SetTimer(double setTimer)
        {
            _timerCounter.Value = setTimer;
        }

        public void TickTack(double deltaTime)
        {
            _timerCounter.Value += deltaTime;
        }

        public TimeSpan ConvertTimeSpan()
        {
            int timerCounterSecond = Mathf.CeilToInt((float)_timerCounter.Value);
            double timerCounterMilliSecond = _timerCounter.Value - Mathf.FloorToInt((float)_timerCounter.Value);
            TimeSpan timeSpan = new TimeSpan(0, 0, timerCounterSecond);
            int hour = timeSpan.Hours;
            int minute = timeSpan.Minutes;
            int second = timeSpan.Seconds;
            // example. 0.11 * 100.0 = 11
            const double floatingDigitCoefficient = 100.0f;
            int millisecond = Mathf.FloorToInt((float)(timerCounterMilliSecond * floatingDigitCoefficient));
            return new TimeSpan(0, hour, minute, second, millisecond);
        }
    }
}
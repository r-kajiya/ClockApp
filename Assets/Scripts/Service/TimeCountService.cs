using System;
using UniRx;
using UnityEngine;

namespace ClockApp
{
    public class TimeCountService
    {
        readonly ReactiveProperty<float> _timerCounter = new();
        public float Timer => _timerCounter.Value;

        public void Subscribe(Action<float> onTimeChanged, CompositeDisposable disposable)
        {
            _timerCounter.Subscribe(onTimeChanged).AddTo(disposable);
        }

        public void SetTimer(float setTimer)
        {
            _timerCounter.Value = setTimer;
        }

        public void TickTack(float deltaTime)
        {
            _timerCounter.Value += deltaTime;
        }

        public TimeSpan ConvertTimeSpan()
        {
            int timerCounterSecond = Mathf.CeilToInt(_timerCounter.Value);
            float timerCounterMilliSecond = _timerCounter.Value - Mathf.FloorToInt(_timerCounter.Value);
            TimeSpan timeSpan = new TimeSpan(0, 0, timerCounterSecond);
            int hour = timeSpan.Hours;
            int minute = timeSpan.Minutes;
            int second = timeSpan.Seconds;
            // example. 0.11 * 100.0 = 11
            const float floatingDigitCoefficient = 100.0f; 
            int millisecond = Mathf.FloorToInt(timerCounterMilliSecond * floatingDigitCoefficient);
            return new TimeSpan(0, hour, minute,second,  millisecond);
        }
    }
}
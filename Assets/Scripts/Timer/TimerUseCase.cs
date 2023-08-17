using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public class TimerUseCase : UseCaseBase<TimerPresenter> ,ITickable, IStartable, IDisposable
    {
        bool _isStart;
        float _timerCount;
        ReactiveProperty<float> _timerCounter;
        readonly CompositeDisposable _disposable = new();

        [Inject]
        public TimerUseCase(TimerPresenter presenter) : base(presenter) { }

        public void Start()
        {
            Presenter.SwitchSettingOrProgress(true);
            Presenter.RegisterActionOnClickCancelButton(OnCancel, _disposable);
            Presenter.RegisterActionOnClickStartButton(OnStart, _disposable);
            Presenter.RegisterActionOnClickPauseButton(OnPause, _disposable);
            Presenter.RegisterActionOnClickResumeButton(OnResume, _disposable);
            _timerCounter = new ReactiveProperty<float>();
            _timerCounter.Subscribe(OnChangedTimerCount).AddTo(_disposable);
            Presenter.SetActiveStartButton(true);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveResumeButton(false);
        }

        public void Tick()
        {
            if (_isStart)
            {
                _timerCounter.Value -= Time.deltaTime;
            }
        }
        
        public void Dispose()
        { 
            _disposable.Dispose();
        }

        void OnNotice()
        {
            // サウンド再生
            OnCancel();
        }

        void OnStart()
        {
            _isStart = true;
            Presenter.SwitchSettingOrProgress(false);
            Presenter.SetActiveStartButton(false);
            Presenter.SetActivePauseButton(true);
            Presenter.SetActiveResumeButton(false);
            TimeSpan settingTimerDateTime = Presenter.SettingTimerTimeSpan();
            _timerCount = (float)settingTimerDateTime.TotalSeconds;
            _timerCounter.Value = _timerCount;
        }

        void OnCancel()
        {
            _isStart = false;
            Presenter.SwitchSettingOrProgress(true);
            Presenter.SetActiveStartButton(true);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveResumeButton(false);
        }

        void OnPause()
        {
            _isStart = false;
            Presenter.SetActiveStartButton(false);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveResumeButton(true);
        }

        void OnResume()
        {
            _isStart = true;
            Presenter.SetActiveStartButton(false);
            Presenter.SetActivePauseButton(true);
            Presenter.SetActiveResumeButton(false);
        }

        void OnChangedTimerCount(float timerCounter)
        {
            if (timerCounter <= 0.0f)
            {
                OnNotice();
            }
            else
            {
                int timerCounterSecond = Mathf.CeilToInt(timerCounter);
                TimeSpan timeSpan = new TimeSpan(0, 0, timerCounterSecond);
                int hour = timeSpan.Hours;
                int minute = timeSpan.Minutes;
                int second = timeSpan.Seconds;
                float fill = timerCounter / _timerCount;
                Presenter.SetProgressTimer(hour, minute, second, fill);       
            }
        }
    }
}


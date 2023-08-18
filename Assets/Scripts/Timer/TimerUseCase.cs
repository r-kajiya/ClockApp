using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public interface ITimerUseCase<out TPresenter, out TView> : IUseCase<TPresenter, TView>
        where TView : ITimerView
        where TPresenter : ITimerPresenter<TView>
    {
        void OnNotice();
        void OnStart();
        void OnCancel();
        void OnPause();
        void OnResume();
    }

    public class TimerUseCase : ITimerUseCase<ITimerPresenter<ITimerView>, ITimerView>, ITickable, IStartable, IDisposable
    {
        public ITimerPresenter<ITimerView> Presenter { get; }

        bool _isStart;
        float _timerCount;
        TimeCountService _timerCounter;
        readonly CompositeDisposable _disposable = new();

        [Inject]
        public TimerUseCase(ITimerPresenter<ITimerView> presenter)
        {
            Presenter = presenter;
        }

        public void Start()
        {
            Presenter.SwitchSettingOrProgress(true);
            Presenter.RegisterActionOnClickCancelButton(OnCancel, _disposable);
            Presenter.RegisterActionOnClickStartButton(OnStart, _disposable);
            Presenter.RegisterActionOnClickPauseButton(OnPause, _disposable);
            Presenter.RegisterActionOnClickResumeButton(OnResume, _disposable);
            _timerCounter = new TimeCountService();
            _timerCounter.Subscribe(OnChangedTimerCount, _disposable);
            Presenter.SetActiveStartButton(true);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveResumeButton(false);
        }

        public void Tick()
        {
            if (_isStart)
            {
                float dt = -Time.deltaTime;
                _timerCounter.TickTack(dt);
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void OnNotice()
        {
            OnCancel();
            Presenter.PlayAlert();
        }

        public void OnStart()
        {
            TimeSpan settingTimerDateTime = Presenter.SettingTimerTimeSpan();
            if (settingTimerDateTime.TotalSeconds == 0)
            {
                return;
            }

            _timerCount = (float)settingTimerDateTime.TotalSeconds;
            _isStart = true;
            Presenter.SwitchSettingOrProgress(false);
            Presenter.SetActiveStartButton(false);
            Presenter.SetActivePauseButton(true);
            Presenter.SetActiveResumeButton(false);
            _timerCounter.SetTimer(_timerCount);
            Presenter.StopAlert();
        }

        public void OnCancel()
        {
            _isStart = false;
            Presenter.SwitchSettingOrProgress(true);
            Presenter.SetActiveStartButton(true);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveResumeButton(false);
            Presenter.StopAlert();
        }

        public void OnPause()
        {
            _isStart = false;
            Presenter.SetActiveStartButton(false);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveResumeButton(true);
        }

        public void OnResume()
        {
            _isStart = true;
            Presenter.SetActiveStartButton(false);
            Presenter.SetActivePauseButton(true);
            Presenter.SetActiveResumeButton(false);
        }

        void OnChangedTimerCount(double time)
        {
            if (_isStart == false)
            {
                return;
            }

            if (time <= 0.0f)
            {
                OnNotice();
            }
            else
            {
                TimeSpan timeSpan = _timerCounter.ConvertTimeSpan();
                int hour = timeSpan.Hours;
                int minute = timeSpan.Minutes;
                int second = timeSpan.Seconds;
                float fill = (float)time / _timerCount;
                Presenter.SetProgressTimer(hour, minute, second, fill);
            }
        }
    }
}
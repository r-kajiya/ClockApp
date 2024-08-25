using System;
using UnityEngine;

namespace ClockApp
{
    public class TimerContainer
    {
        public ITimerPresenter<ITimerView> Presenter { get; }
        
        public TimerContainer(ITimerPresenter<ITimerView> presenter)
        {
            Presenter = presenter;
        }
    }
    
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

    public class TimerUseCase : ITimerUseCase<ITimerPresenter<ITimerView>, ITimerView>
    {
        public ITimerPresenter<ITimerView> Presenter { get; }

        readonly TimeCountService _timerCounter;
        
        bool _isStart;
        float _timerCount;
        
        public TimerUseCase(TimerContainer container)
        {
            Presenter = container.Presenter;
            Presenter.SwitchSettingOrProgress(true);
            Presenter.RegisterActionOnClickCancelButton(OnCancel);
            Presenter.RegisterActionOnClickStartButton(OnStart);
            Presenter.RegisterActionOnClickPauseButton(OnPause);
            Presenter.RegisterActionOnClickResumeButton(OnResume);
            _timerCounter = new TimeCountService();
            SetActiveButtons(true, false, false);
        }

        public void OnUpdate(float dt)
        {
            Tick(dt);
        }

        public void Tick(float dt)
        {
            if (_isStart)
            {
                _timerCounter.TickTack(-dt);
                ChangeTimerCount(_timerCounter.Timer);
            }
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
            SetActiveButtons(false, true, false);
            _timerCounter.SetTimer(_timerCount);
            Presenter.StopAlert();
        }

        public void OnCancel()
        {
            _isStart = false;
            Presenter.SwitchSettingOrProgress(true);
            SetActiveButtons(true, false, false);
            Presenter.StopAlert();
        }

        public void OnPause()
        {
            _isStart = false;
            SetActiveButtons(false, false, true);
        }

        public void OnResume()
        {
            _isStart = true;
            SetActiveButtons(false, true, false);
        }

        void ChangeTimerCount(double time)
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
                int millisecond = timeSpan.Milliseconds;
                float fill = (float)time / _timerCount;
                Presenter.SetProgressTimer(hour, minute, second, millisecond, fill);
            }
        }

        void SetActiveButtons(bool enableStart, bool enablePause, bool enableResume)
        {
            Presenter.SetActiveStartButton(enableStart);
            Presenter.SetActivePauseButton(enablePause);
            Presenter.SetActiveResumeButton(enableResume);
        }
    }
}
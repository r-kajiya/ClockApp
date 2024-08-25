using System;

namespace ClockApp
{
    public interface ITimerPresenter<out TView> : IPresenter<TView>
        where TView : ITimerView
    {
        void RegisterActionOnClickCancelButton(Action onAction);

        void RegisterActionOnClickStartButton(Action onAction);

        void RegisterActionOnClickPauseButton(Action onAction);

        void RegisterActionOnClickResumeButton(Action onAction);

        void SetActiveStartButton(bool enable);

        void SetActivePauseButton(bool enable);

        void SetActiveResumeButton(bool enable);

        void SetProgressTimer(int hour, int minute, int second, int millisecond, float timerFill);

        void SwitchSettingOrProgress(bool switchSetting);

        TimeSpan SettingTimerTimeSpan();

        void PlayAlert();

        void StopAlert();
    }

    public class TimerPresenter : ITimerPresenter<ITimerView>
    {
        public ITimerView View { get; }
        
        public TimerPresenter(ITimerView view)
        {
            View = view;
        }
        
        public void RegisterActionOnClickCancelButton(Action onAction)
        {
            View.CancelButton.onClick.RemoveAllListeners();
            View.CancelButton.onClick.AddListener(()=> onAction?.Invoke());
        }

        public void RegisterActionOnClickStartButton(Action onAction)
        {
            View.StartButton.onClick.RemoveAllListeners();
            View.StartButton.onClick.AddListener(()=> onAction?.Invoke());
        }

        public void RegisterActionOnClickPauseButton(Action onAction)
        {
            View.PauseButton.onClick.RemoveAllListeners();
            View.PauseButton.onClick.AddListener(()=> onAction?.Invoke());
        }

        public void RegisterActionOnClickResumeButton(Action onAction)
        {
            View.ResumeButton.onClick.RemoveAllListeners();
            View.ResumeButton.onClick.AddListener(()=> onAction?.Invoke());
        }

        public void SetActiveStartButton(bool enable)
        {
            View.StartButton.gameObject.SetActive(enable);
        }

        public void SetActivePauseButton(bool enable)
        {
            View.PauseButton.gameObject.SetActive(enable);
        }

        public void SetActiveResumeButton(bool enable)
        {
            View.ResumeButton.gameObject.SetActive(enable);
        }

        public void SetProgressTimer(int hour, int minute, int second, int millisecond, float timerFill)
        {
            View.ProgressHour.text = $"{hour:D2}";
            View.ProgressMinute.text = $"{minute:D2}";
            View.ProgressSecond.text = $"{second:D2}";
            View.ProgressMilliSecond.text = $"{millisecond:D2}";
            View.ProgressRoot.fillAmount = timerFill;
        }

        public void SwitchSettingOrProgress(bool switchSetting)
        {
            View.SettingRoot.gameObject.SetActive(switchSetting);
            View.ProgressRoot.gameObject.SetActive(!switchSetting);
        }

        public TimeSpan SettingTimerTimeSpan()
        {
            int hour = View.HourScrollSnap.CurrentNumber;
            int minute = View.MinuteScrollSnap.CurrentNumber;
            int second = View.SecondScrollSnap.CurrentNumber;

            return new TimeSpan(hour, minute, second);
        }

        public void PlayAlert()
        {
            View.Alert.Play();
        }

        public void StopAlert()
        {
            View.Alert.Stop();
        }
    }
}
using System;
using UniRx;
using VContainer;

namespace ClockApp
{
    public interface ITimerPresenter<out TView> : IPresenter<TView>
        where TView : ITimerView
    {
        void RegisterActionOnClickCancelButton(Action onAction, CompositeDisposable disposable);

        void RegisterActionOnClickStartButton(Action onAction, CompositeDisposable disposable);

        void RegisterActionOnClickPauseButton(Action onAction, CompositeDisposable disposable);

        void RegisterActionOnClickResumeButton(Action onAction, CompositeDisposable disposable);

        void SetActiveStartButton(bool enable);

        void SetActivePauseButton(bool enable);

        void SetActiveResumeButton(bool enable);

        void SetProgressTimer(int hour, int minute, int second, float timerFill);

        void SwitchSettingOrProgress(bool switchSetting);

        TimeSpan SettingTimerTimeSpan();

        void PlayAlert();

        void StopAlert();
    }

    public class TimerPresenter : ITimerPresenter<ITimerView>
    {
        public ITimerView View { get; }

        [Inject]
        public TimerPresenter(ITimerView view)
        {
            View = view;
        }

        const int TAP_INTERVAL_MILLISECOND = 100;

        public void RegisterActionOnClickCancelButton(Action onAction, CompositeDisposable disposable)
        {
            View.CancelButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
        }

        public void RegisterActionOnClickStartButton(Action onAction, CompositeDisposable disposable)
        {
            View.StartButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
        }

        public void RegisterActionOnClickPauseButton(Action onAction, CompositeDisposable disposable)
        {
            View.PauseButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
        }

        public void RegisterActionOnClickResumeButton(Action onAction, CompositeDisposable disposable)
        {
            View.ResumeButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
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

        public void SetProgressTimer(int hour, int minute, int second, float timerFill)
        {
            View.ProgressHour.text = $"{hour:D2}";
            View.ProgressMinute.text = $"{minute:D2}";
            View.ProgressSecond.text = $"{second:D2}";
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
using System;
using UniRx;
using VContainer;

namespace ClockApp
{
    public class FooterPresenter : PresenterBase<FooterView>
    {
        [Inject]
        public FooterPresenter(FooterView view) : base(view) { }

        const int TAP_INTERVAL_MILLISECOND = 100;
        
        public void RegisterActionOnClickStopwatchTransitionButton(Action onAction, CompositeDisposable disposable)
        {
            View.StopwatchTransitionButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_=>
            {
                onAction?.Invoke();
            }).AddTo(disposable);
        }
        
        public void RegisterActionOnClickClockTransitionButton(Action onAction, CompositeDisposable disposable)
        {
            View.ClockTransitionButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_=>
            {
                onAction?.Invoke();
            }).AddTo(disposable);
        }
        
        public void RegisterActionOnClickTimerTransitionButton(Action onAction, CompositeDisposable disposable)
        {
            View.TimerTransitionButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_=>
            {
                onAction?.Invoke();
            }).AddTo(disposable);
        }

        public void SetActiveStopwatch(bool enable)
        {
            View.StopwatchRoot.SetActive(enable);
        }
        
        public void SetActiveClock(bool enable)
        {
            View.ClockRoot.SetActive(enable);
        }
        
        public void SetActiveTimer(bool enable)
        {
            View.TimerRoot.SetActive(enable);
        }
        
    }
}
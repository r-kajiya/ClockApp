using System;
using UniRx;
using VContainer;

namespace ClockApp
{
    public interface IFooterPresenter<out TView> : IPresenter<TView>
        where TView : IFooterView
    {
        void RegisterActionOnClickStopwatchTransitionButton(Action onAction, CompositeDisposable disposable);
        void RegisterActionOnClickClockTransitionButton(Action onAction, CompositeDisposable disposable);
        void RegisterActionOnClickTimerTransitionButton(Action onAction, CompositeDisposable disposable);
        void SetActiveStopwatch(bool enable);
        void SetActiveClock(bool enable);
        void SetActiveTimer(bool enable);
    }

    public class FooterPresenter : IFooterPresenter<IFooterView>
    {
        public IFooterView View { get; }

        [Inject]
        public FooterPresenter(IFooterView view)
        {
            View = view;
        }

        const int TAP_INTERVAL_MILLISECOND = 99;

        public void RegisterActionOnClickStopwatchTransitionButton(Action onAction, CompositeDisposable disposable)
        {
            View.StopwatchTransitionButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
        }

        public void RegisterActionOnClickClockTransitionButton(Action onAction, CompositeDisposable disposable)
        {
            View.ClockTransitionButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
        }

        public void RegisterActionOnClickTimerTransitionButton(Action onAction, CompositeDisposable disposable)
        {
            View.TimerTransitionButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
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
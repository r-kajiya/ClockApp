using System;

namespace ClockApp
{
    public interface IFooterPresenter<out TView> : IPresenter<TView>
        where TView : IFooterView
    {
        void RegisterActionOnClickStopwatchTransitionButton(Action onAction);
        void RegisterActionOnClickClockTransitionButton(Action onAction);
        void RegisterActionOnClickTimerTransitionButton(Action onAction);
        void SetActiveStopwatch(bool enable);
        void SetActiveClock(bool enable);
        void SetActiveTimer(bool enable);
    }

    public class FooterPresenter : IFooterPresenter<IFooterView>
    {
        public IFooterView View { get; }
        
        public FooterPresenter(IFooterView view)
        {
            View = view;
        }

        public void RegisterActionOnClickStopwatchTransitionButton(Action onAction)
        {
            View.StopwatchTransitionButton.onClick.RemoveAllListeners();
            View.StopwatchTransitionButton.onClick.AddListener(()=>onAction?.Invoke());
        }

        public void RegisterActionOnClickClockTransitionButton(Action onAction)
        {
            View.ClockTransitionButton.onClick.RemoveAllListeners();
            View.ClockTransitionButton.onClick.AddListener(()=>onAction?.Invoke());
        }

        public void RegisterActionOnClickTimerTransitionButton(Action onAction)
        {
            View.TimerTransitionButton.onClick.RemoveAllListeners();
            View.TimerTransitionButton.onClick.AddListener(()=>onAction?.Invoke());
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
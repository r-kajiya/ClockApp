using System;

namespace ClockApp
{
    public class FooterContainer
    {
        public IFooterPresenter<IFooterView> Presenter { get; }
        public FooterContainer(IFooterPresenter<IFooterView> presenter)
        {
            Presenter = presenter;
        }
    }
    
    public interface IFooterUseCase<out TPresenter, out TView> : IUseCase<TPresenter, TView>
        where TView : IFooterView
        where TPresenter : IFooterPresenter<TView>
    {
        void ActiveStopwatch();
        void ActiveClock();
        void ActiveTimer();
    }

    public class FooterUseCase : IFooterUseCase<IFooterPresenter<IFooterView>, IFooterView>
    {
        public IFooterPresenter<IFooterView> Presenter { get; }

        Action _onChangeStopwatch;
        Action _onChangeClock;
        Action _onChangeTimer;
        
        public FooterUseCase(FooterContainer container)
        {
            Presenter = container.Presenter;
        }

        public void SetupByContext(Action onChangeStopwatch, Action onChangeClock, Action onChangeTimer)
        {
            Presenter.RegisterActionOnClickStopwatchTransitionButton(onChangeStopwatch);
            Presenter.RegisterActionOnClickClockTransitionButton(onChangeClock);
            Presenter.RegisterActionOnClickTimerTransitionButton(onChangeTimer);
        }
        
        public void OnUpdate(float dt) { }

        public void ActiveStopwatch()
        {
            Presenter.SetActiveStopwatch(true);
            Presenter.SetActiveClock(false);
            Presenter.SetActiveTimer(false);
        }

        public void ActiveClock()
        {
            Presenter.SetActiveStopwatch(false);
            Presenter.SetActiveClock(true);
            Presenter.SetActiveTimer(false);
        }

        public void ActiveTimer()
        {
            Presenter.SetActiveStopwatch(false);
            Presenter.SetActiveClock(false);
            Presenter.SetActiveTimer(true);
        }
    }
}
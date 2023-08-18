using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public interface IFooterUseCase<out TPresenter, out TView> : IUseCase<TPresenter, TView>
        where TView : IFooterView
        where TPresenter : IFooterPresenter<TView>
    {
        void OnStopwatchTransition();
        void OnClockTransition();
        void OnTimerTransition();
    }

    public class FooterUseCase : IFooterUseCase<IFooterPresenter<IFooterView>, IFooterView>, IStartable, IDisposable
    {
        public IFooterPresenter<IFooterView> Presenter { get; }

        readonly CompositeDisposable _disposable = new();

        [Inject]
        public FooterUseCase(IFooterPresenter<IFooterView> presenter)
        {
            Presenter = presenter;
        }

        public void Start()
        {
            Presenter.RegisterActionOnClickStopwatchTransitionButton(OnStopwatchTransition, _disposable);
            Presenter.RegisterActionOnClickClockTransitionButton(OnClockTransition, _disposable);
            Presenter.RegisterActionOnClickTimerTransitionButton(OnTimerTransition, _disposable);

            OnClockTransition();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void OnStopwatchTransition()
        {
            Presenter.SetActiveStopwatch(true);
            Presenter.SetActiveClock(false);
            Presenter.SetActiveTimer(false);
        }

        public void OnClockTransition()
        {
            Presenter.SetActiveStopwatch(false);
            Presenter.SetActiveClock(true);
            Presenter.SetActiveTimer(false);
        }

        public void OnTimerTransition()
        {
            Presenter.SetActiveStopwatch(false);
            Presenter.SetActiveClock(false);
            Presenter.SetActiveTimer(true);
        }
    }
}
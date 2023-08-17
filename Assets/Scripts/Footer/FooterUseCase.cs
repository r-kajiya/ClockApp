using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public class FooterUseCase : UseCaseBase<FooterPresenter> ,IStartable, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        [Inject]
        public FooterUseCase(FooterPresenter presenter) : base(presenter) { }

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

        void OnStopwatchTransition()
        {
            Presenter.SetActiveStopwatch(true);
            Presenter.SetActiveClock(false);
            Presenter.SetActiveTimer(false);
        }
        
        void OnClockTransition()
        {
            Presenter.SetActiveStopwatch(false);
            Presenter.SetActiveClock(true);
            Presenter.SetActiveTimer(false);
        }

        void OnTimerTransition()
        {
            Presenter.SetActiveStopwatch(false);
            Presenter.SetActiveClock(false);
            Presenter.SetActiveTimer(true);   
        }
    }
}


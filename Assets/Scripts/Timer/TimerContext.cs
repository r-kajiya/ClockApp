using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public class TimerContext : LifetimeScope
    {
        [SerializeField]
        TimerView _timerView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TimerUseCase>();
            builder.Register<ITimerPresenter<ITimerView>, TimerPresenter>(Lifetime.Singleton);
            builder.RegisterComponent<ITimerView>(_timerView);
        }
    }
}
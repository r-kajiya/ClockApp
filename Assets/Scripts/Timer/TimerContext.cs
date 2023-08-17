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
            builder.Register<TimerPresenter>(Lifetime.Singleton);
            builder.RegisterComponent(_timerView);
        }
    }
}
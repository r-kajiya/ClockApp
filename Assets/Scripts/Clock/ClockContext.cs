using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public class ClockContext : LifetimeScope
    {
        [SerializeField]
        ClockView _clockView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ClockUseCase>();
            builder.Register<IClockPresenter<IClockView>, ClockPresenter>(Lifetime.Singleton);
            builder.RegisterComponent<IClockView>(_clockView);
        }
    }
}
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public class StopwatchContext : LifetimeScope
    {
        [SerializeField]
        StopwatchView _stopwatchView;

        [SerializeField]
        StopwatchLapCellView _stopwatchLapCellViewPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<StopwatchUseCase>().WithParameter(_stopwatchLapCellViewPrefab);
            builder.Register<IStopwatchPresenter<IStopwatchView>, StopwatchPresenter>(Lifetime.Singleton);
            builder.RegisterComponent<IStopwatchView>(_stopwatchView);
        }
    }
}
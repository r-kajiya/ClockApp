using System;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public interface IClockUseCase<out TPresenter, out TView> : IUseCase<TPresenter, TView>
        where TView : IClockView
        where TPresenter : IClockPresenter<TView>
    {
        void TickTack();
    }

    public class ClockUseCase : IClockUseCase<IClockPresenter<IClockView>, IClockView>, ITickable
    {
        public IClockPresenter<IClockView> Presenter { get; }

        [Inject]
        public ClockUseCase(IClockPresenter<IClockView> presenter)
        {
            Presenter = presenter;
        }

        public void Tick()
        {
            TickTack();
        }

        public void TickTack()
        {
            var localTimeZone = TimeZoneInfo.Local;

            DateTime localTime = TimeZoneService.ConvertDateTime(localTimeZone);
            Presenter.SetDateTime(localTime);

            string localPlaceName = TimeZoneService.StandardName(localTimeZone);
            Presenter.SetPlaceName(localPlaceName);
        }
    }
}
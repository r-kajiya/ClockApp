using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public class ClockUseCase : UseCaseBase<ClockPresenter> ,ITickable
    {
        [Inject]
        public ClockUseCase(ClockPresenter presenter) : base(presenter) { }

        public void Tick()
        {
            var localTimeZone = TimeZoneInfo.Local;
            
            DateTime localTime = TimeZoneService.ConvertDateTime(localTimeZone);
            Presenter.SetDateTime(localTime);
            
            string localPlaceName = TimeZoneService.StandardName(localTimeZone);
            Presenter.SetPlaceName(localPlaceName);
        }
    }
}


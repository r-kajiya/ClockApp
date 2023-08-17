using System;
using System.Collections;
using System.Collections.Generic;
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
            DateTime localTime = LocalTimeZoneDateTime();
            Presenter.SetDateTime(localTime);
            string localPlaceName = LocalTimeZoneStandardName();
            Presenter.SetPlaceName(localPlaceName);
        }

        DateTime LocalTimeZoneDateTime()
        {
            TimeZoneInfo localTimeZoneInfo = TimeZoneInfo.Local;
            DateTime localDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, localTimeZoneInfo);
            return localDateTime;
        }

        string LocalTimeZoneStandardName()
        {
            TimeZoneInfo localTimeZoneInfo = TimeZoneInfo.Local;
            return localTimeZoneInfo.StandardName;
        }
    }
}


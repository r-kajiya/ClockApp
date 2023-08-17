using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ClockApp
{
    public class ClockPresenter : PresenterBase<ClockView>
    {
        [Inject]
        public ClockPresenter(ClockView view) : base(view) { }

        public void SetDateTime(DateTime dateTime)
        {
            View.Hour.text = dateTime.Hour.ToString();
            View.Minute.text = dateTime.Minute.ToString();
        }

        public void SetPlaceName(string placeName)
        {
            View.PlaceName.text = placeName;
        }
    }
}
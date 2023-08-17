using System;
using UniRx;
using UnityEngine;
using VContainer;

namespace ClockApp
{
    public class StopwatchLapCellPresenter : PresenterBase<StopwatchLapCellView>
    {
        [Inject]
        public StopwatchLapCellPresenter(StopwatchLapCellView view) : base(view) { }

        public void SetLapNumber(int number)
        {
            View.LapNumber.text = $"{number:D2}";
        }
        
        public void SetProgressTimer(int minute, int second, int millisecond)
        {
            View.Minute.text = $"{minute:D2}";
            View.Second.text = $"{second:D2}";
            View.Millisecond.text = $"{millisecond:D2}";
        }

        public float CellHeight()
        {
            return ((RectTransform)View.transform).sizeDelta.y;
        }
    }
}
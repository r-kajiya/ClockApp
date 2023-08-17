using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public class StopwatchLapCellUseCase : UseCaseBase<StopwatchLapCellPresenter>
    {
        [Inject]
        public StopwatchLapCellUseCase(StopwatchLapCellPresenter presenter) : base(presenter)
        {
            
        }
        
        public void SetLapNumber(int number)
        {
            Presenter.SetLapNumber(number);
        }
        
        public void SetProgressTimer(int minute, int second, int millisecond)
        {
            Presenter.SetProgressTimer(minute, second, millisecond);
        }

        public float CellHeight()
        {
            return Presenter.CellHeight();
        }
    }
}


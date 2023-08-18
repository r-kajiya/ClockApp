using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public interface IStopwatchLapCellUseCase<out TPresenter, out TView> : IUseCase<TPresenter, TView>
        where TView : IStopwatchLapCellView
        where TPresenter : IStopwatchLapCellPresenter<TView>
    {
        void SetLapNumber(int number);
        void SetProgressTimer(int minute, int second, int millisecond);
        float CellHeight();
    }

    public class StopwatchLapCellUseCase : IStopwatchLapCellUseCase<IStopwatchLapCellPresenter<IStopwatchLapCellView>, IStopwatchLapCellView>
    {
        public IStopwatchLapCellPresenter<IStopwatchLapCellView> Presenter { get; }

        public StopwatchLapCellUseCase(IStopwatchLapCellPresenter<IStopwatchLapCellView> presenter)
        {
            Presenter = presenter;
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
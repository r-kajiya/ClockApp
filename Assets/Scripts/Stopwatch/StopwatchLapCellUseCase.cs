namespace ClockApp
{
    public class StopwatchLapCellContainer
    {
        public IStopwatchLapCellPresenter<IStopwatchLapCellView> Presenter { get; }
        
        public StopwatchLapCellContainer(IStopwatchLapCellPresenter<IStopwatchLapCellView> presenter)
        {
            Presenter = presenter;
        }
    }
    
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

        double _timer;
        public double Timer => _timer;

        public StopwatchLapCellUseCase(StopwatchLapCellContainer container)
        {
            Presenter = container.Presenter;
        }

        public void OnUpdate(float dt) { }

        public void SetLapNumber(int number)
        {
            Presenter.SetLapNumber(number);
        }

        public void SetProgressTimer(int minute, int second, int millisecond)
        {
            Presenter.SetProgressTimer(minute, second, millisecond);
        }

        public void SetTimer(double timer)
        {
            _timer = timer;
        }

        public float CellHeight()
        {
            return Presenter.CellHeight();
        }
    }
}
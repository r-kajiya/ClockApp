namespace ClockApp
{
    public interface IStopwatchLapCellPresenter<out TView> : IPresenter<TView>
        where TView : IStopwatchLapCellView
    {
        void SetLapNumber(int number);
        void SetProgressTimer(int minute, int second, int millisecond);
        float CellHeight();
    }

    public class StopwatchLapCellPresenter : IStopwatchLapCellPresenter<IStopwatchLapCellView>
    {
        public IStopwatchLapCellView View { get; }
        
        public StopwatchLapCellPresenter(IStopwatchLapCellView view)
        {
            View = view;
        }

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
            return View.CellTransform.sizeDelta.y;
        }
    }
}
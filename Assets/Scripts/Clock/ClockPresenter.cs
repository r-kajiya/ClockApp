using System;
using VContainer;

namespace ClockApp
{
    public interface IClockPresenter<out TView> : IPresenter<TView>
        where TView : IClockView
    {
        void SetDateTime(DateTime dateTime);
        void SetPlaceName(string placeName);
    }

    public class ClockPresenter : IClockPresenter<IClockView>
    {
        public IClockView View { get; }

        [Inject]
        public ClockPresenter(IClockView view)
        {
            View = view;
        }

        public void SetDateTime(DateTime dateTime)
        {
            View.Hour.text = $"{dateTime.Hour:D2}";
            View.Minute.text = $"{dateTime.Minute:D2}";
        }

        public void SetPlaceName(string placeName)
        {
            View.PlaceName.text = placeName;
        }
    }
}
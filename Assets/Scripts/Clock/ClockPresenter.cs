using System;

namespace ClockApp
{
    public interface IClockPresenter<out TView> : IPresenter<TView>
        where TView : IClockView
    {
        void SetDateTime(DateTime dateTime);
        void SetPlaceName(string placeName);
        void RegisterActionOnClickPrevPlaceButton(Action onAction);
        void RegisterActionOnClickNextPlaceButton(Action onAction);
        void SetActiveButtons(bool enablePrevButton, bool enableNextButton);
    }

    public class ClockPresenter : IClockPresenter<IClockView>
    {
        public IClockView View { get; }
        
        public ClockPresenter(IClockView view)
        {
            View = view;
        }

        public void SetDateTime(DateTime dateTime)
        {
            View.Year.text = $"{dateTime.Year:D4}";
            View.Month.text = $"{dateTime.Month:D2}";
            View.Day.text = $"{dateTime.Day:D2}";
            View.Hour.text = $"{dateTime.Hour:D2}";
            View.Minute.text = $"{dateTime.Minute:D2}";
        }

        public void SetPlaceName(string placeName)
        {
            View.PlaceName.text = placeName;
        }
        
        public void RegisterActionOnClickPrevPlaceButton(Action onAction)
        {
            View.PrevPlaceButton.onClick.RemoveAllListeners();
            View.PrevPlaceButton.onClick.AddListener(()=>onAction?.Invoke());
        }
        
        public void RegisterActionOnClickNextPlaceButton(Action onAction)
        {
            View.NextPlaceButton.onClick.RemoveAllListeners();
            View.NextPlaceButton.onClick.AddListener(()=>onAction?.Invoke());
        }

        public void SetActiveButtons(bool enablePrevButton, bool enableNextButton)
        {
            View.PrevPlaceButton.interactable = enablePrevButton;
            View.NextPlaceButton.interactable = enableNextButton;
        }
    }
}
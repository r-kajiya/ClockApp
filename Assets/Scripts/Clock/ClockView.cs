using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public interface IClockView : IView
    {
        Text Year { get; }
        Text Month { get; }
        Text Day { get; }
        Text Hour { get; }
        Text Minute { get; }
        Text PlaceName { get; }
        Button PrevPlaceButton { get; }
        Button NextPlaceButton { get; }
    }

    public class ClockView : MonoBehaviour, IClockView
    {
        [SerializeField]
        Text _year;
        public Text Year => _year;

        [SerializeField]
        Text _month;
        public Text Month => _month;
        
        [SerializeField]
        Text _day;
        public Text Day => _day;
        
        [SerializeField]
        Text _hour;
        public Text Hour => _hour;

        [SerializeField]
        Text _minute;
        public Text Minute => _minute;

        [SerializeField]
        Text _placeName;
        public Text PlaceName => _placeName;
        
        [SerializeField]
        Button _prevPlaceButton;
        public Button PrevPlaceButton => _prevPlaceButton;
        
        [SerializeField]
        Button _nextPlaceButton;
        public Button NextPlaceButton => _nextPlaceButton;

        public void RunOnAwake()
        {
            PlaceName.text = string.Empty;
            _year.text = "0000";
            _month.text = "00";
            _day.text = "00";
            _hour.text = "00";
            _minute.text = "00";
        }
    }
}
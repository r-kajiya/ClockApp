using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public interface IClockView : IView
    {
        Text Hour { get; }
        Text Minute { get; }
        Text PlaceName { get; }
    }

    public class ClockView : MonoBehaviour, IClockView
    {
        [SerializeField]
        Text _hour;

        public Text Hour => _hour;

        [SerializeField]
        Text _minute;

        public Text Minute => _minute;

        [SerializeField]
        Text _placeName;

        public Text PlaceName => _placeName;
    }
}
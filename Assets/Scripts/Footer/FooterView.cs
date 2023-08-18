using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public interface IFooterView : IView
    {
        Button StopwatchTransitionButton { get; }
        Button ClockTransitionButton { get; }
        Button TimerTransitionButton { get; }
        GameObject ClockRoot { get; }
        GameObject TimerRoot { get; }
        GameObject StopwatchRoot { get; }
    }

    public class FooterView : MonoBehaviour, IFooterView
    {
        [SerializeField]
        Button _stopwatchTransitionButton;

        public Button StopwatchTransitionButton => _stopwatchTransitionButton;

        [SerializeField]
        Button _clockTransitionButton;

        public Button ClockTransitionButton => _clockTransitionButton;

        [SerializeField]
        Button _timerTransitionButton;

        public Button TimerTransitionButton => _timerTransitionButton;

        [SerializeField]
        GameObject _clockRoot;

        public GameObject ClockRoot => _clockRoot;

        [SerializeField]
        GameObject _timerRoot;

        public GameObject TimerRoot => _timerRoot;

        [SerializeField]
        GameObject _stopwatchRoot;

        public GameObject StopwatchRoot => _stopwatchRoot;
    }
}
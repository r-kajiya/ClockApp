using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public interface IStopwatchView : IView
    {
        Button LapButton { get; }
        Button ResetButton { get; }
        Button StartButton { get; }
        Button PauseButton { get; }
        Text Minute { get; }
        Text Second { get; }
        Text Millisecond { get; }
        Transform LapCellListParent { get; }
    }

    public class StopwatchView : MonoBehaviour, IStopwatchView
    {
        [SerializeField]
        Button _lapButton;
        public Button LapButton => _lapButton;

        [SerializeField]
        Button _resetButton;
        public Button ResetButton => _resetButton;

        [SerializeField]
        Button _startButton;
        public Button StartButton => _startButton;

        [SerializeField]
        Button _pauseButton;
        public Button PauseButton => _pauseButton;

        [SerializeField]
        Text _minute;
        public Text Minute => _minute;

        [SerializeField]
        Text _second;
        public Text Second => _second;

        [SerializeField]
        Text _millisecond;
        public Text Millisecond => _millisecond;

        [SerializeField]
        Transform _lapCellListParent;
        public Transform LapCellListParent => _lapCellListParent;
        
        public void RunOnAwake()
        {
            _minute.text = "00";
            _second.text = "00";
            _millisecond.text = "00";
        }
    }
}
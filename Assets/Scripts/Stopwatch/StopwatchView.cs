using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public class StopwatchView : MonoBehaviour, IView
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
    }
}

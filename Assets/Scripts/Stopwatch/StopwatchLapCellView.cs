using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public interface IStopwatchLapCellView : IView
    {
        Text LapNumber { get; }
        Text Minute { get; }
        Text Second { get; }
        Text Millisecond { get; }
        RectTransform CellTransform { get; }
    }

    public class StopwatchLapCellView : MonoBehaviour, IStopwatchLapCellView
    {
        [SerializeField]
        Text _lapNumber;

        public Text LapNumber => _lapNumber;

        [SerializeField]
        Text _minute;

        public Text Minute => _minute;

        [SerializeField]
        Text _second;

        public Text Second => _second;

        [SerializeField]
        Text _millisecond;

        public Text Millisecond => _millisecond;

        public RectTransform CellTransform => transform as RectTransform;
    }
}
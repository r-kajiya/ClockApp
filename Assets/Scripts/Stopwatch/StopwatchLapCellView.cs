using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public class StopwatchLapCellView : MonoBehaviour, IView
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
    }
}

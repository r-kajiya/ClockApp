using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public class ClockView : MonoBehaviour, IView
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

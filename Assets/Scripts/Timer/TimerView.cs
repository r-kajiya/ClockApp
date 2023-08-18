using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public class TimerView : MonoBehaviour, IView
    {
        [SerializeField]
        Button _cancelButton;
        public Button CancelButton => _cancelButton;
        
        [SerializeField]
        Button _startButton;
        public Button StartButton => _startButton;
        
        [SerializeField]
        Button _pauseButton;
        public Button PauseButton => _pauseButton;
        
        [SerializeField]
        Button _resumeButton;
        public Button ResumeButton => _resumeButton;
        
        [SerializeField]
        GameObject _settingRoot;
        public GameObject SettingRoot => _settingRoot;

        [SerializeField]
        NumberTextScrollSnap _hourScrollSnap;
        public NumberTextScrollSnap HourScrollSnap => _hourScrollSnap;
        
        [SerializeField]
        NumberTextScrollSnap _minuteScrollSnap;
        public NumberTextScrollSnap MinuteScrollSnap => _minuteScrollSnap;
        
        [SerializeField]
        NumberTextScrollSnap _secondScrollSnap;
        public NumberTextScrollSnap SecondScrollSnap => _secondScrollSnap;
        
        [SerializeField]
        Image _progressRoot;
        public Image ProgressRoot => _progressRoot;

        [SerializeField]
        Text _progressHour;
        public Text ProgressHour => _progressHour;
        
        [SerializeField]
        Text _progressMinute;
        public Text ProgressMinute => _progressMinute;
        
        [SerializeField]
        Text _progressSecond;
        public Text ProgressSecond => _progressSecond;

        [SerializeField]
        AudioSource _alert;
        public AudioSource Alert => _alert;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClockApp
{
    public interface ITimerView : IView
    {
        Button CancelButton { get; }
        Button StartButton { get; }
        Button PauseButton { get; }
        Button ResumeButton { get; }
        GameObject SettingRoot { get; }
        NumberTextScrollSnap HourScrollSnap { get; }
        NumberTextScrollSnap MinuteScrollSnap { get; }
        NumberTextScrollSnap SecondScrollSnap { get; }
        Image ProgressRoot { get; }
        Text ProgressHour { get; }
        Text ProgressMinute { get; }
        Text ProgressSecond { get; }
        Text ProgressMilliSecond { get; }
        AudioSource Alert { get; }
    }

    public class TimerView : MonoBehaviour, ITimerView
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
        Text _progressMilliSecond;
        public Text ProgressMilliSecond => _progressMilliSecond;

        [SerializeField]
        AudioSource _alert;
        public AudioSource Alert => _alert;
        
        public void RunOnAwake()
        {
            _settingRoot.gameObject.SetActive(true);
            _progressRoot.gameObject.SetActive(false);
            _cancelButton.gameObject.SetActive(true);
            _startButton.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(false);
        }
    }
}
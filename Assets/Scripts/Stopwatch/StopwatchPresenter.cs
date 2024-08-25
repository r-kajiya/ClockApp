using System;
using UnityEngine;

namespace ClockApp
{
    public interface IStopwatchPresenter<out TView> : IPresenter<TView>
        where TView : IStopwatchView
    {
        Transform LapCellParent { get; }
        void RegisterActionOnClickLapButton(Action onAction);
        void RegisterActionOnClickResetButton(Action onAction);
        void RegisterActionOnClickStartButton(Action onAction);
        void RegisterActionOnClickPauseButton(Action onAction);
        void SetActiveLapButton(bool enable);
        void SetActiveResetButton(bool enable);
        void SetActiveStartButton(bool enable);
        void SetActivePauseButton(bool enable);
        void SetProgressTimer(int minute, int second, int millisecond);
    }

    public class StopwatchPresenter : IStopwatchPresenter<IStopwatchView>
    {
        public IStopwatchView View { get; }

        public Transform LapCellParent => View.LapCellListParent;
        
        public StopwatchPresenter(IStopwatchView view)
        {
            View = view;
        }
        
        public void RegisterActionOnClickLapButton(Action onAction)
        {
            View.LapButton.onClick.RemoveAllListeners();
            View.LapButton.onClick.AddListener(()=>onAction?.Invoke());
        }

        public void RegisterActionOnClickResetButton(Action onAction)
        {
            View.ResetButton.onClick.RemoveAllListeners();
            View.ResetButton.onClick.AddListener(()=>onAction?.Invoke());
        }

        public void RegisterActionOnClickStartButton(Action onAction)
        {
            View.StartButton.onClick.RemoveAllListeners();
            View.StartButton.onClick.AddListener(()=>onAction?.Invoke());
        }

        public void RegisterActionOnClickPauseButton(Action onAction)
        {
            View.PauseButton.onClick.RemoveAllListeners();
            View.PauseButton.onClick.AddListener(()=>onAction?.Invoke());
        }

        public void SetActiveLapButton(bool enable)
        {
            View.LapButton.gameObject.SetActive(enable);
        }

        public void SetActiveResetButton(bool enable)
        {
            View.ResetButton.gameObject.SetActive(enable);
        }

        public void SetActiveStartButton(bool enable)
        {
            View.StartButton.gameObject.SetActive(enable);
        }

        public void SetActivePauseButton(bool enable)
        {
            View.PauseButton.gameObject.SetActive(enable);
        }

        public void SetProgressTimer(int minute, int second, int millisecond)
        {
            View.Minute.text = $"{minute:D2}";
            View.Second.text = $"{second:D2}";
            View.Millisecond.text = $"{millisecond:D2}";
        }
    }
}
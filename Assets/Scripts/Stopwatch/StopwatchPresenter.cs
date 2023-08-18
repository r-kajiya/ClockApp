using System;
using UniRx;
using UnityEngine;
using VContainer;

namespace ClockApp
{
    public interface IStopwatchPresenter<out TView> : IPresenter<TView>
        where TView : IStopwatchView
    {
        Transform LapCellParent { get; }
        void RegisterActionOnClickLapButton(Action onAction, CompositeDisposable disposable);
        void RegisterActionOnClickResetButton(Action onAction, CompositeDisposable disposable);
        void RegisterActionOnClickStartButton(Action onAction, CompositeDisposable disposable);
        void RegisterActionOnClickPauseButton(Action onAction, CompositeDisposable disposable);
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

        [Inject]
        public StopwatchPresenter(IStopwatchView view)
        {
            View = view;
        }

        const int TAP_INTERVAL_MILLISECOND = 100;

        public void RegisterActionOnClickLapButton(Action onAction, CompositeDisposable disposable)
        {
            View.LapButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
        }

        public void RegisterActionOnClickResetButton(Action onAction, CompositeDisposable disposable)
        {
            View.ResetButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
        }

        public void RegisterActionOnClickStartButton(Action onAction, CompositeDisposable disposable)
        {
            View.StartButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
        }

        public void RegisterActionOnClickPauseButton(Action onAction, CompositeDisposable disposable)
        {
            View.PauseButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(TAP_INTERVAL_MILLISECOND)).Subscribe(_ => { onAction?.Invoke(); }).AddTo(disposable);
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
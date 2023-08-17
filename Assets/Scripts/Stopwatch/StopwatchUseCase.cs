using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace ClockApp
{
    public class StopwatchUseCase : UseCaseBase<StopwatchPresenter> ,ITickable, IStartable, IDisposable
    {
        bool _isStart;
        bool _isStarted;
        float _timerCount;
        int _cellCount;
        ReactiveProperty<float> _timerCounter;
        readonly CompositeDisposable _disposable = new();
        readonly StopwatchLapCellView _stopwatchLapCellViewPrefab;
        
        const int CELL_COUNT_MAX = 100;

        [Inject]
        public StopwatchUseCase(StopwatchPresenter presenter, StopwatchLapCellView stopwatchLapCellViewPrefab) : base(presenter)
        {
            _stopwatchLapCellViewPrefab = stopwatchLapCellViewPrefab;
        }

        public void Start()
        {
            Presenter.RegisterActionOnClickLapButton(OnLap, _disposable);
            Presenter.RegisterActionOnClickResetButton(OnReset, _disposable);
            Presenter.RegisterActionOnClickStartButton(OnStart, _disposable);
            Presenter.RegisterActionOnClickPauseButton(OnPause, _disposable);
            
            Presenter.SetActiveStartButton(true);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveLapButton(true);
            Presenter.SetActiveResetButton(false);
            
            _timerCounter = new ReactiveProperty<float>();
            _timerCounter.Subscribe(OnChangedTimerCount).AddTo(_disposable);
            _isStarted = false;
        }

        public void Tick()
        {
            if (_isStart)
            {
                _timerCounter.Value += Time.deltaTime;
            }
        }
        
        void IDisposable.Dispose()
        { 
            _disposable.Dispose();
        }

        void OnStart()
        {
            _isStart = true;
            _isStarted = true;
            Presenter.SetActiveStartButton(false);
            Presenter.SetActivePauseButton(true);
            Presenter.SetActiveLapButton(true);
            Presenter.SetActiveResetButton(false);
        }

        void OnLap()
        {
            if (_isStarted == false)
            {
                return;
            }
            
            Presenter.SetActiveStartButton(false);
            Presenter.SetActivePauseButton(true);
            Presenter.SetActiveLapButton(true);
            Presenter.SetActiveResetButton(false);
            
            if (_cellCount < CELL_COUNT_MAX)
            {
                TimeSpan timeSpan =  BuildTimeCounterTimeSpan(_timerCounter.Value);
                _cellCount++;
                var cell = InstantiateLapCell(timeSpan, _cellCount);
                var rectTransform = Presenter.LapCellParent as RectTransform;
                if (rectTransform != null)
                {
                    rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, cell.CellHeight() * _cellCount);
                }   
            }
        }

        void OnPause()
        {
            if (_isStarted == false)
            {
                return;
            }
            
            _isStart = false;
            Presenter.SetActiveStartButton(true);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveLapButton(false);
            Presenter.SetActiveResetButton(true);
        }

        void OnReset()
        {
            _timerCounter.Value = 0;
            Presenter.SetActiveStartButton(true);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveLapButton(true);
            Presenter.SetActiveResetButton(false);
            
            _cellCount = 0;
            for (int i = 0 ; i < Presenter.LapCellParent.childCount; i++)
            {
                Object.Destroy(Presenter.LapCellParent.GetChild(i).gameObject);
            }

            _isStarted = false;
        }

        StopwatchLapCellUseCase InstantiateLapCell(TimeSpan timeSpan, int lapNumber)
        {
            var stopwatchLapCellView = Object.Instantiate<StopwatchLapCellView>(_stopwatchLapCellViewPrefab, Presenter.LapCellParent);
            var cellPresenter = new StopwatchLapCellPresenter(stopwatchLapCellView);
            var cellUseCase = new StopwatchLapCellUseCase(cellPresenter);
            cellUseCase.SetProgressTimer(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            cellUseCase.SetLapNumber(lapNumber);
            return cellUseCase;
        }

        void OnChangedTimerCount(float timerCounter)
        {
            TimeSpan timeSpan = BuildTimeCounterTimeSpan(timerCounter);
            Presenter.SetProgressTimer(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);     
        }

        TimeSpan BuildTimeCounterTimeSpan(float timerCounter)
        {
            int timerCounterSecond = Mathf.CeilToInt(timerCounter);
            float timerCounterMilliSecond = timerCounter - Mathf.FloorToInt(timerCounter);
            TimeSpan timeSpan = new TimeSpan(0, 0, timerCounterSecond);
            int minute = timeSpan.Minutes;
            int second = timeSpan.Seconds;
            const float floatingDigitCoefficient = 100.0f; // example. 0.11 * 100.0 = 11
            int millisecond = Mathf.FloorToInt(timerCounterMilliSecond * floatingDigitCoefficient);
            return new TimeSpan(0, 0, minute,second,  millisecond);
        }
    }
}


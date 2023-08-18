using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace ClockApp
{
    public class StopwatchUseCase : UseCaseBase<StopwatchPresenter>, ITickable, IStartable, IDisposable
    {
        bool _isStart;
        bool _isStarted;
        float _timerCount;
        int _cellCount;
        TimeCountService _timerCounter;
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

            _timerCounter = new();
            _timerCounter.Subscribe(OnChangedTimerCount, _disposable);
            _isStarted = false;
        }

        public void Tick()
        {
            if (_isStart)
            {
                float dt = Time.deltaTime;
                _timerCounter.TickTack(dt);
            }
        }

        public void Dispose()
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
            BuildLap();
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
            _isStarted = false;
            _timerCounter.SetTimer(0.0f);
            Presenter.SetActiveStartButton(true);
            Presenter.SetActivePauseButton(false);
            Presenter.SetActiveLapButton(true);
            Presenter.SetActiveResetButton(false);
            DestroyAllCell();
        }

        void BuildLap()
        {
            if (_cellCount >= CELL_COUNT_MAX)
            {
                return;
            }

            _cellCount++;

            TimeSpan timeSpan = _timerCounter.ConvertTimeSpan();
            var cell = InstantiateLapCell(timeSpan, _cellCount);
            var rectTransform = Presenter.LapCellParent as RectTransform;
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, cell.CellHeight() * _cellCount);
            }
        }

        StopwatchLapCellUseCase InstantiateLapCell(TimeSpan timeSpan, int lapNumber)
        {
            var stopwatchLapCellView = Object.Instantiate(_stopwatchLapCellViewPrefab, Presenter.LapCellParent);
            var cellPresenter = new StopwatchLapCellPresenter(stopwatchLapCellView);
            var cellUseCase = new StopwatchLapCellUseCase(cellPresenter);
            cellUseCase.SetProgressTimer(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            cellUseCase.SetLapNumber(lapNumber);
            return cellUseCase;
        }

        void DestroyAllCell()
        {
            _cellCount = 0;
            for (int i = 0; i < Presenter.LapCellParent.childCount; i++)
            {
                Object.Destroy(Presenter.LapCellParent.GetChild(i).gameObject);
            }
        }

        void OnChangedTimerCount(float time)
        {
            TimeSpan timeSpan = _timerCounter.ConvertTimeSpan();
            Presenter.SetProgressTimer(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
    }
}
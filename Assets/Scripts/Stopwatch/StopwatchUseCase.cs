using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ClockApp
{
    public class StopwatchContainer
    {
        public IStopwatchPresenter<IStopwatchView> Presenter { get; }
        public IPlayerStopwatchGateway StopwatchGateway { get; }
        public StopwatchLapCellView StopwatchLapCellViewPrefab { get; }

        public StopwatchContainer(IStopwatchPresenter<IStopwatchView> presenter, IPlayerStopwatchGateway stopwatchGateway, StopwatchLapCellView stopwatchLapCellViewPrefab)
        {
            Presenter = presenter;
            StopwatchGateway = stopwatchGateway;
            StopwatchLapCellViewPrefab = stopwatchLapCellViewPrefab;
        }
    }

    public interface IStopwatchUseCase<out TPresenter, out TView> : IUseCase<TPresenter, TView>
        where TView : IStopwatchView
        where TPresenter : IStopwatchPresenter<TView>
    {
        void OnStart();
        void OnLap();
        void OnPause();
        void OnReset();
    }

    public class StopwatchUseCase : IStopwatchUseCase<IStopwatchPresenter<IStopwatchView>, IStopwatchView>
    {
        public IStopwatchPresenter<IStopwatchView> Presenter { get; }

        const int CELL_COUNT_MAX = 100;

        readonly StopwatchContainer _container;
        readonly TimeCountService _timerCounter;
        readonly List<StopwatchLapCellUseCase> _cells = new();

        bool _isStart;
        bool _isStarted;
        float _timerCount;
        int _cellCount;

        public StopwatchUseCase(StopwatchContainer container)
        {
            _container = container;
            Presenter = container.Presenter;
            Presenter.RegisterActionOnClickLapButton(OnLap);
            Presenter.RegisterActionOnClickResetButton(OnReset);
            Presenter.RegisterActionOnClickStartButton(OnStart);
            Presenter.RegisterActionOnClickPauseButton(OnPause);

            _timerCounter = new TimeCountService();
            _isStarted = false;

            var model = _container.StopwatchGateway.GetOwner();
            if (model != null)
            {
                _timerCounter.SetTimer(model.Tick);
                var timeSpan = _timerCounter.ConvertTimeSpan();
                Presenter.SetProgressTimer(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                SetActiveButtons(true, false, false, true);

                if (model.Laps != null && 
                    (model.Laps != null || model.Laps.Length == 0))
                {
                    foreach (double lap in model.Laps)
                    {
                        _cellCount++;
                        var cellTimeCounter = new TimeCountService();
                        cellTimeCounter.SetTimer(lap);
                        var cellTimeSpan = cellTimeCounter.ConvertTimeSpan();
                        var cell = InstantiateLapCell(cellTimeCounter.Timer, cellTimeSpan, _cellCount);
                        var rectTransform = Presenter.LapCellParent as RectTransform;
                        if (rectTransform != null)
                        {
                            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, cell.CellHeight() * _cellCount);
                        }
                        _cells.Add(cell);
                    }   
                }
            }
            else
            {
                SetActiveButtons(true, false, true, false);
            }
        }

        public void OnUpdate(float dt)
        {
            if (_isStart)
            {
                _timerCounter.TickTack(dt);
                ChangeTimerCountUI();
            }
        }

        public void OnStart()
        {
            _isStart = true;
            _isStarted = true;

            SetActiveButtons(false, true, true, false);
        }

        public void OnLap()
        {
            if (_isStarted == false)
            {
                return;
            }

            SetActiveButtons(false, true, true, false);
            BuildLap();
        }

        public void OnPause()
        {
            if (_isStarted == false)
            {
                return;
            }

            _isStart = false;
            SetActiveButtons(true, false, false, true);
        }

        public void OnReset()
        {
            _isStarted = false;
            _timerCounter.SetTimer(0.0f);
            ChangeTimerCountUI();
            SetActiveButtons(true, false, true, false);
            DestroyAllCell();
        }

        public void Dispose()
        {
            if (_cellCount == 0)
            {
                _container.StopwatchGateway.Save(new PlayerStopwatchModel(_timerCounter.Timer, null));
            }
            else
            {
                double[] laps = new double[_cellCount];
                for (int i = 0; i < _cellCount; i++)
                {
                    var cell = _cells[i];
                    laps[i] = cell.Timer;
                }
                _container.StopwatchGateway.Save(new PlayerStopwatchModel(_timerCounter.Timer, laps));    
            }
            
            DestroyAllCell();
        }

        void BuildLap()
        {
            if (_cellCount >= CELL_COUNT_MAX)
            {
                return;
            }

            _cellCount++;

            var timeSpan = _timerCounter.ConvertTimeSpan();
            var cell = InstantiateLapCell(_timerCounter.Timer, timeSpan, _cellCount);
            var rectTransform = Presenter.LapCellParent as RectTransform;
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, cell.CellHeight() * _cellCount);
            }
            
            _cells.Add(cell);
        }

        StopwatchLapCellUseCase InstantiateLapCell(double timer, TimeSpan timeSpan, int lapNumber)
        {
            var stopwatchLapCellView = Object.Instantiate(_container.StopwatchLapCellViewPrefab, Presenter.LapCellParent);
            var cellPresenter = new StopwatchLapCellPresenter(stopwatchLapCellView);
            var cellUseCase = new StopwatchLapCellUseCase(new StopwatchLapCellContainer(cellPresenter));
            cellUseCase.SetTimer(timer);
            cellUseCase.SetProgressTimer(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            cellUseCase.SetLapNumber(lapNumber);
            return cellUseCase;
        }

        void DestroyAllCell()
        {
            _cells.Clear();
            _cellCount = 0;
            for (var i = 0; i < Presenter.LapCellParent.childCount; i++)
            {
                Object.Destroy(Presenter.LapCellParent.GetChild(i).gameObject);
            }
        }

        void ChangeTimerCountUI()
        {
            var timeSpan = _timerCounter.ConvertTimeSpan();
            Presenter.SetProgressTimer(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }

        void SetActiveButtons(bool enableStart, bool enablePause, bool enableLap, bool enableReset)
        {
            Presenter.SetActiveStartButton(enableStart);
            Presenter.SetActivePauseButton(enablePause);
            Presenter.SetActiveLapButton(enableLap);
            Presenter.SetActiveResetButton(enableReset);
        }
    }
}
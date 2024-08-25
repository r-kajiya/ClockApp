using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClockApp
{
    public class StopwatchContextContainer : ContextContainer
    {
        public StopwatchContextContainer(Dictionary<int, IUseCase> useCases): base(useCases) { }
    }
    
    public class StopwatchContext : Context
    {
        [SerializeField]
        StopwatchView _stopwatchView;
        [SerializeField]
        StopwatchLapCellView _stopwatchLapCellViewPrefab;

        int _stopwatchUseCaseIndex;
        StopwatchContextContainer _stopwatchContextContainer;

        public override void RunOnAwake()
        {
            base.RunOnAwake();
            _stopwatchView.RunOnAwake();
        }

        protected override IEnumerator DoPreLoad(ContextContainer container)
        {
            _stopwatchContextContainer = container as StopwatchContextContainer;
            yield return base.DoPreLoad(container);
            
            _stopwatchUseCaseIndex = CommonUseCaseType.MAX;
            var stopwatchUseCase = new StopwatchUseCase(new StopwatchContainer(new StopwatchPresenter(_stopwatchView), new PlayerStopwatchGateway(),_stopwatchLapCellViewPrefab));
            UseCases.Add(_stopwatchUseCaseIndex, stopwatchUseCase);
            
            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.SetupByContext(null, OnChangeClock, OnChangeTimer);
        }

        protected override IEnumerator DoLoad(ContextContainer container)
        {
            yield return (UseCases[CommonUseCaseType.SCENE] as SceneUseCase)?.Change("Stopwatch");
        }

        protected override IEnumerator DoUnload()
        {
            (UseCases[_stopwatchUseCaseIndex] as StopwatchUseCase)?.Dispose();
            yield break;
        }

        void OnChangeClock()
        {
            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.ActiveClock();
            ChangeContext(Contexts["ClockContext"], new ClockContextContainer(_stopwatchContextContainer.CommonUseCases.Map));
        }
        
        void OnChangeTimer()
        {
            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.ActiveTimer();
            ChangeContext(Contexts["TimerContext"], new TimerContextContainer(_stopwatchContextContainer.CommonUseCases.Map));
        }
    }
}
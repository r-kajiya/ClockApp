using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClockApp
{
    public class ClockContextContainer : ContextContainer
    {
        public ClockContextContainer(Dictionary<int, IUseCase> useCases): base(useCases) { }
    }
    
    public class ClockContext : Context
    {
        [SerializeField]
        ClockView _clockView;

        int _clockUseCaseIndex;
        ClockContextContainer _clockContextContainer;

        public override void RunOnAwake()
        {
            base.RunOnAwake();
            _clockView.RunOnAwake();
        }
        
        protected override IEnumerator DoPreLoad(ContextContainer container)
        {
            _clockContextContainer = container as ClockContextContainer;
            
            yield return base.DoPreLoad(container);
            
            _clockUseCaseIndex = CommonUseCaseType.MAX;
            var clockUseCase = new ClockUseCase(new ClockContainer(new ClockPresenter(_clockView), new TimeZoneGateway()));
            UseCases.Add(_clockUseCaseIndex, clockUseCase);
            
            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.SetupByContext(OnChangeStopwatch, null, OnChangeTimer);
        }

        protected override IEnumerator DoLoad(ContextContainer container)
        {
            yield return (UseCases[CommonUseCaseType.SCENE] as SceneUseCase)?.Change("Clock");
        }

        void OnChangeStopwatch()
        {
            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.ActiveStopwatch();
            ChangeContext(Contexts["StopwatchContext"], new StopwatchContextContainer(_clockContextContainer.CommonUseCases.Map));
        }
        
        void OnChangeTimer()
        {
            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.ActiveTimer();
            ChangeContext(Contexts["TimerContext"], new TimerContextContainer(_clockContextContainer.CommonUseCases.Map));
        }
    }
}
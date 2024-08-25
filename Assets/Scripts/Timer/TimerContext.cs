using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClockApp
{
    public class TimerContextContainer : ContextContainer
    {
        public TimerContextContainer(Dictionary<int, IUseCase> useCases): base(useCases) { }
    }

    public class TimerContext : Context
    {
        [SerializeField]
        TimerView _timerView;

        int _timerUseCaseIndex;
        TimerContextContainer _timerContextContainer;
        
        public override void RunOnAwake()
        {
            base.RunOnAwake();
            _timerView.RunOnAwake();
        }

        protected override IEnumerator DoPreLoad(ContextContainer container)
        {
            _timerContextContainer = container as TimerContextContainer;
            yield return base.DoPreLoad(container);

            _timerUseCaseIndex = CommonUseCaseType.MAX;
            var timerUseCase = new TimerUseCase(new TimerContainer(new TimerPresenter(_timerView)));
            UseCases.Add(_timerUseCaseIndex, timerUseCase);

            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.SetupByContext(OnChangeStopwatch, OnChangeClock, null);
        }
        
        protected override IEnumerator DoLoad(ContextContainer container)
        {
            yield return (UseCases[CommonUseCaseType.SCENE] as SceneUseCase)?.Change("Timer");
        }

        void OnChangeStopwatch()
        {
            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.ActiveStopwatch();
            ChangeContext(Contexts["StopwatchContext"], new StopwatchContextContainer(_timerContextContainer.CommonUseCases.Map));
        }

        void OnChangeClock()
        {
            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.ActiveClock();
            ChangeContext(Contexts["ClockContext"], new ClockContextContainer(_timerContextContainer.CommonUseCases.Map));
        }
    }
}
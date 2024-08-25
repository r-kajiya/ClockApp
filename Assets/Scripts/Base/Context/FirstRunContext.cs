using System.Collections;
using UnityEngine;

namespace ClockApp
{
    public class FirstRunContext : Context
    {
        [SerializeField]
        FooterView _footerView;

        public override void RunOnAwake()
        {
            base.RunOnAwake();
            _footerView.RunOnAwake();
        }
        
        protected override IEnumerator DoPreLoad(ContextContainer container)
        {
            var footer = new FooterUseCase(new FooterContainer(new FooterPresenter(_footerView)));
            UseCases.Add(CommonUseCaseType.FOOTER, footer);
            var scene = new SceneUseCase();
            UseCases.Add(CommonUseCaseType.SCENE, scene);
            yield break;
        }

        protected override IEnumerator DoLoad(ContextContainer container)
        {
            yield return (UseCases[CommonUseCaseType.SCENE] as SceneUseCase)?.Change("Clock");
        }

        protected override IEnumerator DoLoaded(ContextContainer container)
        {
            OnChangeClock();
            return base.DoLoaded(container);
        }

        protected override IEnumerator DoUnloaded()
        {
            yield break;
        }
        
        void OnChangeClock()
        {
            (UseCases[CommonUseCaseType.FOOTER] as FooterUseCase)?.ActiveClock();
            ChangeContext(Contexts["ClockContext"], new ClockContextContainer(UseCases));
        }
    }    
}

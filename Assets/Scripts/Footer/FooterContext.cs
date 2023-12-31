using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public class FooterContext : LifetimeScope
    {
        [SerializeField]
        FooterView _footerView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<FooterUseCase>();
            builder.Register<IFooterPresenter<IFooterView>, FooterPresenter>(Lifetime.Singleton);
            builder.RegisterComponent<IFooterView>(_footerView);
        }
    }
}
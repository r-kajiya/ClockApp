namespace ClockApp
{
    public interface IPresenter { }
    
    public abstract class PresenterBase<TView> : IPresenter
        where TView : IView
    {
        protected TView View { get; }

        protected PresenterBase(TView view)
        {
            View = view;
        }
    }
}

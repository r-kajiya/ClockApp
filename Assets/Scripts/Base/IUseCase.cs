namespace ClockApp
{
    public interface IUseCase
    {
        public void OnUpdate(float dt);
    }
    
    public interface IUseCase<out TPresenter, out TView> : 
        IUseCase
        where TView : IView
        where TPresenter : IPresenter<TView>
    {
        public TPresenter Presenter { get; }
    }
}
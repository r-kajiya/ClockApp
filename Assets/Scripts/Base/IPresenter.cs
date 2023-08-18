namespace ClockApp
{
    public interface IPresenter<out TView>  
        where TView : IView
    {
        public TView View { get; }
    }
}

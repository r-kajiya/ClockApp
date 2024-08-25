using System;

namespace ClockApp
{
    public class ClockContainer
    {
        public IClockPresenter<IClockView> Presenter { get; }
        public ITimeZoneGateway TimeZoneGateway { get; }
        
        public ClockContainer(IClockPresenter<IClockView> presenter, ITimeZoneGateway timeZoneGateway)
        {
            Presenter = presenter;
            TimeZoneGateway = timeZoneGateway;
        }
    }

    public interface IClockUseCase<out TPresenter, out TView> : IUseCase<TPresenter, TView>
        where TView : IClockView
        where TPresenter : IClockPresenter<TView>
    {
        void TickTack();
    }

    public class ClockUseCase : IClockUseCase<IClockPresenter<IClockView>, IClockView>
    {
        public IClockPresenter<IClockView> Presenter { get; }
        
        readonly ClockContainer _container;
        TimeZoneModel _selectTimeZoneModel;
        
        public ClockUseCase(ClockContainer container)
        {
            _container = container;
            Presenter = container.Presenter;
            Presenter.RegisterActionOnClickPrevPlaceButton(OnPrevPlace);
            Presenter.RegisterActionOnClickNextPlaceButton(OnNextPlace);
            BuildTimeZone(1);
        }

        public void OnUpdate(float dt)
        {
            TickTack();
        }

        public void TickTack()
        {
            DateTime dateTime = TimeZoneService.ConvertDateTime(TimeZoneInfo.Utc);
            var addTime = dateTime.Add(new TimeSpan(0, _selectTimeZoneModel.UtcDiffHours, 0, 0));
            Presenter.SetDateTime(addTime);
            Presenter.SetPlaceName(_selectTimeZoneModel.PlaceName);
        }

        void OnPrevPlace()
        {
            BuildTimeZone(_selectTimeZoneModel.Id - 1);
        }
        
        void OnNextPlace()
        {
            BuildTimeZone(_selectTimeZoneModel.Id + 1);
        }

        void BuildTimeZone(int id)
        {
            _selectTimeZoneModel = _container.TimeZoneGateway.Get(new TimeZonePrimaryKey(id));
            var prevModel = _container.TimeZoneGateway.Get(new TimeZonePrimaryKey(id - 1));
            var nextModel = _container.TimeZoneGateway.Get(new TimeZonePrimaryKey(id + 1));
            Presenter.SetActiveButtons(prevModel != null, nextModel != null);
        }
    }
}
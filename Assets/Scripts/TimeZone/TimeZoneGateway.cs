using System.Collections.Generic;

namespace ClockApp
{
    public interface ITimeZoneGateway : IGateway<TimeZoneModel, TimeZonePrimaryKey> { }
    
    public class TimeZoneGateway : ITimeZoneGateway
    {
        public TimeZoneModel Get(TimeZonePrimaryKey primaryKey)
        {
            return TimeZoneRepository.I.Get(primaryKey);
        }

        public List<TimeZoneModel> GetAll()
        {
            return TimeZoneRepository.I.GetAll();
        }
    }
}
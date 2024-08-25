namespace ClockApp
{
    public class TimeZoneRepository : Repository<TimeZoneModel, TimeZoneDataStore, TimeZonePrimaryKey>
    {
        public static TimeZoneRepository I { get; }

        static TimeZoneRepository()
        {
            I = new TimeZoneRepository();
        }

        TimeZoneRepository() : base(new TimeZoneDataStore()) {}
    }
}


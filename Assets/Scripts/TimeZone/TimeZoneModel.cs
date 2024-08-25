namespace ClockApp
{
    public class TimeZoneModel : Model
    {
        public int Id { get; }
        public int UtcDiffHours { get; }
        public string PlaceName { get; }

        public TimeZoneModel(int id, int  utcDiffHours, string placeName)
        {
            Id = id;
            UtcDiffHours = utcDiffHours;
            PlaceName = placeName;
        }
    }
}


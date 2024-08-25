namespace ClockApp
{
    public class PlayerStopwatchRepository : Repository<PlayerStopwatchModel, PlayerStopwatchDataStore, PlayerStopwatchPrimaryKey>
    {
        public static PlayerStopwatchRepository I { get; }

        static PlayerStopwatchRepository()
        {
            I = new PlayerStopwatchRepository();
        }

        PlayerStopwatchRepository() : base(new PlayerStopwatchDataStore()) {}
    }
}


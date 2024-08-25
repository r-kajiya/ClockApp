
namespace ClockApp
{
    public class PlayerStopwatchModel : Model
    {
        public int Id { get; }
        public double Tick { get; }
        
        public double[] Laps { get; }

        public PlayerStopwatchModel(double tick, double[] laps)
        {
            Id = 0;
            Tick = tick;
            Laps = laps;
        }
    }
}


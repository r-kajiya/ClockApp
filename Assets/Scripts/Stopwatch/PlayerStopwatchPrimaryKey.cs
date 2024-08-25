namespace ClockApp
{
    public class PlayerStopwatchPrimaryKey : IPrimaryKey<PlayerStopwatchPrimaryKey, PlayerStopwatchModel>
    {
        PlayerStopwatchModel _model = new(0, null);

        public void Setup(PlayerStopwatchModel model)
        {
            _model = model;
        }

        public bool Equals(PlayerStopwatchPrimaryKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            
            if (_model.Id == other._model.Id)
            {
                return true;
            }
            
            return false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            
            return Equals((PlayerStopwatchPrimaryKey) obj);
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}


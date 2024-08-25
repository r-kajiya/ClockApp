namespace ClockApp
{
    public class TimeZonePrimaryKey : IPrimaryKey<TimeZonePrimaryKey, TimeZoneModel>
    {
        TimeZoneModel _model;

        public TimeZonePrimaryKey() { }

        public TimeZonePrimaryKey(int id)
        {
            _model = new TimeZoneModel(id, 0, string.Empty);
        }

        public void Setup(TimeZoneModel model)
        {
            _model = model;
        }

        public bool Equals(TimeZonePrimaryKey other)
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
            
            return Equals((TimeZonePrimaryKey) obj);
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}


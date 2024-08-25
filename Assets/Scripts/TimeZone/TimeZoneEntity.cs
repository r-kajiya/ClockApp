using System;
using UnityEngine;

namespace ClockApp
{
    [Serializable]
    public class TimeZoneEntity : IEntity
    {
        [SerializeField, HideInInspector]
        int id;
        public int Id => id;
        
        [SerializeField, HideInInspector]
        int utcDiffHours = -1;
        public int UtcDiffHours => utcDiffHours;

        [SerializeField, HideInInspector]
        string placeName = "";
        public string PlaceName => placeName;

        public TimeZoneEntity(TimeZoneModel model)
        {
            id = model.Id;
            utcDiffHours = model.UtcDiffHours;
            placeName = model.PlaceName;
        }
    }
}


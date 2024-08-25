using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace ClockApp
{
    [Serializable]
    public class PlayerStopwatchEntity : IEntity
    {
        [SerializeField, HideInInspector]
        double tick;
        public double Tick => tick;
        
        [SerializeField, HideInInspector]
        double[] laps;
        public double[] Laps => laps;

        public PlayerStopwatchEntity(PlayerStopwatchModel model)
        {
            tick = model.Tick;
            laps = model.Laps;
        }
    }
}


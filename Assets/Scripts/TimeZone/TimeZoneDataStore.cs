using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace ClockApp
{
    public class TimeZoneDataStore : IDataStore<TimeZoneModel, TimeZonePrimaryKey>
    {
        const string FILE_PATH = "TimeZone.json";

        [Serializable]
        class Entities
        {
            public TimeZoneEntity[] Values = null;
        }

       public Dictionary<TimeZonePrimaryKey, TimeZoneModel> Load()
        {
            string assetsPath = $"{Application.streamingAssetsPath}/{FILE_PATH}";
            string json = "";
#if UNITY_ANDROID && !UNITY_EDITOR
            WWW www = new WWW(assetsPath);
            while (!www.isDone) { }
            string txtBuffer = string.Empty;
            TextReader txtReader = new StringReader(www.text);
            string description = string.Empty;
            while ((txtBuffer = txtReader.ReadLine()) != null)
            {
                description = description + txtBuffer;
            }
            json = description;
#else
            FileInfo info = new FileInfo(assetsPath);
            StreamReader reader = new StreamReader(info.OpenRead());
            json = reader.ReadToEnd();
            reader.Close();
#endif
            if (string.IsNullOrEmpty(json))
            {
                return new Dictionary<TimeZonePrimaryKey, TimeZoneModel>();
            }

            var map = new Dictionary<TimeZonePrimaryKey, TimeZoneModel>();
            var parse = JsonUtility.FromJson<Entities>(json);

            foreach (var entity in parse.Values)
            {
                TimeZoneModel model = new TimeZoneModel(
                    entity.Id,
                    entity.UtcDiffHours,
                    entity.PlaceName);

                TimeZonePrimaryKey primaryKey = new TimeZonePrimaryKey();
                primaryKey.Setup(model);
                map.Add(primaryKey, model);
            }
            return map;
        }

        public void Save(TimeZoneModel model)
        {
            throw new NotImplementedException();
        }

        public void Remove(TimeZoneModel model)
        {
            throw new NotImplementedException();
        }
    }
}


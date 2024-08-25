using System;
using System.IO;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace ClockApp
{
    public class PlayerStopwatchDataStore : IDataStore<PlayerStopwatchModel, PlayerStopwatchPrimaryKey>
    {
        const string FILE_PATH = "PlayerStopwatch.json";

        [Serializable]
        class Entities
        {
            public PlayerStopwatchEntity[] Values = null;

            public static Entities ConvertFromMap(Dictionary<PlayerStopwatchPrimaryKey, PlayerStopwatchModel> map)
            {
                Entities entities = new Entities();
                entities.Values = new PlayerStopwatchEntity[map.Count];

                int index = 0;
                foreach (var model in map.Values)
                {
                    entities.Values[index] = new PlayerStopwatchEntity(model);
                    index++;
                }

                return entities;
            }

            public static Entities ConvertFromJson(string json)
            {
                Entities entities = JsonUtility.FromJson<Entities>(json);
                return entities;
            }
        }

        public void Save(PlayerStopwatchModel model)
        {
            Dictionary<PlayerStopwatchPrimaryKey, PlayerStopwatchModel> map = Load();
            PlayerStopwatchPrimaryKey primaryKey = new PlayerStopwatchPrimaryKey();
            primaryKey.Setup(model);
            map[primaryKey] = model;
            string json = JsonUtility.ToJson(Entities.ConvertFromMap(map));
            string filePath = $"{Application.persistentDataPath}/{FILE_PATH}";
            StreamWriter writer = File.CreateText(filePath);
            writer.Write(json);
            writer.Close();
        }

        public void Remove(PlayerStopwatchModel model)
        {
            Dictionary<PlayerStopwatchPrimaryKey, PlayerStopwatchModel> map = Load();
            PlayerStopwatchPrimaryKey primaryKey = new PlayerStopwatchPrimaryKey();
            primaryKey.Setup(model);

            if (map.ContainsKey(primaryKey))
            {
                map.Remove(primaryKey);
            }
            else
            {
                return;
            }

            string json = JsonUtility.ToJson(Entities.ConvertFromMap(map));
            string filePath = $"{Application.persistentDataPath}/{FILE_PATH}";
            StreamWriter writer = File.CreateText(filePath);
            writer.Write(json);
            writer.Close();
        }

        public Dictionary<PlayerStopwatchPrimaryKey, PlayerStopwatchModel> Load()
        {
            string filePath = $"{Application.persistentDataPath}/{FILE_PATH}";

            if (!File.Exists(filePath))
            {
                return new Dictionary<PlayerStopwatchPrimaryKey, PlayerStopwatchModel>();
            }

            string json = "";
#if UNITY_ANDROID && !UNITY_EDITOR
            WWW www = new WWW(filePath);
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
            FileInfo info = new FileInfo(filePath);
            StreamReader reader = new StreamReader(info.OpenRead());
            json = reader.ReadToEnd();
            reader.Close();
#endif
            if (string.IsNullOrEmpty(json))
            {
                return new Dictionary<PlayerStopwatchPrimaryKey, PlayerStopwatchModel>();
            }

            var map = new Dictionary<PlayerStopwatchPrimaryKey, PlayerStopwatchModel>();
            var parse = JsonUtility.FromJson<Entities>(json);

            foreach (var entity in parse.Values)
            {
                PlayerStopwatchModel model = new PlayerStopwatchModel(entity.Tick, entity.Laps);
                PlayerStopwatchPrimaryKey primaryKey = new PlayerStopwatchPrimaryKey();
                primaryKey.Setup(model);
                map.Add(primaryKey, model);
            }

            return map;
        }
    }
}
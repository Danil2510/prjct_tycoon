using System;
using UnityEngine;

namespace DefaultNamespace.SaveLoadSystem
{
    public static class SaveLoad
    {
        public static void Save<T>(string key, T data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
        }
        
        public static void Save(string key, object data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
        }

        public static void Load<T>(string key, Action<T> onLoaded)
        {
            if (HasKey(key) == false)
                throw new ArgumentException($"not found key: {key}");

            var json = PlayerPrefs.GetString(key);
            var data = JsonUtility.FromJson<T>(json);

            onLoaded?.Invoke(data);
        }

        public static bool HasKey(string key)
            => PlayerPrefs.HasKey(key);
    }
}
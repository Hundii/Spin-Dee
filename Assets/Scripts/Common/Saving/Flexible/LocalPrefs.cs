using Common.Saving.Flexible;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class LocalPrefs : MonoBehaviour, ILoadingSceneEntity, IPersistentManager
    {
        private Dictionary<string, string> storage;

        private Action<ILoadingSceneEntity> _onReady;

        private bool initialized = false;

        void ILoadingSceneEntity.OnCreation(Action<ILoadingSceneEntity> onReady)
        {
            _onReady = onReady;
        }

        private void Start()
        {
            Load();
        }

        public void SetItem(string key, string value)
        {
            storage[key] = value;
            Save();
        }

        public string GetItem(string key)
        {
            if (storage.TryGetValue(key,out var value))
            {
                return value;
            }
            return null;
        }

        public void RemoveItem(string key)
        {
            storage.Remove(key);
        }

        public void Save()
        {
            FlexibleSaveSystem.Save("LocalPrefs", storage,"Local_Prefs");
        }

        public void Load()
        {
            if (!initialized)
            {
                _onReady.Invoke(this);
                initialized = true;
            }
            storage = FlexibleSaveSystem.Load<Dictionary<string,string>>("LocalPrefs",new(),"Local_Prefs");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SharedDataService : MonoBehaviour, ILoadingSceneEntity
    {
        public object Data { get; private set; }
        private Action<UnityEngine.Object> _onReady;

        void ILoadingSceneEntity.OnCreation(Action<UnityEngine.Object> onReady)
        {
            _onReady = onReady;
        }

        private void Awake()
        {
            _onReady.Invoke(this);
        }

        public void SetData<T>(T data)
        {
            Data = data;
        }

        public T GetData<T>(bool deleteDataAfterGet = true)
        {
            T data = (T)Data;
            if (deleteDataAfterGet)
            {
                DeleteData();
            }
            return data;
        }

        public void DeleteData()
        {
            Data = null;
        }
    }
}

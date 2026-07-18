using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SharedDataService : MonoBehaviour, ILoadingSceneEntity, IPersistentManager
    {
        public object Data { get; private set; }
        private Action<ILoadingSceneEntity> _onReady;

        void ILoadingSceneEntity.OnCreation(Action<ILoadingSceneEntity> onReady)
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
            try
            {
                T data = (T)Data;
                if (deleteDataAfterGet)
                {
                    DeleteData();
                }
                return data;
            }
            catch
            {
                return default;
            }
           
        }

        public void DeleteData()
        {
            Data = null;
        }
    }
}

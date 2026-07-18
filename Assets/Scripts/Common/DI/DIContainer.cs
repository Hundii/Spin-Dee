using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    [DefaultExecutionOrder(-100)]
    public class DIContainer : MonoBehaviour
    {
        public static DIContainer Instance;

        private readonly Dictionary<Type, object> services = new();
        private Dictionary<Type, object> nonPersistentServices = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            Instance.nonPersistentServices = new();
            var services = Instance.services;
            var nonPersistentServices = Instance.nonPersistentServices;

            IPersistentManager[] persistentManagers = GetComponentsInChildren<IPersistentManager>();

            foreach (var entity in persistentManagers)
            {
                Type entityType = entity.GetType();
                if (!services.TryAdd(entityType, entity))
                {
                    Debug.LogWarning($"Entity with type {entityType} already exists in services");
                }
            }

            INonPersistentManager[] nonPersistentManagers = GetComponentsInChildren<INonPersistentManager>();

            foreach (var entity in nonPersistentManagers)
            {
                Type entityType = entity.GetType();
                if (!nonPersistentServices.TryAdd(entityType, entity))
                {
                    Debug.LogWarning($"Entity with type {entityType} already exists in non psersistent services");
                }
            }
        }

        public static void RegisterAsPersistent(IPersistentManager manager)
        {
            Instance.services.TryAdd(manager.GetType(),manager);
        }

        public static void RegisterAsNonPersistent(INonPersistentManager manager)
        {
            Instance.nonPersistentServices.TryAdd(manager.GetType(), manager);
        }

        public T GetService<T>()
        {
            if (services.TryGetValue(typeof(T), out object value))
            {
                return (T)value;
            }
            if (nonPersistentServices.TryGetValue(typeof(T), out object nonPersistentManager))
            {
                return (T)nonPersistentManager;
            }
            return GetServiceByInterface<T>();
        }

        public T GetServiceByInterface<T>()
        {
            var persistentKeys = services.Keys.ToList();
            foreach (var key in persistentKeys)
            {
                if (typeof(T).IsAssignableFrom(key))
                {
                    return (T)services[key];
                }
            }
            var nonPersistentKeys = services.Keys.ToList();
            foreach (var key in nonPersistentKeys)
            {
                if (typeof(T).IsAssignableFrom(key))
                {
                    return (T)nonPersistentServices[key];
                }
            }
            throw new Exception($"Service of type {typeof(T)} was not found in registered services");
        }
        public static T Inject<T>()
        {
            if (Instance == null)
            {
                return default;
            }
            return Instance.GetService<T>();
        }
    }
}

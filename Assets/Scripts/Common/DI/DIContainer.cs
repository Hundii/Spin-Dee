using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class DIContainer : MonoBehaviour
    {
        public static DIContainer Instance;

        [SerializeField] private SceneLoadHelper sceneLoadHelper;
        private readonly Dictionary<Type, object> services = new();
        private readonly List<UnityEngine.Object> readyServicesList = new();
        private Dictionary<Type, object> nonPersistentServices = new();
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Multiple DIContainers were found");
                Destroy(Instance);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            ILoadingSceneEntity[] entites = GetComponentsInChildren<ILoadingSceneEntity>();

            foreach (var entity in entites)
            {
                Type entityType = entity.GetType();
                if (!services.TryAdd(entityType, entity))
                {
                    Debug.LogWarning($"Entity with type {entityType} already exists in services");
                }
                else
                {
                    entity.OnCreation(OnServiceIsReady);
                }
            }
        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            nonPersistentServices = new();
            var managers = Utility.FindObjectsOfType<INonPersistentManager>();
            foreach (var manager in managers)
            {
                var type = manager.GetType();
                nonPersistentServices.Add(type, manager);
            }
        }

        private void OnServiceIsReady(UnityEngine.Object obj)
        {
            readyServicesList.Add(obj);
            if (readyServicesList.Count == services.Count)
            {
                Debug.Log("DI Container ready");
                if (!sceneLoadHelper.loadScene)
                {
                    return;
                }
                var sceneToLoad = sceneLoadHelper.SceneToLoadAfterLoadingScene;
#if UNITY_EDITOR
                sceneToLoad = sceneLoadHelper.EditorSceneToLoadAfterLoadingScene;
#endif
                if (string.IsNullOrEmpty(sceneToLoad) || sceneToLoad == "LoadingScene")
                {
                    return;
                }
                SceneManager.LoadScene(sceneToLoad);
            }
        }

        public List<object> GetNotReadyServices()
        {
            List<object> notReadyServices = new();
            foreach (var service in readyServicesList)
            {
                if (!services.Values.Contains(service))
                {
                    notReadyServices.Add(service);
                }
            }
            return notReadyServices;
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
            throw new Exception($"Service of type {typeof(T)} was not found in registered services");
        }

        public T GetServiceByInterface<T>()
        {
            var keys = services.Keys.ToList();
            foreach (var key in keys)
            {
                if (typeof(T).IsAssignableFrom(key))
                {
                    return (T)services[key];
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

        public static T InjectInterface<T>()
        {
            if (Instance == null)
            {
                return default;
            }
            return Instance.GetServiceByInterface<T>();
        }
    }
}

using System;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    [DefaultExecutionOrder(-99)]
    public class LoadingSceneManager : MonoBehaviour
    {
        [SerializeField] private string loadingSceneName = "LoadingScene";
        [SerializeField] private SceneLoadHelper sceneLoadHelper;
        private readonly List<ILoadingSceneEntity> entitiesList = new();
        private readonly List<ILoadingSceneEntity> readyEntitiesList = new();

        private void Awake()
        {
            ILoadingSceneEntity[] entites = Utility.FindObjectsOfType<ILoadingSceneEntity>().ToArray();

            foreach (var entity in entites)
            {
                entitiesList.Add(entity);
                entity.OnCreation(OnServiceIsReady);
            }
        }
        private void OnServiceIsReady(ILoadingSceneEntity obj)
        {
            readyEntitiesList.Add(obj);
            if (readyEntitiesList.Count == entitiesList.Count)
            {
                Debug.Log($"{loadingSceneName} ready");
                if (!sceneLoadHelper.loadScene)
                {
                    return;
                }
                var sceneToLoad = sceneLoadHelper.SceneToLoadAfterLoadingScene;
#if UNITY_EDITOR
                sceneToLoad = sceneLoadHelper.EditorSceneToLoadAfterLoadingScene;
#endif
                if (string.IsNullOrEmpty(sceneToLoad) || sceneToLoad == loadingSceneName)
                {
                    return;
                }
                SceneManager.LoadScene(sceneToLoad);
            }
        }

        public List<object> GetNotReadyServices()
        {
            List<object> notReadyServices = new();
            foreach (var service in readyEntitiesList)
            {
                if (!entitiesList.Contains(service))
                {
                    notReadyServices.Add(service);
                }
            }
            return notReadyServices;
        }
    }
}

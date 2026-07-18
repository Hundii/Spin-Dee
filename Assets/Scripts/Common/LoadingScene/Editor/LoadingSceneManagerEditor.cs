using UnityEditor;
using UnityEngine;

namespace Common.Editor
{
    [CustomEditor(typeof(LoadingSceneManager))]
    public class LoadingSceneManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var diContainer = (LoadingSceneManager)target;
            if (GUILayout.Button("Show not ready services"))
            {
                var services = diContainer.GetNotReadyServices();
                Debug.Log("Not ready services: ");
                foreach (var service in services)
                {
                    Debug.LogError(service);
                }
            }
        }
    }
}

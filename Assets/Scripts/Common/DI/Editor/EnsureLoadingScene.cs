using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Common.Editor
{
    [InitializeOnLoad]
    public static class EnsureLoadingScene
    {
        private const string LoadingSceneName = "LoadingScene";

        static EnsureLoadingScene()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                var sceneName = SceneManager.GetActiveScene().name;

                SceneLoadHelper helper = AssetDatabase.LoadAssetAtPath<SceneLoadHelper>(
                    "Assets/Resources/SO/SceneLoading/SceneLoadingHelper.asset");

                if (!sceneName.Equals(LoadingSceneName))
                {
                    helper.EditorSceneToLoadAfterLoadingScene = sceneName;
                    EditorUtility.SetDirty(helper);
                    AssetDatabase.SaveAssets();

                    if (SceneManager.GetActiveScene().isDirty)
                    {
                        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    }


                    EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(
                        $"Assets/Scenes/{LoadingSceneName}.unity"
                    );
                }
            }
        }
    }
}

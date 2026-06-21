using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "Common/SceneLoadHelper")]
    public class SceneLoadHelper : ScriptableObject
    {
        public string SceneToLoadAfterLoadingScene;
        public string EditorSceneToLoadAfterLoadingScene;
        public bool loadScene;
    }
}

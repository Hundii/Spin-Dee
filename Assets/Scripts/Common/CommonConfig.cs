using UnityEditor;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName ="Common/Config")]
    public class CommonConfig : ScriptableSingleton<CommonConfig>
    {
        [Header("SOContainerGenerator")]
        public string generatedScriptFolderPath = "Assets/Scripts/Core/Generated/";
        public string generatedSCriptNameSpace = "Core.Generated";
        public string generatedScriptableObjectMenuName = "Common/SOContainer";
    }
}

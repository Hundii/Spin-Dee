using System.Text;
using UnityEditor;
using UnityEngine;

namespace Common
{
    public abstract class ContainedSO : ScriptableObject
    {
#if UNITY_EDITOR
        protected string GetFolderPath(bool wholePath = false)
        {
            var g = AssetDatabase.FindAssets($"t:Script {nameof(SOContainerGenerator)}");
            string fullPath = AssetDatabase.GUIDToAssetPath(g[0]);
            if (wholePath)
            {
                return fullPath;
            }
            var parts = fullPath.Split('/');
            StringBuilder path = new(50);
            for (int i = 0; i < parts.Length - 1; i++)
            {
                path.Append(parts[i] + '/');
            }
            return path.ToString();
        }
        public void CreateContainer<T>(string contentPath) where T : Object
        {
            SOContainerGenerator.CreateScript<T>(contentPath);
        }
#endif
    }
}

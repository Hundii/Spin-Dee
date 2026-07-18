using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Common
{
    public static class SOContainerGenerator
    {
#if UNITY_EDITOR
        public static readonly string FolderPath = Utility.NormalizeFilePath(CommonConfig.instance.generatedScriptFolderPath);
        public static readonly string Namespace = CommonConfig.instance.generatedSCriptNameSpace;
        public static readonly string MenuName = Utility.NormalizeFilePath(CommonConfig.instance.generatedScriptableObjectMenuName);
        private static string GetFolderPathOfClass<T>()
        {
            var guid = AssetDatabase.FindAssets($"t:Script {typeof(T).Name}");
            string fullPath = AssetDatabase.GUIDToAssetPath(guid[0]);
            var parts = fullPath.Split('/');
            StringBuilder path = new(50);
            for (int i = 0; i < parts.Length - 1; i++)
            {
                path.Append(parts[i] + '/');
            }
            return path.ToString();
        }
        public static void CreateScript<T>(string contentPath) where T : Object
        {
            StringBuilder content = new(500);
            string className = typeof(T).Name;
            string fullClassName = className.EndsWith("SO") ? className + "Container" : className + "SOContainer";
            T[] objects = Resources.LoadAll<T>("");
            content.Append("\n//This is a generated script. You should not touch it.\n\n");
            content.Append($"using Common;\nusing UnityEngine;\nusing UnityEditor;\nusing System.Linq;\n");
            content.Append($"namespace {Namespace}\n");
            content.Append("{\n");
            content.Append($"[CreateAssetMenu(menuName = \"{MenuName.Replace('\\', '/')}/{className}\",fileName = \"{className}Container\")]\n" +
                $"public partial class {fullClassName} : GeneratedSOContainer\n{{\n");
            content.Append($"public {className}[] {className.ToPascalCase()}Array;\n");
            foreach (var obj in objects)
            {
                content.Append($"public {className} {obj.name.ToPascalCase()};\n");
            }
            content.Append("#if UNITY_EDITOR\n");
            content.Append("public override void FindReferences()\n{");
            content.Append($"{className}[] objects = Resources.LoadAll<{className}>(\"\");\n");
            content.Append($"{className.ToPascalCase()}Array = objects;\n");
            foreach (var obj in objects)
            {
                content.Append($"{obj.name.ToPascalCase()} = objects.First(x=>x.name == \"{obj.name}\");\n");
            }
            content.Append("EditorUtility.SetDirty(this);\n");
            content.Append("}\n");
            content.Append("#endif\n");
            content.Append("}\n");
            content.Append("}\n");
            string folderPath = string.IsNullOrEmpty(FolderPath) ? GetFolderPathOfClass<T>() : FolderPath;
            try
            {
                if (File.Exists($"{folderPath}/{fullClassName}.cs"))
                {
                    File.Delete($"{folderPath}/{fullClassName}.cs");
                }
                File.WriteAllText($"{folderPath}/{fullClassName}.cs", content.ToString());
                AssetDatabase.Refresh();
            }
            catch (System.Exception e)
            {
                Debug.LogError("File creation failed");
                Debug.LogError(e.Message);
                Debug.LogError(content.ToString());
            }
        }

        public static Object SelectContainer<T>()
        {
            string className = typeof(T).Name;
            string fullClassName = className.EndsWith("SO")
                    ? className + "Container"
                    : className + "SOContainer";

            var guids = AssetDatabase.FindAssets($"t:{fullClassName}");

            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                Object asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                return asset;
            }
            else
            {
                Debug.LogWarning($"No container asset found for {fullClassName}");
            }
            return null;
        }
#endif
    }
}

using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Common.Editor
{
    [CustomEditor(typeof(ContainedSO), true)]
    public class ContainedSOEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ContainedSO scr = (ContainedSO)target;
            string path = AssetDatabase.GetAssetPath(scr);
            path = path.Replace("Assets/Resources/", "");
            var parts = path.Split('/');
            StringBuilder str = new(50);
            for (int i = 0; i < parts.Length - 1; i++)
            {
                str.Append(parts[i] + '/');
            }
            if (str.Length > 0)
            {
                str.Remove(str.ToString().Length - 1, 1);
            }

            Type derivedType = target.GetType();
            if (GUILayout.Button("Create container"))
            {
                var method = typeof(ContainedSO).GetMethod("CreateContainer");
                var genericMethod = method.MakeGenericMethod(derivedType);
                genericMethod.Invoke(target, new object[] { str.ToString() });
            }
            if (GUILayout.Button("Jump to Container"))
            {
                var method = typeof(ContainedSO).GetMethod("SelectContainer");
                var genericMethod = method.MakeGenericMethod(derivedType);
                var asset = (UnityEngine.Object)genericMethod.Invoke(target, new object[] { });
                Selection.activeObject = asset;
                EditorGUIUtility.PingObject(asset);
            }
        }
    }
}


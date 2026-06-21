using UnityEditor;
using UnityEngine;

namespace Common.Editor
{
    [CustomEditor(typeof(GeneratedSOContainer), true)]
    public class GeneratedSOContainerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GeneratedSOContainer container = (GeneratedSOContainer)target;
            if (GUILayout.Button("Find references"))
            {
                container.FindReferences();
            }
        }
    }
}

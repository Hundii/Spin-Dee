using Common.UI;
using UnityEditor;
using UnityEngine;

namespace Common.Editor
{
    [CustomEditor(typeof(Spacer))]
    public class SpacerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var spacer = (Spacer)target;
            if (GUILayout.Button("Hide spacer"))
            {
                spacer.HideSpacer();
            }
            if (GUILayout.Button("Hide all spacers"))
            {
                SetAllSpacers(false);
            }
            if (GUILayout.Button("Show spacer"))
            {
                spacer.ShowSpacer();
            }
            if (GUILayout.Button("Show all spacers"))
            {
                SetAllSpacers(true);
            }
        }


        private void SetAllSpacers(bool show)
        {
            var spacers = FindObjectsByType<Spacer>(
                FindObjectsSortMode.None);

            foreach (var spacer in spacers)
            {
                Undo.RecordObject(spacer, "Toggle Spacers");

                if (show)
                {
                    spacer.ShowSpacer();
                }
                else
                {
                    spacer.HideSpacer();
                }

                EditorUtility.SetDirty(spacer);
            }
        }
    }
}

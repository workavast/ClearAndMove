using App.UI.LayoutSpawners;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.UI.LayoutSpawners.Editor
{
    [CustomEditor(typeof(VerticalSpawner))]
    public class VerticalSpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            VerticalSpawner spawner = (VerticalSpawner)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Update Objects"))
            {
                spawner.SpawnObjects();
            }

            if (GUILayout.Button("Clear All Children"))
            {
                spawner.ClearChildren();
            }
        }
    }
}
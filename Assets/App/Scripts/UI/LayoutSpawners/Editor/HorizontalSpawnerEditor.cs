using App.UI.LayoutSpawners;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.UI.LayoutSpawners.Editor
{
    [CustomEditor(typeof(HorizontalSpawner))]
    public class HorizontalSpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            HorizontalSpawner spawner = (HorizontalSpawner)target;

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
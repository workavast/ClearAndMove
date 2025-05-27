using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MaterialsInChildrenReplace : EditorWindow
{
    [SerializeField] private List<GameObject> targetObjects = new List<GameObject>();
    [SerializeField] private Material newMaterial;

    SerializedObject serializedObject;

    [MenuItem("Tools/Materials In Children Replacer")]
    private static void ShowWindow()
    {
        var window = GetWindow<MaterialsInChildrenReplace>("Replace Materials");
        window.serializedObject = new SerializedObject(window);
    }

    private void OnGUI()
    {
        if (serializedObject == null)
        {
            serializedObject = new SerializedObject(this);
        }

        serializedObject.Update();

        EditorGUILayout.LabelField("Target Objects or Prefabs", EditorStyles.boldLabel);

        var targetsProperty = serializedObject.FindProperty("targetObjects");
        EditorGUILayout.PropertyField(targetsProperty, new GUIContent("Targets"), true);

        var materialProperty = serializedObject.FindProperty("newMaterial");
        EditorGUILayout.PropertyField(materialProperty, new GUIContent("New Material"));

        if (GUILayout.Button("Replace Materials"))
        {
            serializedObject.ApplyModifiedProperties();

            if (targetObjects == null || targetObjects.Count == 0 || newMaterial == null)
            {
                Debug.LogError("Please assign at least one Target and a New Material.");
                return;
            }

            ReplaceMaterials();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ReplaceMaterials()
    {
        var totalCount = 0;

        foreach (var target in targetObjects)
        {
            if (target == null) continue;

            var count = 0;
            var renderers = target.GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers)
            {
                Undo.RecordObject(renderer, "Replace Materials");

                var newMats = new Material[renderer.sharedMaterials.Length];
                for (var i = 0; i < newMats.Length; i++)
                {
                    newMats[i] = newMaterial;
                }
                renderer.sharedMaterials = newMats;
                count++;
            }

            Debug.Log($"Replaced materials on {count} renderers in {target.name}");
            EditorUtility.SetDirty(target);
            totalCount += count;
        }

        Debug.Log($"Done! Replaced materials on total {totalCount} renderers across {targetObjects.Count} objects.");
    }
}

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ReplaceMaterialsInChildren : EditorWindow
{
    [SerializeField]
    private List<GameObject> targetObjects = new List<GameObject>();
    [SerializeField]
    private Material newMaterial;

    SerializedObject serializedObject;

    [MenuItem("Tools/Replace Materials In Children")]
    static void ShowWindow()
    {
        var window = GetWindow<ReplaceMaterialsInChildren>("Replace Materials");
        window.serializedObject = new SerializedObject(window);
    }

    void OnGUI()
    {
        if (serializedObject == null)
        {
            serializedObject = new SerializedObject(this);
        }

        serializedObject.Update();

        EditorGUILayout.LabelField("Target Objects or Prefabs", EditorStyles.boldLabel);

        SerializedProperty targetsProperty = serializedObject.FindProperty("targetObjects");
        EditorGUILayout.PropertyField(targetsProperty, new GUIContent("Targets"), true);

        SerializedProperty materialProperty = serializedObject.FindProperty("newMaterial");
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

    void ReplaceMaterials()
    {
        int totalCount = 0;

        foreach (var target in targetObjects)
        {
            if (target == null) continue;

            int count = 0;
            var renderers = target.GetComponentsInChildren<Renderer>(true);
            foreach (var renderer in renderers)
            {
                Undo.RecordObject(renderer, "Replace Materials");

                Material[] newMats = new Material[renderer.sharedMaterials.Length];
                for (int i = 0; i < newMats.Length; i++)
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

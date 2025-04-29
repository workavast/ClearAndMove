using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class SetStaticShadowCasterOnPrefabs : EditorWindow
{
    [SerializeField]
    private List<GameObject> prefabsToProcess = new List<GameObject>();

    [MenuItem("Tools/Set Static Shadow Caster on Prefabs")]
    private static void ShowWindow()
    {
        var window = GetWindow<SetStaticShadowCasterOnPrefabs>();
        window.titleContent = new GUIContent("Static Shadow Caster Setter");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Prefabs to Process", EditorStyles.boldLabel);
        SerializedObject so = new SerializedObject(this);
        SerializedProperty prefabsProperty = so.FindProperty("prefabsToProcess");

        EditorGUILayout.PropertyField(prefabsProperty, true);
        so.ApplyModifiedProperties();

        GUILayout.Space(10);

        if (GUILayout.Button("Apply Static Shadow Caster"))
        {
            ApplyStaticShadowCaster();
        }
    }

    private void ApplyStaticShadowCaster()
    {
        foreach (var prefab in prefabsToProcess)
        {
            if (prefab == null)
            {
                Debug.LogWarning("Skipped null prefab.");
                continue;
            }

            string prefabPath = AssetDatabase.GetAssetPath(prefab);
            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError($"Prefab '{prefab.name}' is not a valid asset.");
                continue;
            }

            GameObject prefabRoot = PrefabUtility.LoadPrefabContents(prefabPath);
            if (prefabRoot == null)
            {
                Debug.LogError($"Failed to load prefab at path: {prefabPath}");
                continue;
            }

            MeshRenderer[] renderers = prefabRoot.GetComponentsInChildren<MeshRenderer>(true);
            int modifiedCount = 0;

            foreach (var renderer in renderers)
            {
                if (renderer != null && !renderer.staticShadowCaster)
                {
                    renderer.staticShadowCaster = true;
                    EditorUtility.SetDirty(renderer);
                    modifiedCount++;
                }
            }

            PrefabUtility.SaveAsPrefabAsset(prefabRoot, prefabPath);
            PrefabUtility.UnloadPrefabContents(prefabRoot);

            Debug.Log($"Processed '{prefab.name}': Modified {modifiedCount} MeshRenderers.");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

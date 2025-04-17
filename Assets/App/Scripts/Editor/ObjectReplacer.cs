#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ObjectReplacer : EditorWindow
{
    private GameObject prefab;
    private List<GameObject> objectsToReplace = new List<GameObject>();
    private Vector2 scrollPosition;

    [MenuItem("Tools/Object Replacer")]
    public static void ShowWindow()
    {
        GetWindow<ObjectReplacer>("Object Replacer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Object Replacement Tool", EditorStyles.boldLabel);

        // Prefab selection
        prefab = (GameObject)EditorGUILayout.ObjectField("Replace with Prefab", prefab, typeof(GameObject), false);

        // Objects to replace list
        EditorGUILayout.Space();
        GUILayout.Label("Objects to Replace:");

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        for (int i = 0; i < objectsToReplace.Count; i++)
        {
            objectsToReplace[i] = (GameObject)EditorGUILayout.ObjectField(
                $"Object {i + 1}",
                objectsToReplace[i],
                typeof(GameObject),
                true);
        }
        EditorGUILayout.EndScrollView();

        // Add/remove objects buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Selected Objects"))
        {
            AddSelectedObjects();
        }
        if (GUILayout.Button("Clear List"))
        {
            objectsToReplace.Clear();
        }
        EditorGUILayout.EndHorizontal();

        // Replacement button
        EditorGUI.BeginDisabledGroup(prefab == null || objectsToReplace.Count == 0);
        if (GUILayout.Button("Replace Objects"))
        {
            ReplaceObjects();
        }
        EditorGUI.EndDisabledGroup();

        if (prefab == null)
        {
            EditorGUILayout.HelpBox("Please assign a prefab to replace with", MessageType.Warning);
        }
        else if (objectsToReplace.Count == 0)
        {
            EditorGUILayout.HelpBox("Please add objects to replace", MessageType.Warning);
        }
    }

    private void AddSelectedObjects()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (!objectsToReplace.Contains(obj))
            {
                objectsToReplace.Add(obj);
            }
        }
    }

    private void ReplaceObjects()
    {
        if (!EditorUtility.DisplayDialog("Confirm Replacement",
            $"Are you sure you want to replace {objectsToReplace.Count} objects?",
            "Replace",
            "Cancel")) return;

        Undo.SetCurrentGroupName("Object Replacement");
        int group = Undo.GetCurrentGroup();

        foreach (GameObject oldObject in objectsToReplace)
        {
            if (oldObject == null) continue;

            // Save transform values
            Transform oldTransform = oldObject.transform;
            Vector3 position = oldTransform.position;
            Quaternion rotation = oldTransform.rotation;
            Vector3 scale = oldTransform.localScale;
            Transform parent = oldTransform.parent;

            // Create new object
            GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            newObject.transform.SetParent(parent);
            newObject.transform.SetPositionAndRotation(position, rotation);
            newObject.transform.localScale = scale;

            // Register undo and destroy old object
            Undo.RegisterCreatedObjectUndo(newObject, "Replace Object");
            Undo.DestroyObjectImmediate(oldObject);
        }

        Undo.CollapseUndoOperations(group);
        objectsToReplace.Clear();
    }
}
#endif
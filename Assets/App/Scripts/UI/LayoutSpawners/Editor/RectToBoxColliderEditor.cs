using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RectToBoxCollider))]
public class RectToBoxColliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RectToBoxCollider script = (RectToBoxCollider)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Update Collider"))
        {
            script.UpdateColliderSize();
        }
    }
}
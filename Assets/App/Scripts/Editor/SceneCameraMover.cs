using UnityEditor;
using UnityEngine;

namespace App.Scripts.Editor
{
    public class SceneCameraMover : EditorWindow
    {
        private GameObject targetObject;

        [MenuItem("Tools/Move to Scene Camera")]
        public static void ShowWindow()
        {
            GetWindow<SceneCameraMover>("Scene Camera Mover");
        }

        void OnGUI()
        {
            GUILayout.Label("Move Object to Scene Camera", EditorStyles.boldLabel);

            // Поле для выбора объекта
            targetObject = (GameObject)EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true);

            if (GUILayout.Button("Move to Scene Camera"))
            {
                MoveObjectToSceneCamera();
            }
        }

        void MoveObjectToSceneCamera()
        {
            if (targetObject == null)
            {
                Debug.LogWarning("No target object assigned!");
                return;
            }

            // Получаем текущую сцену камеры редактора
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null && sceneView.camera != null)
            {
                Undo.RecordObject(targetObject.transform, "Move to Scene Camera");
            
                // Устанавливаем позицию и поворот
                targetObject.transform.position = sceneView.camera.transform.position;
                targetObject.transform.rotation = sceneView.camera.transform.rotation;
            
                Debug.Log($"Moved {targetObject.name} to scene camera");
            }
        }
    }
}
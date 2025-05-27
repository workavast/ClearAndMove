using System.IO;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Editor
{
    public class SceneViewScreenshotWindow : EditorWindow
    {
        int width = 1920;
        int height = 1080;
        string saveFolder = "Screenshots";
        string fileName = "SceneViewScreenshot";

        [MenuItem("Tools/Scene View Screenshot Window")]
        public static void ShowWindow() 
            => GetWindow<SceneViewScreenshotWindow>("Scene View Screenshot");

        private void OnGUI()
        {
            GUILayout.Label("Screenshot Settings", EditorStyles.boldLabel);
            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);
            fileName = EditorGUILayout.TextField("File Name", fileName);
            saveFolder = EditorGUILayout.TextField("Save Folder (relative to project)", saveFolder);

            GUILayout.Space(10);

            if (GUILayout.Button("Take Screenshot")) 
                TakeScreenshot();
        }

        private void TakeScreenshot()
        {
            var sceneView = SceneView.lastActiveSceneView;
            if (sceneView == null)
            {
                Debug.LogError("No active Scene View found!");
                return;
            }

            var camera = sceneView.camera;
            if (camera == null)
            {
                Debug.LogError("Scene View camera not found!");
                return;
            }

            var rt = new RenderTexture(width, height, 24);
            var screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);

            camera.targetTexture = rt;
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenShot.Apply();

            camera.targetTexture = null;
            RenderTexture.active = null;
            DestroyImmediate(rt);

            var directory = Path.Combine(Application.dataPath, saveFolder);
            if (!Directory.Exists(directory)) 
                Directory.CreateDirectory(directory);

            var filePath = Path.Combine(directory, fileName + ".png");
            File.WriteAllBytes(filePath, screenShot.EncodeToPNG());

            Debug.Log($"Screenshot saved to: {filePath}");
            AssetDatabase.Refresh();
        }
    }
}

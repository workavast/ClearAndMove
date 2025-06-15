using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace App.CameraBehaviour.AntiAliasing
{
    [RequireComponent(typeof(Camera))]
    public class CameraAntiAliasing : MonoBehaviour
    {
        [Inject] private readonly AntiAliasingProvider _antiAliasingProvider;
        
        private Camera _camera;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _antiAliasingProvider.OnAntialiasingModeChanged += UpdateAntialiasing;
            UpdateAntialiasing();
        }

        private void OnDestroy()
        {
            _antiAliasingProvider.OnAntialiasingModeChanged -= UpdateAntialiasing;
        }

        private void UpdateAntialiasing()
        {
            var camData = _camera.GetUniversalAdditionalCameraData();
            camData.antialiasing = _antiAliasingProvider.AntialiasingMode;
        }
    }
}
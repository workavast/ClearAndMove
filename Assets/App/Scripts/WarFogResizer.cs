using UnityEngine;

namespace App
{
    public class WarFogResizer : MonoBehaviour
    {
        [SerializeField] private Camera warFogCamera;
        [SerializeField] private RenderTexture renderTexture;
        
        private Vector2Int _prevSize;

        private void Awake()
        {
            _prevSize = new Vector2Int(Screen.width, Screen.height);
            SetSize(_prevSize);
        }

        private void Update()
        {
            var newSize = new Vector2Int(Screen.width, Screen.height);
            if (newSize != _prevSize)
            {
                _prevSize = newSize;
                SetSize(_prevSize);
            }
        }

        private void SetSize(Vector2Int newSize)
        {
            if (renderTexture.width == newSize.x && renderTexture.height == newSize.y)
                return;

            renderTexture.Release();
            renderTexture.width = newSize.x;
            renderTexture.height = newSize.y;
            renderTexture.Create();

            warFogCamera.Render();
        }
    }
}
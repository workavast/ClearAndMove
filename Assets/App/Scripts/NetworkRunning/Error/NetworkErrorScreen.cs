using App.CursorBehaviour;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.NetworkRunning
{
    public class NetworkErrorScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private Button closeBtn;
        
        private CursorVisibilityBehaviour _cursorVisibilityBehaviour;

        public void Initialize(CursorVisibilityBehaviour cursorVisibilityBehaviour)
        {
            _cursorVisibilityBehaviour = cursorVisibilityBehaviour;
        }
        
        private void Awake()
        {
            closeBtn.onClick.AddListener(Close);
        }

        private void OnEnable() 
            => _cursorVisibilityBehaviour.Show();

        private void OnDisable() 
            => _cursorVisibilityBehaviour.Hide();
        
        public void ShowError(string error)
        {
            gameObject.SetActive(true);
            tmpText.text = error;
        }
        
        private void Close() 
            => gameObject.SetActive(false);
    }
}
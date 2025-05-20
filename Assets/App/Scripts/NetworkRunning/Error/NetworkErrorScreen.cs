using App.CursorBehaviour;
using Fusion;
using Fusion.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace App.NetworkRunning.Error
{
    public class NetworkErrorScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private Button closeBtn;
        [Space] 
        [SerializeField] private LocalizedString shutdownString;
        [SerializeField] private LocalizedString disconnectString;
        [SerializeField] private LocalizedString connectFailedString;
        
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

        public void Show(ShutdownReason error) 
            => ShowError($"{shutdownString.GetLocalizedString()} {error}");

        public void Show(NetDisconnectReason error)
            => ShowError($"{disconnectString.GetLocalizedString()} {error}");
        
        public void Show(NetConnectFailedReason error)
            => ShowError($"{connectFailedString.GetLocalizedString()} {error}");

        public void ShowError(string error)
        {
            gameObject.SetActive(true);
            tmpText.text = error;
        }
        
        private void Close() 
            => gameObject.SetActive(false);
    }
}
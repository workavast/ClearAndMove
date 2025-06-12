using App.DisconnectProviding;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI
{
    [RequireComponent(typeof(Button))]
    public class DisconnectButton : MonoBehaviour
    {
        [Inject] private readonly IDisconnectInvoker _disconnectInvoker;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Disconnect);
        }

        private void Disconnect()
            => _disconnectInvoker.Disconnect();
    }
}
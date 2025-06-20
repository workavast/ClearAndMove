using App.ScenesLoading;
using App.Session.Creation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI.SessionCreation
{
    [RequireComponent(typeof(Button))]
    public class QuickStartSessionButton : MonoBehaviour
    {
        [SerializeField] private bool skipLoadingScreen;
        [Inject] private SessionCreator _sessionCreator;

        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(QuickStart);
        }

        private void QuickStart() 
            => _sessionCreator.QuickStart(ScenesConfig.Lobby, skipLoadingScreen);
    }
}
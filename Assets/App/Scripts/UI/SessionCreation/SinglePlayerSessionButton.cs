using App.ScenesLoading;
using App.Session.Creation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.UI.SessionCreation
{
    [RequireComponent(typeof(Button))]
    public class SinglePlayerSessionButton : MonoBehaviour
    {
        [SerializeField] private SceneType scene;
        [SerializeField] private bool skipLoadingScene;
        
        [Inject] private SessionCreator _sessionCreator;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(StartSinglePlayerSession);
        }

        private void StartSinglePlayerSession() 
            => _sessionCreator.CreateSinglePlayer(scene.GetIndex(), skipLoadingScene);
    }
}
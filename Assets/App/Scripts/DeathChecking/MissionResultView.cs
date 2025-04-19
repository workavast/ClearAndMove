using UnityEngine;

namespace App.DeathChecking
{
    public class MissionResultView : MonoBehaviour
    {
        [SerializeField] private DeathChecker deathChecker;
        [Space] 
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject looseScreen;
        
        private void OnEnable()
        {
            winScreen.SetActive(!deathChecker.AllPlayersUnAlive);
            looseScreen.SetActive(deathChecker.AllPlayersUnAlive);
        }
    }
}
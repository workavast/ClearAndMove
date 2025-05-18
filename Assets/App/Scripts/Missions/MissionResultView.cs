using App.Missions.MissionState;
using UnityEngine;

namespace App.Missions
{
    public class MissionResultView : MonoBehaviour
    {
        [SerializeField] private NetMissionState missionState;
        [Space] 
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject looseScreen;
        
        private void OnEnable()
        {
            winScreen.SetActive(missionState.IsCompleted);
            looseScreen.SetActive(missionState.IsFail);
        }
    }
}
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Lobby.Diegetic
{
    public class DiegeticTarget : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private DiegeticTarget nextDiegeticTarget;
        [SerializeField] private DiegeticTarget prevDiegeticTarget;
        [SerializeField] private LocalizedString localizedName;
        
        public void Activate()
        {
            cinemachineCamera.Priority = 10;
        }

        public void Deactivate()
        {
            cinemachineCamera.Priority = 0;
        }

        public bool TryGetNextTarget(out DiegeticTarget diegeticTarget)
        {
            diegeticTarget = nextDiegeticTarget;
            return diegeticTarget != null;
        }
        
        public bool TryGetPrevTarget(out DiegeticTarget diegeticTarget)
        {
            diegeticTarget = prevDiegeticTarget;
            return diegeticTarget != null;
        }

        public string GetLocalizedName() 
            => localizedName.GetLocalizedString();
    }
}
using Unity.Cinemachine;
using UnityEngine;

namespace App.Lobby.Diegetic
{
    public class DiegeticTarget : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private DiegeticTarget nextDiegeticTarget;
        [SerializeField] private DiegeticTarget prevDiegeticTarget;
        
        public void Activate()
        {
            cinemachineCamera.Priority = 10;
        }

        public void Deactivate()
        {
            cinemachineCamera.Priority = 0;
        }

        public bool TryGetNextTable(out DiegeticTarget diegeticTarget)
        {
            diegeticTarget = nextDiegeticTarget;
            return diegeticTarget != null;
        }
        
        public bool TryGetPrevTable(out DiegeticTarget diegeticTarget)
        {
            diegeticTarget = prevDiegeticTarget;
            return diegeticTarget != null;
        }
    }
}
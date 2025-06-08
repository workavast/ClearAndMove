using Unity.Cinemachine;
using UnityEngine;

namespace App.Lobby.Diegetic
{
    public class Table : MonoBehaviour
    {
        [SerializeField] public CinemachineCamera cinemachineCamera;
        
        [field: SerializeField] public Transform lookPoint {get; private set;}
        [field: SerializeField] public Transform cameraPosition {get; private set;}

        public void Activate()
        {
            cinemachineCamera.Priority = 10;
        }

        public void Deactivate()
        {
            cinemachineCamera.Priority = 0;
        }
    }
}
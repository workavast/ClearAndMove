using UnityEngine;

namespace App.Lobby.Missions.Map
{
    public class ViewBlocker : MonoBehaviour
    {
        public void SetState(bool isActive) 
            => gameObject.SetActive(isActive);
    }
}
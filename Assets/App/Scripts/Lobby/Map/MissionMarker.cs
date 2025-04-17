using System;
using App.Missions;
using UnityEngine;
using UnityEngine.UI;

namespace App.Lobby.Map
{
    public class MissionMarker : MonoBehaviour
    {
        [field: SerializeField] public MissionConfig MissionConfig { get; private set; }
        [SerializeField] private Button markerButton;
        [SerializeField] private GameObject center;
        
        public event Action<MissionMarker> OnClicked;

        private void Awake() 
            => markerButton.onClick.AddListener(() => OnClicked?.Invoke(this));

        public void SetState(bool isSelected) 
            => center.SetActive(isSelected);
    }
}
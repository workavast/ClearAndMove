using System;
using App.Missions;
using UnityEngine;
using UnityEngine.UI;

namespace App.Lobby.Missions.Map
{
    public class MapMissionMarker : MonoBehaviour
    {
        [field: SerializeField] public MissionConfig MissionConfig { get; private set; }
        [SerializeField] private Button markerButton;
        [SerializeField] private GameObject center;
        
        public event Action<MapMissionMarker> OnClicked;

        private void Awake() 
            => markerButton.onClick.AddListener(() => OnClicked?.Invoke(this));

        public void SetState(bool isSelected) 
            => center.SetActive(isSelected);

        public void SetInteractableState(bool isInteractable) 
            => markerButton.interactable = isInteractable;
    }
}
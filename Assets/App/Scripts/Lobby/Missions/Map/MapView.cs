using System;
using System.Linq;
using App.Missions;
using App.UI.WindowsSwitching;
using UnityEngine;

namespace App.Lobby.Missions.Map
{
    public class MapView : MonoBehaviour, IWindow
    {
        [field: SerializeField] public int InitializePriority { get; private set; }
        [field: SerializeField] public string Id { get; private set; }
        
        [SerializeField] private NetMapViewModel netMapViewModel;
        [SerializeField] private ViewBlocker viewBlocker;
        [SerializeField] private MissionMarker[] missionMarkers;

        private MissionMarker _lastSelectedMarker;

        public void Awake()
        {
            foreach (var missionMarker in missionMarkers)
            {
                missionMarker.SetState(false);
                missionMarker.OnClicked += Select;
            }
        }

        private void OnEnable()
        {
            netMapViewModel.OnActiveMissionChanged += UpdateActiveMarker;
            UpdateActiveMarker(netMapViewModel.GetActiveMission());
            viewBlocker.SetState(!netMapViewModel.HasStateAuthority);
        }

        private void OnDisable()
        {
            netMapViewModel.OnActiveMissionChanged -= UpdateActiveMarker;
        }

        public void Toggle(bool isVisible) 
            => gameObject.SetActive(isVisible);
        
        private void UpdateActiveMarker(MissionConfig missionConfig)
        {
            if (_lastSelectedMarker != null)
            {
                if (_lastSelectedMarker.MissionConfig == missionConfig)
                    return;
                _lastSelectedMarker.SetState(false);
            }

            var marker = missionMarkers.FirstOrDefault(m => m.MissionConfig == missionConfig);
            if (marker == null)
                throw new NullReferenceException($"Cant find marker with this mission config: [{missionConfig}]");

            _lastSelectedMarker = marker;
            _lastSelectedMarker.SetState(true);
        }

        private void Select(MissionMarker marker)
        {
            if (_lastSelectedMarker == marker)
                return;
            
            if (_lastSelectedMarker != null)
                _lastSelectedMarker.SetState(false);
            
            _lastSelectedMarker = marker;
            _lastSelectedMarker.SetState(true);
            
            netMapViewModel.SetMissionIndex(_lastSelectedMarker.MissionConfig);
        }
    }
}
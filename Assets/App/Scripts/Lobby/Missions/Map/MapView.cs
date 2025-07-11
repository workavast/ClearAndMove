using System;
using System.Linq;
using App.Missions;
using App.Missions.MissionsProgress;
using App.UI.WindowsSwitching;
using UnityEngine;
using Zenject;

namespace App.Lobby.Missions.Map
{
    public class MapView : MonoBehaviour, IWindow
    {
        [field: SerializeField] public int InitializePriority { get; private set; }
        [field: SerializeField] public string Id { get; private set; }
        
        [SerializeField] private NetMapViewModel netMapViewModel;
        [SerializeField] private ViewBlocker viewBlocker;
        [SerializeField] private MapMissionMarker[] missionMarkers;

        [Inject] private readonly CompletedMissionsModel _completedMissionsModel;
        
        private MapMissionMarker _lastSelectedMarker;

        public void Awake()
        {
            for (int i = 0; i < missionMarkers.Length; i++)
            {
                missionMarkers[i].SetState(false);
                var missionIsAvailable = _completedMissionsModel.IsAvailable(i);
                missionMarkers[i].SetInteractableState(missionIsAvailable);
                missionMarkers[i].OnClicked += Select;
            }

        }

        private void OnEnable()
        {
            netMapViewModel.OnInitialized += UpdateViewBlocker;
            UpdateViewBlocker();
            
            netMapViewModel.OnActiveMissionChanged += UpdateActiveMarker;
            UpdateActiveMarker(netMapViewModel.GetActiveMission());
        }

        private void UpdateViewBlocker() 
            => viewBlocker.SetState(!netMapViewModel.HasStateAuthority);

        private void OnDisable()
        {
            netMapViewModel.OnInitialized -= UpdateViewBlocker;
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

        private void Select(MapMissionMarker marker)
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
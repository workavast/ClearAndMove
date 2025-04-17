using System;
using System.Linq;
using App.Lobby.Map;
using App.Missions;
using App.UI.WindowsSwitching;
using Avastrad.UI.UiSystem;
using UnityEngine;

namespace App.UI.MissionSelection
{
    public class MissionSelectorPresenter : MonoBehaviour, IWindow, IInitializable
    {
        [field: SerializeField] public int InitializePriority { get; private set; }
        [field: SerializeField] public string Id { get; private set; }
        [SerializeField] private MissionMarker[] missionMarkers;

        private MissionMarker _lastSelectedMarker;
        
        public event Action<MissionConfig> OnMissionClicked;

        public void Initialize()
        {
            foreach (var missionMarker in missionMarkers)
            {
                missionMarker.SetState(false);
                missionMarker.OnClicked += Select;
            }
        }
        
        public void SetActiveMarker(MissionConfig missionConfig)
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
            OnMissionClicked?.Invoke(_lastSelectedMarker.MissionConfig);
        }

        public void Toggle(bool isVisible) 
            => gameObject.SetActive(isVisible);
    }
}
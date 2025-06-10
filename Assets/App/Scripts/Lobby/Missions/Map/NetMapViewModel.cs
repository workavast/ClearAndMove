using System;
using App.Missions;
using Fusion;
using UnityEngine;
using Zenject;

namespace App.Lobby.Missions.Map
{
    public class NetMapViewModel : NetworkBehaviour, IMissionMapViewModel
    {
        [OnChangedRender(nameof(ActiveMissionIndexChanged))] [field: ReadOnly]
        [Networked] private int NetSelectedMissionIndex { get; set; } = 0;
        
        [Inject] private readonly IMissionsMapModel _mapModel;

        public int SelectedMissionIndex => _mapModel.SelectedMissionIndex;

        public event Action OnInitialized;
        public event Action<int> OnActiveMissionIndexChanged;
        public event Action<MissionConfig> OnActiveMissionChanged;

        public override void Spawned()
        {
            if (NetSelectedMissionIndex != 0)
                ActiveMissionIndexChanged();
            OnInitialized?.Invoke();
        }

        public MissionConfig GetActiveMission()
            => _mapModel.GetActiveMission();

        public MissionConfig GetMission(int activeMissionIndex)
            => _mapModel.GetMission(activeMissionIndex);

        public void SetMissionIndex(MissionConfig missionConfig)
        {
            if (!HasStateAuthority)
            {
                Debug.LogError("You try set mission index when you doesnt have state authority");
                return;
            }

            var missionIndex = _mapModel.GetMissionIndex(missionConfig);
            NetSelectedMissionIndex = missionIndex;
        }

        private void ActiveMissionIndexChanged()
        {
            _mapModel.SetMissionIndex(NetSelectedMissionIndex);
            OnActiveMissionIndexChanged?.Invoke(NetSelectedMissionIndex);
            OnActiveMissionChanged?.Invoke(GetActiveMission());
        }
    }
}
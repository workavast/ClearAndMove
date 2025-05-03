using System;
using App.Missions;
using UnityEngine;

namespace App.Lobby.Missions.Map
{
    public class MapModel : IMissionsMapModel
    {
        public int SelectedMissionIndex { get; private set; } = 0;
     
        private readonly MissionsConfig _missionsConfig;
        
        public event Action<int> OnActiveMissionIndexChanged;
        public event Action<MissionConfig> OnActiveMissionChanged;

        public MapModel(MissionsConfig missionsConfig)
        {
            _missionsConfig = missionsConfig;
        }
        
        public MissionConfig GetActiveMission() 
            => GetMission(SelectedMissionIndex);

        public MissionConfig GetMission(int activeMissionIndex)
            => _missionsConfig.Missions[activeMissionIndex];

        public void SetMissionIndex(int missionIndex)
        {
            if (missionIndex == SelectedMissionIndex)
            {
                Debug.LogWarning("You try set mission index that already active");
                return;
            }

            SelectedMissionIndex = missionIndex;
            
            OnActiveMissionIndexChanged?.Invoke(SelectedMissionIndex);
            OnActiveMissionChanged?.Invoke(_missionsConfig.Missions[SelectedMissionIndex]);
        }

        public int GetMissionIndex(MissionConfig missionConfig) 
            => _missionsConfig.GetIndex(missionConfig);
    }
}
using System;
using App.Missions;

namespace App.Lobby.Missions.Map
{
    public interface IMissionsMapModel
    {
        public int SelectedMissionIndex { get; }

        public event Action<int> OnActiveMissionIndexChanged;
        public event Action<MissionConfig> OnActiveMissionChanged;

        public MissionConfig GetMission(int activeMissionIndex);
        public MissionConfig GetActiveMission();
        public int GetMissionIndex(MissionConfig missionConfig);
        public void SetMissionIndex(int missionIndex);
    }
}
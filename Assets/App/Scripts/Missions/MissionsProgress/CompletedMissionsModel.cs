using System;
using System.Collections.Generic;
using Avastrad.Extensions;
using UnityEngine;

namespace App.Missions.MissionsProgress
{
    [Serializable]
    public class CompletedMissionsModel
    {
        public IReadOnlyList<int> CompletedMissions => completedMissions;
        
        [SerializeField] private List<int> completedMissions = new();
        
        public void SetMissionState(int missionIndex, bool isCompleted)
        {
            if (isCompleted) 
                completedMissions.TryAdd(missionIndex);
            else
                completedMissions.TryRemove(missionIndex);
            
            Save();
        }

        public bool IsAvailable(int index)
        {
            if (index <= 0)
                return true;

            if (completedMissions.Contains(index))
                return true;
            
            if (completedMissions.Contains(index - 1))
                return true;
            
            return false;
        }

        public void Load(CompletedMissionsModel save)
        {
            completedMissions.Clear();
            completedMissions.AddRange(save.completedMissions);
        }
        
        public void Save()
            => CompletedMissionsSaver.Save(this);
    }
}
using System;
using Avastrad.SavingAndLoading;

namespace App.Missions.MissionsProgress
{
    public static class CompletedMissionsSaver
    {
        private static readonly JsonSaveAndLoader JsonSaveAndLoader = new JsonSaveAndLoader("/CompletedMissions.json");
        
        public static bool Exist() 
            => JsonSaveAndLoader.Exist();
        
        public static CompletedMissionsModel Load()
        {
            if (Exist())
                return JsonSaveAndLoader.Load<CompletedMissionsModel>();

            throw new NullReferenceException("Doesnt have save");
        }
        
        public static void Save(CompletedMissionsModel completedMissionsModel)
        {
            JsonSaveAndLoader.Save(completedMissionsModel);
        }
        
        public static void Delete()
        {
            JsonSaveAndLoader.DeleteSave();
        }
    }
}
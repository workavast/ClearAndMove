using App.Entities.Player;
using App.Missions.MissionState;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;

namespace App.ExtractionZone.FSM
{
    public abstract class ExtractionZoneState : State<ExtractionZoneState>
    {
        protected readonly NetExtractionZone NetExtractionZone;
        protected readonly ExtractionZoneConfig Config;
        protected readonly NetMissionState NetMissionState;
        protected readonly PlayersEntitiesRepository PlayersEntitiesRepository;
        protected readonly ExtractionZoneView ExtractionZoneView;
        
        protected NetworkRunner Runner => NetExtractionZone.Runner;
        protected Transform transform => NetExtractionZone.transform;
        protected float ExtractionRadius => Config.ExtractionRadius;
        protected float ExtractionTime => Config.ExtractionTime;
        
        protected ExtractionZoneState(NetExtractionZone netExtractionZone, ExtractionZoneConfig config, 
            NetMissionState netMissionState, PlayersEntitiesRepository playersEntitiesRepository, ExtractionZoneView extractionZoneView)
        {
            NetExtractionZone = netExtractionZone;
            NetMissionState = netMissionState;
            PlayersEntitiesRepository = playersEntitiesRepository;
            ExtractionZoneView = extractionZoneView;
            Config = config;
        }
        
        protected bool AllPlayersInZone()
        {
            if (PlayersEntitiesRepository.PlayerEntities.Count <= 0)
                return false;

            foreach (var playerEntity in PlayersEntitiesRepository.PlayerEntities)
            {
                var distance = Vector3.Distance(playerEntity.transform.position, transform.position);
                if (distance > ExtractionRadius) 
                    return false;
            }

            return true;
        }
    }
}
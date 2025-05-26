using App.Entities.Player;
using App.Missions.MissionState;
using Avastrad.Extensions;
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

        private readonly Collider _zoneCollider;
        
        protected NetworkRunner Runner => NetExtractionZone.Runner;
        protected Transform transform => NetExtractionZone.transform;
        protected float ExtractionTime => Config.ExtractionTime;
        
        protected ExtractionZoneState(NetExtractionZone netExtractionZone, ExtractionZoneConfig config,
            NetMissionState netMissionState, PlayersEntitiesRepository playersEntitiesRepository,
            ExtractionZoneView extractionZoneView, Collider zoneCollider)
        {
            NetExtractionZone = netExtractionZone;
            NetMissionState = netMissionState;
            PlayersEntitiesRepository = playersEntitiesRepository;
            ExtractionZoneView = extractionZoneView;
            _zoneCollider = zoneCollider;
            Config = config;
        }
        
        protected bool AllPlayersInZone()
        {
            if (PlayersEntitiesRepository.PlayerEntities.Count <= 0)
                return false;

            foreach (var playerEntity in PlayersEntitiesRepository.PlayerEntities)
            {
                if (playerEntity.IsAlive)
                    if (!_zoneCollider.Contains(playerEntity.Position))
                        return false;
            }

            return true;
        }
    }
}
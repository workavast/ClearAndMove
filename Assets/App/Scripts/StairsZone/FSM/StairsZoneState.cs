using App.Entities.Player;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;

namespace App.StairsZone.FSM
{
    public abstract class StairsZoneState : State<StairsZoneState>
    {
        protected readonly NetStairsZone NetStairsZone;
        protected readonly StairsZoneConfig Config;
        protected readonly PlayersEntitiesRepository PlayersEntitiesRepository;
        protected readonly StairsZoneView StairsZoneView;

        protected Transform MovePoint => NetStairsZone.MovePoint;
        protected NetworkRunner Runner => NetStairsZone.Runner;
        protected Transform transform => NetStairsZone.transform;
        protected float ExtractionRadius => Config.ExtractionRadius;
        protected float ExtractionTime => Config.ExtractionTime;
        
        protected StairsZoneState(NetStairsZone netStairsZone, StairsZoneConfig config, 
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView)
        {
            NetStairsZone = netStairsZone;
            PlayersEntitiesRepository = playersEntitiesRepository;
            StairsZoneView = stairsZoneView;
            Config = config;
        }
        
        protected bool AllAlivePlayersInZone()
        {
            if (PlayersEntitiesRepository.PlayerEntities.Count <= 0)
                return false;

            foreach (var playerEntity in PlayersEntitiesRepository.PlayerEntities)
            {
                var distance = Vector3.Distance(playerEntity.transform.position, transform.position);
                if (playerEntity.IsAlive && distance > ExtractionRadius) 
                    return false;
            }

            return true;
        }
    }
}
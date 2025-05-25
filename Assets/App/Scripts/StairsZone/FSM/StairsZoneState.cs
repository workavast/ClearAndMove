using App.Entities.Player;
using Avastrad.Extensions;
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
        private readonly Collider _zoneCollider;

        protected Transform MovePoint => NetStairsZone.MovePoint;
        protected NetworkRunner Runner => NetStairsZone.Runner;
        protected Transform transform => NetStairsZone.transform;
        protected float ExtractionTime => Config.ExtractionTime;
        
        protected StairsZoneState(NetStairsZone netStairsZone, StairsZoneConfig config, 
            PlayersEntitiesRepository playersEntitiesRepository, StairsZoneView stairsZoneView, Collider zoneCollider)
        {
            NetStairsZone = netStairsZone;
            PlayersEntitiesRepository = playersEntitiesRepository;
            StairsZoneView = stairsZoneView;
            _zoneCollider = zoneCollider;
            Config = config;
        }

        protected bool HasKnockoutPlayer() 
            => PlayersEntitiesRepository.HasKnockoutPlayer();

        protected bool AllAlivePlayersInZone()
        {
            if (PlayersEntitiesRepository.PlayerEntities.Count <= 0)
                return false;

            foreach (var playerEntity in PlayersEntitiesRepository.PlayerEntities)
            {
                if (playerEntity.IsAlive)
                    if (!_zoneCollider.Contains(playerEntity.transform.position)) 
                        return false;   
            }

            return true;
        }
    }
}
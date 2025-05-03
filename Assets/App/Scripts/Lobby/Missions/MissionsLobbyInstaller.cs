using App.Lobby.Missions.Map;
using App.Missions;
using UnityEngine;
using Zenject;

namespace App.Lobby.Missions
{
    public class MissionsLobbyInstaller : MonoInstaller
    {
        [SerializeField] private MissionsConfig missionsConfig;
        
        public override void InstallBindings()
        {
            var mapModel = new MapModel(missionsConfig);
            Container.BindInterfacesTo<MapModel>().FromInstance(mapModel).AsSingle();
        }
    }
}
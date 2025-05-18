using UnityEngine;
using Zenject;

namespace App.Missions.MissionState
{
    public class MissionStateInstaller : MonoInstaller
    {
        [SerializeField] private NetMissionState missionState;

        public override void InstallBindings()
        {
            Container.Bind<NetMissionState>().FromInstance(missionState).AsSingle();
        }
    }
}
using Zenject;

namespace App.Missions.SessionData
{
    public class MissionSessionDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MissionSessionDataRepository>().FromNew().AsSingle().NonLazy();
        }
    }
}
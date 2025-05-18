using Zenject;

namespace App.Missions.MissionsProgress
{
    public class CompletedMissionsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CompletedMissionsModel>().FromNew().AsSingle();
        }
    }
}
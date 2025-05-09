using App.Entities.Enemy;
using App.Entities.Player;
using App.Entities.Player.SelectionPlayerEntity;
using Zenject;

namespace App.Entities
{
    public class EntitiesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepositories();
            BindPlayerEntitySelection();
            
            Container.Bind<LocalPlayerProvider>().FromNew().AsSingle();
        }

        private void BindRepositories()
        {
            Container.BindInterfacesAndSelfTo<PlayersEntitiesRepository>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesRepository>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<EntitiesRepository>().FromNew().AsSingle();
        }
        
        private void BindPlayerEntitySelection()
        {
            Container.BindInterfacesAndSelfTo<SelectedPlayerEntityChanger>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<SelectedPlayerEntityInitializer>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SelectedPlayerEntityProvider>().FromNew().AsSingle();
        }
    }
}
using Zenject;

namespace App.Entities.Player.SelectionPlayerEntity
{
    public class PlayersObserveInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayersObserver>().FromNew().AsSingle().NonLazy();
        }
    }
}
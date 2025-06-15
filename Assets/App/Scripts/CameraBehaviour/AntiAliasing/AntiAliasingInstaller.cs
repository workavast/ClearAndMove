using Zenject;

namespace App.CameraBehaviour.AntiAliasing
{
    public class AntiAliasingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AntiAliasingProvider>().FromNew().AsSingle();
        }
    }
}
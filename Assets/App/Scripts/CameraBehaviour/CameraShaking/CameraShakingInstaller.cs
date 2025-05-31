using Zenject;

namespace App.CameraBehaviour.CameraShaking
{
    public class CameraShakingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CameraShakeProvider>().FromNew().AsSingle();
        }
    }
}
using Zenject;

namespace App.Particles
{
    public class ParticlesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ParticleFactory>().FromNew().AsSingle();
        }
    }
}
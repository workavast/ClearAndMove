using App.App;
using Zenject;

namespace App.Damage
{
    public class DamageApplicatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var playersConfig = AppInfrastructure.MissionInfrastructure.PlayerDamageConfig;
            var enemiesConfig = AppInfrastructure.MissionInfrastructure.EnemyDamageConfig;
            
            Container.Bind<DamageApplicatorFactory>().FromNew().AsSingle().WithArguments(playersConfig, enemiesConfig);
        }
    }
}
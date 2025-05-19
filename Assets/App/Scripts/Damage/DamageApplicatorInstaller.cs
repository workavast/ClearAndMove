using App.App;
using Zenject;

namespace App.Damage
{
    public class DamageApplicatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var playerMissionModifiers = AppInfrastructure.PlayerMissionModifiers;
            var enemyMissionModifiers = AppInfrastructure.EnemyMissionModifiers;
            
            Container.Bind<DamageApplicatorFactory>().FromNew().AsSingle().WithArguments(playerMissionModifiers, enemyMissionModifiers);
        }
    }
}
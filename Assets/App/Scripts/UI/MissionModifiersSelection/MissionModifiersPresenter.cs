using App.App;
using App.UI.WindowsSwitching;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.MissionModifiersSelection
{
    public class MissionModifiersPresenter : MonoBehaviour, IWindow
    {
        [field: SerializeField] public string Id { get; private set; }
        [SerializeField] private Toggle playerFriendlyFireToggle;
        [SerializeField] private Toggle enemyFriendlyFireToggle;

        private void Start()
        {
            playerFriendlyFireToggle.SetIsOnWithoutNotify(AppInfrastructure.MissionInfrastructure.PlayerDamageConfig.HasFriendlyFire);
            enemyFriendlyFireToggle.SetIsOnWithoutNotify(AppInfrastructure.MissionInfrastructure.EnemyDamageConfig.HasFriendlyFire);
            
            playerFriendlyFireToggle.onValueChanged.AddListener(TogglePlayerFriendlyFire);
            enemyFriendlyFireToggle.onValueChanged.AddListener(ToggleEnemyFriendlyFire);
        }

        public void Toggle(bool isVisible) 
            => gameObject.SetActive(isVisible);

        public void TogglePlayerFriendlyFire(bool hasFriendlyFire) 
            => AppInfrastructure.MissionInfrastructure.SetPlayerFriendlyFire(hasFriendlyFire);

        public void ToggleEnemyFriendlyFire(bool hasFriendlyFire) 
            => AppInfrastructure.MissionInfrastructure.SetEnemyFriendlyFire(hasFriendlyFire);
    }
}
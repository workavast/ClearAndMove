using App.App;
using App.UI.SliderExt;
using App.UI.WindowsSwitching;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.MissionModifiersSelection
{
    public class MissionModifiersPresenter : MonoBehaviour, IWindow
    {
        [field: SerializeField] public string Id { get; private set; }
        [SerializeField] private Toggle playerFriendlyFireToggle;
        [SerializeField] private SliderWithStep playerDamageScaleSlider;
        [SerializeField] private Toggle enemyFriendlyFireToggle;
        [SerializeField] private SliderWithStep enemyDamageScaleSlider;

        private void Start()
        {
            playerFriendlyFireToggle.SetIsOnWithoutNotify(AppInfrastructure.MissionInfrastructure.PlayerDamageConfig.HasFriendlyFire);
            enemyFriendlyFireToggle.SetIsOnWithoutNotify(AppInfrastructure.MissionInfrastructure.EnemyDamageConfig.HasFriendlyFire);
            playerFriendlyFireToggle.onValueChanged.AddListener(TogglePlayerFriendlyFire);
            enemyFriendlyFireToggle.onValueChanged.AddListener(ToggleEnemyFriendlyFire);
         
            playerDamageScaleSlider.SetValue(AppInfrastructure.MissionInfrastructure.PlayerDamageConfig.DamageScale);
            enemyDamageScaleSlider.SetValue(AppInfrastructure.MissionInfrastructure.EnemyDamageConfig.DamageScale);
            playerDamageScaleSlider.OnValueCHanged += SetPlayerDamageScale;
            enemyDamageScaleSlider.OnValueCHanged += SetEnemyDamageScale;
        }

        public void Toggle(bool isVisible) 
            => gameObject.SetActive(isVisible);

        public void TogglePlayerFriendlyFire(bool hasFriendlyFire) 
            => AppInfrastructure.MissionInfrastructure.SetPlayerFriendlyFire(hasFriendlyFire);

        public void SetPlayerDamageScale(float damageScale) 
            => AppInfrastructure.MissionInfrastructure.SetPlayerDamageScale(damageScale);
        
        public void ToggleEnemyFriendlyFire(bool hasFriendlyFire) 
            => AppInfrastructure.MissionInfrastructure.SetEnemyFriendlyFire(hasFriendlyFire);
        
        public void SetEnemyDamageScale(float damageScale) 
            => AppInfrastructure.MissionInfrastructure.SetEnemyDamageScale(damageScale);
    }
}
using App.Lobby.Missions.Map;
using App.UI.SliderExt;
using App.UI.WindowsSwitching;
using UnityEngine;
using UnityEngine.UI;

namespace App.Lobby.Missions.MissionModifiers
{
    public class MissionModifiersView : MonoBehaviour, IWindow
    {
        [field: SerializeField] public string Id { get; private set; }
        [Space]
        [SerializeField] private ViewBlocker viewBlocker;
        [SerializeField] private NetMissionModifiersViewModel viewModel;
        [Space]
        [SerializeField] private Toggle playerFriendlyFireToggle;
        [SerializeField] private SliderWithStep playerDamageScaleSlider;
        [SerializeField] private Toggle playerAutoReloadingToggle;
        [SerializeField] private Toggle playerDropMagazineFireToggle;
        [Space]
        [SerializeField] private Toggle enemyFriendlyFireToggle;
        [SerializeField] private SliderWithStep enemyDamageScaleSlider;
        
        private void OnEnable()
        {
            viewModel.OnInitialized += UpdateViewBlocker;
            UpdateViewBlocker();
            
            viewModel.OnDataChanged += UpdateView;
            UpdateView();
        }
        
        private void Start()
        {
            playerFriendlyFireToggle.onValueChanged.AddListener(TogglePlayerFriendlyFire);
            playerDamageScaleSlider.OnValueChanged += SetPlayerDamageScale;
            playerAutoReloadingToggle.onValueChanged.AddListener(SetPlayerAutoReloading);
            playerDropMagazineFireToggle.onValueChanged.AddListener(SetPlayerDropMagazine);
         
            enemyFriendlyFireToggle.onValueChanged.AddListener(ToggleEnemyFriendlyFire);
            enemyDamageScaleSlider.OnValueChanged += SetEnemyDamageScale;
        }

        private void OnDisable()
        {
            viewModel.OnInitialized -= UpdateViewBlocker;
            viewModel.OnDataChanged -= UpdateView;
        }

        private void UpdateViewBlocker() 
            => viewBlocker.SetState(!viewModel.HasStateAuthority);

        private void UpdateView()
        {
            playerFriendlyFireToggle.SetIsOnWithoutNotify(viewModel.PlayersFriendlyFire);
            playerDamageScaleSlider.SetValue(viewModel.PlayersDamageScale, false);
            playerAutoReloadingToggle.SetIsOnWithoutNotify(viewModel.PlayersAutoReloading);
            playerDropMagazineFireToggle.SetIsOnWithoutNotify(viewModel.PlayersDropMagazine);
            
            enemyFriendlyFireToggle.SetIsOnWithoutNotify(viewModel.EnemiesFriendlyFire);
            enemyDamageScaleSlider.SetValue(viewModel.EnemiesDamageScale, false);
        }

        public void Toggle(bool isVisible) 
            => gameObject.SetActive(isVisible);

        private void TogglePlayerFriendlyFire(bool hasFriendlyFire) 
            => viewModel.SetPlayerFriendlyFire(hasFriendlyFire);

        private void SetPlayerDamageScale(float damageScale) 
            => viewModel.SetPlayerDamageScale(damageScale);

        private void SetPlayerAutoReloading(bool autoReloading) 
            => viewModel.SetPlayerAutoReloading(autoReloading);

        private void SetPlayerDropMagazine(bool dropMagazine) 
            => viewModel.SetPlayerDropMagazine(dropMagazine);

        private void ToggleEnemyFriendlyFire(bool hasFriendlyFire) 
            => viewModel.SetEnemyFriendlyFire(hasFriendlyFire);

        private void SetEnemyDamageScale(float damageScale) 
            => viewModel.SetEnemyDamageScale(damageScale);
    }
}
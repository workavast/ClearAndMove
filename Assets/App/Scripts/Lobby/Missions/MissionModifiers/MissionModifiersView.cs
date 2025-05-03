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
        [SerializeField] private Toggle enemyFriendlyFireToggle;
        [SerializeField] private SliderWithStep enemyDamageScaleSlider;
        
        private void OnEnable()
        {
            viewBlocker.SetState(!viewModel.HasStateAuthority);
            viewModel.OnDataChanged += UpdateView;
            UpdateView();
        }
        
        private void Start()
        {
            playerFriendlyFireToggle.onValueChanged.AddListener(TogglePlayerFriendlyFire);
            enemyFriendlyFireToggle.onValueChanged.AddListener(ToggleEnemyFriendlyFire);
         
            playerDamageScaleSlider.OnValueChanged += SetPlayerDamageScale;
            enemyDamageScaleSlider.OnValueChanged += SetEnemyDamageScale;
        }

        private void OnDisable()
        {
            viewModel.OnDataChanged -= UpdateView;
        }

        private void UpdateView()
        {
            playerFriendlyFireToggle.SetIsOnWithoutNotify(viewModel.PlayersFriendlyFire);
            enemyFriendlyFireToggle.SetIsOnWithoutNotify(viewModel.EnemiesFriendlyFire);
            
            playerDamageScaleSlider.SetValue(viewModel.PlayersDamageScale, false);
            enemyDamageScaleSlider.SetValue(viewModel.EnemiesDamageScale, false);
        }

        public void Toggle(bool isVisible) 
            => gameObject.SetActive(isVisible);

        public void TogglePlayerFriendlyFire(bool hasFriendlyFire) 
            => viewModel.SetPlayerFriendlyFire(hasFriendlyFire);

        public void SetPlayerDamageScale(float damageScale) 
            => viewModel.SetPlayerDamageScale(damageScale);
        
        public void ToggleEnemyFriendlyFire(bool hasFriendlyFire) 
            => viewModel.SetEnemyFriendlyFire(hasFriendlyFire);
        
        public void SetEnemyDamageScale(float damageScale) 
            => viewModel.SetEnemyDamageScale(damageScale);
    }
}
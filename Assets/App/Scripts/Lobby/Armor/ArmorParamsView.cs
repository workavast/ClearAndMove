using App.Armor;
using App.Entities;
using App.Players;
using UnityEngine;
using UnityEngine.UI;

namespace App.Lobby.Armor
{
    public class ArmorParamsView : MonoBehaviour
    {
        [SerializeField] private EntityConfig entityConfig;
        [SerializeField] private ArmorsConfig armorsConfig;

        [SerializeField] private Slider walkSpeedView;
        [SerializeField] private Slider sprintSpeedView;
        [SerializeField] private Slider damageScaleView;
        
        private void Awake()
        {
            PlayerData.OnArmorLevelChanged += UpdateView;

            walkSpeedView.minValue = 0;
            walkSpeedView.maxValue = entityConfig.WalkSpeed;
            sprintSpeedView.minValue = 0;
            sprintSpeedView.maxValue = entityConfig.SprintSpeed;
            damageScaleView.minValue = 0;
            damageScaleView.maxValue = armorsConfig.GetArmor(0).DamageScale;
            UpdateView();
        }

        private void OnDestroy()
        {
            PlayerData.OnArmorLevelChanged -= UpdateView;
        }

        private void UpdateView()
        {
            var walkSpeed = entityConfig.WalkSpeed - armorsConfig.GetArmor(PlayerData.EquippedArmorLevel).WalkSpeedDecrease;
            var sprintSpeed = entityConfig.SprintSpeed - armorsConfig.GetArmor(PlayerData.EquippedArmorLevel).SprintSpeedDecrease;
            var damageScale = armorsConfig.GetArmor(PlayerData.EquippedArmorLevel).DamageScale;
            
            walkSpeedView.value = walkSpeed;
            sprintSpeedView.value = sprintSpeed;
            damageScaleView.value = damageScale;
        }
    }
}
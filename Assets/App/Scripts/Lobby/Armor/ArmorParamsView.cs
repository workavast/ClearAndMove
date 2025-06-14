using App.Armor;
using App.Entities;
using App.Players;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Lobby.Armor
{
    public class ArmorParamsView : MonoBehaviour
    {
        [SerializeField] private EntityConfig entityConfig;
        [Space]
        [SerializeField] private Slider walkSpeedView;
        [SerializeField] private Slider sprintSpeedView;
        [SerializeField] private Slider damageScaleView;
        
        [Inject] private readonly ArmorConfigsRep _armorConfigsRep;
        
        private void Awake()
        {
            PlayerData.OnArmorLevelChanged += UpdateView;

            walkSpeedView.minValue = 0;
            walkSpeedView.maxValue = entityConfig.WalkSpeed;
            sprintSpeedView.minValue = 0;
            sprintSpeedView.maxValue = entityConfig.SprintSpeed;
            damageScaleView.minValue = 0;
            damageScaleView.maxValue = _armorConfigsRep.GetArmor(0).DamageScale;
            UpdateView();
        }

        private void OnDestroy()
        {
            PlayerData.OnArmorLevelChanged -= UpdateView;
        }

        private void UpdateView()
        {
            var walkSpeed = entityConfig.WalkSpeed - _armorConfigsRep.GetArmor(PlayerData.EquippedArmorLevel).WalkSpeedDecrease;
            var sprintSpeed = entityConfig.SprintSpeed - _armorConfigsRep.GetArmor(PlayerData.EquippedArmorLevel).SprintSpeedDecrease;
            var damageScale = _armorConfigsRep.GetArmor(PlayerData.EquippedArmorLevel).DamageScale;
            
            walkSpeedView.value = walkSpeed;
            sprintSpeedView.value = sprintSpeed;
            damageScaleView.value = damageScale;
        }
    }
}
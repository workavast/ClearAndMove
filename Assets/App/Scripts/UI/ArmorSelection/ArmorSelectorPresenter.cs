using System.Collections.Generic;
using App.Armor;
using App.Players;
using App.UI.Selection;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Zenject;

namespace App.UI.ArmorSelection
{
    public class ArmorSelectorPresenter : Selector<int>
    {
        [SerializeField] private LocalizedString localizedString;
        
        [Inject] private readonly ArmorsConfig _armorsConfig;

        private void OnEnable() 
            => LocalizationSettings.SelectedLocaleChanged += UpdateBtns;

        private void OnDisable() 
            => LocalizationSettings.SelectedLocaleChanged -= UpdateBtns;

        protected override IReadOnlyList<int> GetIds()
        {
            var list = new List<int>(_armorsConfig.MaxArmorLevel);
            for (int i = 0; i < _armorsConfig.MaxArmorLevel; i++)
                list.Add(i);

            return list;
        }

        protected override string GetName(int id) 
            => localizedString.GetLocalizedString(id);

        protected override void Select(int id) 
            => PlayerData.EquipArmor(id);
        
        private void UpdateBtns(Locale _) 
            => UpdateBtns();
    }
}
using System.Collections.Generic;
using App.Armor;
using App.Players;
using App.UI.Selection;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Zenject;

namespace App.Lobby.Armor
{
    public class ArmorSelector : Selector<int, ArmorSelectBtn>
    {
        [SerializeField] private LocalizedString localizedString;
        
        [Inject] private readonly ArmorConfigsRep _configs;

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += UpdateBtns;
            UpdateBtns();
        }

        private void OnDisable() 
            => LocalizationSettings.SelectedLocaleChanged -= UpdateBtns;

        protected override IReadOnlyList<int> GetIds()
        {
            var list = new List<int>(_configs.MaxArmorLevel);
            for (int i = 0; i < _configs.MaxArmorLevel; i++)
                list.Add(i);

            return list;
        }

        protected override string GetName(int id) 
            => localizedString.GetLocalizedString(id);

        protected override bool Is(int a, int b) => a == b;

        protected override int GetCurrentActiveId()
            => PlayerData.EquippedArmorLevel;

        protected override void OnSelect(int id) 
            => PlayerData.EquipArmor(id);
        
        private void UpdateBtns(Locale _) 
            => UpdateBtns();
        
        protected override void UpdateBtn(int id, ArmorSelectBtn button)
        {
            button.SetData(id, GetName(id));
            button.SetIcon(_configs.GetArmor(id).Icon);
        }
    }
}
using System.Collections.Generic;
using Avastrad.DictionaryInspector;
using UnityEngine;

namespace App.Weapons.View
{
    [CreateAssetMenu(fileName = nameof(WeaponViewsConfig), menuName = Consts.WeaponConfigsPath + nameof(WeaponViewsConfig))]
    public class WeaponViewsConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<WeaponId, WeaponView> views;

        public IReadOnlyDictionary<WeaponId, WeaponView> Views => views;
    }
}
using System.Collections.Generic;
using App.Tools.ConfigsRepositories;
using UnityEngine;

namespace App.Weapons
{
    [CreateAssetMenu(fileName = nameof(WeaponConfigsRep), menuName = Consts.WeaponConfigsPath + nameof(WeaponConfigsRep))]
    public class WeaponConfigsRep : ConfigsRepository<WeaponConfig>
    {
        private readonly List<WeaponId> _weaponIds = new();
        private readonly Dictionary<WeaponId, WeaponConfig> _weaponConfigs = new();
        
        public IReadOnlyList<WeaponId> WeaponIds => _weaponIds;
        public IReadOnlyDictionary<WeaponId, WeaponConfig> WeaponConfigs => _weaponConfigs;

        private bool _isInitialized;

        public void Initialize(bool forceInitialisation)
        {
            if (_isInitialized && !forceInitialisation)
            {
                Debug.LogError("Is already initialized");
                return;
            }
            
            _weaponIds.Clear();
            _weaponConfigs.Clear();
            _weaponConfigs.EnsureCapacity(Configs.Count);
            foreach (var weaponConfig in Configs)
            {
                if (_weaponConfigs.ContainsKey(weaponConfig.Id))
                    Debug.LogError($"Duplicate: {weaponConfig.Id} | {weaponConfig}");
                else
                {
                    _weaponIds.Add(weaponConfig.Id);
                    _weaponConfigs.Add(weaponConfig.Id, weaponConfig);
                }
            }

            _isInitialized = true;
            Debug.Log("Initialized");
        }

        public Sprite GetIcon(WeaponId weaponId) 
            => _weaponConfigs[weaponId].Icon;
    }
}
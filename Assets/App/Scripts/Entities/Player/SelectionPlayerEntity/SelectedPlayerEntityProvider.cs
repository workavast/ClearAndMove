using System;
using Fusion;
using UnityEngine;

namespace App.Entities.Player.SelectionPlayerEntity
{
    public class SelectedPlayerEntityProvider
    {
        public bool HasEntity => _netTargetEntity != null;
        public Vector3 Position => _netTargetEntity.transform.position;
        
        public float MaxHealthPoints => _netTargetEntity.MaxHealthPoints;
        public float CurrentHealthPoints => _netTargetEntity.NetHealthPoints;

        public int MaxAmmo => _netTargetEntity.MaxAmmo;
        public int CurrentAmmo => _netTargetEntity.CurrentAmmo;

        public PlayerRef ActiveTarget => _netTargetEntity.PlayerRef;
        
        private NetPlayerEntity _netTargetEntity;

        public event Action OnWeaponShot; 
        
        public void SetTargetEntity(NetPlayerEntity netPlayerEntity)
        {
            ClearTargetEntity();
            _netTargetEntity = netPlayerEntity;
            
            if (!_netTargetEntity.IsNull()) 
                _netTargetEntity.OnWeaponShot += WeaponShot;
        }
        
        private void ClearTargetEntity()
        {
            if (_netTargetEntity == null)
                return;

            _netTargetEntity.OnWeaponShot -= WeaponShot;
            _netTargetEntity = null;
        }

        private void WeaponShot() 
            => OnWeaponShot?.Invoke();
    }
}
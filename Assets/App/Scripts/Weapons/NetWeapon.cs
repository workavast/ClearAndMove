using System.Collections.Generic;
using App.App;
using App.Entities;
using App.Weapons.FSM;
using App.Weapons.Shooting;
using App.Weapons.View;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;
using Zenject;

namespace App.Weapons
{
    [RequireComponent(typeof(StateMachineController))]
    public class NetWeapon : NetworkBehaviour, IStateMachineOwner
    {
        [SerializeField] private InterfaceReference<IEntity> owner;
        [SerializeField] private NetWeaponModel netWeaponModel;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private WeaponViewHolder weaponViewHolder;
        
        [Inject] private readonly WeaponConfigsRep _weaponConfigsRep;
        [Inject] private readonly ShooterFactory _shooterFactory;
        
        public bool CanShot => netWeaponModel.NetMagazine > 0;
        public bool CanReload => _reloadingState.CanReload;
        public bool RequiredReload => netWeaponModel.NetMagazine <= 0 && CanReload;
        public WeaponId NetEquippedWeapon => netWeaponModel.EquippedWeapon;

        public int MaxMagazineAmmo => WeaponConfig.MagazineSize;
        public int CurrentMagazineAmmo => netWeaponModel.NetMagazine;
        public int FullAmmoSize => netWeaponModel.NetFullAmmoSize;
        public float SpreadAngle => WeaponConfig.MinSpreadAngle + (WeaponConfig.MaxSpreadAngle - WeaponConfig.MinSpreadAngle) * netWeaponModel.CurrentSpreadRatio;
        
        private WeaponConfig WeaponConfig => netWeaponModel.WeaponConfig;
        private Shooter Shooter => netWeaponModel.Shooter;
        
        private int _visibleFireCount;
        private WeaponStateMachine _fsm;
        private ReloadingState _reloadingState;
        private ShotReadyState _shotReadyState;

        void IStateMachineOwner.CollectStateMachines(List<IStateMachine> stateMachines)
        {
            if (owner.Value.EntityType == EntityType.Player)
                _reloadingState = new ReloadingState(netWeaponModel, weaponViewHolder, AppInfrastructure.PlayerMissionModifiers);
            else
                _reloadingState = new ReloadingState(netWeaponModel, weaponViewHolder, AppInfrastructure.EnemyMissionModifiers);

            _shotReadyState = new ShotReadyState(netWeaponModel, weaponViewHolder);
            
            _fsm = new WeaponStateMachine("Weapon", _shotReadyState, _reloadingState);

            stateMachines.Add(_fsm);            
        }
        
        public override void Spawned()
        {
            netWeaponModel.Shooter = _shooterFactory.CreateShoot(GetComponent<IEntity>());

            ApplyWeapon(NetEquippedWeapon);
            
            netWeaponModel.OnEquippedWeaponChanged += ApplyWeapon;

            _visibleFireCount = netWeaponModel.NetFireCount;
        }

        public override void FixedUpdateNetwork()
        {
            if (_fsm.ActiveStateId == _reloadingState.StateId) 
                _fsm.TryActivateState(_shotReadyState);
        }

        public override void Render()
        {
            if (_visibleFireCount < netWeaponModel.NetFireCount)
            {
                weaponViewHolder.ShotVfx();
                weaponViewHolder.ShotSfx();
                for (int i = _visibleFireCount; i < netWeaponModel.NetFireCount; i++)
                {
                    var data = netWeaponModel.NetProjectileData[i % netWeaponModel.NetProjectileData.Length];
                    Shooter.ShootView(ref data);
                }

                _visibleFireCount = netWeaponModel.NetFireCount;
            }
        }

        public void SetWeapon(WeaponId weaponId) 
            => netWeaponModel.SetWeapon(weaponId);

        public bool TryShoot() 
            => _fsm.ActiveState.TryShot();

        public void TryReload() 
            => _fsm.TryActivateState(_reloadingState);

        public void OnDrawGizmos() 
            => Shooter?.OnDrawGizmos();
        
        private void ApplyWeapon(WeaponId weaponId)
        {
            netWeaponModel.WeaponConfig = _weaponConfigsRep.WeaponConfigs[weaponId];
            Shooter.SetData(shootPoint, netWeaponModel.WeaponConfig);
            
            netWeaponModel.NetFireRatePause = TickTimer.CreateFromSeconds(Runner, 0);
            netWeaponModel.NetReloadTimer = TickTimer.CreateFromSeconds(Runner, 0);
            netWeaponModel.NetMagazine = WeaponConfig.MagazineSize;
            netWeaponModel.NetFullAmmoSize = WeaponConfig.NetFullAmmoSize - WeaponConfig.MagazineSize;
            
            weaponViewHolder.SetWeaponView(WeaponConfig.ViewPrefab);
        }
    }
}
using System;
using App.Players;
using App.Weapons;
using Fusion;
using Zenject;

namespace App.Lobby.SessionData
{
    public class NetLobbySessionData : NetworkBehaviour
    {
        [Networked, OnChangedRender(nameof(ReadyStateChanged))] [field: ReadOnly] public bool IsReady { get; private set; }
        [Networked, OnChangedRender(nameof(SelectedArmorChanged))] [field: ReadOnly] public int SelectedArmor { get; private set; }
        [Networked, OnChangedRender(nameof(SelectedWeaponChanged))] [field: ReadOnly] public WeaponId SelectedWeapon { get; private set; } = WeaponId.Scar;

        [Inject] private readonly LobbySessionDataRepository _playersSessionDataRepository;
        
        public PlayerRef PlayerRef => Object.InputAuthority;
        
        public event Action OnDespawned;
        public event Action OnReadyStateChanged;
        public event Action OnSelectedWeaponChanged;
        public event Action OnSelectedArmorChanged;

        public override void Spawned()
        {
            if (!_playersSessionDataRepository.TryRegister(PlayerRef, this))
                Runner.Despawn(GetComponent<NetworkObject>());

            if (HasInputAuthority)
            {
                SetWeapon();
                SetArmor();

                PlayerData.OnWeaponChanged += SetWeapon;
                PlayerData.OnArmorLevelChanged += SetArmor;
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            PlayerData.OnWeaponChanged -= SetWeapon;
            PlayerData.OnArmorLevelChanged -= SetArmor;
            
            _playersSessionDataRepository.TryRemove(PlayerRef);
            OnDespawned?.Invoke();
        }
        
        private void SetWeapon() 
            => RPC_SetWeapon(PlayerData.SelectedWeapon);
        private void SetArmor() 
            => RPC_SetArmor(PlayerData.EquippedArmorLevel);
        
        private void ReadyStateChanged() 
            => OnReadyStateChanged?.Invoke();
        
        private void SelectedWeaponChanged() 
            => OnSelectedWeaponChanged?.Invoke();

        private void SelectedArmorChanged() 
            => OnSelectedArmorChanged?.Invoke();

        public void ChangeReadyState(bool isReady) 
            => RPC_ChangeReadyState(isReady);

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_ChangeReadyState(bool isReady) 
            => IsReady = isReady;

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_SetWeapon(WeaponId selectedWeapon) 
            => SelectedWeapon = selectedWeapon;

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_SetArmor(int selectedArmor) 
            => SelectedArmor = selectedArmor;
    }
}
using App.App;
using App.PlayerInput;
using Fusion;
using UnityEngine;

namespace App.Entities.Player
{
    public class NetPlayerController : NetworkBehaviour
    {
        [SerializeField] private NetEntity netEntity;

        private IReadOnlyMissionModifiers _missionModifiers;
        
        private void Awake()
        {
            _missionModifiers = AppInfrastructure.PlayerMissionModifiers;
        }

        public override void FixedUpdateNetwork()
        {
            if (!netEntity.IsAlive)
                return;

            var hasInput = GetInput(out PlayerInputData input);
            if (hasInput)
            {
                netEntity.RotateByLookDirection(input.LookDirection);

                var isSprint = input.Buttons.IsSet(PlayerButtons.Sprint);
                netEntity.CalculateVelocity(input.HorizontalInput, input.VerticalInput, isSprint);
                
                if ((HasStateAuthority || HasInputAuthority) && input.Buttons.IsSet(PlayerButtons.Fire))
                    netEntity.TryShoot();

                var reloadRequest = input.Buttons.IsSet(PlayerButtons.Reload) || (netEntity.RequiredReload && _missionModifiers.AutoReloading);
                if ((HasStateAuthority || HasInputAuthority) && reloadRequest) 
                    netEntity.TryReload();
            }
            
#if UNITY_EDITOR
            if (HasStateAuthority && Input.GetKeyDown(KeyCode.Z)) 
                netEntity.TakeDamage(999, netEntity);
#endif
        }
    }
}
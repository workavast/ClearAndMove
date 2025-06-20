using System;
using App.Armor;
using App.Dissolving;
using App.Entities.Health;
using App.Interaction;
using App.Weapons;
using Avastrad.EventBusFramework;
using Avastrad.Extensions;
using Fusion;
using UnityEngine;

namespace App.Entities
{
    public abstract class NetEntity : NetworkBehaviour, IEntity, IInteractor
    {
        [SerializeField] private EntityConfig config;
        [SerializeField] private EntityView entityView;
        [SerializeField] private NetHealth health;
        [SerializeField] private Hitbox hitbox;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private NetworkCharacterController netCharacterController;
        [SerializeField] private NetInteractorZone netInteractorZone;
        [field: SerializeField] public DissolvesUpdater DissolvesUpdater { get; private set; }

        [Networked] [field: ReadOnly, SerializeField] public Vector3 NetVelocity { get; private set; }
        [Networked] [OnChangedRender(nameof(UpdateArmorConfig))] [field: ReadOnly] public int NetArmorLevel { get; private set; }

        public bool IsSpawned { get; private set; }
        public GameObject GameObject => gameObject;
        public EntityIdentifier Identifier { get; } = new();
        public abstract EntityType EntityType { get; }
        public float MaxHealthPoints => health.MaxHealthPoints;
        public float NetHealthPoints => health.NetHealthPoints;
        public Vector3 Position => transform.position;

        public bool RequiredReload => NetWeapon.RequiredReload;
        public bool CanReload => NetWeapon.CanReload;
        public int MaxMagazineAmmo => NetWeapon.MaxMagazineAmmo;
        public int CurrentMagazineAmmo => NetWeapon.CurrentMagazineAmmo;
        public int FullAmmoSize => NetWeapon.FullAmmoSize;
        public float SpreadAngle => NetWeapon.SpreadAngle;

        protected float Gravity => config.Gravity;
        protected float WalkSpeed => config.WalkSpeed - _armor.WalkSpeedDecrease;
        protected float SprintSpeed => config.SprintSpeed - _armor.SprintSpeedDecrease;
        protected float MoveAcceleration => config.MoveAcceleration - _armor.MoveAccelerationDecrease;
        
        public bool IsAlive => IsSpawned && health.IsAlive;
        public bool IsKnockout => IsSpawned && health.IsKnockout;
        public bool IsDead => IsSpawned && health.IsDead;
        public bool IsKnockoutOrDead => IsSpawned && (health.IsKnockout || health.IsDead);
        
        protected abstract IEventBus EventBus { get; set; }
        protected abstract ArmorConfigsRep ArmorConfigsRep { get; set; }
        protected NetWeapon NetWeapon;

        private ArmorConfig _armor;

        public event Action OnKnockout;
        public event Action OnDeath;
        public event Action<IEntity> OnDeathEntity;
        public event Action OnWeaponShot; 

        protected virtual void Awake()
        {
            NetWeapon = GetComponent<NetWeapon>();

            health.OnKnockout += () => OnKnockout?.Invoke();
            health.OnDeath += () => OnDeath?.Invoke();
            health.OnDeathEntity += (_) => OnDeathEntity?.Invoke(this);
        }

        public override void Spawned()
        {
            IsSpawned = true;
            _armor = ArmorConfigsRep.GetArmor(0);
            hitbox.HitboxActive = 
                characterController.enabled = 
                netCharacterController.enabled = true;
            
            netInteractorZone.SetVisibility(HasInputAuthority);
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            IsSpawned = false;
        }

        public override void FixedUpdateNetwork()
        {
            hitbox.HitboxActive = 
                characterController.enabled = 
                    netCharacterController.enabled = health.IsAlive;
        }

        public override void Render()
        {
            entityView.MoveView(NetVelocity, SprintSpeed);
            entityView.SetAliveState(IsAlive);
            
            hitbox.HitboxActive = 
                characterController.enabled = 
                    netCharacterController.enabled = health.IsAlive;
        }

        public void TakeDamage(float damage, IEntity killer) 
            => health.TakeDamage(damage, killer);

        public void SetWeapon(WeaponId weaponId)
        {
            NetWeapon = GetComponent<NetWeapon>();
            NetWeapon.SetWeapon(weaponId);
        }

        public void TryInteract(bool isInteract) 
            => netInteractorZone.TtyInteract(this, isInteract);

        public void SetArmor(int armorLevel) 
            => NetArmorLevel = armorLevel;

        public ArmorConfig GetArmor()
            => _armor;

        public abstract string GetName();

        public void TryShoot()
        {
            if (!health.IsAlive)
            {
                Debug.LogError($"You try shoot by entity that un full alive: [{gameObject.name}]");
                return;
            }
            
            if (NetWeapon.CanShot && NetWeapon.TryShoot())
                OnWeaponShot?.Invoke();
        }
        
        public void TryReload()
        {
            if (NetWeapon.CanReload) 
                NetWeapon.TryReload();
        }
        
        public void RotateByLookDirection(Vector2 lookDirection)
        {
            if (!health.IsAlive)
            {
                Debug.LogError($"You try rotate entity that un full alive: [{gameObject.name}]");
                return;
            }
            
            Vector3 lookPoint;
            if (lookDirection == default || lookDirection == Vector2.zero)
                lookPoint = entityView.transform.position + entityView.transform.forward;
            else
                lookPoint = entityView.transform.position + lookDirection.X0Y();
                
            entityView.SetLookPoint(lookPoint);
        }
        
        public void CalculateVelocity(float horizontalInput, float verticalInput, bool isSprint)
        {
            if (!health.IsAlive)
            {
                Debug.LogError($"You try move entity that un full alive: [{gameObject.name}]");
                return;
            }
            
            if (!characterController.enabled)
            {
                Debug.LogWarning($"You try move entity that characterController un active: [{gameObject.name}]");
                return;
            }
            
            var targetVelocity = GetUnscaledVelocity(horizontalInput, verticalInput, isSprint);
            
            var currentVelocity = new Vector3(NetVelocity.x, 0, NetVelocity.z);
            currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, MoveAcceleration * Runner.DeltaTime);
            currentVelocity.y = 0;
            
            NetVelocity = new Vector3(currentVelocity.x, targetVelocity.y, currentVelocity.z);
            characterController.Move(NetVelocity * Runner.DeltaTime);
        }

        private Vector3 GetUnscaledVelocity(float horizontalInput, float verticalInput, bool isSprint)
        {
            var moveDirection = Vector3.right * horizontalInput + Vector3.forward * verticalInput;
            var moveSpeed = isSprint ? SprintSpeed : WalkSpeed;
           
            var unscaledGravityVelocity = Gravity * Vector3.up;
            var unscaledVelocity = moveSpeed * moveDirection;
            
            return  unscaledGravityVelocity + unscaledVelocity;
        }
        
        private void UpdateArmorConfig() 
            => _armor = ArmorConfigsRep.GetArmor(NetArmorLevel);
    }
}
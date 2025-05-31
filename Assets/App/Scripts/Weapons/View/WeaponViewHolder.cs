using System;
using System.Collections;
using App.Audio.Sources;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace App.Weapons.View
{
    public class WeaponViewHolder : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSourceHolderPoolable reloadStartSfxPrefab;
        [SerializeField] private AudioSourceHolderPoolable reloadEndSfxPrefab;
        [SerializeField] private Transform weaponsParent;
        [SerializeField] private HandsConstraint handsConstraint;
        
        [Inject] private readonly AudioFactory _audioFactory;
        [Inject] private readonly DiContainer _container;

        private const string DefaultState = "Default";
        private const string ReloadingState = "Reloading";
        private const string DeathState = "Death";
        
        private WeaponView _weaponView;

        public void SetWeaponView(WeaponView prefab)
        {
            if (_weaponView != null)
            {
                if (_weaponView.name == prefab.name)
                    return;
                Destroy(_weaponView.gameObject);
            }

            _weaponView = _container.InstantiatePrefab(prefab, weaponsParent).GetComponent<WeaponView>();
            _weaponView.name = prefab.name;
            animator.runtimeAnimatorController = _weaponView.AnimatorController;
            animator.Rebind();
            handsConstraint.SetWeapon(_weaponView);
        }
        
        public void Default()
        {
            animator.speed = 1;
            animator.Play(DefaultState);
        }
        
        /// <param name="initialTime">Value between 0 and 1</param>
        public void Reloading(float duration, float initialTime)
        {
            animator.Play(ReloadingState, -1, initialTime);
            StartCoroutine(AdjustSpeed(duration));
        }

        /// <param name="initialTime">Value between 0 and 1</param>
        public void Death(float duration)
        {
            animator.Play(DeathState, -1, 0);
            StartCoroutine(AdjustSpeed(duration));
        }
        
        private IEnumerator AdjustSpeed(float duration)
        {
            yield return null;

            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            var length = stateInfo.length * animator.speed;

            animator.speed = length / duration;
        }
        
        public void ReloadStartSfx() 
            => _audioFactory.Create(reloadStartSfxPrefab, transform.position);
        
        public void ReloadEndSfx() 
            => _audioFactory.Create(reloadEndSfxPrefab, transform.position);

        public void ShotVfx() 
            => _weaponView.ShotVfx();

        public void ShotSfx()
            => _weaponView.ShotSfx();
        
        [Serializable]
        private struct HandsConstraint
        {
            [SerializeField] private TwoBoneIKConstraint rightHandConstraint;
            [SerializeField] private TwoBoneIKConstraint leftHandConstraint;
            [SerializeField] private RigBuilder rigBuilder;

            public void SetWeapon(WeaponView weaponView)
            {
                rightHandConstraint.data.target = weaponView.RightHandPoint;
                leftHandConstraint.data.target = weaponView.LeftHandPoint;
                rigBuilder.Build();
            }
        }
    }
}
using App.Audio.Sources;
using UnityEngine;
using Zenject;

namespace App.Weapons.View
{
    public class WeaponView : MonoBehaviour
    {
        [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }
        [field: SerializeField] public Transform RightHandPoint { get; private set; }
        [field: SerializeField] public Transform LeftHandPoint { get; private set; }
        
        [SerializeField] private WeaponViewConfig config;
        [SerializeField] private Transform barrelPoint;
        [SerializeField] private AudioSourceHolderPoolable shotSfxPrefab;

        [Inject] private AudioFactory _audioFactory;

        public void ShotVfx()
        {
            Instantiate(config.ShotSmoke, barrelPoint);
        }

        public void ShotSfx()
        {
            var pitchOffset = Random.Range(-config.MaxPitchOffset, config.MaxPitchOffset);
            _audioFactory.Create(shotSfxPrefab, barrelPoint.position, 1 + pitchOffset);
        }
    }
}
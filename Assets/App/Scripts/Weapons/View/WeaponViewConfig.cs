using App.Audio.Sources;
using App.Particles;
using UnityEngine;

namespace App.Weapons.View
{
    [CreateAssetMenu(fileName = nameof(WeaponViewConfig), menuName = Consts.WeaponConfigsPath + nameof(WeaponViewConfig))]
    public class WeaponViewConfig : ScriptableObject
    {
        [field: SerializeField] public ParticleHolder ShotSmokePrefab { get; private set; }
        [field: SerializeField] public AudioSourceHolderPoolable ShotSfxPrefab { get; private set; }
        [field: SerializeField, Range(-3, 3)] public float MinPitch { get; private set; } = 0.9f;
        [field: SerializeField, Range(-3, 3)] public float MaxPitch { get; private set; } = 1.1f;
    }
}
using System;
using App.Audio;
using UnityEngine;

namespace App.Settings.Volume
{
    [Serializable]
    public class VolumeSettingsModel : ISettingsModel
    {
        [field: SerializeField] public float MasterVolume { get; private set; }
        [field: SerializeField] public float MusicVolume { get; private set; }
        [field: SerializeField] public float EffectsVolume { get; private set; }

        public float DefaultMasterVolume => _config.DefaultMasterVolume;
        public float DefaultMusicVolume => _config.DefaultMusicVolume;
        public float DefaultEffectsVolume => _config.DefaultEffectsVolume;
        
        private readonly VolumeSettingsConfig _config;
        private readonly AudioVolumeChanger _audioVolumeChanger;
        
        public VolumeSettingsModel(VolumeSettingsConfig config)
        {
            _config = config;
            _audioVolumeChanger = new AudioVolumeChanger(_config.AudioMixer, config.MasterParam, config.EffectsParam,
                config.MusicParam);
            
            MasterVolume = DefaultMasterVolume;
            MusicVolume = DefaultMusicVolume;
            EffectsVolume = DefaultEffectsVolume;
        }
    
        public void SetVolume(VolumeType volumeType, float value)
        {
            switch (volumeType)
            {
                case VolumeType.Master:
                    MasterVolume = value;
                    break;
                case VolumeType.Effects:
                    EffectsVolume = value;
                    break;
                case VolumeType.Music:
                    MusicVolume = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(volumeType), volumeType, null);
            }
            
            Apply();
        }
        
        public void SetVolumeTemporary(VolumeType volumeType, float value) 
            => _audioVolumeChanger.SetVolume(volumeType, value);

        public void Apply()
        {
            _audioVolumeChanger.SetVolume(VolumeType.Master, MasterVolume);
            _audioVolumeChanger.SetVolume(VolumeType.Effects, EffectsVolume);
            _audioVolumeChanger.SetVolume(VolumeType.Music, MusicVolume);
        }

        public void ResetToDefault()
        {
            MasterVolume = DefaultMasterVolume;
            MusicVolume = DefaultMusicVolume;
            EffectsVolume = DefaultEffectsVolume;
            Apply();
        }

        public void Load(VolumeSettingsModel model)
        {
            MasterVolume = model.MasterVolume;
            MusicVolume = model.MusicVolume;
            EffectsVolume = model.EffectsVolume;
        }
    }
}
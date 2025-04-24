using System;
using App.Audio;
using UnityEngine;
using Zenject;

namespace App.Settings.Volume
{
    public class VolumeSettingsViewModel : MonoBehaviour, ISettingsViewModel
    {
        [Inject] private SettingsModel _settingsModel;

        private VolumeSettingsModel Model => _settingsModel.VolumeSettingsModel;
        
        public float MasterVolume { get; private set; }
        public float MusicVolume { get; private set; }
        public float EffectsVolume { get; private set; }

        public Action OnChanged;
        
        public void Initialize()
        {
            MasterVolume = Model.MasterVolume;
            MusicVolume = Model.MusicVolume;
            EffectsVolume = Model.EffectsVolume;
        }

        public void ApplySettings()
        {
            Model.SetVolume(VolumeType.Master, MasterVolume);
            Model.SetVolume(VolumeType.Effects, EffectsVolume);
            Model.SetVolume(VolumeType.Music, MusicVolume);
        }

        public void ResetSettings()
        {
            MasterVolume = Model.MasterVolume; 
            EffectsVolume = Model.EffectsVolume;
            MusicVolume = Model.MusicVolume;
            ApplySettings();
        }

        public void ResetToDefault()
        {
            MasterVolume = Model.DefaultMasterVolume; 
            EffectsVolume = Model.DefaultEffectsVolume;
            MusicVolume = Model.DefaultMusicVolume;
            ApplySettings();
        }

        public void Set(VolumeType volumeType, float value, bool notify)
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

            Model.SetVolumeTemporary(volumeType, value);
            if (notify)
                OnChanged?.Invoke();
        }

        public float GetVolume(VolumeType volumeType)
        {
            switch (volumeType)
            {
                case VolumeType.Master:
                    return MasterVolume;
                case VolumeType.Effects:
                    return EffectsVolume;
                case VolumeType.Music:
                    return MusicVolume;
                default:
                    throw new ArgumentOutOfRangeException(nameof(volumeType), volumeType, null);
            }
        }
    }
}
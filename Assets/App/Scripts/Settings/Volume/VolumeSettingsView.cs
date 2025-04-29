using App.Audio;
using Avastrad.UI.Elements;
using UnityEngine;

namespace App.Settings.Volume
{
    [RequireComponent(typeof(ExtendedSlider))]
    public class VolumeSettingsView : MonoBehaviour, ISettingsView
    {
        [SerializeField] private VolumeType volumeType;
        [SerializeField] private VolumeSettingsViewModel viewModel;
        
        private ExtendedSlider _slider;
        
        public void Initialize()
        {
            _slider = GetComponent<ExtendedSlider>();
            UpdateView();
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            _slider.onValueChanged.AddListener(SetVolume);
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            _slider.onValueChanged.RemoveListener(SetVolume);
        }

        private void UpdateView() 
            => _slider.SetValueWithoutNotify(viewModel.GetVolume(volumeType));

        private void SetVolume(float newVolume) 
            => viewModel.Set(volumeType, newVolume, false);
    }
}
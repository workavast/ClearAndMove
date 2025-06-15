using App.UI.SliderExt;
using UnityEngine;

namespace App.Settings.Brightness
{
    public class BrightnessSettingsView : MonoBehaviour, ISettingsView
    {
        [SerializeField] private BrightnessSettingsViewModel viewModel;
        [SerializeField] private SliderWithStep slider;
        
        public void Initialize()
        {
            slider.SetMinValue(viewModel.MinValue);
            slider.SetMaxValue(viewModel.MaxValue);
            
            UpdateView();
        }

        public void OnEnabledManual()
        {
            viewModel.OnChanged += UpdateView;
            slider.OnValueChanged += SetValue;
            UpdateView();
        }

        public void OnDisabledManual()
        {
            viewModel.OnChanged -= UpdateView;
            slider.OnValueChanged -= SetValue;
        }

        private void UpdateView() 
            => slider.SetValue(viewModel.Value, false);

        private void SetValue(float value) 
            => viewModel.SetValue(value, false);
    }
}
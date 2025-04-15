using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.SliderExt
{
    public class SliderWithStep : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField, Min(0)] private float step;
        
        public float Value => slider.value * step;
        
        public event Action<float> OnValueCHanged;
        
        private void Awake()
        {
            slider.onValueChanged.AddListener(OnSlideValueChanged);
        }

        public void SetValue(float value, bool notify = true)
        {
            value = Mathf.Round(value / step) * step;
            
            if (notify)
                slider.value = value / step;
            else
                slider.SetValueWithoutNotify(value / step);
        }
        
        private void OnSlideValueChanged(float value)
        {
            value *= step;
            OnValueCHanged?.Invoke(value);
        }
    }
}
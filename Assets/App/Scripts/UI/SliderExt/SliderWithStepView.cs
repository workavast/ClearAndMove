using System.Globalization;
using TMPro;
using UnityEngine;

namespace App.UI.SliderExt
{
    public class SliderWithStepView : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueView;
        [SerializeField] private SliderWithStep sliderWithStep;

        private void OnEnable()
        {
            sliderWithStep.OnValueCHanged += UpdateView;
            UpdateView(sliderWithStep.Value);
        }

        private void OnDisable() 
            => sliderWithStep.OnValueCHanged -= UpdateView;

        private void UpdateView(float value) 
            => valueView.text = value.ToString(CultureInfo.InvariantCulture);
    }
}
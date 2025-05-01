using App.Core.Timer;
using UnityEngine;

namespace App.StairsZone
{
    [RequireComponent(typeof(RectTransform))]
    public class StairsZoneView : MonoBehaviour
    {
        [SerializeField] private CountdownView countdownView;
        
        private RectTransform _rectTransform;
        
        private void Awake() 
            => _rectTransform = GetComponent<RectTransform>();

        public void ToggleVisibility(bool isVisible) 
            => gameObject.SetActive(isVisible);

        public void ToggleCountdownVisibility(bool isVisible) 
            => countdownView.ToggleVisibility(isVisible);

        public void SetSize(float radius)
        {
            _rectTransform.sizeDelta = new Vector2(radius * 2, radius * 2);
        }
    }
}
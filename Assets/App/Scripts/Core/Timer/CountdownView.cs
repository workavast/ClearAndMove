using TMPro;
using UnityEngine;

namespace App.Core.Timer
{
    public class CountdownView : MonoBehaviour
    {
        [SerializeField] private InterfaceReference<ITimer> timer;
        [SerializeField] private TMP_Text tmpText;

        private void LateUpdate() 
            => tmpText.text = GetTime();

        public void ToggleVisibility(bool isVisible) 
            => gameObject.SetActive(isVisible);

        private string GetTime()
        {
            var time = timer.Value.GetTime();
            return $"{time.Minutes:00}:{time.Seconds:00}";
        }
    }
}
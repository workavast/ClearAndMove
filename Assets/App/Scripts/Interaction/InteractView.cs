using UnityEngine;
using UnityEngine.UI;

namespace App.Interaction
{
    public class InteractView : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image fillImage;
        [SerializeField] private NetInteractiveZone netInteractiveZone;

        private void LateUpdate()
        {
            if (netInteractiveZone.IsVisible)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.up);
                canvas.gameObject.SetActive(true);
                fillImage.fillAmount = netInteractiveZone.InteractPercentage;
            }
            else
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}
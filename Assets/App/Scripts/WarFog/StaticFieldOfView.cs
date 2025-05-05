using UnityEngine;

namespace App.WarFog
{
    public class StaticFieldOfView : MonoBehaviour
    {
        public void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}
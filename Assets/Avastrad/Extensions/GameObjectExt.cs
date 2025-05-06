using UnityEngine;

namespace Avastrad.Extensions
{
    public static class GameObjectExt
    {
        public static bool TrySetActive(this GameObject gameObject, bool isActive)
        {
            if (gameObject == null) 
                return false;
            
            gameObject.SetActive(isActive);
            return true;
        }
    }
}
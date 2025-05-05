using UnityEngine;

namespace App
{
    public static class ColliderExt
    {
        public static bool Contains(this Collider checkCollider, Vector3 point) 
            => checkCollider.bounds.Contains(point);
        
        public static bool ContainsByY(this Collider checkCollider, Vector3 point)
        {
            var maxY = checkCollider.bounds.max.y;
            var minY = checkCollider.bounds.min.y;
            return minY <= point.y && point.y <= maxY;
        }
    }
}
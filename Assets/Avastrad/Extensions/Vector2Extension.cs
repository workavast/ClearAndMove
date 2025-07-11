using UnityEngine;

namespace Avastrad.Extensions
{
    public static class Vector2Extension
    {
        public static Vector3 X0Y(this Vector2 vector2) 
            => new(vector2.x, 0, vector2.y);

        public static Vector3 XY0(this Vector2 vector2, float o = 0) 
            => new(vector2.x, vector2.y, o);
        
        public static Vector2 GetRandomDirection()
        {
            float xRandom = Random.Range(-1f, 1f);
            float yRandom = Random.Range(-1f, 1f);

            return new Vector2(xRandom, yRandom).normalized;
        }
        
        public static Vector2 GetPointOnCircle(this Vector2 center, float minDistance, float maxDistance)
        {
            float randomDistance = Random.Range(minDistance, maxDistance);
            return center + GetRandomDirection() * randomDistance;
        }
        
        public static Vector2 ClampInCircle(this Vector2 point, Vector2 center, float radius)
        {
            var result = point;
            var distanceFromCenter = Vector2.Distance(point, center);
            
            if (distanceFromCenter > radius)
            {
                var newPosDir = point.normalized;
                result = newPosDir * radius;
            }
            
            return result;
        }
        
        public static bool PointInCircle(this Vector2 point, Vector2 center, float radius)
        {
            var distanceFromCenter = Vector2.Distance(point, center);
            return distanceFromCenter < radius;
        }
        
        public static Vector2 GetRandom(Vector2 min, Vector2 max)
        {
            float xRandom = Random.Range(min.x, max.x);
            float yRandom = Random.Range(min.y, max.y);

            return new Vector2(xRandom, yRandom);
        }
    }
}
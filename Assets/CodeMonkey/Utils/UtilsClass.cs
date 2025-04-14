using UnityEngine;

namespace CodeMonkey.Utils
{
	public static class UtilsClass
	{
		public static Vector3 GetVectorFromAngle(float angle)
		{
			// angle = 0 -> 360
			float angleRad = angle * (Mathf.PI / 180f);
			return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
		}

		public static float GetAngleFromVectorFloat(Vector3 dir)
		{
			dir = dir.normalized;
			float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			if (n < 0) n += 360;

			return n;
		}
	}
}
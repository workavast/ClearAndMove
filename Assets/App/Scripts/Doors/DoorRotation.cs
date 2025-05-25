using System;
using UnityEngine;

namespace App.Doors
{
    [Serializable]
    public class DoorRotation
    {
        [field: SerializeField, Range(0, 180)] public float Rotation { get; private set; }
        [field: SerializeField, Min(0)] public float ExecuteTime { get; private set; }

        public DoorRotation(float rotation = 90, float executeTime = 1)
        {
            ExecuteTime = executeTime;
            Rotation = rotation;
        }
    }
}
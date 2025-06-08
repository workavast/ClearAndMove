using System;
using Unity.Cinemachine;
using UnityEngine;

namespace App.Lobby.Diegetic
{
    public class DiegeticCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private CinemachineRotationComposer composer;
        [SerializeField] private CinemachineFollow follow;
        
        [SerializeField] private DampingConfig rotationDamping;
        [SerializeField] private DampingConfig positionDamping;
        
        public void SetData(Transform lookPoint, Transform positionPoint)
        {
            cinemachineCamera.LookAt = lookPoint;
            
            if (positionPoint == cinemachineCamera.Follow)
                return;

            var prevTargetPoint = cinemachineCamera.Follow;
            cinemachineCamera.Follow = positionPoint;

            rotationDamping.StartMove(prevTargetPoint.position, cinemachineCamera.Follow.position);
            positionDamping.StartMove(prevTargetPoint.position, cinemachineCamera.Follow.position);
        }

        private void LateUpdate()
        {
            // composer.Damping = rotationDamping.GetDamping(cinemachineCamera.transform.position);
            // follow.TrackerSettings.PositionDamping = positionDamping.GetDamping(cinemachineCamera.transform.position);
        }

        [Serializable]
        private class DampingConfig
        {
            [SerializeField, Range(0, 1)] private float minPercentageToReduceDamping;
            [SerializeField] private float scale;
            [SerializeField] private float minScale = 0.1f;
            [SerializeField] private Vector3 damping;

            private float _fullLenght;
            private Vector3 _targetPosition;

            public void StartMove(Vector3 startPosition, Vector3 targetPosition)
            {
                _targetPosition = targetPosition;
                _fullLenght = Vector3.Distance(startPosition, targetPosition);
            }
            
            public Vector3 GetDamping(Vector3 currentPosition)
            {
                var distance = Vector3.Distance(currentPosition, _targetPosition);
                var percentage = distance / _fullLenght;

                if (percentage < minPercentageToReduceDamping)
                {
                    var dynamicScale = percentage / minPercentageToReduceDamping;
                    dynamicScale = Mathf.Clamp(Mathf.Pow(dynamicScale, this.scale), minScale, float.PositiveInfinity);
                    return  damping * dynamicScale;
                }
                else
                {
                    return damping;
                }
            }
        }
    }
}
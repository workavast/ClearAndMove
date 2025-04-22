using App.Entities;
using UnityEngine;
using UnityEngine.AI;

namespace App.Ai.FSMs.Movement
{
    public class RandomMove : MovementState
    {
        private readonly NavMeshPath _path = new();
        private readonly float _minDistance;
        private readonly float _maxDistance;
        private readonly float _tolerance;
        
        private int _currentCorner;
        
        public RandomMove(NetEntity entity, AiModel aiModel, float minDistance, float maxDistance, float tolerance) 
            : base(entity, aiModel)
        {
            _minDistance = minDistance;
            _maxDistance = maxDistance;
            _tolerance = tolerance;
        }
        
        protected override void OnEnterState()
        {
            var moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            var moveDistance = Random.Range(_minDistance, _maxDistance);
            var targetPosition = Entity.transform.position + moveDirection * moveDistance;
            
            Entity.GetPath(targetPosition, _path);
            _currentCorner = 0;
        }
        
        protected override void OnEnterStateRender()
        {
            Entity.DrawPath(_path);
        }

        protected override void OnFixedUpdate()
        {
            Entity.LookAtPoint(AiModel.LastVisibleTargetPosition);
            if (Entity.MoveToPoint(_path, _tolerance, ref _currentCorner))
            {
                TryActivateState<Stay>();
                return;
            }
        }
    }
}
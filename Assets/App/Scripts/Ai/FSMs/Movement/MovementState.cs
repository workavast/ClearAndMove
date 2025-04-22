using App.Entities;
using Avastrad.Vector2Extension;
using Fusion.Addons.FSM;
using UnityEngine;

namespace App.Ai.FSMs.Movement
{
    public class MovementState : State<MovementState>
    {
        protected readonly AiModel AiModel;
        protected readonly NetEntity Entity;

        protected MovementState(NetEntity entity, AiModel aiModel)
        {
            Entity = entity;
            AiModel = aiModel;
        }
        
        protected bool ArrivePosition(Vector3 targetPosition, float tolerance) 
            => Vector2.Distance(Entity.transform.position.XZ(), targetPosition.XZ()) <= tolerance;
    }
}
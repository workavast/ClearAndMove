using App.Interaction;
using Fusion;
using UnityEngine;

namespace App.Doors.FSM.SpecificStates
{
    public abstract class Open : DoorState
    { 
        protected Open(NetDoor netDoor, DoorRotation doorRotation, GameObject door) : base(netDoor, doorRotation, door)
        {
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            Owner.NetIsInteractable = false;
        }

        protected override void OnFixedUpdate()
        {
            if (!Owner.NetExecuteTimer.RemainingTime(Runner).HasValue)
                return;

            var remainingTime = Owner.NetExecuteTimer.RemainingTime(Runner).Value;
            remainingTime = Mathf.Clamp(remainingTime, 0, float.MaxValue);
            SetRotation(1 - remainingTime / DoorRotation.ExecuteTime);
            
            if (remainingTime <= 0) 
                Owner.NetExecuteTimer = TickTimer.None;
        }
        
        public override void Interact(IInteractor _) 
            => TryActivateState<Close>();
    }
}
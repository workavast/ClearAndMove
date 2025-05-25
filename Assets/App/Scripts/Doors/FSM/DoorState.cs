using App.Interaction;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;

namespace App.Doors.FSM
{
    public abstract class DoorState : State<DoorState>
    {
        protected readonly NetDoor Owner;
        protected readonly DoorRotation DoorRotation;
        protected readonly GameObject Door;
        
        protected NetworkRunner Runner => Owner.Runner;
        protected Transform transform => Owner.transform;
        protected float Rotation => DoorRotation.Rotation;
        protected abstract int directionRotation { get; }

        protected DoorState(NetDoor netDoor, DoorRotation doorRotation, GameObject door)
        {
            Owner = netDoor;
            DoorRotation = doorRotation;
            Door = door;
        }

        protected override void OnEnterState()
        {
            Owner.NetIsInteractable = false;
            Owner.NetExecuteTimer = TickTimer.CreateFromSeconds(Runner, DoorRotation.ExecuteTime);
        }

        public void SetRotation(float percentage)
        {
            Door.transform.localRotation = Quaternion.Euler(0, Rotation * directionRotation * percentage, 0);
        }
        
        public abstract void Interact(IInteractor interactor);
    }
}
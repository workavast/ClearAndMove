using App.Interaction;
using Fusion;
using UnityEngine;

namespace App.Doors.FSM.SpecificStates
{
    public class Close : DoorState
    {
        protected override int directionRotation => _directionRotationSign;
        private int _directionRotationSign;

        public Close(NetDoor netDoor, DoorRotation doorRotation, GameObject door) : base(netDoor, doorRotation, door)
        {
        }

        protected override void OnFixedUpdate()
        {
            if (!Owner.NetExecuteTimer.RemainingTime(Runner).HasValue)
                return;

            var angle = Door.transform.localRotation.eulerAngles.y;
            if (angle > 180) 
                angle -= 360;
            _directionRotationSign = (int)Mathf.Sign(angle);
            
            var remainingTime = Owner.NetExecuteTimer.RemainingTime(Runner).Value;
            remainingTime = Mathf.Clamp(remainingTime, 0, float.MaxValue);
            SetRotation(remainingTime / DoorRotation.ExecuteTime);

            if (remainingTime <= 0)
            {
                Owner.NetIsInteractable = true;
                Owner.NetExecuteTimer = TickTimer.None;
            }
        }
        
        public override void Interact(IInteractor interactor)
        {
            var doorPosition = Door.transform.position;
            var interactorPosition = interactor.Position;
    
            // Направление от двери к взаимодействующему объекту
            var directionToInteractor = interactorPosition - doorPosition;
    
            // Определяем, находится ли interactor справа от двери (по локальной оси X)
            var isInteractorOnRight = Vector3.Dot(directionToInteractor, Door.transform.forward) > 0;
    
            // Если interactor справа → открываем влево (OpenLeft), иначе вправо (OpenRight)
            if (isInteractorOnRight)
                TryActivateState<OpenRight>();
            else
                TryActivateState<OpenLeft>();
        }
    }
}
using System.Collections.Generic;
using App.Doors.FSM;
using App.Doors.FSM.SpecificStates;
using App.Interaction;
using DCFApixels;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;

namespace App.Doors
{
    public class NetDoor : NetworkBehaviour, IStateMachineOwner, IInteractive
    {
        [SerializeField] private GameObject door;
        [SerializeField] private float maxInteractTime;
        [SerializeField] private DoorRotation doorRotation = new();

        private float Rotation => doorRotation.Rotation;
        
        [Networked] public TickTimer NetExecuteTimer { get; set; }
        [Networked] public bool NetIsInteractable { get; set; } = true;
        [Networked] private float NetInteractTime { get; set; }

        public bool IsInteractable => NetIsInteractable;
        public float MaxInteractTime => maxInteractTime;
        public float CurrentInteractTime => NetInteractTime;

        private StateMachine<DoorState> _fsm;

        private Close _close;
        private OpenLeft _openLeft;
        private OpenRight _openRight;
        
        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            _close = new Close(this, doorRotation, door);
            _openLeft = new OpenLeft(this, doorRotation, door);
            _openRight = new OpenRight(this, doorRotation, door);
            _fsm = new StateMachine<DoorState>("Door", _close, _openLeft, _openRight);
            
            stateMachines.Add(_fsm);
        }

        public override void Spawned()
        {
            Runner.SetIsSimulated(Object, true);
        }

        public void Interact(IInteractor interactor)
        {
            if (!IsInteractable)
            {
                Debug.LogError($"You try interact with interactable, when it not interactable: {IsInteractable}");
                return;
            }
            
            _fsm.ActiveState.Interact(interactor);
        }
        
        public void AddInteractTime(IInteractor interactor, float value)
        {
            var interactTime = Mathf.Clamp(NetInteractTime + value, 0, MaxInteractTime);
            if (CurrentInteractTime >= MaxInteractTime)
            {
                Interact(interactor);
                NetInteractTime = 0;
            }
            else
            {
                NetInteractTime = interactTime;
            }
        }

        public void SetInteractTime(float value) 
            => NetInteractTime = Mathf.Clamp(value, 0, MaxInteractTime);

        private void OnDrawGizmosSelected()
        {
            var doorPosition = transform.position;
            var doorForward = transform.right;

            const int gizmoRadius = 1;
            
            var openLeftDirection = Quaternion.AngleAxis(-Rotation, Vector3.up) * doorForward;
            DebugX.Draw(Color.blue).Line(doorPosition, doorPosition + openLeftDirection * gizmoRadius);
            DrawArc(doorPosition, doorForward, -Rotation, gizmoRadius, Color.blue);

            var openRightDirection = Quaternion.AngleAxis(Rotation, Vector3.up) * doorForward;
            DebugX.Draw(Color.red).Line(doorPosition, doorPosition + openRightDirection * gizmoRadius);
            DrawArc(doorPosition, doorForward, Rotation, gizmoRadius, Color.red);
        }

        private static void DrawArc(Vector3 center, Vector3 forward, float angle, float radius, Color color)
        {
            const int segments = 20;
            var angleStep = angle / segments;
            var prevPoint = center + forward * radius;

            for (int i = 1; i <= segments; i++)
            {
                var currentAngle = angleStep * i;
                var nextPoint = center + Quaternion.AngleAxis(currentAngle, Vector3.up) * forward * radius;
                Gizmos.color = color;
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
        }
    }
}
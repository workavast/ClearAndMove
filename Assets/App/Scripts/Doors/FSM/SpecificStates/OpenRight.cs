using UnityEngine;

namespace App.Doors.FSM.SpecificStates
{
    public class OpenRight : Open
    {
        protected override int directionRotation => 1;

        public OpenRight(NetDoor netDoor, DoorRotation doorRotation, GameObject door) : base(netDoor, doorRotation, door)
        {
        }
    }
}
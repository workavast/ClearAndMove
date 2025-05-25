using UnityEngine;

namespace App.Doors.FSM.SpecificStates
{
    public class OpenLeft : Open
    {
        protected override int directionRotation => -1;

        public OpenLeft(NetDoor netDoor, DoorRotation doorRotation, GameObject door) : base(netDoor, doorRotation, door)
        {
        }
    }
}
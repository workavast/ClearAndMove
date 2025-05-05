using Avastrad.EventBusFramework;
using Fusion;

namespace App.EventBus
{
    public struct OnPlayerDeath : IEvent
    {
        public readonly PlayerRef PlayerRef;

        public OnPlayerDeath(PlayerRef playerRef)
        {
            PlayerRef = playerRef;
        }
    }
}
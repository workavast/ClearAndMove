using Avastrad.EventBusFramework;

namespace App.EventBus
{
    public struct OnLevelChanged : IEvent
    {
        public readonly int ActiveLevelIndex;

        public OnLevelChanged(int activeLevelIndex)
        {
            ActiveLevelIndex = activeLevelIndex;
        }
    }
}
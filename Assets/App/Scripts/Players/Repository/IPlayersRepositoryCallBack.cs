using Avastrad.CheckOnNullLibrary;
using Fusion;

namespace App.Players.Repository
{
    public interface IPlayersRepositoryCallBack
    {
        public int Priority { get; }

        public void OnPlayerJoined(PlayerRef playerRef);
        public void OnPlayerLeft(PlayerRef playerRef);
        public void OnPlayerChanged();

        public static int Composer(IPlayersRepositoryCallBack a, IPlayersRepositoryCallBack b)
        {
            if (a.IsAnyNull() && b.IsAnyNull())
                return 0;

            if (a.Priority == b.Priority)
                return 0;

            if (a.Priority > b.Priority)
                return -1;
            else
                return 1;
        }
    }
}
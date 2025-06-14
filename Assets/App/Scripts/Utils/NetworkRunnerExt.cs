using System.Linq;
using Fusion;

namespace App.Utils
{
    public static class NetworkRunnerExt
    {
        public static PlayerRef GetNextPlayer(this NetworkRunner runner, PlayerRef currentPlayer)
        {
            var count = runner.ActivePlayers.Count();
            if (currentPlayer.AsIndex < count)
                return PlayerRef.FromIndex(currentPlayer.AsIndex + 1);
            else
                return PlayerRef.FromIndex(1);
        }

        public static PlayerRef GetPrevPlayer(this NetworkRunner runner, PlayerRef currentPlayer)
        {
            var count = runner.ActivePlayers.Count();
            if (currentPlayer.AsIndex > 1)
                return PlayerRef.FromIndex(currentPlayer.AsIndex - 1);
            else
                return PlayerRef.FromIndex(count);
        }
    }
}
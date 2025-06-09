using App.Players.SessionData;

namespace App.Lobby.SessionData
{
    public class NetLobbySessionDataRegistrator : NetSessionDataRegistrator<NetLobbySessionData>
    {
        public override int Priority => -10;
    }
}

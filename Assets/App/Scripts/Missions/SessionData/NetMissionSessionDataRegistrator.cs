using App.Players.SessionData;

namespace App.Missions.SessionData
{
    public class NetMissionSessionDataRegistrator : NetSessionDataRegistrator<NetMissionSessionData>
    {
        public override int Priority => -10;
    }
}

namespace App.Players.SessionData.Global
{
    public class NetGlobalSessionDataRegistrator : NetSessionDataRegistrator<NetGlobalSessionData>
    {
        public override int Priority => 0;
    }
}

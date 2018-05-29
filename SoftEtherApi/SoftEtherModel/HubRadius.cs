namespace SoftEtherApi.SoftEtherModel
{
    public class HubRadius : BaseSoftEtherModel<HubRadius>
    {
        public string HubName;
        public uint  RadiusPort;
        public uint  RadiusRetryInterval;
        public string RadiusSecret;
        public string RadiusServerName;
    }
}
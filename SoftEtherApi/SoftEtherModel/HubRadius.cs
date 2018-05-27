namespace SoftEtherApi.SoftEtherModel
{
    public class HubRadius : BaseSoftEtherModel<HubRadius>
    {
        public string HubName;
        public int RadiusPort;
        public int RadiusRetryInterval;
        public string RadiusSecret;
        public string RadiusServerName;
    }
}
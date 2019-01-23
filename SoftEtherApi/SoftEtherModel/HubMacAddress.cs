using System;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubMacAddress : BaseSoftEtherModel<HubMacAddress>
    {
        public DateTime CreatedTime;
        public string HubName;
        public uint Key;
        public byte[] MacAddress;
        public string RemoteHostname;
        public uint RemoteItem;
        public string SessionName;
        public DateTime UpdatedTime;
        public uint VlanId;
    }
}
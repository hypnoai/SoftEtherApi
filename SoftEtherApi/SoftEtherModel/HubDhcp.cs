using System;
using System.Net;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubDhcp : BaseSoftEtherModel<HubDhcp>
    {
        public DateTime ExpireTime;
        public string HubName;
        public uint Id;
        public IPAddress IpAddress;
        public byte[] IpAddress_ipv6_array;
        public bool IpAddress_ipv6_bool;
        public uint IpAddress_ipv6_scope_id;
        public DateTime LeasedTime;
        public byte[] MacAddress;
        public IPAddress Mask;
        public uint NumItem;
        public string Hostname;
    }
}
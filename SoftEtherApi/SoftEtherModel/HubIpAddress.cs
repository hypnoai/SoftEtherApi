using System;
using System.Net;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubIpAddress : BaseSoftEtherModel<HubIpAddress>
    {
        public DateTime CreatedTime;
        public bool DhcpAllocated;
        public string HubName;
        public IPAddress Ip;
        public byte[] Ip_ipv6_array;
        public bool Ip_ipv6_bool;
        public uint Ip_ipv6_scope_id;
        public IPAddress IpV6;
        public byte[] IpV6_ipv6_array;
        public bool IpV6_ipv6_bool;
        public uint IpV6_ipv6_scope_id;
        public uint Key;
        public string RemoteHostname;
        public uint RemoteItem;
        public string SessionName;
        public DateTime UpdatedTime;
    }
}
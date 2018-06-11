using System;
using System.Net;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class ConnectionList : BaseSoftEtherModel<ConnectionList>
    {
        public DateTime ConnectedTime;
        public string Hostname;
        public IPAddress  Ip;
        public byte[] Ip_ipv6_array;
        public uint  Ip_ipv6_bool;
        public uint  Ip_ipv6_scope_id;
        public string Name;
        public uint  Port;
        public uint  Type;
    }
}
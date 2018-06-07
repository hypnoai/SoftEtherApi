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
        public byte[] Ipipv6_array;
        public uint  Ipipv6_bool;
        public uint  Ipipv6_scope_id;
        public string Name;
        public uint  Port;
        public uint  Type;
    }
}
using System;
using System.Net;

namespace SoftEtherApi.SoftEtherModel
{
    public class ConnectionInfo : BaseSoftEtherModel<ConnectionInfo>
    {
        public uint  ClientBuild;
        public string ClientStr;
        public uint  ClientVer;
        public DateTime ConnectedTime;
        public string Hostname;
        public IPAddress  Ip;
        public byte[] Ipipv6_array;
        public uint  Ipipv6_bool;
        public uint  Ipipv6_scope_id;
        public string Name;
        public uint  Port;
        public uint  ServerBuild;
        public string ServerStr;
        public uint  ServerVer;
        public uint  Type;
    }
}
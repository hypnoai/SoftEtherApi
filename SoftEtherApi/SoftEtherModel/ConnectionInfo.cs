using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class ConnectionInfo : BaseSoftEtherModel<ConnectionInfo>
    {
        public int ClientBuild;
        public string ClientStr;
        public int ClientVer;
        public DateTime ConnectedTime;
        public string Hostname;
        public int Ip;
        public byte[] Ipipv6_array;
        public int Ipipv6_bool;
        public int Ipipv6_scope_id;
        public string Name;
        public int Port;
        public int ServerBuild;
        public string ServerStr;
        public int ServerVer;
        public int Type;
    }
}


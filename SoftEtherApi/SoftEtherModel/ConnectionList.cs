using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class ConnectionList : BaseSoftEtherModel<ConnectionList>
    {
        public DateTime ConnectedTime;
        public string Hostname;
        public int Ip;
        public byte[] Ipipv6_array;
        public int Ipipv6_bool;
        public int Ipipv6_scope_id;
        public string Name;
        public int Port;
        public int Type;
    }
}
using System;
using System.Collections.Generic;

namespace SoftEtherApi.SoftEtherModel
{
    public class ConnectionList : BaseSoftEtherModel<ConnectionList>
    {
        public List<DateTime> ConnectedTime;
        public List<string> Hostname;
        public List<int> Ip;
        public List<byte[]> Ipipv6_array;
        public List<int> Ipipv6_bool;
        public List<int> Ipipv6_scope_id;
        public List<string> Name;
        public List<int> Port;
        public List<int> Type;
    }
}


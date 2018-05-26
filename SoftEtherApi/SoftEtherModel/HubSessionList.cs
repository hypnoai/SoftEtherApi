using System.Collections.Generic;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubSessionList : BaseSoftEtherModel<HubSessionList>
    {
        public List<int> BridgeMode;
        public List<int> Client_BridgeMode;
        public List<int> Client_MonitorMode;
        public List<int> CurrentNumTcp;
        public List<string> Hostname;
        public string HubName;
        public List<int> Ip;
        public List<byte[]> Ipipv6_array;
        public List<int> Ipipv6_bool;
        public List<int> Ipipv6_scope_id;
        public List<int> IsDormant;
        public List<int> IsDormantEnabled;
        public List<int> Layer3Mode;
        public List<int> LinkMode;
        public List<int> MaxNumTcp;
        public List<string> Name;
        public List<long> PacketNum;
        public List<long> PacketSize;
        public List<string> RemoteHostname;
        public List<int> RemoteSession;
        public List<int> SecureNATMode;
        public List<byte[]> UniqueId;
        public List<string> Username;
        public List<int> VLanId;
        public List<long> LastCommDormant;
    }
}


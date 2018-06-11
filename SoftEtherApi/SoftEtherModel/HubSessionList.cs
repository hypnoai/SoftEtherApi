using System.Net;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubSessionList : BaseSoftEtherModel<HubSessionList>
    {
        public bool  BridgeMode;
        public bool  Client_BridgeMode;
        public bool  Client_MonitorMode;
        public uint  CurrentNumTcp;
        public string Hostname;
        public string HubName;
        public IPAddress  Ip;
        public byte[] Ip_ipv6_array;
        public uint  Ip_ipv6_bool;
        public uint  Ip_ipv6_scope_id;
        public bool  IsDormant;
        public bool  IsDormantEnabled;
        public ulong LastCommDormant;
        public bool  Layer3Mode;
        public bool  LinkMode;
        public uint  MaxNumTcp;
        public string Name;
        public ulong PacketNum;
        public ulong PacketSize;
        public string RemoteHostname;
        public bool  RemoteSession;
        public bool  SecureNATMode;
        public byte[] UniqueId;
        public string Username;
        public uint  VLanId;
    }
}
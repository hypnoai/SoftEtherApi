namespace SoftEtherApi.SoftEtherModel
{
    public class HubSessionList : BaseSoftEtherModel<HubSessionList>
    {
        public uint  BridgeMode;
        public uint  Client_BridgeMode;
        public uint  Client_MonitorMode;
        public uint  CurrentNumTcp;
        public string Hostname;
        public string HubName;
        public uint  Ip;
        public byte[] Ipipv6_array;
        public uint  Ipipv6_bool;
        public uint  Ipipv6_scope_id;
        public uint  IsDormant;
        public uint  IsDormantEnabled;
        public ulong LastCommDormant;
        public uint  Layer3Mode;
        public uint  LinkMode;
        public uint  MaxNumTcp;
        public string Name;
        public ulong PacketNum;
        public ulong PacketSize;
        public string RemoteHostname;
        public uint  RemoteSession;
        public uint  SecureNATMode;
        public byte[] UniqueId;
        public string Username;
        public uint  VLanId;
    }
}
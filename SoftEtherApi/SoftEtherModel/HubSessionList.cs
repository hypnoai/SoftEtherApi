namespace SoftEtherApi.SoftEtherModel
{
    public class HubSessionList : BaseSoftEtherModel<HubSessionList>
    {
        public int BridgeMode;
        public int Client_BridgeMode;
        public int Client_MonitorMode;
        public int CurrentNumTcp;
        public string Hostname;
        public string HubName;
        public int Ip;
        public byte[] Ipipv6_array;
        public int Ipipv6_bool;
        public int Ipipv6_scope_id;
        public int IsDormant;
        public int IsDormantEnabled;
        public long LastCommDormant;
        public int Layer3Mode;
        public int LinkMode;
        public int MaxNumTcp;
        public string Name;
        public long PacketNum;
        public long PacketSize;
        public string RemoteHostname;
        public int RemoteSession;
        public int SecureNATMode;
        public byte[] UniqueId;
        public string Username;
        public int VLanId;
    }
}
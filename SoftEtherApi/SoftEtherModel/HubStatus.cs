using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubStatus : BaseSoftEtherModel<HubStatus>
    {
        public DateTime CreatedTime;
        public string HubName;
        public uint  HubType;
        public DateTime LastCommTime;
        public DateTime LastLoginTime;
        public uint  NumAccessLists;
        public uint  NumGroups;
        public uint  NumIpTables;
        public uint  NumLogin;
        public uint  NumMacTables;
        public uint  NumSessions;
        public uint  NumSessionsBridge;
        public uint  NumSessionsClient;
        public uint  NumUsers;
        public bool Online;
        public ulong RecvBroadcastBytes;
        public ulong RecvBroadcastCount;
        public ulong RecvUnicastBytes;
        public ulong RecvUnicastCount;
        public bool SecureNATEnabled;
        public ulong SendBroadcastBytes;
        public ulong SendBroadcastCount;
        public ulong SendUnicastBytes;
        public ulong SendUnicastCount;
    }
}
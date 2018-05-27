using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubStatus : BaseSoftEtherModel<HubStatus>
    {
        public DateTime CreatedTime;
        public string HubName;
        public int HubType;
        public DateTime LastCommTime;
        public DateTime LastLoginTime;
        public int NumAccessLists;
        public int NumGroups;
        public int NumIpTables;
        public int NumLogin;
        public int NumMacTables;
        public int NumSessions;
        public int NumSessionsBridge;
        public int NumSessionsClient;
        public int NumUsers;
        public bool Online;
        public long RecvBroadcastBytes;
        public long RecvBroadcastCount;
        public long RecvUnicastBytes;
        public long RecvUnicastCount;
        public bool SecureNATEnabled;
        public long SendBroadcastBytes;
        public long SendBroadcastCount;
        public long SendUnicastBytes;
        public long SendUnicastCount;
    }
}
using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubList : BaseSoftEtherModel<HubList>
    {
        public DateTime CreatedTime;
        public long ExRecvBroadcastBytes;
        public long ExRecvBroadcastCount;
        public long ExRecvUnicastBytes;
        public long ExRecvUnicastCount;
        public long ExSendBroadcastBytes;
        public long ExSendBroadcastCount;
        public long ExSendUnicastBytes;
        public long ExSendUnicastCount;
        public string HubName;
        public int HubType;
        public int IsTrafficFilled;
        public DateTime LastCommTime;
        public DateTime LastLoginTime;
        public int NumGroups;
        public int NumIpTables;
        public int NumLogin;
        public int NumMacTables;
        public int NumSessions;
        public int NumUsers;
        public int Online;
    }
}
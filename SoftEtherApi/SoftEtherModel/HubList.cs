using System;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubList : BaseSoftEtherModel<HubList>
    {
        public DateTime CreatedTime;
        public ulong ExRecvBroadcastBytes;
        public ulong ExRecvBroadcastCount;
        public ulong ExRecvUnicastBytes;
        public ulong ExRecvUnicastCount;
        public ulong ExSendBroadcastBytes;
        public ulong ExSendBroadcastCount;
        public ulong ExSendUnicastBytes;
        public ulong ExSendUnicastCount;
        public string HubName;
        public HubType  HubType;
        public uint  IsTrafficFilled;
        public DateTime LastCommTime;
        public DateTime LastLoginTime;
        public uint  NumGroups;
        public uint  NumIpTables;
        public uint  NumLogin;
        public uint  NumMacTables;
        public uint  NumSessions;
        public uint  NumUsers;
        public uint  Online;
    }
}
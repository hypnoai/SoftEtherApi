using System;
using System.Collections.Generic;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubList : BaseSoftEtherModel<HubList>
    {
        public List<DateTime> CreatedTime;
        public List<long> ExRecvBroadcastBytes;
        public List<long> ExRecvBroadcastCount;
        public List<long> ExRecvUnicastBytes;
        public List<long> ExRecvUnicastCount;
        public List<long> ExSendBroadcastBytes;
        public List<long> ExSendBroadcastCount;
        public List<long> ExSendUnicastBytes;
        public List<long> ExSendUnicastCount;
        public List<string> HubName;
        public List<int> HubType;
        public List<int> IsTrafficFilled;
        public List<DateTime> LastCommTime;
        public List<DateTime> LastLoginTime;
        public List<int> NumGroups;
        public List<int> NumIpTables;
        public List<int> NumLogin;
        public List<int> NumMacTables;
        public List<int> NumSessions;
        public List<int> NumUsers;
        public List<int> Online;
    }
}


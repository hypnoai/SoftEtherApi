using System;
using System.Collections.Generic;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubUserList : BaseSoftEtherModel<HubUserList>
    {
        public List<int> AuthType;
        public List<int> DenyAccess;
        public List<long> ExRecvBroadcastBytes;
        public List<long> ExRecvBroadcastCount;
        public List<long> ExRecvUnicastBytes;
        public List<long> ExRecvUnicastCount;
        public List<long> ExSendBroadcastBytes;
        public List<long> ExSendBroadcastCount;
        public List<long> ExSendUnicastBytes;
        public List<long> ExSendUnicastCount;
        public List<long> Expires;
        public List<string> GroupName;
        public string HubName;
        public List<int> IsExpiresFilled;
        public List<int> IsTrafficFilled;
        public List<DateTime> LastLoginTime;
        public List<string> Name;
        public List<string> Note;
        public List<int> NumLogin;
        public List<string> Realname;
    }
}


using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubUserList : BaseSoftEtherModel<HubUserList>
    {
        public int AuthType;
        public int DenyAccess;
        public long Expires;
        public long ExRecvBroadcastBytes;
        public long ExRecvBroadcastCount;
        public long ExRecvUnicastBytes;
        public long ExRecvUnicastCount;
        public long ExSendBroadcastBytes;
        public long ExSendBroadcastCount;
        public long ExSendUnicastBytes;
        public long ExSendUnicastCount;
        public string GroupName;
        public string HubName;
        public int IsExpiresFilled;
        public int IsTrafficFilled;
        public DateTime LastLoginTime;
        public string Name;
        public string Note;
        public int NumLogin;
        public string Realname;
    }
}
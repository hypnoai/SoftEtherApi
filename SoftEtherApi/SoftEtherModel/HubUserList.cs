using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubUserList : BaseSoftEtherModel<HubUserList>
    {
        public uint  AuthType;
        public uint  DenyAccess;
        public ulong Expires;
        public ulong ExRecvBroadcastBytes;
        public ulong ExRecvBroadcastCount;
        public ulong ExRecvUnicastBytes;
        public ulong ExRecvUnicastCount;
        public ulong ExSendBroadcastBytes;
        public ulong ExSendBroadcastCount;
        public ulong ExSendUnicastBytes;
        public ulong ExSendUnicastCount;
        public string GroupName;
        public string HubName;
        public uint  IsExpiresFilled;
        public uint  IsTrafficFilled;
        public DateTime LastLoginTime;
        public string Name;
        public string Note;
        public uint  NumLogin;
        public string Realname;
    }
}
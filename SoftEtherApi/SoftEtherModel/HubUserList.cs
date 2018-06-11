using System;
using SoftEtherApi.Infrastructure;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubUserList : BaseSoftEtherModel<HubUserList>
    {
        public AuthType  AuthType;
        public bool  DenyAccess;
        public DateTime Expires;
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
        public bool  IsExpiresFilled;
        public bool  IsTrafficFilled;
        public DateTime LastLoginTime;
        public string Name;
        public string Note;
        public uint  NumLogin;
        public string Realname;

        //Fix as IsExpiresFilled is always true
        public bool HasExpires => Expires != SoftEtherConverter.LocalEpoch;
    }
}
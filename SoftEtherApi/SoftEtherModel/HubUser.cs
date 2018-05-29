using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubUser : BaseSoftEtherModel<HubUser>
    {
        public uint  AuthType;
        public DateTime CreatedTime;
        public DateTime ExpireTime;
        public string GroupName;
        public byte[] HashedKey;
        public string HubName;
        public string Name;
        public string Note;
        public byte[] NtLmSecureHash;
        public uint  NumLogin;
        public string Realname;
        public ulong RecvBroadcastBytes;
        public ulong RecvBroadcastCount;
        public ulong RecvUnicastBytes;
        public ulong RecvUnicastCount;
        public ulong SendBroadcastBytes;
        public ulong SendBroadcastCount;
        public ulong SendUnicastBytes;
        public ulong SendUnicastCount;
        public DateTime UpdatedTime;
    }
}
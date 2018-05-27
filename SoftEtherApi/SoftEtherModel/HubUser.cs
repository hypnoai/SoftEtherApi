using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubUser : BaseSoftEtherModel<HubUser>
    {
        public int AuthType;
        public DateTime CreatedTime;
        public DateTime ExpireTime;
        public string GroupName;
        public byte[] HashedKey;
        public string HubName;
        public string Name;
        public string Note;
        public byte[] NtLmSecureHash;
        public int NumLogin;
        public string Realname;
        public long RecvBroadcastBytes;
        public long RecvBroadcastCount;
        public long RecvUnicastBytes;
        public long RecvUnicastCount;
        public long SendBroadcastBytes;
        public long SendBroadcastCount;
        public long SendUnicastBytes;
        public long SendUnicastCount;
        public DateTime UpdatedTime;
    }
}
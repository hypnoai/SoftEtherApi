using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubGroup : BaseSoftEtherModel<HubGroup>
    {
        public string HubName;
        public string Name;
        public string Note;
        public string Realname;
        public ulong RecvBroadcastBytes;
        public ulong RecvBroadcastCount;
        public ulong RecvUnicastBytes;
        public ulong RecvUnicastCount;
        public ulong SendBroadcastBytes;
        public ulong SendBroadcastCount;
        public ulong SendUnicastBytes;
        public ulong SendUnicastCount;
    }
}
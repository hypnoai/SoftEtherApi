namespace SoftEtherApi.SoftEtherModel
{
    public class HubGroup : BaseSoftEtherModel<HubGroup>
    {
        public string HubName;
        public string Name;
        public string Note;
        public string Realname;
        public long RecvBroadcastBytes;
        public long RecvBroadcastCount;
        public long RecvUnicastBytes;
        public long RecvUnicastCount;
        public long SendBroadcastBytes;
        public long SendBroadcastCount;
        public long SendUnicastBytes;
        public long SendUnicastCount;
    }
}


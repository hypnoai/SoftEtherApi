using System.Collections.Generic;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubLog : BaseSoftEtherModel<HubLog>
    {
        public string HubName;
        public List<int> PacketLogConfig;
        public int PacketLogSwitchType;
        public int SavePacketLog;
        public int SaveSecurityLog;
        public int SecurityLogSwitchType;
    }
}


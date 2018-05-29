using System.Collections.Generic;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubLog : BaseSoftEtherModel<HubLog>
    {
        public string HubName;
        public List<int> PacketLogConfig;
        public uint  PacketLogSwitchType;
        public uint  SavePacketLog;
        public uint  SaveSecurityLog;
        public uint  SecurityLogSwitchType;
    }
}
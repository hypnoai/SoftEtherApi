using System.Collections.Generic;
using SoftEtherApi.Model;

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
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class SecureNatStatus : BaseSoftEtherModel<SecureNatStatus>
    {
        public string HubName;
        public bool IsKernelMode;
        public uint NumDhcpClients;
        public uint NumDnsSessions;
        public uint NumIcmpSessions;
        public uint NumTcpSessions;
        public uint NumUdpSessions;
        public bool IsRawIpMode;
    }
}
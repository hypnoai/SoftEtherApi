using System.Net;

namespace SoftEtherApi.SoftEtherModel
{
    public class VirtualHostOptions : BaseSoftEtherModel<VirtualHostOptions>
    {
        public uint ApplyDhcpPushRoutes;
        public IPAddress DhcpDnsServerAddress;
        public IPAddress DhcpDnsServerAddress2;
        public byte[] DhcpDnsServerAddress2ipv6_array;
        public bool DhcpDnsServerAddress2ipv6_bool;
        public uint DhcpDnsServerAddress2ipv6_scope_id;
        public byte[] DhcpDnsServerAddressipv6_array;
        public bool DhcpDnsServerAddressipv6_bool;
        public uint DhcpDnsServerAddressipv6_scope_id;
        public string DhcpDomainName;
        public uint DhcpExpireTimeSpan;
        public IPAddress DhcpGatewayAddress;
        public byte[] DhcpGatewayAddressipv6_array;
        public bool DhcpGatewayAddressipv6_bool;
        public uint DhcpGatewayAddressipv6_scope_id;
        public IPAddress DhcpLeaseIPEnd;
        public byte[] DhcpLeaseIPEndipv6_array;
        public bool DhcpLeaseIPEndipv6_bool;
        public uint DhcpLeaseIPEndipv6_scope_id;
        public IPAddress DhcpLeaseIPStart;
        public byte[] DhcpLeaseIPStartipv6_array;
        public bool DhcpLeaseIPStartipv6_bool;
        public uint DhcpLeaseIPStartipv6_scope_id;
        public IPAddress DhcpSubnetMask;
        public byte[] DhcpSubnetMaskipv6_array;
        public bool DhcpSubnetMaskipv6_bool;
        public uint DhcpSubnetMaskipv6_scope_id;
        public IPAddress Ip;
        public byte[] Ipipv6_array;
        public bool Ipipv6_bool;
        public uint Ipipv6_scope_id;
        public byte[] MacAddress;
        public IPAddress Mask;
        public byte[] Maskipv6_array;
        public bool Maskipv6_bool;
        public uint Maskipv6_scope_id;
        public uint Mtu;
        public uint NatTcpTimeout;
        public uint NatUdpTimeout;
        public string RpcHubName;
        public bool SaveLog;
        public bool UseDhcp;
        public bool UseNat;
        public string DhcpPushRoutes;
    }
}


using System.Net;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class VirtualHostOptions : BaseSoftEtherModel<VirtualHostOptions>
    {
        public uint ApplyDhcpPushRoutes;
        public IPAddress DhcpDnsServerAddress;
        public IPAddress DhcpDnsServerAddress2;
        public byte[] DhcpDnsServerAddress2_ipv6_array;
        public bool DhcpDnsServerAddress2_ipv6_bool;
        public uint DhcpDnsServerAddress2_ipv6_scope_id;
        public byte[] DhcpDnsServerAddress_ipv6_array;
        public bool DhcpDnsServerAddress_ipv6_bool;
        public uint DhcpDnsServerAddress_ipv6_scope_id;
        public string DhcpDomainName;
        public uint DhcpExpireTimeSpan;
        public IPAddress DhcpGatewayAddress;
        public byte[] DhcpGatewayAddress_ipv6_array;
        public bool DhcpGatewayAddress_ipv6_bool;
        public uint DhcpGatewayAddress_ipv6_scope_id;
        public IPAddress DhcpLeaseIPEnd;
        public byte[] DhcpLeaseIPEnd_ipv6_array;
        public bool DhcpLeaseIPEnd_ipv6_bool;
        public uint DhcpLeaseIPEnd_ipv6_scope_id;
        public IPAddress DhcpLeaseIPStart;
        public byte[] DhcpLeaseIPStart_ipv6_array;
        public bool DhcpLeaseIPStart_ipv6_bool;
        public uint DhcpLeaseIPStart_ipv6_scope_id;
        public IPAddress DhcpSubnetMask;
        public byte[] DhcpSubnetMask_ipv6_array;
        public bool DhcpSubnetMask_ipv6_bool;
        public uint DhcpSubnetMask_ipv6_scope_id;
        public IPAddress Ip;
        public byte[] Ip_ipv6_array;
        public bool Ip_ipv6_bool;
        public uint Ip_ipv6_scope_id;
        public byte[] MacAddress;
        public IPAddress Mask;
        public byte[] Mask_ipv6_array;
        public bool Mask_ipv6_bool;
        public uint Mask_ipv6_scope_id;
        public uint Mtu;
        public uint NatTcpTimeout;
        public uint NatUdpTimeout;
        public string RpcHubName;
        public bool SaveLog;
        public bool UseDhcp;
        public bool UseNat;
        public DhcpRouteCollection DhcpPushRoutes;
    }
}


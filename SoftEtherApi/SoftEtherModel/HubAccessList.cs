using System.Net;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubAccessList : BaseSoftEtherModel<HubAccessList>
    {
        public uint  Active;
        public uint  CheckDstMac;
        public uint  CheckSrcMac;
        public uint  CheckTcpState;
        public uint  Delay;
        public IPAddress  DestIpAddress;
        public byte[] DestIpAddress6;
        public byte[] DestIpAddressipv6_array;
        public uint  DestIpAddressipv6_bool;
        public uint  DestIpAddressipv6_scope_id;
        public uint  DestPortEnd;
        public uint  DestPortStart;
        public IPAddress  DestSubnetMask;
        public byte[] DestSubnetMask6;
        public byte[] DestSubnetMaskipv6_array;
        public uint  DestSubnetMaskipv6_bool;
        public uint  DestSubnetMaskipv6_scope_id;
        public string DestUsername;
        public uint  Discard;
        public byte[] DstMacAddress;
        public byte[] DstMacMask;
        public uint  Established;
        public string HubName;
        public uint  Id;
        public uint  IsIPv6;
        public uint  Jitter;
        public uint  Loss;
        public string Note;
        public uint  Priority;
        public uint  Protocol;
        public string RedirectUrl;
        public IPAddress  SrcIpAddress;
        public byte[] SrcIpAddress6;
        public byte[] SrcIpAddressipv6_array;
        public uint  SrcIpAddressipv6_bool;
        public uint  SrcIpAddressipv6_scope_id;
        public byte[] SrcMacAddress;
        public byte[] SrcMacMask;
        public uint  SrcPortEnd;
        public uint  SrcPortStart;
        public IPAddress  SrcSubnetMask;
        public byte[] SrcSubnetMask6;
        public byte[] SrcSubnetMaskipv6_array;
        public uint  SrcSubnetMaskipv6_bool;
        public uint  SrcSubnetMaskipv6_scope_id;
        public string SrcUsername;
        public uint  UniqueId;
    }
}
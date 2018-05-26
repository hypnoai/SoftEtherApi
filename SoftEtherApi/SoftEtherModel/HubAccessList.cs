using System.Collections.Generic;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubAccessList : BaseSoftEtherModel<HubAccessList>
    {
        public List<int> Active;
        public List<int> CheckDstMac;
        public List<int> CheckSrcMac;
        public List<int> CheckTcpState;
        public List<int> Delay;
        public List<int> DestIpAddress;
        public List<byte[]> DestIpAddress6;
        public List<byte[]> DestIpAddressipv6_array;
        public List<int> DestIpAddressipv6_bool;
        public List<int> DestIpAddressipv6_scope_id;
        public List<int> DestPortEnd;
        public List<int> DestPortStart;
        public List<int> DestSubnetMask;
        public List<byte[]> DestSubnetMask6;
        public List<byte[]> DestSubnetMaskipv6_array;
        public List<int> DestSubnetMaskipv6_bool;
        public List<int> DestSubnetMaskipv6_scope_id;
        public List<string> DestUsername;
        public List<int> Discard;
        public List<byte[]> DstMacAddress;
        public List<byte[]> DstMacMask;
        public List<int> Established;
        public string HubName;
        public List<int> Id;
        public List<int> IsIPv6;
        public List<int> Jitter;
        public List<int> Loss;
        public List<string> Note;
        public List<int> Priority;
        public List<int> Protocol;
        public List<string> RedirectUrl;
        public List<int> SrcIpAddress;
        public List<byte[]> SrcIpAddress6;
        public List<byte[]> SrcIpAddressipv6_array;
        public List<int> SrcIpAddressipv6_bool;
        public List<int> SrcIpAddressipv6_scope_id;
        public List<byte[]> SrcMacAddress;
        public List<byte[]> SrcMacMask;
        public List<int> SrcPortEnd;
        public List<int> SrcPortStart;
        public List<int> SrcSubnetMask;
        public List<byte[]> SrcSubnetMask6;
        public List<byte[]> SrcSubnetMaskipv6_array;
        public List<int> SrcSubnetMaskipv6_bool;
        public List<int> SrcSubnetMaskipv6_scope_id;
        public List<string> SrcUsername;
        public List<int> UniqueId;
    }
}


namespace SoftEtherApi.SoftEtherModel
{
    public class HubAccessList : BaseSoftEtherModel<HubAccessList>
    {
        public int Active;
        public int CheckDstMac;
        public int CheckSrcMac;
        public int CheckTcpState;
        public int Delay;
        public int DestIpAddress;
        public byte[] DestIpAddress6;
        public byte[] DestIpAddressipv6_array;
        public int DestIpAddressipv6_bool;
        public int DestIpAddressipv6_scope_id;
        public int DestPortEnd;
        public int DestPortStart;
        public int DestSubnetMask;
        public byte[] DestSubnetMask6;
        public byte[] DestSubnetMaskipv6_array;
        public int DestSubnetMaskipv6_bool;
        public int DestSubnetMaskipv6_scope_id;
        public string DestUsername;
        public int Discard;
        public byte[] DstMacAddress;
        public byte[] DstMacMask;
        public int Established;
        public string HubName;
        public int Id;
        public int IsIPv6;
        public int Jitter;
        public int Loss;
        public string Note;
        public int Priority;
        public int Protocol;
        public string RedirectUrl;
        public int SrcIpAddress;
        public byte[] SrcIpAddress6;
        public byte[] SrcIpAddressipv6_array;
        public int SrcIpAddressipv6_bool;
        public int SrcIpAddressipv6_scope_id;
        public byte[] SrcMacAddress;
        public byte[] SrcMacMask;
        public int SrcPortEnd;
        public int SrcPortStart;
        public int SrcSubnetMask;
        public byte[] SrcSubnetMask6;
        public byte[] SrcSubnetMaskipv6_array;
        public int SrcSubnetMaskipv6_bool;
        public int SrcSubnetMaskipv6_scope_id;
        public string SrcUsername;
        public int UniqueId;
    }
}
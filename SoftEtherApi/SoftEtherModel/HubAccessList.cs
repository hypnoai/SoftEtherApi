using System.Net;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubAccessList : BaseSoftEtherModel<HubAccessList>
    {
        public string HubName;
        public uint  Id;
        public uint  UniqueId;
        
        public string Note = "";
        public bool  Active;
        public bool  Discard;
        public uint  Priority;
        public string SrcUsername = "";
        public string DestUsername = "";
        
        public IPAddress  DestIpAddress = IPAddress.Any;
        public IPAddress  DestSubnetMask = IPAddress.Any;
        public IPAddress  SrcIpAddress = IPAddress.Any;
        public IPAddress  SrcSubnetMask = IPAddress.Any;
        public uint  Protocol = 0;
        public uint  SrcPortEnd = 0;
        public uint  SrcPortStart = 0;
        public uint  DestPortEnd = 0;
        public uint  DestPortStart = 0;
        
        public string RedirectUrl = "";
        
        public uint  Jitter;
        public uint  Loss;
        public uint  Delay;
        
        public bool  CheckTcpState = false;
        public bool  Established;
        
        public bool  CheckSrcMac = false;
        public byte[] SrcMacAddress = new byte[6];
        public byte[] SrcMacMask = new byte[6];
        public bool  CheckDstMac = false;
        public byte[] DstMacAddress = new byte[6];
        public byte[] DstMacMask = new byte[6];
        
        public bool  IsIPv6 = false;
        public byte[] DestIpAddress6 = new byte[16];
        public byte[] DestIpAddressipv6_array = new byte[16];
        public bool  DestIpAddressipv6_bool;
        public uint  DestIpAddressipv6_scope_id;
        public byte[] DestSubnetMask6 = new byte[16];
        public byte[] DestSubnetMaskipv6_array = new byte[16];
        public bool  DestSubnetMaskipv6_bool;
        public uint  DestSubnetMaskipv6_scope_id;
        public byte[] SrcIpAddress6 = new byte[16];
        public byte[] SrcIpAddressipv6_array = new byte[16];
        public bool  SrcIpAddressipv6_bool;
        public uint  SrcIpAddressipv6_scope_id;
        public byte[] SrcSubnetMask6 = new byte[16];
        public byte[] SrcSubnetMaskipv6_array = new byte[16];
        public bool  SrcSubnetMaskipv6_bool;
        public uint  SrcSubnetMaskipv6_scope_id;
    }
}
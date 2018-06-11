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
        public byte[] DestIpAddress_ipv6_array = new byte[16];
        public bool  DestIpAddress_ipv6_bool;
        public uint  DestIpAddress_ipv6_scope_id;
        public byte[] DestSubnetMask6 = new byte[16];
        public byte[] DestSubnetMask_ipv6_array = new byte[16];
        public bool  DestSubnetMask_ipv6_bool;
        public uint  DestSubnetMask_ipv6_scope_id;
        public byte[] SrcIpAddress6 = new byte[16];
        public byte[] SrcIpAddress_ipv6_array = new byte[16];
        public bool  SrcIpAddress_ipv6_bool;
        public uint  SrcIpAddress_ipv6_scope_id;
        public byte[] SrcSubnetMask6 = new byte[16];
        public byte[] SrcSubnetMask_ipv6_array = new byte[16];
        public bool  SrcSubnetMask_ipv6_bool;
        public uint  SrcSubnetMask_ipv6_scope_id;
    }
}
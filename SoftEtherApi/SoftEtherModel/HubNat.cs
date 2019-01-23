using System;
using System.Net;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class HubNat : BaseSoftEtherModel<HubNat>
    {
        public DateTime CreatedTime;
        public string DestHost;
        public IPAddress DestIp;
        public byte[] DestIp_ipv6_array;
        public bool DestIp_ipv6_bool;
        public uint DestIp_ipv6_scope_id;
        public uint DestPort;
        public string HubName;
        public uint Id;
        public DateTime LastCommTime;
        public uint NumItem;
        public uint Protocol;
        public ulong RecvSize;
        public ulong SendSize;
        public string SrcHost;
        public IPAddress SrcIp;
        public byte[] SrcIp_ipv6_array;
        public bool SrcIp_ipv6_bool;
        public uint SrcIp_ipv6_scope_id;
        public uint SrcPort;
        public uint TcpStatus;
    }
}
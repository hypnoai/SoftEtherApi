using System.Collections.Generic;
using System.Linq;
using System.Net;
using SoftEtherApi.Extensions;
using SoftEtherApi.Model;
using SoftEtherApi.SoftEtherModel;

namespace SoftEtherApi.Infrastructure
{
    public static class AccessListFactory
    {
        private const uint DhcpPriority = 1000;
        private const uint GatewayNatPriority = 2000;
        private const uint NetworkNatPriority = 3000;
        public const uint DenyDevicesPriority = 4000;
        public const uint AllowDevicesPriority = 5000;
        public const uint CatchAllPriority = 10000;
        
        public static HubAccessList Dhcp(uint priority, string name = "DHCP", bool denyAccess = false)
        {
            var accessList = new HubAccessList
            {
                Active = true,
                Priority = priority,
                DestIpAddress = IPAddress.Broadcast,
                DestSubnetMask = IPAddress.Broadcast,
                Protocol = 17,
                DestPortStart = 67,
                DestPortEnd = 68,
                Discard = denyAccess,
                Note = name
            };
            return accessList;
        }

        public static HubAccessList CatchAll(uint priority, string name = "Catch ALL", bool denyAccess = false)
        {
            var accessList = new HubAccessList
            {
                Active = true,
                Priority = priority,
                Discard = denyAccess,
                Note = name
            };
            return accessList;
        }

        public static IEnumerable<HubAccessList> AccessToDevice(uint priority, string name,
            IPAddress device,
            IPAddress network, IPAddress networkSubnet, bool denyAccess = false)
        {
            return new List<HubAccessList>
            {
                new HubAccessList
                {
                    Active = true,
                    Priority = priority,
                    SrcIpAddress = device,
                    SrcSubnetMask = IPAddress.Broadcast,
                    DestIpAddress = network.GetNetworkAddress(networkSubnet),
                    DestSubnetMask = networkSubnet,
                    Discard = denyAccess,
                    Note = $"AccessFromDevice-{name}"
                },
                new HubAccessList
                {
                    Active = true,
                    Priority = priority,
                    SrcIpAddress = network.GetNetworkAddress(networkSubnet),
                    SrcSubnetMask = networkSubnet,
                    DestIpAddress = device,
                    DestSubnetMask = IPAddress.Broadcast,
                    Discard = denyAccess,
                    Note = $"AccessToDevice-{name}"
                }
            };
        }
        
        public static IEnumerable<HubAccessList> AccessToNetwork(uint priority, string name,
            IPAddress network, IPAddress networkSubnet,
            IPAddress otherNetwork, IPAddress otherNetworkSubnet, bool denyAccess = false)
        {
            return new List<HubAccessList>
            {
                new HubAccessList
                {
                    Active = true,
                    Priority = priority,
                    SrcIpAddress = network.GetNetworkAddress(networkSubnet),
                    SrcSubnetMask = networkSubnet,
                    DestIpAddress = otherNetwork.GetNetworkAddress(otherNetworkSubnet),
                    DestSubnetMask = otherNetworkSubnet,
                    Discard = denyAccess,
                    Note = $"AccessFromNetwork-{name}"
                },
                new HubAccessList
                {
                    Active = true,
                    Priority = priority,
                    SrcIpAddress = otherNetwork.GetNetworkAddress(otherNetworkSubnet),
                    SrcSubnetMask = otherNetworkSubnet,
                    DestIpAddress = network.GetNetworkAddress(networkSubnet),
                    DestSubnetMask = networkSubnet,
                    Discard = denyAccess,
                    Note = $"AccessToNetwork-{name}"
                }
            };
        }

        public static List<HubAccessList> AllowNetworkOnly(
            string network, string networkSubnet,
            IPAddress secureNatGateway, IPAddress secureNatSubnet)
        {
            return AllowNetworkOnly(IPAddress.Parse(network), IPAddress.Parse(networkSubnet), secureNatGateway,
                secureNatSubnet);
        }

        public static List<HubAccessList> AllowNetworkOnly(
            IPAddress network, IPAddress networkSubnet,
            IPAddress secureNatGateway, IPAddress secureNatSubnet)
        {
            var result = new List<HubAccessList>
            {
                Dhcp(DhcpPriority),
                CatchAll(CatchAllPriority, denyAccess: true)
            };
            
            result.AddRange(AccessToDevice(GatewayNatPriority, "NatGateway", secureNatGateway, secureNatGateway, secureNatSubnet));
            result.AddRange(AccessToNetwork(NetworkNatPriority, "NAT-Network", network, networkSubnet, secureNatGateway, secureNatSubnet));

            return result;
        }

        public static List<HubAccessList> AllowDevicesOnly(IPAddress secureNatGateway, IPAddress secureNatSubnet, 
            params AccessDevice[] accessDevices)
        {
            var result = new List<HubAccessList>
            {
                Dhcp(DhcpPriority),
                CatchAll(CatchAllPriority, denyAccess: true)
            };
            
            result.AddRange(AccessToDevice(GatewayNatPriority, "NatGateway", secureNatGateway, secureNatGateway, secureNatSubnet));
            result.AddRange(accessDevices.SelectMany(m => AccessToDevice(AllowDevicesPriority, m.Name, m.Ip, secureNatGateway, secureNatSubnet)));
            
            return result;
        }
    }
}
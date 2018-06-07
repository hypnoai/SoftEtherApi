using System.Collections.Generic;
using System.Net;
using SoftEtherApi.Extensions;
using SoftEtherApi.SoftEtherModel;

namespace SoftEtherApi.Infrastructure
{
    public static class AccessListFactory
    {
        public static HubAccessList CreateAllowDhcp(uint priority, string name = "DHCP")
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
                Discard = false,
                Note = name
            };
            return accessList;
        }

        public static HubAccessList CreateGatewayToSecureNat(uint priority, IPAddress gateway, IPAddress gatewaySubnet,
            string name = "Gateway to NAT")
        {
            var accessList = new HubAccessList
            {
                Active = true,
                Priority = priority,
                SrcIpAddress = gateway,
                SrcSubnetMask = IPAddress.Broadcast,
                DestIpAddress = gateway.GetNetworkAddress(gatewaySubnet),
                DestSubnetMask = gatewaySubnet,
                Discard = false,
                Note = name
            };
            return accessList;
        }

        public static HubAccessList CreateSecureNatToGateway(uint priority, IPAddress gateway, IPAddress gatewaySubnet,
            string name = "NAT to Gateway")
        {
            var accessList = new HubAccessList
            {
                Active = true,
                Priority = priority,
                SrcIpAddress = gateway.GetNetworkAddress(gatewaySubnet),
                SrcSubnetMask = gatewaySubnet,
                DestIpAddress = gateway,
                DestSubnetMask = IPAddress.Broadcast,
                Discard = false,
                Note = name
            };
            return accessList;
        }

        public static HubAccessList CreateSecureNatToNetwork(uint priority,
            IPAddress secureNat, IPAddress secureNatSubnet,
            IPAddress network, IPAddress networkSubnet,
            string name = "NAT to Network")
        {
            var accessList = new HubAccessList
            {
                Active = true,
                Priority = priority,
                SrcIpAddress = secureNat.GetNetworkAddress(secureNatSubnet),
                SrcSubnetMask = secureNatSubnet,
                DestIpAddress = network.GetNetworkAddress(networkSubnet),
                DestSubnetMask = networkSubnet,
                Discard = false,
                Note = name
            };
            return accessList;
        }

        public static HubAccessList CreateNetworkToSecureNat(uint priority,
            IPAddress network, IPAddress networkSubnet,
            IPAddress secureNat, IPAddress secureNatSubnet,
            string name = "Network to NAT")
        {
            var accessList = new HubAccessList
            {
                Active = true,
                Priority = priority,
                SrcIpAddress = network.GetNetworkAddress(networkSubnet),
                SrcSubnetMask = networkSubnet,
                DestIpAddress = secureNat.GetNetworkAddress(secureNatSubnet),
                DestSubnetMask = secureNatSubnet,
                Discard = false,
                Note = name
            };
            return accessList;
        }

        public static HubAccessList CreateBlockAll(uint priority, string name = "Block ALL")
        {
            var accessList = new HubAccessList
            {
                Active = true,
                Priority = priority,
                Discard = true,
                Note = name
            };
            return accessList;
        }

        public static List<HubAccessList> CreateNatOnly(
            IPAddress network, IPAddress networkSubnet,
            IPAddress secureNatGateway, IPAddress secureNatSubnet)
        {
            return new List<HubAccessList>
            {
                CreateAllowDhcp(1000),
                CreateGatewayToSecureNat(2000, secureNatGateway, secureNatSubnet),
                CreateSecureNatToGateway(3000, secureNatGateway, secureNatSubnet),
                CreateSecureNatToNetwork(4000, secureNatGateway, secureNatSubnet, network, networkSubnet),
                CreateNetworkToSecureNat(5000, network, networkSubnet, secureNatGateway, secureNatSubnet),
                CreateBlockAll(6000)
            };
        }
    }
}
using System;
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
        
        private const string AccessFromDevicePrefix = "AccessFromDevice-";
        private const string AccessToDevicePrefix = "AccessToDevice-";
        
        private const string AccessFromNetworkPrefix = "AccessFromNetwork-";
        private const string AccessToNetworkPrefix = "AccessToNetwork-";

        private const string NatGatewayString = "NatGateway";
        private const string NatNetworkString = "NAT-Network";
        
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
                    Note = $"{AccessFromDevicePrefix}{name}"
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
                    Note = $"{AccessToDevicePrefix}{name}"
                }
            };
        }
        
        public static AccessDevice DeviceToAccess(HubAccessList device)
        {
            if(device.Note.StartsWith(AccessFromDevicePrefix))
                return new AccessDevice(device.SrcIpAddress, device.Note.Substring(AccessFromDevicePrefix.Length));
            
            if(device.Note.StartsWith(AccessToDevicePrefix))
                return new AccessDevice(device.DestIpAddress, device.Note.Substring(AccessToDevicePrefix.Length));

            return null;
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
                    Note = $"{AccessFromNetworkPrefix}{name}"
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
                    Note = $"{AccessToNetworkPrefix}{name}"
                }
            };
        }

        public static IEnumerable<HubAccessList> FilterDevicesOnly(IEnumerable<HubAccessList> accessLists)
        {
            return accessLists.Where(m => m.Priority == AllowDevicesPriority || m.Priority == DenyDevicesPriority).ToList();
        }
        
        public static IEnumerable<Tuple<IPAddress, string>> GetDevicesOnly(IEnumerable<HubAccessList> accessLists)
        {
            var filtered = FilterDevicesOnly(accessLists);
            return filtered.Where(m => m.Note.StartsWith(AccessFromDevicePrefix))
                .Select(m => new Tuple<IPAddress, string>(m.SrcIpAddress, m.Note.Substring(AccessFromDevicePrefix.Length))).ToList();
        }

        public static List<HubAccessList> ReplaceDevices(IEnumerable<HubAccessList> accessLists, params AccessDevice[] accessDevices)
        {
            //we need the gatewayAccess rule
            var gatewayAccess = accessLists.Where(m => !string.IsNullOrWhiteSpace(m.Note))
                .Single(m => m.Note.StartsWith(AccessFromDevicePrefix) && m.Note.EndsWith(NatGatewayString));
            
            var newList = accessLists.Except(FilterDevicesOnly(accessLists)).ToList();
            newList.AddRange(accessDevices.SelectMany(m => AccessToDevice(AllowDevicesPriority, m.Name, m.Ip, gatewayAccess.SrcIpAddress, gatewayAccess.DestSubnetMask)));
            return newList;
        }
        
        public static List<HubAccessList> AppendDevices(IEnumerable<HubAccessList> accessLists, params AccessDevice[] accessDevices)
        {
            //we need the gatewayAccess rule
            var gatewayAccess = accessLists.Where(m => !string.IsNullOrWhiteSpace(m.Note))
                .Single(m => m.Note.StartsWith(AccessFromDevicePrefix) && m.Note.EndsWith(NatGatewayString));
            
            var newList = accessLists.ToList();
            newList.AddRange(accessDevices.SelectMany(m => AccessToDevice(AllowDevicesPriority, m.Name, m.Ip, gatewayAccess.SrcIpAddress, gatewayAccess.DestSubnetMask)));
            return newList;
        }

        public static List<HubAccessList> RemoveDevices(IEnumerable<HubAccessList> accessLists, params AccessDevice[] accessDevices)
        {
            var devices = FilterDevicesOnly(accessLists)
                .Select(m => new Tuple<HubAccessList, AccessDevice>(m, DeviceToAccess(m))).ToList();

            var removeDevices = devices
                .Where(m => accessDevices.Any(u => m.Item2.Ip.ToString() == u.Ip.ToString() && m.Item2.Name == u.Name))
                .Select(m => m.Item1)
                .GroupBy(m => m.Note) //Only take one of the same kind (multiple devices could have the same ip and name)
                .Select(m => m.First())
                .ToList();

            var newList = accessLists.Except(removeDevices).ToList();
            return newList;
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
            
            result.AddRange(AccessToDevice(GatewayNatPriority, NatGatewayString, secureNatGateway, secureNatGateway, secureNatSubnet));
            result.AddRange(AccessToNetwork(NetworkNatPriority, NatNetworkString, network, networkSubnet, secureNatGateway, secureNatSubnet));

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
            
            result.AddRange(AccessToDevice(GatewayNatPriority, NatGatewayString, secureNatGateway, secureNatGateway, secureNatSubnet));
            result.AddRange(accessDevices.SelectMany(m => AccessToDevice(AllowDevicesPriority, m.Name, m.Ip, secureNatGateway, secureNatSubnet)));
            
            return result;
        }
    }
}
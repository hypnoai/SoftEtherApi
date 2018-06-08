using System;
using System.Collections.Generic;
using System.Linq;
using SoftEtherApi.Containers;
using SoftEtherApi.Model;
using SoftEtherApi.SoftEtherModel;

namespace SoftEtherApi.Api
{
    public class SoftEtherHub
    {
        private readonly SoftEther _softEther;

        public SoftEtherHub(SoftEther softEther)
        {
            _softEther = softEther;
        }

        public Hub SetOnline(string hubName, bool online)
        {
            var requestData =
                new SoftEtherParameterCollection
                {
                    {"HubName", hubName},
                    {"Online", online}
                };

            var rawData = _softEther.CallMethod("SetHubOnline", requestData);
            return Hub.Deserialize(rawData);
        }

        public Hub Get(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("GetHub", requestData);
            return Hub.Deserialize(rawData);
        }
        
        public SoftEtherResult EnableSecureNat(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("EnableSecureNAT", requestData);
            return SoftEtherResult.Deserialize(rawData);
        }
        
        public SoftEtherResult DisableSecureNat(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("DisableSecureNAT", requestData);
            return SoftEtherResult.Deserialize(rawData);
        }

        public VirtualHostOptions GetSecureNatOptions(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"RpcHubName", hubName}
            };

            var rawData = _softEther.CallMethod("GetSecureNATOption", requestData);
            var model = VirtualHostOptions.Deserialize(rawData);
            model.RpcHubName = hubName; //Fix, as softEther clears the hubname
            return model;
        }

        public VirtualHostOptions SetSecureNatDhcpPushRoutes(string hubName, DhcpRouteCollection routes)
        {
            var options = GetSecureNatOptions(hubName);
            if (!options.Valid())
                return options;

            options.DhcpPushRoutes = routes;
            return SetSecureNatOptions(hubName, options);
        }

        public VirtualHostOptions SetSecureNatOptions(string hubName, VirtualHostOptions options)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"RpcHubName", options.RpcHubName},
                {"MacAddress", options.MacAddress},
                {"Ip", options.Ip},
                {"Mask", options.Mask},
                {"UseNat", options.UseNat},
                {"Mtu", options.Mtu},
                {"NatTcpTimeout", options.NatTcpTimeout},
                {"NatUdpTimeout", options.NatUdpTimeout},
                {"UseDhcp", options.UseDhcp},
                {"DhcpLeaseIPStart", options.DhcpLeaseIPStart},
                {"DhcpLeaseIPEnd", options.DhcpLeaseIPEnd},
                {"DhcpSubnetMask", options.DhcpSubnetMask},
                {"DhcpExpireTimeSpan", options.DhcpExpireTimeSpan},
                {"DhcpGatewayAddress", options.DhcpGatewayAddress},
                {"DhcpDnsServerAddress", options.DhcpDnsServerAddress},
                {"DhcpDnsServerAddress2", options.DhcpDnsServerAddress2},
                {"DhcpDomainName", options.DhcpDomainName},
                {"SaveLog", options.SaveLog},
                {"ApplyDhcpPushRoutes", options.ApplyDhcpPushRoutes},
                {"DhcpPushRoutes", options.DhcpPushRoutes.ToString()}
            };

            var rawData = _softEther.CallMethod("SetSecureNATOption", requestData);
            return VirtualHostOptions.Deserialize(rawData);
        }

        public SoftEtherList<HubList> GetList()
        {
            var rawData = _softEther.CallMethod("EnumHub");
            return HubList.DeserializeMany(rawData);
        }

        public HubRadius GetRadius(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("GetHubRadius", requestData);
            return HubRadius.Deserialize(rawData);
        }

        public HubStatus GetStatus(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("GetHubStatus", requestData);
            return HubStatus.Deserialize(rawData);
        }

        public HubLog GetLog(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("GetHubLog", requestData);
            return HubLog.Deserialize(rawData);
        }

        public SoftEtherList<HubAccessList> GetAccessList(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("EnumAccess", requestData);
            return HubAccessList.DeserializeMany(rawData);
        }

        public List<HubAccessList> AddAccessList(string hubName, IEnumerable<HubAccessList> accessList)
        {
            return accessList.Select(m => AddAccessList(hubName, m)).ToList();
        }
        
        public HubAccessList AddAccessList(string hubName, HubAccessList accessList)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Id", accessList.Id},
                {"Note", SoftEtherValueType.UnicodeString, accessList.Note},
                {"Active", accessList.Active},
                {"Priority", accessList.Priority},
                {"Discard", accessList.Discard},
                {"SrcIpAddress", accessList.SrcIpAddress},
                {"SrcSubnetMask", accessList.SrcSubnetMask},
                {"DestIpAddress", accessList.DestIpAddress},
                {"DestSubnetMask", accessList.DestSubnetMask},
                {"Protocol", accessList.Protocol},
                {"SrcPortStart", accessList.SrcPortStart},
                {"SrcPortEnd", accessList.SrcPortEnd},
                {"DestPortStart", accessList.DestPortStart},
                {"DestPortEnd", accessList.DestPortEnd},
                {"SrcUsername", accessList.SrcUsername},
                {"DestUsername", accessList.DestUsername},
                {"CheckSrcMac", accessList.CheckSrcMac},
                {"SrcMacAddress", accessList.SrcMacAddress},
                {"SrcMacMask", accessList.SrcMacMask},
                {"CheckDstMac", accessList.CheckDstMac},
                {"DstMacAddress", accessList.DstMacAddress},
                {"DstMacMask", accessList.DstMacMask},
                {"CheckTcpState", accessList.CheckTcpState},
                {"Established", accessList.Established},
                {"Delay", accessList.Delay},
                {"Jitter", accessList.Jitter},
                {"Loss", accessList.Loss},
                {"IsIPv6", accessList.IsIPv6},
                {"UniqueId", accessList.UniqueId},
                {"RedirectUrl", accessList.RedirectUrl}
            };

            var rawData = _softEther.CallMethod("AddAccess", requestData);
            return HubAccessList.Deserialize(rawData);
        }

        public SoftEtherList<HubAccessList> SetAccessList(string hubName, IEnumerable<HubAccessList> accessList)
        {
            return SetAccessList(hubName, accessList.ToArray());
        }
        
        public SoftEtherList<HubAccessList> SetAccessList(string hubName, params HubAccessList[] accessList)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Id", accessList.Select(m => m.Id)},
                {"Note", SoftEtherValueType.UnicodeString, accessList.Select(m => m.Note)},
                {"Active", accessList.Select(m => m.Active)},
                {"Priority", accessList.Select(m => m.Priority)},
                {"Discard", accessList.Select(m => m.Discard)},
                {"SrcIpAddress", accessList.Select(m => m.SrcIpAddress)},
                {"SrcSubnetMask", accessList.Select(m => m.SrcSubnetMask)},
                {"DestIpAddress", accessList.Select(m => m.DestIpAddress)},
                {"DestSubnetMask", accessList.Select(m => m.DestSubnetMask)},
                {"Protocol", accessList.Select(m => m.Protocol)},
                {"SrcPortStart", accessList.Select(m => m.SrcPortStart)},
                {"SrcPortEnd", accessList.Select(m => m.SrcPortEnd)},
                {"DestPortStart", accessList.Select(m => m.DestPortStart)},
                {"DestPortEnd", accessList.Select(m => m.DestPortEnd)},
                {"SrcUsername", accessList.Select(m => m.SrcUsername)},
                {"DestUsername", accessList.Select(m => m.DestUsername)},
                {"CheckSrcMac", accessList.Select(m => m.CheckSrcMac)},
                {"SrcMacAddress", accessList.Select(m => m.SrcMacAddress)},
                {"SrcMacMask", accessList.Select(m => m.SrcMacMask)},
                {"CheckDstMac", accessList.Select(m => m.CheckDstMac)},
                {"DstMacAddress", accessList.Select(m => m.DstMacAddress)},
                {"DstMacMask", accessList.Select(m => m.DstMacMask)},
                {"CheckTcpState", accessList.Select(m => m.CheckTcpState)},
                {"Established", accessList.Select(m => m.Established)},
                {"Delay", accessList.Select(m => m.Delay)},
                {"Jitter", accessList.Select(m => m.Jitter)},
                {"Loss", accessList.Select(m => m.Loss)},
                {"IsIPv6", accessList.Select(m => m.IsIPv6)},
                {"UniqueId", accessList.Select(m => m.UniqueId)},
                {"RedirectUrl", accessList.Select(m => m.RedirectUrl)}
            };

            var rawData = _softEther.CallMethod("SetAccessList", requestData);
            return HubAccessList.DeserializeMany(rawData);
        }

        public SoftEtherList<HubSessionList> GetSessionList(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("EnumSession", requestData);
            return HubSessionList.DeserializeMany(rawData);
        }

        public SoftEtherList<HubUserList> GetUserList(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("EnumUser", requestData);
            return HubUserList.DeserializeMany(rawData);
        }

        public HubUser GetUser(string hubName, string name)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Name", name}
            };

            var rawData = _softEther.CallMethod("GetUser", requestData);
            return HubUser.Deserialize(rawData);
        }

        public HubGroup GetGroup(string hubName, string name)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Name", name}
            };

            var rawData = _softEther.CallMethod("GetGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }

        public HubGroup DeleteGroup(string hubName, string name)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Name", name}
            };

            var rawData = _softEther.CallMethod("DeleteGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }

        public Hub Delete(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName}
            };

            var rawData = _softEther.CallMethod("DeleteHub", requestData);
            return Hub.Deserialize(rawData);
        }

        public Hub Create(string name, string password, bool online, bool noAnonymousEnumUser = true,
            HubType hubType = HubType.Standalone, int maxSession = 0)
        {
            var hashPair = _softEther.CreateHashAnSecure(password);

            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", name},
                {"HashedPassword", hashPair.Hash},
                {"SecurePassword",hashPair.Secure},
                {"Online", online},
                {"MaxSession", maxSession},
                {"NoEnum", noAnonymousEnumUser},
                {"HubType", (int)hubType}
            };

            var rawData = _softEther.CallMethod("CreateHub", requestData);
            return Hub.Deserialize(rawData);
        }

        public HubGroup CreateGroup(string hubName, string name, string realName = null, string note = null)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Name", name},
                {"Realname", SoftEtherValueType.UnicodeString, realName},
                {"Note", SoftEtherValueType.UnicodeString, note}
            };

            var rawData = _softEther.CallMethod("CreateGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }

        public HubGroup SetGroup(string hubName, string name, string realName, string note)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Name", name},
                {"Realname", SoftEtherValueType.UnicodeString, realName},
                {"Note", SoftEtherValueType.UnicodeString, note}
            };

            var rawData = _softEther.CallMethod("SetGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }

        public HubGroup SetGroup(string hubName, HubGroup group)
        {
            return SetGroup(hubName, group.Name, group.Realname, group.Note);
        }

        public HubGroup ChangeGroupNote(string hubName, string name, string note)
        {
            var group = GetGroup(hubName, name);
            group.Note = note;
            return SetGroup(hubName, group);
        }

        public HubUser CreateUser(string hubName, string name, string password, string groupName = null,
            string realName = null, string note = null, DateTime? expireTime = null)
        {
            var hashPair = _softEther.CreateHashAndNtLm(name, password);

            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Name", name},
                {"GroupName", groupName},
                {"Realname", SoftEtherValueType.UnicodeString, realName},
                {"Note", SoftEtherValueType.UnicodeString, note},
                {"ExpireTime", expireTime},
                {"AuthType", (int)AuthType.Password},
                {"HashedKey", hashPair.Hash},
                {"NtLmSecureHash", hashPair.Secure}
            };

            var rawData = _softEther.CallMethod("CreateUser", requestData);
            return HubUser.Deserialize(rawData);
        }

        public HubUser SetUser(string hubName, HubUser user)
        {
            return SetUser(hubName, user.Name, user.GroupName, user.Realname, user.Note,
                user.CreatedTime, user.UpdatedTime, user.ExpireTime,
                user.NumLogin, user.AuthType, user.HashedKey, user.NtLmSecureHash);
        }

        public HubUser SetUser(string hubName, string name,
            string groupName,
            string realName, string note,
            DateTime createTime,
            DateTime updatedTime,
            DateTime expireTime,
            uint numLogin,
            AuthType authType,
            byte[] hashedPw,
            byte[] securePw)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Name", name},
                {"GroupName", groupName},
                {"Realname", SoftEtherValueType.UnicodeString, realName},
                {"Note", SoftEtherValueType.UnicodeString, note},
                {"CreatedTime", createTime},
                {"UpdatedTime", updatedTime},
                {"ExpireTime", expireTime},
                {"NumLogin", numLogin},
                {"AuthType", (int)authType},
                {"HashedKey", hashedPw},
                {"NtLmSecureHash", securePw}
            };

            var rawData = _softEther.CallMethod("SetUser", requestData);
            return HubUser.Deserialize(rawData);
        }

        public HubUser DeleteUser(string hubName, string name)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"HubName", hubName},
                {"Name", name}
            };

            var rawData = _softEther.CallMethod("DeleteUser", requestData);
            return HubUser.Deserialize(rawData);
        }

        public HubUser SetUserExpireDate(string hubName, string name, DateTime expireDate)
        {
            var user = GetUser(hubName, name);
            user.ExpireTime = expireDate;
            return SetUser(hubName, user);
        }

        public HubUser SetUserPassword(string hubName, string name, string password)
        {
            var user = GetUser(hubName, name);
            var hashPair = _softEther.CreateHashAndNtLm(name, password);
            
            user.HashedKey = hashPair.Hash;
            user.NtLmSecureHash = hashPair.Secure;
            
            return SetUser(hubName, user);
        }
    }
}
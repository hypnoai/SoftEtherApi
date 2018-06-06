using System;
using SoftEtherApi.Containers;
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

        public VirtualHostOptions GetSecureNatOptions(string hubName)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"RpcHubName", hubName}
            };

            var rawData = _softEther.CallMethod("GetSecureNATOption", requestData);
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
                {"AuthType", 1}, //auth_type = 0 for no auth, 1 for password auth
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
            uint authType,
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
                {"AuthType", authType}, //auth_type = 0 for no auth, 1 for password auth
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
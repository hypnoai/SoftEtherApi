using System;
using System.Collections.Generic;
using System.Text;
using SoftEtherApi.Infrastructure;
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
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})},
                    {"Online", ("int", new object[] {online ? 1 : 0})}
                };

            var rawData = _softEther.CallMethod("SetHubOnline", requestData);
            return Hub.Deserialize(rawData);
        }

        public Hub Get(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, object[])> {{"HubName", ("string", new object[] {hubName})}};

            var rawData = _softEther.CallMethod("GetHub", requestData);
            return Hub.Deserialize(rawData);
        }

        public SoftEtherList<HubList> GetList()
        {
            var rawData = _softEther.CallMethod("EnumHub");
            return HubList.DeserializeMany(rawData);
        }

        public HubRadius GetRadius(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, object[])> {{"HubName", ("string", new object[] {hubName})}};

            var rawData = _softEther.CallMethod("GetHubRadius", requestData);
            return HubRadius.Deserialize(rawData);
        }

        public HubStatus GetStatus(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, object[])> {{"HubName", ("string", new object[] {hubName})}};

            var rawData = _softEther.CallMethod("GetHubStatus", requestData);
            return HubStatus.Deserialize(rawData);
        }

        public HubLog GetLog(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, object[])> {{"HubName", ("string", new object[] {hubName})}};

            var rawData = _softEther.CallMethod("GetHubLog", requestData);
            return HubLog.Deserialize(rawData);
        }

        public SoftEtherList<HubAccessList> GetAccessList(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, object[])> {{"HubName", ("string", new object[] {hubName})}};

            var rawData = _softEther.CallMethod("EnumAccess", requestData);
            return HubAccessList.DeserializeMany(rawData);
        }

        public SoftEtherList<HubSessionList> GetSessionList(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, object[])> {{"HubName", ("string", new object[] {hubName})}};

            var rawData = _softEther.CallMethod("EnumSession", requestData);
            return HubSessionList.DeserializeMany(rawData);
        }

        public SoftEtherList<HubUserList> GetUserList(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, object[])> {{"HubName", ("string", new object[] {hubName})}};

            var rawData = _softEther.CallMethod("EnumUser", requestData);
            return HubUserList.DeserializeMany(rawData);
        }

        public HubUser GetUser(string hubName, string name)
        {
            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})},
                    {"Name", ("string", new object[] {name})}
                };

            var rawData = _softEther.CallMethod("GetUser", requestData);
            return HubUser.Deserialize(rawData);
        }

        public HubGroup GetGroup(string hubName, string name)
        {
            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})},
                    {"Name", ("string", new object[] {name})}
                };

            var rawData = _softEther.CallMethod("GetGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }

        public HubGroup DeleteGroup(string hubName, string name)
        {
            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})},
                    {"Name", ("string", new object[] {name})}
                };

            var rawData = _softEther.CallMethod("DeleteGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }

        public Hub Delete(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})}
                };

            var rawData = _softEther.CallMethod("DeleteHub", requestData);
            return Hub.Deserialize(rawData);
        }
        
        public Hub Create(string name, string password, bool online, bool noAnonymousEnumUser = true, HubType hubType = HubType.Standalone, int maxSession = 0)
        {
            var (hashedPw, securePw) = _softEther.CreateHashAnSecure(password);
            
            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {name})},
                    {"HashedPassword", ("raw", new object[] {hashedPw})},
                    {"SecurePassword", ("raw", new object[] {securePw})},
                    {"Online", ("int", new object[] {online})},
                    {"MaxSession", ("int", new object[] {maxSession})},
                    {"NoEnum", ("int", new object[] {noAnonymousEnumUser})},
                    {"HubType", ("int", new object[] {hubType})}
                };

            var rawData = _softEther.CallMethod("CreateHub", requestData);
            return Hub.Deserialize(rawData);
        }

        public HubGroup CreateGroup(string hubName, string name, string realName = null, string note = null)
        {
            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})},
                    {"Name", ("string", new object[] {name})},
                    {"Realname", ("ustring", new object[] {realName})},
                    {"Note", ("ustring", new object[] {note})}
                };

            var rawData = _softEther.CallMethod("CreateGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }

        public HubGroup SetGroup(string hubName, string name, string realName, string note)
        {
            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})},
                    {"Name", ("string", new object[] {name})},
                    {"Realname", ("ustring", new object[] {realName})},
                    {"Note", ("ustring", new object[] {note})}
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
            var (hashedPw, securePw) = _softEther.CreateHashAndNtLm(name, password);

            var expireTimeValue = expireTime.HasValue ? (long?) SoftEther.DateTimeToLong(expireTime.Value) : null;

            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})},
                    {"Name", ("string", new object[] {name})},
                    {"GroupName", ("string", new object[] {groupName})},
                    {"Realname", ("ustring", new object[] {realName})},
                    {"Note", ("ustring", new object[] {note})},
                    {"ExpireTime", ("int64", new object[] {expireTimeValue})},
                    {"AuthType", ("int", new object[] {1})}, //auth_type = 0 for no auth, 1 for password auth
                    {"HashedKey", ("raw", new object[] {hashedPw})},
                    {"NtLmSecureHash", ("raw", new object[] {securePw})}
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
            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})},
                    {"Name", ("string", new object[] {name})},
                    {"GroupName", ("string", new object[] {groupName})},
                    {"Realname", ("ustring", new object[] {realName})},
                    {"Note", ("ustring", new object[] {note})},
                    {"CreatedTime", ("int64", new object[] {SoftEther.DateTimeToLong(createTime)})},
                    {"UpdatedTime", ("int64", new object[] {SoftEther.DateTimeToLong(updatedTime)})},
                    {"ExpireTime", ("int64", new object[] {SoftEther.DateTimeToLong(expireTime)})},
                    {"NumLogin", ("int", new object[] {numLogin})},
                    {"AuthType", ("int", new object[] {authType})}, //auth_type = 0 for no auth, 1 for password auth
                    {"HashedKey", ("raw", new object[] {hashedPw})},
                    {"NtLmSecureHash", ("raw", new object[] {securePw})}
                };

            var rawData = _softEther.CallMethod("SetUser", requestData);
            return HubUser.Deserialize(rawData);
        }

        public HubUser DeleteUser(string hubName, string name)
        {
            var requestData =
                new Dictionary<string, (string, object[])>
                {
                    {"HubName", ("string", new object[] {hubName})},
                    {"Name", ("string", new object[] {name})}
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
            (user.HashedKey, user.NtLmSecureHash) = _softEther.CreateHashAndNtLm(name, password);
            return SetUser(hubName, user);
        }
    }
}
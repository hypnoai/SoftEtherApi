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
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})},
                    {"Online", ("int", new dynamic[] {online ? 1 : 0})}
                };

            var rawData = _softEther.CallMethod("SetHubOnline", requestData);
            return Hub.Deserialize(rawData);
        }

        public Hub Get(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])> {{"HubName", ("string", new dynamic[] {hubName})}};

            var rawData = _softEther.CallMethod("GetHub", requestData);
            return Hub.Deserialize(rawData);
        }

        public HubList GetList()
        {
            var rawData = _softEther.CallMethod("EnumHub");
            return HubList.Deserialize(rawData);
        }

        public HubRadius GetRadius(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])> {{"HubName", ("string", new dynamic[] {hubName})}};

            var rawData = _softEther.CallMethod("GetHubRadius", requestData);
            return HubRadius.Deserialize(rawData);
        }

        public HubStatus GetStatus(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])> {{"HubName", ("string", new dynamic[] {hubName})}};

            var rawData = _softEther.CallMethod("GetHubStatus", requestData);
            return HubStatus.Deserialize(rawData);
        }

        public HubLog GetLog(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])> {{"HubName", ("string", new dynamic[] {hubName})}};

            var rawData = _softEther.CallMethod("GetHubLog", requestData);
            return HubLog.Deserialize(rawData);
        }

        public HubAccessList GetAccessList(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])> {{"HubName", ("string", new dynamic[] {hubName})}};

            var rawData = _softEther.CallMethod("EnumAccess", requestData);
            return HubAccessList.Deserialize(rawData);
        }
        
        public HubSessionList GetSessionList(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])> {{"HubName", ("string", new dynamic[] {hubName})}};

            var rawData = _softEther.CallMethod("EnumSession", requestData);
            return HubSessionList.Deserialize(rawData);
        }

        public HubUserList GetUserList(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])> {{"HubName", ("string", new dynamic[] {hubName})}};

            var rawData = _softEther.CallMethod("EnumUser", requestData);
            return HubUserList.Deserialize(rawData);
        }

        public HubUser GetUser(string hubName, string name)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})},
                    {"Name", ("string", new dynamic[] {name})}
                };

            var rawData = _softEther.CallMethod("GetUser", requestData);
            return HubUser.Deserialize(rawData);
        }
        
        public HubGroup GetGroup(string hubName, string name)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})},
                    {"Name", ("string", new dynamic[] {name})}
                };

            var rawData = _softEther.CallMethod("GetGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }
        
        public HubGroup DeleteGroup(string hubName, string name)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})},
                    {"Name", ("string", new dynamic[] {name})}
                };

            var rawData = _softEther.CallMethod("DeleteGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }
        
        public Hub Delete(string hubName)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})}
                };

            var rawData = _softEther.CallMethod("DeleteHub", requestData);
            return Hub.Deserialize(rawData);
        }

        public HubGroup CreateGroup(string hubName, string name, string realName = null, string note = null)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})},
                    {"Name", ("string", new dynamic[] {name})},
                    {"Realname", ("ustring", new dynamic[] {realName})},
                    {"Note", ("ustring", new dynamic[] {note})}
                };

            var rawData = _softEther.CallMethod("CreateGroup", requestData);
            return HubGroup.Deserialize(rawData);
        }
        
        public HubGroup SetGroup(string hubName, string name, string realName, string note)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})},
                    {"Name", ("string", new dynamic[] {name})},
                    {"Realname", ("ustring", new dynamic[] {realName})},
                    {"Note", ("ustring", new dynamic[] {note})}
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
        
        public HubUser CreateUser(string hubName, string name, string password, string groupName = null, string realName = null, string note = null, DateTime? expireTime = null)
        {
            var (hashedPw, securePw) = CreateHashAndNtLm(name, password);

            var expireTimeValue = expireTime.HasValue? (long?)SoftEther.DateTimeToLong(expireTime.Value) : null; 
            
            var requestData =
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})},
                    {"Name", ("string", new dynamic[] {name})},
                    {"GroupName", ("string", new dynamic[] {groupName})},
                    {"Realname", ("ustring", new dynamic[] {realName})},
                    {"Note", ("ustring", new dynamic[] {note})},
                    {"ExpireTime", ("int64", new dynamic[] {expireTimeValue})},
                    {"AuthType", ("int", new dynamic[] {1})}, //auth_type = 0 for no auth, 1 for password auth
                    {"HashedKey", ("raw", new dynamic[] {hashedPw})},
                    {"NtLmSecureHash", ("raw", new dynamic[] {securePw})}
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
            int numLogin,
            int authType,
            byte[] hashedPw,
            byte[] securePw)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})},
                    {"Name", ("string", new dynamic[] {name})},
                    {"GroupName", ("string", new dynamic[] {groupName})},
                    {"Realname", ("ustring", new dynamic[] {realName})},
                    {"Note", ("ustring", new dynamic[] {note})},
                    {"CreatedTime", ("int64", new dynamic[] {SoftEther.DateTimeToLong(createTime)})},
                    {"UpdatedTime", ("int64", new dynamic[] {SoftEther.DateTimeToLong(updatedTime)})},
                    {"ExpireTime", ("int64", new dynamic[] {SoftEther.DateTimeToLong(expireTime)})},
                    {"NumLogin", ("int", new dynamic[] {numLogin})},
                    {"AuthType", ("int", new dynamic[] {authType})}, //auth_type = 0 for no auth, 1 for password auth
                    {"HashedKey", ("raw", new dynamic[] {hashedPw})},
                    {"NtLmSecureHash", ("raw", new dynamic[] {securePw})}
                };

            var rawData = _softEther.CallMethod("SetUser", requestData);
            return HubUser.Deserialize(rawData);
        }
        
        public HubUser DeleteUser(string hubName, string name)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])>
                {
                    {"HubName", ("string", new dynamic[] {hubName})},
                    {"Name", ("string", new dynamic[] {name})}
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
            (user.HashedKey, user.NtLmSecureHash) = CreateHashAndNtLm(name, password);
            return SetUser(hubName, user);
        }

        public (byte[], byte[]) CreateHashAndNtLm(string name, string password)
        {
            var hashedPwCreator = new SHA0();
            hashedPwCreator.Update(Encoding.ASCII.GetBytes(password));
            var hashedPw = hashedPwCreator.Update(Encoding.ASCII.GetBytes(name.ToUpper())).Digest();

            var securePwCreator = new MD4();
            var securePw = securePwCreator.Update(Encoding.Unicode.GetBytes(password)).Digest();
            return (hashedPw, securePw);
        }
    }
}
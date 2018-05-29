using System.Collections.Generic;
using SoftEtherApi.SoftEtherModel;

namespace SoftEtherApi.Api
{
    public class SoftEtherServer
    {
        private readonly SoftEther _softEther;

        public SoftEtherServer(SoftEther softEther)
        {
            _softEther = softEther;
        }

        public ServerInfo GetInfo()
        {
            var rawData = _softEther.CallMethod("GetServerInfo");
            return ServerInfo.Deserialize(rawData);
        }

        public ServerStatus GetStatus()
        {
            var rawData = _softEther.CallMethod("GetServerStatus");
            return ServerStatus.Deserialize(rawData);
        }

        public SoftEtherList<PortListenerList> GetPortListenerList()
        {
            var rawData = _softEther.CallMethod("EnumListener");
            return PortListenerList.DeserializeMany(rawData);
        }

        public ServerCert GetCert()
        {
            var rawData = _softEther.CallMethod("GetServerCert");
            return ServerCert.Deserialize(rawData);
        }

        public ServerCipher GetCipher()
        {
            var rawData = _softEther.CallMethod("GetServerCipher");
            return ServerCipher.Deserialize(rawData);
        }

        public SoftEtherList<ConnectionList> GetConnectionList()
        {
            var rawData = _softEther.CallMethod("EnumConnection");
            return ConnectionList.DeserializeMany(rawData);
        }

        public ConnectionInfo GetConnectionInfo(string name)
        {
            var requestData =
                new Dictionary<string, (string, object[])> {{"Name", ("string", new object[] {name})}};

            var rawData = _softEther.CallMethod("GetConnectionInfo", requestData);
            return ConnectionInfo.Deserialize(rawData);
        }
    }
}
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

        public PortListeners GetPortListeners()
        {
            var rawData = _softEther.CallMethod("EnumListener");
            return PortListeners.Deserialize(rawData);
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
        
        public ConnectionList GetConnections()
        {
            var rawData = _softEther.CallMethod("EnumConnection");
            return ConnectionList.Deserialize(rawData);
        }
        
        public ConnectionInfo GetConnectionInfo(string name)
        {
            var requestData =
                new Dictionary<string, (string, dynamic[])> {{"Name", ("string", new dynamic[] {name})}};
            
            var rawData = _softEther.CallMethod("GetConnectionInfo", requestData);
            return ConnectionInfo.Deserialize(rawData);
        }
    }
}
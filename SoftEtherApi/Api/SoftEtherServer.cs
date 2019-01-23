using SoftEtherApi.Containers;
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
        
        public SoftEtherList<ServerEthernetDevice> GetEthernetDeviceList()
        {
            var rawData = _softEther.CallMethod("EnumEthernet");
            return ServerEthernetDevice.DeserializeMany(rawData);
        }
        
        public SoftEtherResult RebootServer()
        {
            var rawData = _softEther.CallMethod("RebootServer");
            return SoftEtherResult.Deserialize(rawData);
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
        
        public L2tpSettings GetL2tpSettings()
        {
            var rawData = _softEther.CallMethod("GetIPsecServices");
            return L2tpSettings.Deserialize(rawData);
        }
        
        public L2tpSettings SetL2tpSettings(L2tpSettings settings)
        {
            var requestData = new SoftEtherParameterCollection
            {
                {"L2TP_Raw", settings.L2TP_Raw},
                {"L2TP_IPsec", settings.L2TP_IPsec},
                {"EtherIP_IPsec", settings.EtherIP_IPsec},
                {"L2TP_DefaultHub", settings.L2TP_DefaultHub},
                {"IPsec_Secret", settings.IPsec_Secret}
            };
            
            var rawData = _softEther.CallMethod("SetIPsecServices", requestData);
            return L2tpSettings.Deserialize(rawData);
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
            var requestData = new SoftEtherParameterCollection
            {
                {"Name", name}
            };

            var rawData = _softEther.CallMethod("GetConnectionInfo", requestData);
            return ConnectionInfo.Deserialize(rawData);
        }
    }
}
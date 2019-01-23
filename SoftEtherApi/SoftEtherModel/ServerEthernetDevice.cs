using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class ServerEthernetDevice : BaseSoftEtherModel<ServerEthernetDevice>
    {
        public string DeviceName;
        public uint NumItem;
        public string NetworkConnectionName;
    }
}
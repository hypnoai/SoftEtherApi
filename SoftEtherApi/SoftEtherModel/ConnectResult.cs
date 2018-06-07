using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class ConnectResult : BaseSoftEtherModel<ConnectResult>
    {
        public uint  build;
        public string hello;
        public byte[] pencore;
        public byte[] random;
        public uint  version;
    }
}
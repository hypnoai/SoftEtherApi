using SoftEtherApi.SoftEtherModel;

namespace SoftetherApi.SoftEtherModel
{
    public class ConnectResult : BaseSoftEtherModel<ConnectResult>
    {
        public int build;
        public string hello;
        public byte[] random;
        public int version;
        public byte[] pencore;
    }
}


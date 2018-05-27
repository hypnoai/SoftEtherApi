namespace SoftEtherApi.SoftEtherModel
{
    public class ConnectResult : BaseSoftEtherModel<ConnectResult>
    {
        public int build;
        public string hello;
        public byte[] pencore;
        public byte[] random;
        public int version;
    }
}
namespace SoftEtherApi.SoftEtherModel
{
    public class Hub : BaseSoftEtherModel<Hub>
    {
        public byte[] HashedPassword;
        public string HubName;
        public uint  HubType;
        public uint  MaxSession;
        public uint  NoEnum;
        public uint  Online;
        public byte[] SecurePassword;
    }
}
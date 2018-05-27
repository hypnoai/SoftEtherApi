namespace SoftEtherApi.SoftEtherModel
{
    public class Hub : BaseSoftEtherModel<Hub>
    {
        public byte[] HashedPassword;
        public string HubName;
        public int HubType;
        public int MaxSession;
        public int NoEnum;
        public int Online;
        public byte[] SecurePassword;
    }
}
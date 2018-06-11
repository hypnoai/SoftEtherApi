using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class Hub : BaseSoftEtherModel<Hub>
    {
        public byte[] HashedPassword;
        public string HubName;
        public HubType  HubType;
        public uint  MaxSession;
        public bool  NoEnum;
        public bool  Online;
        public byte[] SecurePassword;
    }
}
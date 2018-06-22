using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class L2tpSettings : BaseSoftEtherModel<L2tpSettings>
    {
        public bool L2TP_Raw; //L2TP without encryption
        public bool L2TP_IPsec; //L2TP with IPSec
        public bool EtherIP_IPsec;
        public string L2TP_DefaultHub;
        public string IPsec_Secret;
    }
}


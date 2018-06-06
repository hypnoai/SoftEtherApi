using System.Security.Cryptography.X509Certificates;

namespace SoftEtherApi.SoftEtherModel
{
    public class ServerCert : BaseSoftEtherModel<ServerCert>
    {
        public X509Certificate Cert;
        public uint  Flag1;
    }
}
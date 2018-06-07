using System.Security.Cryptography.X509Certificates;
using SoftEtherApi.Model;

namespace SoftEtherApi.SoftEtherModel
{
    public class ServerCert : BaseSoftEtherModel<ServerCert>
    {
        public X509Certificate Cert;
        public uint  Flag1;
    }
}
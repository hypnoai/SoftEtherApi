namespace SoftEtherApi.Model
{
    public enum AuthType : int
    {
        Anonymous = 0, // Anonymous authentication
        Password = 1, // Password authentication
        Usercert = 2, // User certificate authentication
        Rootcert = 3, // Root certificate which is issued by trusted Certificate Authority
        Radius = 4, // Radius authentication
        Nt = 5, // Windows NT authentication
        OpenvpnCert = 98, // TLS client certificate authentication
        Ticket = 99, // Ticket authentication
    }
}
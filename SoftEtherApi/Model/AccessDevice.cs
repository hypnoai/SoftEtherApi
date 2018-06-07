using System.Net;

namespace SoftEtherApi.Model
{
    public class AccessDevice
    {
        public IPAddress Ip { get; set; }
        public string Name { get; set; }

        public AccessDevice(IPAddress ip, string name)
        {
            Ip = ip;
            Name = name;
        }
        
        public AccessDevice(string ip, string name)
        {
            Ip = IPAddress.Parse(ip);
            Name = name;
        }
    }
}
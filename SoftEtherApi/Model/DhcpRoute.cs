using System.Collections.Generic;
using System.Linq;

namespace SoftEtherApi.Model
{
    public class DhcpRoute
    {
        public string IpNetwork { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }

        public DhcpRoute(string ipNetwork, string subnet, string gateway)
        {
            IpNetwork = ipNetwork;
            Subnet = subnet;
            Gateway = gateway;
        }

        public static List<DhcpRoute> FromCsv(string val)
        {
            var routeStream = val.Split(new char[] {' ', ','}).Select(m => m.Trim()).Where(m => m.Length > 0).ToList();
            return routeStream.Select(m => m.Split('/')).Select(m => new DhcpRoute(m[0], m[1], m[2])).ToList();
        }

        public override string ToString()
        {
            return $"{IpNetwork}/{Subnet}/{Gateway}";
        }
    }
}
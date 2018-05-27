using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class ServerInfo : BaseSoftEtherModel<ServerInfo>
    {
        public string KernelName;
        public string OsProductName;
        public int OsServicePack;
        public string OsSystemName;
        public int OsType;
        public string OsVendorName;
        public string OsVersion;
        public DateTime ServerBuildDate;
        public string ServerBuildInfoString;
        public int ServerBuildInt;
        public string ServerFamilyName;
        public string ServerHostName;
        public string ServerProductName;
        public int ServerType;
        public int ServerVerInt;
        public string ServerVersionString;
    }
}
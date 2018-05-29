using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class ServerInfo : BaseSoftEtherModel<ServerInfo>
    {
        public string KernelName;
        public string OsProductName;
        public uint  OsServicePack;
        public string OsSystemName;
        public uint  OsType;
        public string OsVendorName;
        public string OsVersion;
        public DateTime ServerBuildDate;
        public string ServerBuildInfoString;
        public uint  ServerBuildInt;
        public string ServerFamilyName;
        public string ServerHostName;
        public string ServerProductName;
        public uint  ServerType;
        public uint  ServerVerInt;
        public string ServerVersionString;
    }
}
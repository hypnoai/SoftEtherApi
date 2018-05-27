using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class ServerStatus : BaseSoftEtherModel<ServerStatus>
    {
        public int AssignedBridgeLicenses;
        public int AssignedBridgeLicensesTotal;
        public int AssignedClientLicenses;
        public int AssignedClientLicensesTotal;
        public long CurrentTick;
        public DateTime CurrentTime;
        public long FreeMemory;
        public long FreePhys;
        public int NumGroups;
        public int NumHubDynamic;
        public int NumHubStandalone;
        public int NumHubStatic;
        public int NumHubTotal;
        public int NumIpTables;
        public int NumMacTables;
        public int NumSessionsLocal;
        public int NumSessionsRemote;
        public int NumSessionsTotal;
        public int NumTcpConnections;
        public int NumTcpConnectionsLocal;
        public int NumTcpConnectionsRemote;
        public int NumUsers;
        public long RecvBroadcastBytes;
        public long RecvBroadcastCount;
        public long RecvUnicastBytes;
        public long RecvUnicastCount;
        public long SendBroadcastBytes;
        public long SendBroadcastCount;
        public long SendUnicastBytes;
        public long SendUnicastCount;
        public int ServerType;
        public DateTime StartTime;
        public long TotalMemory;
        public long TotalPhys;
        public long UsedMemory;
        public long UsedPhys;
    }
}
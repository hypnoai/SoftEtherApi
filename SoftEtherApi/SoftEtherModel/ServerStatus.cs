using System;

namespace SoftEtherApi.SoftEtherModel
{
    public class ServerStatus : BaseSoftEtherModel<ServerStatus>
    {
        public uint  AssignedBridgeLicenses;
        public uint  AssignedBridgeLicensesTotal;
        public uint  AssignedClientLicenses;
        public uint  AssignedClientLicensesTotal;
        public ulong CurrentTick;
        public DateTime CurrentTime;
        public ulong FreeMemory;
        public ulong FreePhys;
        public uint  NumGroups;
        public uint  NumHubDynamic;
        public uint  NumHubStandalone;
        public uint  NumHubStatic;
        public uint  NumHubTotal;
        public uint  NumIpTables;
        public uint  NumMacTables;
        public uint  NumSessionsLocal;
        public uint  NumSessionsRemote;
        public uint  NumSessionsTotal;
        public uint  NumTcpConnections;
        public uint  NumTcpConnectionsLocal;
        public uint  NumTcpConnectionsRemote;
        public uint  NumUsers;
        public ulong RecvBroadcastBytes;
        public ulong RecvBroadcastCount;
        public ulong RecvUnicastBytes;
        public ulong RecvUnicastCount;
        public ulong SendBroadcastBytes;
        public ulong SendBroadcastCount;
        public ulong SendUnicastBytes;
        public ulong SendUnicastCount;
        public uint  ServerType;
        public DateTime StartTime;
        public ulong TotalMemory;
        public ulong TotalPhys;
        public ulong UsedMemory;
        public ulong UsedPhys;
    }
}
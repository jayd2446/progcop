using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace ProgCop
{
    internal enum Protocol
    {
        TCP,
        UDP
    }

    // Enum to define the set of values used to indicate the type of table returned by 
    // calls made to the function 'GetExtendedTcpTable'.
    internal enum TcpTableClass
    {
        TCP_TABLE_BASIC_LISTENER,
        TCP_TABLE_BASIC_CONNECTIONS,
        TCP_TABLE_BASIC_ALL,
        TCP_TABLE_OWNER_PID_LISTENER,
        TCP_TABLE_OWNER_PID_CONNECTIONS,
        TCP_TABLE_OWNER_PID_ALL,
        TCP_TABLE_OWNER_MODULE_LISTENER,
        TCP_TABLE_OWNER_MODULE_CONNECTIONS,
        TCP_TABLE_OWNER_MODULE_ALL
    }

    // Enum to define the set of values used to indicate the type of table returned by calls
    // made to the function GetExtendedUdpTable.
    internal enum UdpTableClass
    {
        UDP_TABLE_BASIC,
        UDP_TABLE_OWNER_PID,
        UDP_TABLE_OWNER_MODULE
    }

    // Enum for different possible states of TCP connection
    internal enum MibTcpState
    {
        CLOSED = 1,
        LISTENING = 2,
        SYN_SENT = 3,
        SYN_RCVD = 4,
        ESTABLISHED = 5,
        FIN_WAIT1 = 6,
        FIN_WAIT2 = 7,
        CLOSE_WAIT = 8,
        CLOSING = 9,
        LAST_ACK = 10,
        TIME_WAIT = 11,
        DELETE_TCB = 12,
        NONE = 0
    }

    // The structure contains information that describes an IPv4 TCP connection with 
    // IPv4 addresses, ports used by the TCP connection, and the specific process ID
    // (PID) associated with connection.
    [StructLayout(LayoutKind.Sequential)]
    internal struct MIB_TCPROW_OWNER_PID
    {
        internal MibTcpState state;
        internal uint localAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] internal byte[] localPort;
        internal uint remoteAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] internal byte[] remotePort;
        internal int owningPid;
    }

    // The structure contains a table of process IDs (PIDs) and the IPv4 TCP links that 
    // are context bound to these PIDs.
    [StructLayout(LayoutKind.Sequential)]
    internal struct MIB_TCPTABLE_OWNER_PID
    {
        internal uint dwNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
        internal MIB_TCPROW_OWNER_PID[] table;
    }

    // The structure contains an entry from the User Datagram Protocol (UDP) listener
    // table for IPv4 on the local computer. The entry also includes the process ID
    // (PID) that issued the call to the bind function for the UDP endpoint.
    [StructLayout(LayoutKind.Sequential)]
    internal struct MIB_UDPROW_OWNER_PID
    {
        internal uint localAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        internal byte[] localPort;
        internal int owningPid;
    }

    // The structure contains the User Datagram Protocol (UDP) listener table for IPv4
    // on the local computer. The table also includes the process ID (PID) that issued
    // the call to the bind function for each UDP endpoint.
    [StructLayout(LayoutKind.Sequential)]
    internal struct MIB_UDPTABLE_OWNER_PID
    {
        internal uint dwNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
        internal UdpProcessRecord[] table;
    }

    // This class provides access an IPv4 TCP connection addresses and ports and its
    // associated Process IDs and names.
    [StructLayout(LayoutKind.Sequential)]
    internal class TcpProcessRecord
    {
        internal IPAddress LocalAddress { get; set; }
        internal ushort LocalPort { get; set; }
        internal IPAddress RemoteAddress { get; set; }
        internal ushort RemotePort { get; set; }
        internal MibTcpState State { get; set; }
        internal int ProcessId { get; set; }
        internal string ProcessName { get; set; }
        internal string ProcessFullPath { get; set; }

        internal TcpProcessRecord(IPAddress localIp, IPAddress remoteIp, ushort localPort,
            ushort remotePort, int pId, MibTcpState state)
        {
            LocalAddress = localIp;
            RemoteAddress = remoteIp;
            LocalPort = localPort;
            RemotePort = remotePort;
            State = state;
            ProcessId = pId;

            if (Process.GetProcesses().Any(process => process.Id == pId))
            {
                Process foundProcess = Process.GetProcessById(ProcessId);
                ProcessName = foundProcess.ProcessName;
                ProcessFullPath = foundProcess.MainModule.FileName;
            }
        }
    }

    // This class provides access an IPv4 UDP connection addresses and ports and its
    // associated Process IDs and names.
    [StructLayout(LayoutKind.Sequential)]
    internal class UdpProcessRecord
    {
        internal IPAddress LocalAddress { get; set; }
        internal uint LocalPort { get; set; }
        internal int ProcessId { get; set; }
        internal string ProcessName { get; set; }
        internal string ProcessFullPath { get; set; }

        internal UdpProcessRecord(IPAddress localAddress, uint localPort, int pId)
        {
            LocalAddress = localAddress;
            LocalPort = localPort;
            ProcessId = pId;

            if (Process.GetProcesses().Any(process => process.Id == pId))
            {
                Process foundProcess = Process.GetProcessById(ProcessId);
                ProcessName = foundProcess.ProcessName;
                ProcessFullPath = foundProcess.MainModule.FileName;
            }
        }
    }

    internal class ConnectedProcessesLookup
    {
        //IPv4
        //TODO: Add support for IPv6 later
        private const int AF_INET = 2;

        private List<TcpProcessRecord> _activeTcpConnections;
        private List<UdpProcessRecord> _activeUdpConnections;

        internal ConnectedProcessesLookup()
        {
            _activeTcpConnections = new List<TcpProcessRecord>();
            _activeUdpConnections = new List<UdpProcessRecord>();
        }

        //TODO: Do we need to have view for Tcp and Udp separately or should we just combine the lists into one list?
        internal List<TcpProcessRecord> LookupForTcpConnectedProcesses()
        {
            return _activeTcpConnections;
        }

        internal List<UdpProcessRecord> LookupForUdpConnectedProcesses()
        {
            return _activeUdpConnections;
        }
    }
}

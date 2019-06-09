using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
        internal MIB_UDPROW_OWNER_PID[] table;
    }

    //Simple helper to get the process fullpath from pID
    internal class ProcessMainModuleFilePath
    {
        internal static string GetPath(int pId)
        {
            string wmiQueryString = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + pId;

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQueryString))
            {
                using (ManagementObjectCollection results = searcher.Get())
                {
                    ManagementObject mo = results.Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        return (string)mo["ExecutablePath"];
                    }
                }
            }

            return null;
        }

        internal static string GetFilename(int pId)
        {
            string wmiQueryString = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + pId;

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQueryString))
            {
                using (ManagementObjectCollection results = searcher.Get())
                {
                    ManagementObject mo = results.Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        return  System.IO.Path.GetFileName((string)mo["ExecutablePath"]);
                    }
                }
            }

            return null;
        }
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
        internal string Protocol { get; set; }

        internal TcpProcessRecord(IPAddress localIp, IPAddress remoteIp, ushort localPort,
            ushort remotePort, int pId, MibTcpState state)
        {
            LocalAddress = localIp;
            RemoteAddress = remoteIp;
            LocalPort = localPort;
            RemotePort = remotePort;
            State = state;
            ProcessId = pId;
            Protocol = "TCP";
            ProcessName = Process.GetProcessById(pId).ProcessName;
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
        internal string Protocol { get; set; }

        internal UdpProcessRecord(IPAddress localAddress, uint localPort, int pId)
        {
            LocalAddress = localAddress;
            LocalPort = localPort;
            ProcessId = pId;
            Protocol = "UDP";
            ProcessName = Process.GetProcessById(pId).ProcessName;
        }
    }

    internal class ConnectedProcessesLookup
    {
        //IPv4
        //TODO: Add support for IPv6 later
        private const int AF_INET = 2;

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder, 
                                                       int ulAf, TcpTableClass tableClass, uint reserved = 0);

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedUdpTable(IntPtr pUdpTable, ref int pdwSize, bool bOrder, 
                                                                         int ulAf, UdpTableClass tableClass, uint reserved = 0);

        internal List<TcpProcessRecord> LookupForTcpConnectedProcesses()
        {
            List<TcpProcessRecord> activeTcpConnections = new List<TcpProcessRecord>();
            int bufferSize = 0;

            // Getting the size of TCP table, that is returned in 'bufferSize' variable. We need to have this here and not inside the try
            //block as we need to be able to free tcpTableRecordsPtr in finally-block
            uint result = GetExtendedTcpTable(IntPtr.Zero, ref bufferSize, true, AF_INET, TcpTableClass.TCP_TABLE_OWNER_PID_ALL);
            IntPtr tcpTableRecordsPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                // The size of the table returned in 'bufferSize' variable in previous
                // call must be used in this subsequent call to 'GetExtendedTcpTable'
                // function in order to successfully retrieve the table.
                result = GetExtendedTcpTable(tcpTableRecordsPtr, ref bufferSize, true, AF_INET, TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

                if (result != 0)
                    throw new Exception("GetExtendedTcpTable failed");

                // Marshals data from an unmanaged block of memory to a newly allocated
                // managed object 'tcpRecordsTable' of type 'MIB_TCPTABLE_OWNER_PID'
                // to get number of entries of the specified TCP table structure.
                MIB_TCPTABLE_OWNER_PID tcpRecordsTable = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(tcpTableRecordsPtr, typeof(MIB_TCPTABLE_OWNER_PID));
                IntPtr tableRowPtr = (IntPtr)((long)tcpTableRecordsPtr + Marshal.SizeOf(tcpRecordsTable.dwNumEntries));

                for (int row = 0; row < tcpRecordsTable.dwNumEntries; row++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(tableRowPtr, typeof(MIB_TCPROW_OWNER_PID));

                    TcpProcessRecord record = new TcpProcessRecord(new IPAddress(tcpRow.localAddr), new IPAddress(tcpRow.remoteAddr),
                                              BitConverter.ToUInt16(new byte[2] { tcpRow.localPort[1], tcpRow.localPort[0] }, 0),
                                              BitConverter.ToUInt16(new byte[2] { tcpRow.remotePort[1], tcpRow.remotePort[0] }, 0), tcpRow.owningPid, tcpRow.state);

                    tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(tcpRow));
                    
                    activeTcpConnections.Add(record);
                }
            }
            catch(Exception ex)
            {
                //TODO: Write to the system log
            }
            finally
            {
                Marshal.FreeHGlobal(tcpTableRecordsPtr);
            }

            return activeTcpConnections?.Distinct().ToList<TcpProcessRecord>();
        }

        internal List<UdpProcessRecord> LookupForUdpConnectedProcesses()
        {
            int bufferSize = 0;
            List<UdpProcessRecord> activeUdpConnections = new List<UdpProcessRecord>();
            // Getting the size of UDP table, that is returned in 'bufferSize' variable.
            uint result = GetExtendedUdpTable(IntPtr.Zero, ref bufferSize, true, AF_INET, UdpTableClass.UDP_TABLE_OWNER_PID);
            IntPtr udpTableRecordPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                // The size of the table returned in 'bufferSize' variable in previous
                // call must be used in this subsequent call to 'GetExtendedUdpTable'
                // function in order to successfully retrieve the table.
                result = GetExtendedUdpTable(udpTableRecordPtr, ref bufferSize, true, AF_INET, UdpTableClass.UDP_TABLE_OWNER_PID);

                if(result != 0)
                    throw new Exception("GetExtendedUdpTable failed");

                // Marshals data from an unmanaged block of memory to a newly allocated
                // managed object 'udpRecordsTable' of type 'MIB_UDPTABLE_OWNER_PID'
                // to get number of entries of the specified TCP table structure.
                MIB_UDPTABLE_OWNER_PID udpRecordsTable = (MIB_UDPTABLE_OWNER_PID)Marshal.PtrToStructure(udpTableRecordPtr, typeof(MIB_UDPTABLE_OWNER_PID));
                IntPtr tableRowPtr = (IntPtr)((long)udpTableRecordPtr + Marshal.SizeOf(udpRecordsTable.dwNumEntries));
     
                // Reading and parsing the UDP records one by one from the table and
                // storing them in a list of 'UdpProcessRecord' structure type objects.
                for (int i = 0; i < udpRecordsTable.dwNumEntries; i++)
                {
                    MIB_UDPROW_OWNER_PID udpRow = (MIB_UDPROW_OWNER_PID)Marshal.PtrToStructure(tableRowPtr, typeof(MIB_UDPROW_OWNER_PID));
                    UdpProcessRecord record = new UdpProcessRecord(new IPAddress(udpRow.localAddr),
                                                                  BitConverter.ToUInt16(new byte[2] { udpRow.localPort[1], udpRow.localPort[0] }, 0), 
                                                                  udpRow.owningPid);
                    tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(udpRow));
                    activeUdpConnections.Add(record);
                }
            }
          
            catch (Exception ex)
            {
                //TODO: Write to the system log
            }
            finally
            {
                Marshal.FreeHGlobal(udpTableRecordPtr);
            }

            return activeUdpConnections?.Distinct().ToList<UdpProcessRecord>();
        }
    }
}

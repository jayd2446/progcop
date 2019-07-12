using System.Diagnostics;

namespace ProgCop
{
    internal static class Logger
    {
        private static string pAppName = "ProgCop";
        private static string pLogToWrite = "Application";
        
        internal static void Write(string message)
        {
            if (!EventLog.SourceExists(pAppName))
                EventLog.CreateEventSource(pAppName, pLogToWrite);

            EventLog.WriteEntry(pAppName, message, EventLogEntryType.Warning);
        }
    }
}

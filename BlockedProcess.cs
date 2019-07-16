using System;

namespace ProgCop
{
    internal class BlockedProcess : IEquatable<BlockedProcess>
    {
        internal BlockedProcess(string processPath, string processName, bool stateBlocked)
        {
            StateBlocked = stateBlocked;
            ProcessName = processName;
            ProcessPath = processPath;
        }

        internal bool StateBlocked { get; set; }
        internal string ProcessName { get; }
        internal string ProcessPath { get; }

        bool IEquatable<BlockedProcess>.Equals(BlockedProcess other)
        { 
            if (ProcessPath.Equals(other.ProcessPath))
                return true;

            return false;
        }
    }
}

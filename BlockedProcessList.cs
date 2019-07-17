using System;
using System.Collections;
using System.Collections.Generic;
using WindowsFirewallHelper;
using WindowsFirewallHelper.FirewallAPIv2.Rules;

namespace ProgCop
{
    internal class BlockedProcessList : IEnumerable<BlockedProcess>
    {
        private List<BlockedProcess> pProcesses;

        internal BlockedProcessList()
        {
            pProcesses = new List<BlockedProcess>();
        }

        internal void Load()
        {
            
        }

        internal bool Save()
        {

            return true;
        }

        internal void Add(BlockedProcess process)
        {
            pProcesses.Add(process);
        }

        internal void Remove(BlockedProcess process)
        {
            pProcesses.Remove(process);
        }

        internal void RemoveByProcessName(string name)
        {
            foreach (BlockedProcess p in pProcesses)
            {
                if (p.ProcessName.Equals(name))
                {
                    pProcesses.Remove(p);
                    break;
                }
            }
        }

        internal bool GetProcessStateByProcessName(string name)
        {
            foreach (BlockedProcess p in pProcesses)
            {
                if (p.ProcessName.Equals(name))
                {
                    return p.StateBlocked;
                }
            }

            return false;
        }

        internal bool Contains(BlockedProcess process)
        {
            foreach (BlockedProcess p in pProcesses)
                if (p.Equals(process))
                    return true;

            return false;
        }

        internal bool ContainsProcessNamed(string name)
        {
            foreach (BlockedProcess p in pProcesses)
                if (p.ProcessName.Equals(name))
                    return true;

            return false;
        }

        internal BlockedProcess GetProcessByName(string name)
        {
            foreach (BlockedProcess p in pProcesses)
                if (p.ProcessName.Equals(name))
                    return p;

            return null;
        }

        IEnumerator<BlockedProcess> IEnumerable<BlockedProcess>.GetEnumerator()
        {
            return pProcesses.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return pProcesses.GetEnumerator();
        }
    }
}

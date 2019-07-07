using System;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using WindowsFirewallHelper;
using WindowsFirewallHelper.FirewallAPIv2.Rules;
using System.Diagnostics;
using System.ComponentModel;

namespace ProgCop
{
    internal partial class MainWindow : Form
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);

        private System.Windows.Forms.Timer _timer;

        internal MainWindow()
        {
            InitializeComponent();

            Font = SystemFonts.MessageBoxFont;
            
            SetWindowTheme(listView1BlockedApplications.Handle, "explorer", null);

            toolBarButtonDelProg.Enabled = false;

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 5000;
            _timer.Tick += _timer_Tick1;
            _timer.Start();
        }

        private void UpdateListViewSafely()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, e) => {
                List<TcpProcessRecord> recordsTcpNew = new ConnectedProcessesLookup().LookupForTcpConnectedProcesses();
                e.Result = recordsTcpNew;
            };

            worker.RunWorkerCompleted += (s, e) => {
                List<TcpProcessRecord> recordsTcpNew = (List<TcpProcessRecord>)e.Result;
                foreach (TcpProcessRecord record in recordsTcpNew)
                {
                    if (!listViewInternetConnectedProcesses.Items.ContainsKey(record.ProcessId.ToString()))
                    {
                        ListViewItem itemNew = new ListViewItem(new string[] { record.ProcessName, record.LocalAddress.ToString(),
                                                                           record.RemoteAddress.ToString(), record.LocalPort.ToString(),
                                                                           record.RemotePort.ToString(), record.State.ToString(),
                                                                           record.ProcessId.ToString() });
                        itemNew.Tag = record.ProcessId;
                        itemNew.Name = record.ProcessId.ToString();
                        listViewInternetConnectedProcesses.Items.Add(itemNew);

                    }
                }

                //TODO: We need to remove connections that are no longer connected (in the records list)
                foreach (ListViewItem item in listViewInternetConnectedProcesses.Items)
                {
                    bool found = false;
                    foreach (TcpProcessRecord record in recordsTcpNew)
                    {
                        if ((int)item.Tag == record.ProcessId)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found == false)
                    {
                        listViewInternetConnectedProcesses.Items.Remove(item);
                    }


                }


                listViewInternetConnectedProcesses.Sorting = SortOrder.Ascending;
                listViewInternetConnectedProcesses.Sort();
                //Need to reset the native theme everytime after calling .sort()
                SetWindowTheme(listViewInternetConnectedProcesses.Handle, "explorer", null);
            };

            worker.RunWorkerAsync();
        }

        internal void RefreshProcesses()
        {
            UpdateListViewSafely();
        }

        private void _timer_Tick1(object sender, EventArgs e)
        {
            RefreshProcesses();
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            RefreshProcesses();
        }

        private void ToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch(e.Button.Name)
            {
                case "toolBarButtonAddProg":
                    break;
                case "toolBarButtonDelProg":
                    Unblock();
                    break;
                case "toolBarButtonSettings":
                    break;
            }
        }

        private void MenuItemContextBlock_Click(object sender, EventArgs e)
        {
            if (listViewInternetConnectedProcesses.SelectedItems.Count > 0)
            {
                int pid = (int)listViewInternetConnectedProcesses.SelectedItems[0].Tag;
                Block(pid);
            }
        }

        private void Block(int PID)
        {
            string path = ProcessMainModuleFilePath.GetPath(PID);

            if (path == null)
            {
                MessageBox.Show("Can't find path for the process. Probably an internal Windows process.");
                return;
            }

            var rule = FirewallManager.Instance.CreateApplicationRule(FirewallManager.Instance.GetProfile().Type,
                                                                      @"ProgCop Rule " + Guid.NewGuid().ToString("B"),
                                                                      FirewallAction.Block, path); ;
            rule.Direction = FirewallDirection.Outbound;
            rule.Protocol = FirewallProtocol.Any;

            FirewallManager.Instance.Rules.Add(rule);

            ListViewItem itemNew = new ListViewItem(new string[] { path, "BLOCKED" });
            itemNew.Tag = rule;
            listView1BlockedApplications.Items.Add(itemNew);
        }

        private void Block(string path)
        {
            var rule = FirewallManager.Instance.CreateApplicationRule(FirewallManager.Instance.GetProfile().Type,
                                                                      @"ProgCop Rule " + Guid.NewGuid().ToString("B"),
                                                                      FirewallAction.Block, path); ;
            rule.Direction = FirewallDirection.Outbound;
            rule.Protocol = FirewallProtocol.Any;

            FirewallManager.Instance.Rules.Add(rule);

            ListViewItem itemNew = new ListViewItem(new string[] { path, "BLOCKED" });
            itemNew.Tag = rule;
            listView1BlockedApplications.Items.Add(itemNew);
        }

        private void MenuItemContextOpenFileLocation_Click(object sender, EventArgs e)
        {
            int pid = (int)listViewInternetConnectedProcesses.SelectedItems[0].Tag;
            string path = ProcessMainModuleFilePath.GetPath(pid);

            if (File.Exists(path))
                Process.Start("explorer.exe", "/select," + path);
        }

        //TODO: These need error handling
        private void ListViewInternetConnectedProcesses_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
                if(listViewInternetConnectedProcesses.FocusedItem.Bounds.Contains(e.Location))
                    contextMenuConnectedItems.Show(listViewInternetConnectedProcesses, e.Location);
        }

        private void Unblock()
        {
            if (listView1BlockedApplications.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1BlockedApplications.SelectedItems[0];
                var rule = (IRule)item.Tag;
                var theRule = FirewallManager.Instance.Rules.SingleOrDefault(r => r.Name == rule.Name);

                FirewallManager.Instance.Rules.Remove(theRule);
                listView1BlockedApplications.Items.Remove(item);
            }
        }

        private void MenuItemContextUnblock_Click(object sender, EventArgs e)
        {
            Unblock();
        }

        private void ListView1BlockedApplications_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                if (listView1BlockedApplications.FocusedItem.Bounds.Contains(e.Location))
                    contextMenuBlockedApplications.Show(listView1BlockedApplications, e.Location);
        }

        private void ListView1BlockedApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1BlockedApplications.SelectedItems.Count > 0)
                toolBarButtonDelProg.Enabled = true;
            else
                toolBarButtonDelProg.Enabled = false;
        }
    }
}

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
        private static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);

        private enum ShieldButtonImageColor
        {
            Normal = 3,
            Gray = 4
        }

        private System.Windows.Forms.Timer pTimer;
        private List<string> pBlockedProcessNames;

        internal MainWindow()
        {
            InitializeComponent();

            Font = SystemFonts.MessageBoxFont;
            
            SetWindowTheme(listView1BlockedApplications.Handle, "explorer", null);

            toolBarButtonRulesEnabled.Pushed = true;
            toolBarButtonRulesEnabled.ImageIndex = (int)ShieldButtonImageColor.Normal;
            toolBarButtonDelProg.Enabled = false;

            //TODO: In the future we need to load these from somewhere as well as blocked apps too etc.
            pBlockedProcessNames = new List<string>();

            if (listView1BlockedApplications.Items.Count > 0)
            {
                //TODO: Get the saved button state in here. We need to set the state in here in order to
                //disabled state grayed out thing to work. Setting pushed=true will enable colors for the button even
                //it is disabled.
                toolBarButtonRulesEnabled.Pushed = true;
                EnableDisableRules();
                toolBarButtonRulesEnabled.Enabled = true;
            }
            else
            {
                toolBarButtonRulesEnabled.Enabled = false;
                toolBarButtonRulesEnabled.ImageIndex = (int)ShieldButtonImageColor.Gray;
            }

            pTimer = new System.Windows.Forms.Timer();
            pTimer.Interval = 5000;
            pTimer.Tick += _timer_Tick1;
            pTimer.Start();
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
                    int processId = record.ProcessId;
 
                    if (pBlockedProcessNames.Contains(record.ProcessName))
                    {
                        listViewInternetConnectedProcesses.Items.RemoveByKey(processId.ToString());
                    }
                    else
                    {
                        if (!listViewInternetConnectedProcesses.Items.ContainsKey(processId.ToString()))
                        {
                            ListViewItem itemNew = new ListViewItem(new string[] { record.ProcessName, record.LocalAddress.ToString(),
                                                                                record.RemoteAddress.ToString(), record.LocalPort.ToString(),
                                                                                record.RemotePort.ToString(), record.State.ToString(),
                                                                                record.ProcessId.ToString()
                                                                                }
                                                                    );
                            itemNew.Tag = record.ProcessId;
                            itemNew.Name = record.ProcessId.ToString();
                            listViewInternetConnectedProcesses.Items.Add(itemNew);
                        }
                    }
                }

                //Handle processes that are no longer running on the system, but are on the list still
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
                    if (!found)
                        listViewInternetConnectedProcesses.Items.Remove(item);
                }

                listViewInternetConnectedProcesses.Sorting = SortOrder.Ascending;
                listViewInternetConnectedProcesses.Sort();
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
                    SelectFileFromDiskAndBlock();
                    break;
                case "toolBarButtonDelProg":
                    Unblock();
                    break;
                case "toolBarButtonSettings":
                    break;
                case "toolBarButtonRulesEnabled":
                    EnableDisableRules();
                    break;
            }
        }

        private void EnableDisableRules()
        {
            foreach(ListViewItem item in listView1BlockedApplications.Items)
            {
                var rule = (IRule)item.Tag;
                var theRule = FirewallManager.Instance.Rules.SingleOrDefault(r => r.Name == rule.Name);

                if (toolBarButtonRulesEnabled.Pushed)
                {
                    theRule.IsEnable = true;
                    item.SubItems[2].Text = "BLOCKED";
                    item.ForeColor = Color.DarkGreen;
                }
                else
                {
                    theRule.IsEnable = false;
                    item.SubItems[2].Text = "UNBLOCKED";
                    item.ForeColor = Color.Red;
                }
            }
        }

        private void BlockSelectedProcess()
        {
            if (listViewInternetConnectedProcesses.SelectedItems.Count > 0)
            {
                int pid = (int)listViewInternetConnectedProcesses.SelectedItems[0].Tag;
                Block(pid);
            }
        }

        private void MenuItemContextBlock_Click(object sender, EventArgs e)
        {
            BlockSelectedProcess();
        }

        private void SelectFileFromDiskAndBlock()
        {
            this.Cursor = Cursors.WaitCursor;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select an application";
            dlg.Filter = "Executable files (*.exe)|*.exe";

            if (dlg.ShowDialog() == DialogResult.OK)
                Block(dlg.FileName);

            this.Cursor = Cursors.Default;
        }

        private void Block(int PID)
        {
            string path = ProcessMainModuleFilePath.GetPath(PID);

            if (path == null)
            {
                MessageBox.Show(this, "Can't find path for the process. Probably an internal Windows process.",
                    "ProgCop error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var rule = FirewallManager.Instance.CreateApplicationRule(FirewallManager.Instance.GetProfile().Type,
                                                                      @"ProgCop Rule " + Guid.NewGuid().ToString("B"),
                                                                      FirewallAction.Block, path); ;
            rule.Direction = FirewallDirection.Outbound;
            rule.Protocol = FirewallProtocol.Any;

            FirewallManager.Instance.Rules.Add(rule);
            string processName = Process.GetProcessById(PID).ProcessName;

            ListViewItem itemNew = new ListViewItem(new string[] { path, processName, "BLOCKED" });
            itemNew.Tag = rule;

            //We use this in unblock to be able to remove item from blocked list
            itemNew.Name = processName;
            itemNew.ForeColor = Color.Green;
            listView1BlockedApplications.Items.Add(itemNew);

            if(!toolBarButtonRulesEnabled.Enabled)
            {
                toolBarButtonRulesEnabled.Enabled = true;
                toolBarButtonRulesEnabled.ImageIndex = (int)ShieldButtonImageColor.Normal;
            }

            if (!pBlockedProcessNames.Contains(processName))
                pBlockedProcessNames.Add(processName);
        }

        private void Block(string path)
        {
            var rule = FirewallManager.Instance.CreateApplicationRule(FirewallManager.Instance.GetProfile().Type,
                                                                      @"ProgCop Rule " + Guid.NewGuid().ToString("B"),
                                                                      FirewallAction.Block, path); ;
            rule.Direction = FirewallDirection.Outbound;
            rule.Protocol = FirewallProtocol.Any;

            FirewallManager.Instance.Rules.Add(rule);
            
            string processName = Path.GetFileNameWithoutExtension(path);

            ListViewItem itemNew = new ListViewItem(new string[] { path, processName, "BLOCKED" });
            itemNew.Tag = rule;
            itemNew.Name = processName;
            itemNew.ForeColor = Color.Green;
            listView1BlockedApplications.Items.Add(itemNew);

            if (!toolBarButtonRulesEnabled.Enabled)
            {
                toolBarButtonRulesEnabled.Enabled = true;
                toolBarButtonRulesEnabled.ImageIndex = (int)ShieldButtonImageColor.Normal;
            }

            if (!pBlockedProcessNames.Contains(processName))
                pBlockedProcessNames.Add(processName);
        }

        private void MenuItemContextOpenFileLocation_Click(object sender, EventArgs e)
        {
            int pid = (int)listViewInternetConnectedProcesses.SelectedItems[0].Tag;
            string path = ProcessMainModuleFilePath.GetPath(pid);

            if (File.Exists(path))
                Process.Start("explorer.exe", "/select," + path);
        }

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

                if (FirewallManager.Instance.Rules.Remove(theRule))
                {
                    pBlockedProcessNames.Remove(item.Name);
                    listView1BlockedApplications.Items.Remove(item);

                    if (toolBarButtonRulesEnabled.Enabled && listView1BlockedApplications.Items.Count == 0)
                    {
                        toolBarButtonRulesEnabled.Enabled = false;
                        toolBarButtonRulesEnabled.ImageIndex = (int)ShieldButtonImageColor.Gray;
                    }
                }
                else
                {
                    MessageBox.Show("Removing rule " + rule.Name + " failed. Please contact support.", "ProgCop error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            {
                toolBarButtonDelProg.Enabled = true;
            }
            else
            {
                toolBarButtonDelProg.Enabled = false;
            }
        }

        private void MenuItemAddProg_Click(object sender, EventArgs e)
        {
            SelectFileFromDiskAndBlock();
        }

        private void MenuItemBlock_Click(object sender, EventArgs e)
        {
            BlockSelectedProcess();
        }

        private void MenuItemUnBlock_Click(object sender, EventArgs e)
        {
            Unblock();
        }

        private void MenuItemEditMenu_Popup(object sender, EventArgs e)
        {
            if (listView1BlockedApplications.SelectedItems.Count == 0)
            {
                menuItemUnBlock.Enabled = false;
                menuItemUnBlock.Text = "Unblock";
            }
            else
            {
                menuItemUnBlock.Enabled = true;
                menuItemUnBlock.Text = "Unblock " + listView1BlockedApplications.SelectedItems[0].Text;
            }

            if (listViewInternetConnectedProcesses.SelectedItems.Count == 0)
            {
                menuItemBlock.Enabled = false;
                menuItemBlock.Text = "Block";
            }
            else
            {
                menuItemBlock.Enabled = true;
                menuItemBlock.Text = "Block " + listViewInternetConnectedProcesses.SelectedItems[0].Text;
            }
        }
    }
}

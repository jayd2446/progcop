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
        private System.Windows.Forms.Timer pTimerStatus;
        private BlockedProcessList pBlockedProcessList;

        internal MainWindow()
        {
            InitializeComponent();

            Font = SystemFonts.MessageBoxFont;
            
            SetWindowTheme(listView1BlockedApplications.Handle, "explorer", null);

            toolBarButtonRulesEnabled.Pushed = true;
            toolBarButtonRulesEnabled.ImageIndex = (int)ShieldButtonImageColor.Normal;
            toolBarButtonDelProg.Enabled = false;

            notifyIcon1.Visible = Properties.Settings.Default.ShowInTray;

            //TODO: In the future we need to load these from somewhere as well as blocked apps too etc.
            //pBlockedProcessNames = new List<string>();
            pBlockedProcessList = new BlockedProcessList();

            toolBarButtonUnblockOnly.Enabled = false;
            toolBarButtonBlockOnly.Enabled = false;

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
            pTimerStatus = new System.Windows.Forms.Timer();

            pTimerStatus.Interval = 3000;
            pTimer.Interval = 5000;
            pTimer.Tick += _timer_Tick1;
            pTimerStatus.Tick += PTimerStatus_Tick;

            pTimer.Start();
            pTimerStatus.Start();

            HandleStatusBar();
        }

        private void HandleStatusBar()
        {
            if (IsWindowsFirewallEnabled())
                statusBarPanel2.Text = "Firewall enabled";
            else
                statusBarPanel2.Text = "Firewall not enabled";
        }

        private void PTimerStatus_Tick(object sender, EventArgs e)
        {
            HandleStatusBar();
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
 
                    if (pBlockedProcessList.ContainsProcessNamed(record.ProcessName) && 
                    pBlockedProcessList.GetProcessStateByProcessName(record.ProcessName) == true)
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
                    ShowSettingsDialog();
                    break;
                case "toolBarButtonRulesEnabled":
                    EnableDisableRules();
                    break;
                case "toolBarButtonUnblockOnly":
                    EnableDisableSelectedRule(false);
                    break;
                case "toolBarButtonBlockOnly":
                    EnableDisableSelectedRule(true);
                    break;
            }
        }

        private void EnableDisableRules()
        {
            BlockedProcess process = null;
            bool actionWasBlocking = false;
            List<int> indexesToRemoveFromView = new List<int>();

            foreach(ListViewItem item in listView1BlockedApplications.Items)
            {
                var rule = (IRule)item.Tag;
                var theRule = FirewallManager.Instance.Rules.SingleOrDefault(r => r.Name == rule.Name);

                if(theRule == null)
                {
                    Logger.Write("EnableDisableRules(): rule does not exist. Only removing it from the view.");

                    indexesToRemoveFromView.Add(item.Index);
                    continue;
                }

                process = pBlockedProcessList.GetProcessByName(item.Name);

                if (toolBarButtonRulesEnabled.Pushed)
                {
                    theRule.IsEnable = true;
                    item.SubItems[2].Text = "BLOCKED";
                    item.ForeColor = Color.DarkGreen;
                    if (process != null)
                        process.StateBlocked = true;

                    actionWasBlocking = true;
                }
                else
                {
                    theRule.IsEnable = false;
                    item.SubItems[2].Text = "UNBLOCKED";
                    item.ForeColor = Color.Red;
                    if (process != null)
                        process.StateBlocked = false;

                    actionWasBlocking = false;
                }
            }

            if(listView1BlockedApplications.SelectedItems.Count > 0)
            {
                if(actionWasBlocking)
                {
                    toolBarButtonBlockOnly.Enabled = false;
                    toolBarButtonUnblockOnly.Enabled = true;
                }
                else
                {
                    toolBarButtonBlockOnly.Enabled = true;
                    toolBarButtonUnblockOnly.Enabled = false;
                }
            }

            foreach (int i in indexesToRemoveFromView)
            {
                pBlockedProcessList.RemoveByProcessName(listView1BlockedApplications.Items[i].Name);
                listView1BlockedApplications.Items.RemoveAt(i);
            }
       }

        private void EnableDisableSelectedRule(bool stateBlocked)
        {
            BlockedProcess process = null;

            if(listView1BlockedApplications.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1BlockedApplications.SelectedItems[0];
                var rule = (IRule)item.Tag;
                var theRule = FirewallManager.Instance.Rules.SingleOrDefault(r => r.Name == rule.Name);

                if(theRule == null)
                {
                    Logger.Write("EnableDisableSelectedRule(): rule does not exist. Only removing it from the view.");
                    pBlockedProcessList.RemoveByProcessName(item.Name);
                    listView1BlockedApplications.Items.Remove(item);
                    return;
                }

                process = pBlockedProcessList.GetProcessByName(item.Name);

                if(stateBlocked)
                {
                    theRule.IsEnable = true;
                    item.SubItems[2].Text = "BLOCKED";
                    item.ForeColor = Color.DarkGreen;
                    if (process != null)
                        process.StateBlocked = true;
                    
                    toolBarButtonUnblockOnly.Enabled = true;
                    toolBarButtonBlockOnly.Enabled = false;
                }
                else
                {
                    theRule.IsEnable = false;
                    item.SubItems[2].Text = "UNBLOCKED";
                    item.ForeColor = Color.Red;
                    if (process != null)
                        process.StateBlocked = false;

                    //We need to handle the toolbar button state here too
                    toolBarButtonUnblockOnly.Enabled = false;
                    toolBarButtonBlockOnly.Enabled = true;
                }
            }
            else
            {
                new MessageBoxEx("ProgCop Information", "Please select a rule first.", 
                    MessageBoxExType.Information).ShowDialog(this);
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
                Logger.Write("Block(): Can't find path for the process. Probably an internal Windows process.");
                new MessageBoxEx("ProgCop Warning", "Can't find path for the process. Probably an internal Windows process.",
                                MessageBoxExType.Warning).ShowDialog(this);

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

            if (!pBlockedProcessList.ContainsProcessNamed(processName))
                pBlockedProcessList.Add(new BlockedProcess(path, processName, true));
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

            if (!pBlockedProcessList.ContainsProcessNamed(processName))
                pBlockedProcessList.Add(new BlockedProcess(path, processName, true));
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
            void HandleRulesButton()
            {
                if (toolBarButtonRulesEnabled.Enabled && listView1BlockedApplications.Items.Count == 0)
                {
                    toolBarButtonRulesEnabled.Enabled = false;
                    toolBarButtonRulesEnabled.ImageIndex = (int)ShieldButtonImageColor.Gray;
                }
            }

            if (listView1BlockedApplications.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1BlockedApplications.SelectedItems[0];
                var rule = (IRule)item.Tag;
                var theRule = FirewallManager.Instance.Rules.SingleOrDefault(r => r.Name == rule.Name);

                if(theRule != null)
                {
                    Logger.Write("Unblock(): Rule does not exist. Only removing it from the view.");
                    listView1BlockedApplications.Items.Remove(item);
                    pBlockedProcessList.RemoveByProcessName(item.Name);
                    HandleRulesButton();
                    return;
                }

                if (FirewallManager.Instance.Rules.Remove(theRule))
                {
                    pBlockedProcessList.RemoveByProcessName(item.Name);
                    listView1BlockedApplications.Items.Remove(item);
                    HandleRulesButton();
                }
                else
                {
                    Logger.Write("Unblock(): Removing rule " + rule.Name +  " failed");
                    new MessageBoxEx("ProgCop Warning", "Removing rule " + rule.Name + " failed. Please contact support.",
                                        MessageBoxExType.Warning).ShowDialog(this);
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

                ListViewItem item = listView1BlockedApplications.SelectedItems[0];
                var rule = (IRule)item.Tag;
                var theRule = FirewallManager.Instance.Rules.SingleOrDefault(r => r.Name == rule.Name);

                if (theRule != null)
                {
                    if (theRule.IsEnable)
                    {
                        toolBarButtonBlockOnly.Enabled = false;
                        toolBarButtonUnblockOnly.Enabled = true;
                    }
                    else
                    {
                        toolBarButtonBlockOnly.Enabled = true;
                        toolBarButtonUnblockOnly.Enabled = false;
                    }
                }
            }
            else
            {
                toolBarButtonDelProg.Enabled = false;
                toolBarButtonUnblockOnly.Enabled = false;
                toolBarButtonBlockOnly.Enabled = false;
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
                menuItemUnBlock.Text = "Remove <application>";
                menuItemBlockSelected.Enabled = false;
                menuItemUnblockSelected.Enabled = false;
            }
            else
            {
                menuItemUnBlock.Enabled = true;
                menuItemUnBlock.Text = "Remove " + listView1BlockedApplications.SelectedItems[0].Text;

                ListViewItem item = listView1BlockedApplications.SelectedItems[0];
                var rule = (IRule)item.Tag;
                var theRule = FirewallManager.Instance.Rules.SingleOrDefault(r => r.Name == rule.Name);

                if (theRule != null)
                {
                    if (theRule.IsEnable)
                    {
                        menuItemUnblockSelected.Enabled = true;
                        menuItemUnblockSelected.Text = "Unblock " + listView1BlockedApplications.SelectedItems[0].Text;

                        menuItemBlockSelected.Enabled = false;
                        menuItemBlockSelected.Text = "Block " + listView1BlockedApplications.SelectedItems[0].Text;
                    }
                    else
                    {
                        menuItemUnblockSelected.Enabled = false;
                        menuItemBlockSelected.Enabled = true;
                        menuItemBlockSelected.Text = "Block " + listView1BlockedApplications.SelectedItems[0].Text;
                        menuItemUnblockSelected.Text = "Unblock " + listView1BlockedApplications.SelectedItems[0].Text;
                    }
                }
            }

            if (listViewInternetConnectedProcesses.SelectedItems.Count == 0)
            {
                menuItemBlock.Enabled = false;
                menuItemBlock.Text = "Add <application>";
            }
            else
            {
                menuItemBlock.Enabled = true;
                menuItemBlock.Text = "Add " + listViewInternetConnectedProcesses.SelectedItems[0].Text;
            }

            menuItemEnableDisableAll.Checked = toolBarButtonRulesEnabled.Pushed;
            menuItemEnableDisableAll.Enabled = toolBarButtonRulesEnabled.Enabled;
            if (menuItemEnableDisableAll.Checked)
                menuItemEnableDisableAll.Text = "Unblock All";
            else
                menuItemEnableDisableAll.Text = "Block All";
        }

        private void MenuItemEnableDisableAll_Click(object sender, EventArgs e)
        {
            if(menuItemEnableDisableAll.Checked)
            {
                menuItemEnableDisableAll.Checked = false;
                toolBarButtonRulesEnabled.Pushed = false;
                EnableDisableRules();
            }
            else
            {
                menuItemEnableDisableAll.Checked = true;
                toolBarButtonRulesEnabled.Pushed = true;
                EnableDisableRules();
            }
        }

        private void MenuItemBlockSelected_Click(object sender, EventArgs e)
        {
            EnableDisableSelectedRule(true);
        }

        private void MenuItemUnblockSelected_Click(object sender, EventArgs e)
        {
            EnableDisableSelectedRule(false);
        }

        private void ContextMenuConnectedItems_Popup(object sender, EventArgs e)
        {
            if (listViewInternetConnectedProcesses.SelectedItems.Count > 0)
                menuItemContextBlock.Text = "Add " + listViewInternetConnectedProcesses.SelectedItems[0].Text;
        }

        private bool IsWindowsFirewallEnabled()
        {
            Type FWManagerType = Type.GetTypeFromProgID("HNetCfg.FwMgr");
            dynamic FWManager = Activator.CreateInstance(FWManagerType);

            

            return FWManager.LocalPolicy.CurrentProfile.FirewallEnabled;
        }

        private void MainWindow_Load(object sender, EventArgs e) 
        {
            statusBarPanel1.Text = "Ready";
            if (!FirewallManager.Instance.IsSupported)
            {
                Logger.Write("Firewall API not supported on this system. Abort.");
                new MessageBoxEx("ProgCop Warning", "Firewall API not supported on this system. Abort.",
                                        MessageBoxExType.Warning).ShowDialog(this);
                Application.Exit();
            }

            statusBarPanel3.Text = FirewallManager.Instance.GetProfile().ToString();
        }

        private void ResizeAutoSizeColumn(ListView listView, int autoSizeColumnIndex)
        {
            int otherColumnsWidth = 0;

            foreach (ColumnHeader header in listView.Columns)
                if (header.Index != autoSizeColumnIndex)
                    otherColumnsWidth += header.Width;

            int autoSizeColumnWidth = listView.ClientRectangle.Width - otherColumnsWidth;

            if (listView.Columns[autoSizeColumnIndex].Width != autoSizeColumnWidth)
                listView.Columns[autoSizeColumnIndex].Width = autoSizeColumnWidth;
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        { 
            ResizeAutoSizeColumn(listView1BlockedApplications, 0);
            ResizeAutoSizeColumn(listViewInternetConnectedProcesses, 0);

            if(WindowState == FormWindowState.Minimized)
            {
                if (Properties.Settings.Default.MinimizeToTray)
                {
                    ShowInTaskbar = false;
                }
            }
        }

        private void ShowSettingsDialog()
        {
            if(new SettingsDialog().ShowDialog() == DialogResult.OK)
            {
                notifyIcon1.Visible = Properties.Settings.Default.ShowInTray;
                ShowInTaskbar = true;
            }
        }

        private void MenuItemSettings_Click(object sender, EventArgs e)
        {
            ShowSettingsDialog();
        }

        private void MenuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }
    }
}

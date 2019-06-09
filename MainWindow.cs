using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace ProgCop
{
    internal partial class MainWindow : Form
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);

        private ConnectedProcessesLookup _lookup;
        private Timer _timer;

        internal MainWindow()
        {
            InitializeComponent();

            Font = SystemFonts.MessageBoxFont;
            SetWindowTheme(listViewInternetConnectedProcesses.Handle, "explorer", null);
            SetWindowTheme(listView1BlockedApplications.Handle, "explorer", null);

            _lookup = new ConnectedProcessesLookup();
            _timer = new Timer();
            _timer.Interval = 5000;
            _timer.Tick += _timer_Tick1;
            _timer.Start();
        }

        private void _timer_Tick1(object sender, EventArgs e)
        {
            listViewInternetConnectedProcesses.BeginUpdate();
            List<TcpProcessRecord> recordsTcpNew = _lookup.LookupForTcpConnectedProcesses(progressBarConnectedItems);
           
            foreach (TcpProcessRecord record in recordsTcpNew)
            {
                if (!listViewInternetConnectedProcesses.Items.ContainsKey(record.ProcessId.ToString()))
                {
                    ListViewItem itemNew = new ListViewItem(new string[] { record.ProcessName, record.LocalAddress.ToString(),
                                                                           record.RemoteAddress.ToString(), record.LocalPort.ToString(),
                                                                           record.RemotePort.ToString(), record.State.ToString(),
                                                                           record.Protocol });
                    itemNew.Tag = record.ProcessId;
                    itemNew.Name = record.ProcessId.ToString();
                    listViewInternetConnectedProcesses.Items.Add(itemNew);
                }

            }

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
                if(found == false)
                {
                    listViewInternetConnectedProcesses.Items.Remove(item);
                }
            }
            listViewInternetConnectedProcesses.EndUpdate();
        }

        private void UpdateConnectedProcessesView()
        {
            listViewInternetConnectedProcesses.Items.Clear();
            toolBarButtonRefreshConnected.Enabled = false;
            progressBarConnectedItems.Visible = true;

            List<TcpProcessRecord> tcpRecords = _lookup.LookupForTcpConnectedProcesses(progressBarConnectedItems);
            List<UdpProcessRecord> udpRecords = _lookup.LookupForUdpConnectedProcesses(progressBarConnectedItems);

            foreach(TcpProcessRecord record in tcpRecords)
            {
                ListViewItem item = new ListViewItem(new string[] { record.ProcessName, record.LocalAddress.ToString(), record.RemoteAddress.ToString(),
                record.LocalPort.ToString(), record.RemotePort.ToString(), record.State.ToString(), record.Protocol });
                item.Tag = record.ProcessId;
                item.Name = record.ProcessId.ToString();
                listViewInternetConnectedProcesses.Items.Add(item);
            }

            foreach (UdpProcessRecord record in udpRecords)
            {
                ListViewItem item = new ListViewItem(new string[] { record.ProcessName, record.LocalAddress.ToString(), "",
                record.LocalPort.ToString(), "", "", record.Protocol });
                item.Tag = record.ProcessId;
                item.Name = record.ProcessId.ToString();
                listViewInternetConnectedProcesses.Items.Add(item);
            }

            progressBarConnectedItems.Visible = false;
            toolBarButtonRefreshConnected.Enabled = true;
            listViewInternetConnectedProcesses.Sorting = SortOrder.Ascending;
            listViewInternetConnectedProcesses.Sort();

            //For some reason after calling Sort() for the listview, we need to set the native theme again
            SetWindowTheme(listViewInternetConnectedProcesses.Handle, "explorer", null);
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            UpdateConnectedProcessesView();
        }

        private void ToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch(e.Button.Name)
            {
                case "toolBarButtonAddProg":
                    break;
                case "toolBarButtonDelProg":
                    break;
                case "toolBarButtonSettings":
                    break;
                case "toolBarButtonRefreshConnected":
                    UpdateConnectedProcessesView();
                    break;
            }
        }

        private void MenuItemContextBlock_Click(object sender, EventArgs e)
        {

        }

        private void MenuItemUnblock_Click(object sender, EventArgs e)
        {

        }

        private void MenuItemOpenFileLocation_Click(object sender, EventArgs e)
        {

        }
    }
}

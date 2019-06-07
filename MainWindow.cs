using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProgCop
{
    internal partial class MainWindow : Form
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);

        private ConnectedProcessesLookup _lookup;

        internal MainWindow()
        {
            InitializeComponent();

            Font = SystemFonts.MessageBoxFont;
            SetWindowTheme(listViewInternetConnectedProcesses.Handle, "explorer", null);
            SetWindowTheme(listView1BlockedApplications.Handle, "explorer", null);

            _lookup = new ConnectedProcessesLookup();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            UpdateConnectedProcessesView();
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
                item.Tag = record.ProcessFullPath;
                listViewInternetConnectedProcesses.Items.Add(item);
            }

            foreach (UdpProcessRecord record in udpRecords)
            {
                ListViewItem item = new ListViewItem(new string[] { record.ProcessName, record.LocalAddress.ToString(), "",
                record.LocalPort.ToString(), "", "", record.Protocol });
                item.Tag = record.ProcessFullPath;
                listViewInternetConnectedProcesses.Items.Add(item);
            }

            progressBarConnectedItems.Visible = false;
            toolBarButtonRefreshConnected.Enabled = true;
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
                case "toolBarButtonBlockApplication":
                    break;
                case "toolBarButtonUnblockApplication":
                    break;
                case "toolBarButtonRefreshConnected":
                    UpdateConnectedProcessesView();
                    break;
                    

            }
        }
    }
}

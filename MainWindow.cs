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

        private void UpdateConnectedProcessesView()
        {
            listViewInternetConnectedProcesses.Items.Clear();

            List<TcpProcessRecord> tcpProcesses = _lookup.LookupForTcpConnectedProcesses();
            List<UdpProcessRecord> udpProcesses = _lookup.LookupForUdpConnectedProcesses();

            if(tcpProcesses != null)
            {
                foreach (TcpProcessRecord record in tcpProcesses)
                {
                    ListViewItem item = new ListViewItem(new string[] { record.ProcessName, record.LocalAddress.ToString(), record.RemoteAddress.ToString(),
                                                                        record.LocalPort.ToString(), record.RemotePort.ToString(), record.State.ToString() });

                    item.Tag = record.ProcessFullPath;
                    listViewInternetConnectedProcesses.Items.Add(item);
                }
            }

            if(udpProcesses != null)
            {
                foreach (UdpProcessRecord record in udpProcesses)
                {
                    ListViewItem item = new ListViewItem(new string[] { record.ProcessName, record.LocalAddress.ToString(), "",
                                                                        record.LocalPort.ToString(), "", "" });

                    item.Tag = record.ProcessFullPath;
                    listViewInternetConnectedProcesses.Items.Add(item);
                }
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            UpdateConnectedProcessesView();
            Cursor = Cursors.Default;
        }
    }
}

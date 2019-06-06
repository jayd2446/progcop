using System;
using System.Drawing;
using System.Runtime.InteropServices;
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

            foreach(TcpProcessRecord record in _lookup.LookupForTcpConnectedProcesses())
            {
                ListViewItem item = new ListViewItem(new string[] { record.ProcessName, record.LocalAddress.ToString(), record.RemoteAddress.ToString(),
                    record.LocalPort.ToString(), record.RemotePort.ToString(), record.State.ToString() });

                listViewInternetConnectedProcesses.Items.Add(item);
            }

            
        }

      

     
    }
}

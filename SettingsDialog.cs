using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProgCop
{
    internal partial class SettingsDialog : Form
    {
        internal SettingsDialog()
        {
            InitializeComponent();
            Font = SystemFonts.MessageBoxFont;

            checkBoxipv6.Checked = Properties.Settings.Default.UseIPV6;
            checkboxMinToTray.Checked = Properties.Settings.Default.MinimizeToTray;
            checkBoxShowInTray.Checked = Properties.Settings.Default.ShowInTray;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UseIPV6 = checkBoxipv6.Checked;
            Properties.Settings.Default.MinimizeToTray = checkboxMinToTray.Checked;
            Properties.Settings.Default.ShowInTray = checkBoxShowInTray.Checked;

            Properties.Settings.Default.Save();
            
            DialogResult = DialogResult.OK;
        }
    }
}

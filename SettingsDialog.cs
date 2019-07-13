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

            HandleMinToTrayCheckbox();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UseIPV6 = checkBoxipv6.Checked;
            Properties.Settings.Default.MinimizeToTray = checkboxMinToTray.Checked;
            Properties.Settings.Default.ShowInTray = checkBoxShowInTray.Checked;

            Properties.Settings.Default.Save();
            
            DialogResult = DialogResult.OK;
        }

        private void HandleMinToTrayCheckbox()
        {
            if (!checkBoxShowInTray.Checked)
            {
                checkboxMinToTray.Checked = false;
                checkboxMinToTray.Enabled = false;
            }
            else
            {
                checkboxMinToTray.Enabled = true;
                checkboxMinToTray.Checked = Properties.Settings.Default.MinimizeToTray;

            }
        }

        private void CheckBoxShowInTray_CheckedChanged(object sender, EventArgs e)
        {
            HandleMinToTrayCheckbox();
        }
    }
}

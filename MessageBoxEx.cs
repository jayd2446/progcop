using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProgCop
{
    internal enum MessageBoxExType
    {
        Warning = 1,
        Information = 2,
    }

    internal partial class MessageBoxEx : Form
    {
        internal MessageBoxEx(string title, string text, MessageBoxExType type)
        {
            InitializeComponent();

            this.Font = SystemFonts.MessageBoxFont;
            this.Text = title;

            //Setting maximum size will allow label to wrap text no multiple lines.
            this.labelText.MaximumSize = new Size(250, 0);
            this.labelText.AutoSize = true;
            this.labelText.Text = text;
            
            switch(type)
            {
                case MessageBoxExType.Information:
                    pictureBoxTypeIcon.Image = Properties.Resources.info;
                    break;
                case MessageBoxExType.Warning:
                    pictureBoxTypeIcon.Image = Properties.Resources.warning;
                    break;
            }

            this.CenterToParent();
        }
    }
}

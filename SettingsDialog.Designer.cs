namespace ProgCop
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxipv6 = new System.Windows.Forms.CheckBox();
            this.checkBoxShowInTray = new System.Windows.Forms.CheckBox();
            this.checkboxMinToTray = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(286, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // checkBoxipv6
            // 
            this.checkBoxipv6.AutoSize = true;
            this.checkBoxipv6.Location = new System.Drawing.Point(22, 28);
            this.checkBoxipv6.Name = "checkBoxipv6";
            this.checkBoxipv6.Size = new System.Drawing.Size(70, 17);
            this.checkBoxipv6.TabIndex = 1;
            this.checkBoxipv6.Text = "Use IPv6";
            this.checkBoxipv6.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowInTray
            // 
            this.checkBoxShowInTray.AutoSize = true;
            this.checkBoxShowInTray.Location = new System.Drawing.Point(19, 30);
            this.checkBoxShowInTray.Name = "checkBoxShowInTray";
            this.checkBoxShowInTray.Size = new System.Drawing.Size(119, 17);
            this.checkBoxShowInTray.TabIndex = 2;
            this.checkBoxShowInTray.Text = "Show in system tray";
            this.checkBoxShowInTray.UseVisualStyleBackColor = true;
            this.checkBoxShowInTray.CheckedChanged += new System.EventHandler(this.CheckBoxShowInTray_CheckedChanged);
            // 
            // checkboxMinToTray
            // 
            this.checkboxMinToTray.AutoSize = true;
            this.checkboxMinToTray.Location = new System.Drawing.Point(19, 53);
            this.checkboxMinToTray.Name = "checkboxMinToTray";
            this.checkboxMinToTray.Size = new System.Drawing.Size(133, 17);
            this.checkboxMinToTray.TabIndex = 3;
            this.checkboxMinToTray.Text = "Minimize to system tray";
            this.checkboxMinToTray.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxipv6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 75);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Networking";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxShowInTray);
            this.groupBox2.Controls.Add(this.checkboxMinToTray);
            this.groupBox2.Location = new System.Drawing.Point(12, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Window";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(286, 40);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(371, 203);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProgCop Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxipv6;
        private System.Windows.Forms.CheckBox checkBoxShowInTray;
        private System.Windows.Forms.CheckBox checkboxMinToTray;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCancel;
    }
}
namespace ProgCop
{
    partial class MessageBoxEx
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
            this.labelText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxTypeIcon = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTypeIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(264, 8);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(118, 13);
            this.labelText.MaximumSize = new System.Drawing.Size(200, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(35, 13);
            this.labelText.TabIndex = 3;
            this.labelText.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Location = new System.Drawing.Point(1, 124);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 42);
            this.panel1.TabIndex = 4;
            // 
            // pictureBoxTypeIcon
            // 
            this.pictureBoxTypeIcon.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxTypeIcon.Name = "pictureBoxTypeIcon";
            this.pictureBoxTypeIcon.Size = new System.Drawing.Size(100, 84);
            this.pictureBoxTypeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxTypeIcon.TabIndex = 2;
            this.pictureBoxTypeIcon.TabStop = false;
            // 
            // MessageBoxEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(355, 166);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.pictureBoxTypeIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxEx";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageBoxEx";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTypeIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.PictureBox pictureBoxTypeIcon;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Panel panel1;
    }
}
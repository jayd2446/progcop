namespace ProgCop
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.imageList1Toolbar = new System.Windows.Forms.ImageList(this.components);
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "File";
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1});
            this.toolBar1.ButtonSize = new System.Drawing.Size(32, 32);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1Toolbar;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Margin = new System.Windows.Forms.Padding(4);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(868, 44);
            this.toolBar1.TabIndex = 1;
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 0;
            this.toolBarButton1.Name = "toolBarButton1";
            // 
            // imageList1Toolbar
            // 
            this.imageList1Toolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1Toolbar.ImageStream")));
            this.imageList1Toolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1Toolbar.Images.SetKeyName(0, "fire.png");
            this.imageList1Toolbar.Images.SetKeyName(1, "fire_extinguisher.png");
            // 
            // statusBar1
            // 
            this.statusBar1.CausesValidation = false;
            this.statusBar1.Location = new System.Drawing.Point(0, 766);
            this.statusBar1.Margin = new System.Windows.Forms.Padding(4);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(868, 27);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "Ready";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(868, 793);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.toolBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "ProgCop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ImageList imageList1Toolbar;
        private System.Windows.Forms.StatusBar statusBar1;
    }
}


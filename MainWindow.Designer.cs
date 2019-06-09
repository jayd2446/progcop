namespace ProgCop
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButtonAddProg = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonDelProg = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSeparator = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSettings = new System.Windows.Forms.ToolBarButton();
            this.imageList1Toolbar = new System.Windows.Forms.ImageList(this.components);
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.listView1BlockedApplications = new System.Windows.Forms.ListView();
            this.columnHeaderBlockedApplicationName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderBlockedState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewInternetConnectedProcesses = new System.Windows.Forms.ListView();
            this.columnHeaderProcessName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocalAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRemote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocalPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRemotePort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuConnectedItems = new System.Windows.Forms.ContextMenu();
            this.menuItemContextBlock = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemContextOpenFileLocation = new System.Windows.Forms.MenuItem();
            this.contextMenuBlockedApplications = new System.Windows.Forms.ContextMenu();
            this.menuItemContextUnblock = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.toolBarButtonAddProg,
            this.toolBarButtonDelProg,
            this.toolBarButtonSeparator,
            this.toolBarButtonSettings});
            this.toolBar1.ButtonSize = new System.Drawing.Size(32, 32);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1Toolbar;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(716, 44);
            this.toolBar1.TabIndex = 1;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBar1_ButtonClick);
            // 
            // toolBarButtonAddProg
            // 
            this.toolBarButtonAddProg.ImageIndex = 0;
            this.toolBarButtonAddProg.Name = "toolBarButtonAddProg";
            this.toolBarButtonAddProg.ToolTipText = "Block an application";
            // 
            // toolBarButtonDelProg
            // 
            this.toolBarButtonDelProg.ImageIndex = 2;
            this.toolBarButtonDelProg.Name = "toolBarButtonDelProg";
            this.toolBarButtonDelProg.ToolTipText = "Unblock an application";
            // 
            // toolBarButtonSeparator
            // 
            this.toolBarButtonSeparator.Name = "toolBarButtonSeparator";
            this.toolBarButtonSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonSettings
            // 
            this.toolBarButtonSettings.ImageIndex = 1;
            this.toolBarButtonSettings.Name = "toolBarButtonSettings";
            this.toolBarButtonSettings.ToolTipText = "ProgCop settings";
            // 
            // imageList1Toolbar
            // 
            this.imageList1Toolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1Toolbar.ImageStream")));
            this.imageList1Toolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1Toolbar.Images.SetKeyName(0, "application_add.png");
            this.imageList1Toolbar.Images.SetKeyName(1, "setting_tools.png");
            this.imageList1Toolbar.Images.SetKeyName(2, "application_delete.png");
            // 
            // statusBar1
            // 
            this.statusBar1.CausesValidation = false;
            this.statusBar1.Location = new System.Drawing.Point(0, 762);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(716, 22);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "Ready";
            // 
            // listView1BlockedApplications
            // 
            this.listView1BlockedApplications.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1BlockedApplications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderBlockedApplicationName,
            this.columnHeaderBlockedState});
            this.listView1BlockedApplications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1BlockedApplications.FullRowSelect = true;
            this.listView1BlockedApplications.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1BlockedApplications.HideSelection = false;
            this.listView1BlockedApplications.Location = new System.Drawing.Point(0, 0);
            this.listView1BlockedApplications.MultiSelect = false;
            this.listView1BlockedApplications.Name = "listView1BlockedApplications";
            this.listView1BlockedApplications.ShowGroups = false;
            this.listView1BlockedApplications.Size = new System.Drawing.Size(716, 243);
            this.listView1BlockedApplications.TabIndex = 0;
            this.listView1BlockedApplications.UseCompatibleStateImageBehavior = false;
            this.listView1BlockedApplications.View = System.Windows.Forms.View.Details;
            this.listView1BlockedApplications.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView1BlockedApplications_MouseClick);
            // 
            // columnHeaderBlockedApplicationName
            // 
            this.columnHeaderBlockedApplicationName.Text = "Application";
            this.columnHeaderBlockedApplicationName.Width = 550;
            // 
            // columnHeaderBlockedState
            // 
            this.columnHeaderBlockedState.Text = "State";
            this.columnHeaderBlockedState.Width = 120;
            // 
            // listViewInternetConnectedProcesses
            // 
            this.listViewInternetConnectedProcesses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewInternetConnectedProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderProcessName,
            this.columnHeaderLocalAddr,
            this.columnHeaderRemote,
            this.columnHeaderLocalPort,
            this.columnHeaderRemotePort,
            this.columnHeaderState,
            this.columnHeaderPID});
            this.listViewInternetConnectedProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewInternetConnectedProcesses.FullRowSelect = true;
            this.listViewInternetConnectedProcesses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewInternetConnectedProcesses.HideSelection = false;
            this.listViewInternetConnectedProcesses.Location = new System.Drawing.Point(0, 0);
            this.listViewInternetConnectedProcesses.Name = "listViewInternetConnectedProcesses";
            this.listViewInternetConnectedProcesses.ShowGroups = false;
            this.listViewInternetConnectedProcesses.Size = new System.Drawing.Size(716, 471);
            this.listViewInternetConnectedProcesses.TabIndex = 0;
            this.listViewInternetConnectedProcesses.UseCompatibleStateImageBehavior = false;
            this.listViewInternetConnectedProcesses.View = System.Windows.Forms.View.Details;
            this.listViewInternetConnectedProcesses.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListViewInternetConnectedProcesses_MouseClick);
            // 
            // columnHeaderProcessName
            // 
            this.columnHeaderProcessName.Text = "Process name";
            this.columnHeaderProcessName.Width = 180;
            // 
            // columnHeaderLocalAddr
            // 
            this.columnHeaderLocalAddr.Text = "Local addr";
            this.columnHeaderLocalAddr.Width = 100;
            // 
            // columnHeaderRemote
            // 
            this.columnHeaderRemote.Text = "Remote addr";
            this.columnHeaderRemote.Width = 100;
            // 
            // columnHeaderLocalPort
            // 
            this.columnHeaderLocalPort.Text = "Local port";
            this.columnHeaderLocalPort.Width = 80;
            // 
            // columnHeaderRemotePort
            // 
            this.columnHeaderRemotePort.Text = "Remote port";
            this.columnHeaderRemotePort.Width = 80;
            // 
            // columnHeaderState
            // 
            this.columnHeaderState.Text = "State";
            this.columnHeaderState.Width = 90;
            // 
            // columnHeaderPID
            // 
            this.columnHeaderPID.Text = "PID";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 44);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1BlockedApplications);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewInternetConnectedProcesses);
            this.splitContainer1.Size = new System.Drawing.Size(716, 718);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.TabIndex = 3;
            // 
            // contextMenuConnectedItems
            // 
            this.contextMenuConnectedItems.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemContextBlock,
            this.menuItem4,
            this.menuItemContextOpenFileLocation});
            // 
            // menuItemContextBlock
            // 
            this.menuItemContextBlock.Index = 0;
            this.menuItemContextBlock.Text = "Block";
            this.menuItemContextBlock.Click += new System.EventHandler(this.MenuItemContextBlock_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "-";
            // 
            // menuItemContextOpenFileLocation
            // 
            this.menuItemContextOpenFileLocation.Index = 2;
            this.menuItemContextOpenFileLocation.Text = "Open file location";
            this.menuItemContextOpenFileLocation.Click += new System.EventHandler(this.MenuItemContextOpenFileLocation_Click);
            // 
            // contextMenuBlockedApplications
            // 
            this.contextMenuBlockedApplications.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemContextUnblock});
            // 
            // menuItemContextUnblock
            // 
            this.menuItemContextUnblock.Index = 0;
            this.menuItemContextUnblock.Text = "Unblock";
            this.menuItemContextUnblock.Click += new System.EventHandler(this.MenuItemContextUnblock_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(716, 784);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.toolBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "MainWindow";
            this.Text = "ProgCop";
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton toolBarButtonAddApplication;
        private System.Windows.Forms.ImageList imageList1Toolbar;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.ToolBarButton toolBarButtonAddProg;
        private System.Windows.Forms.ToolBarButton toolBarButtonSeparator;
        private System.Windows.Forms.ListView listView1BlockedApplications;
        private System.Windows.Forms.ListView listViewInternetConnectedProcesses;
        private System.Windows.Forms.ToolBarButton toolBarButtonDelProg;
        private System.Windows.Forms.ColumnHeader columnHeaderProcessName;
        private System.Windows.Forms.ColumnHeader columnHeaderLocalAddr;
        private System.Windows.Forms.ColumnHeader columnHeaderRemote;
        private System.Windows.Forms.ColumnHeader columnHeaderLocalPort;
        private System.Windows.Forms.ColumnHeader columnHeaderRemotePort;
        private System.Windows.Forms.ColumnHeader columnHeaderState;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader columnHeaderPID;
        private System.Windows.Forms.ContextMenu contextMenuConnectedItems;
        private System.Windows.Forms.MenuItem menuItemContextBlock;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemContextOpenFileLocation;
        private System.Windows.Forms.ColumnHeader columnHeaderBlockedApplicationName;
        private System.Windows.Forms.ColumnHeader columnHeaderBlockedState;
        private System.Windows.Forms.ToolBarButton toolBarButtonSettings;
        private System.Windows.Forms.ContextMenu contextMenuBlockedApplications;
        private System.Windows.Forms.MenuItem menuItemContextUnblock;
    }
}


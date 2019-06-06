﻿namespace ProgCop
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
            this.toolBarButtonBlockApplication = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonUnblockApplication = new System.Windows.Forms.ToolBarButton();
            this.imageList1Toolbar = new System.Windows.Forms.ImageList(this.components);
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.splitContainerListviews = new System.Windows.Forms.SplitContainer();
            this.listView1BlockedApplications = new System.Windows.Forms.ListView();
            this.listViewInternetConnectedProcesses = new System.Windows.Forms.ListView();
            this.columnHeaderProcessName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocalAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRemote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocalPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRemotePort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerListviews)).BeginInit();
            this.splitContainerListviews.Panel1.SuspendLayout();
            this.splitContainerListviews.Panel2.SuspendLayout();
            this.splitContainerListviews.SuspendLayout();
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
            this.toolBarButtonBlockApplication,
            this.toolBarButtonUnblockApplication});
            this.toolBar1.ButtonSize = new System.Drawing.Size(32, 32);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1Toolbar;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(770, 44);
            this.toolBar1.TabIndex = 1;
            // 
            // toolBarButtonAddProg
            // 
            this.toolBarButtonAddProg.ImageIndex = 0;
            this.toolBarButtonAddProg.Name = "toolBarButtonAddProg";
            this.toolBarButtonAddProg.ToolTipText = "Add new rule";
            // 
            // toolBarButtonDelProg
            // 
            this.toolBarButtonDelProg.ImageIndex = 4;
            this.toolBarButtonDelProg.Name = "toolBarButtonDelProg";
            this.toolBarButtonDelProg.ToolTipText = "Delete rule";
            // 
            // toolBarButtonSeparator
            // 
            this.toolBarButtonSeparator.Name = "toolBarButtonSeparator";
            this.toolBarButtonSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonBlockApplication
            // 
            this.toolBarButtonBlockApplication.ImageIndex = 1;
            this.toolBarButtonBlockApplication.Name = "toolBarButtonBlockApplication";
            this.toolBarButtonBlockApplication.ToolTipText = "Block application";
            // 
            // toolBarButtonUnblockApplication
            // 
            this.toolBarButtonUnblockApplication.ImageIndex = 2;
            this.toolBarButtonUnblockApplication.Name = "toolBarButtonUnblockApplication";
            this.toolBarButtonUnblockApplication.ToolTipText = "Unblock application";
            // 
            // imageList1Toolbar
            // 
            this.imageList1Toolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1Toolbar.ImageStream")));
            this.imageList1Toolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1Toolbar.Images.SetKeyName(0, "application_add.png");
            this.imageList1Toolbar.Images.SetKeyName(1, "firewall.png");
            this.imageList1Toolbar.Images.SetKeyName(2, "globe_africa.png");
            this.imageList1Toolbar.Images.SetKeyName(3, "setting_tools.png");
            this.imageList1Toolbar.Images.SetKeyName(4, "application_delete.png");
            // 
            // statusBar1
            // 
            this.statusBar1.CausesValidation = false;
            this.statusBar1.Location = new System.Drawing.Point(0, 631);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(770, 22);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "Ready";
            // 
            // splitContainerListviews
            // 
            this.splitContainerListviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerListviews.Location = new System.Drawing.Point(0, 44);
            this.splitContainerListviews.Name = "splitContainerListviews";
            this.splitContainerListviews.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerListviews.Panel1
            // 
            this.splitContainerListviews.Panel1.Controls.Add(this.listView1BlockedApplications);
            this.splitContainerListviews.Panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            // 
            // splitContainerListviews.Panel2
            // 
            this.splitContainerListviews.Panel2.Controls.Add(this.listViewInternetConnectedProcesses);
            this.splitContainerListviews.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.splitContainerListviews.Size = new System.Drawing.Size(770, 587);
            this.splitContainerListviews.SplitterDistance = 238;
            this.splitContainerListviews.TabIndex = 3;
            // 
            // listView1BlockedApplications
            // 
            this.listView1BlockedApplications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1BlockedApplications.FullRowSelect = true;
            this.listView1BlockedApplications.HideSelection = false;
            this.listView1BlockedApplications.Location = new System.Drawing.Point(3, 0);
            this.listView1BlockedApplications.Name = "listView1BlockedApplications";
            this.listView1BlockedApplications.ShowGroups = false;
            this.listView1BlockedApplications.Size = new System.Drawing.Size(764, 238);
            this.listView1BlockedApplications.TabIndex = 0;
            this.listView1BlockedApplications.UseCompatibleStateImageBehavior = false;
            this.listView1BlockedApplications.View = System.Windows.Forms.View.Details;
            // 
            // listViewInternetConnectedProcesses
            // 
            this.listViewInternetConnectedProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderProcessName,
            this.columnHeaderLocalAddr,
            this.columnHeaderRemote,
            this.columnHeaderLocalPort,
            this.columnHeaderRemotePort,
            this.columnHeaderState});
            this.listViewInternetConnectedProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewInternetConnectedProcesses.FullRowSelect = true;
            this.listViewInternetConnectedProcesses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewInternetConnectedProcesses.HideSelection = false;
            this.listViewInternetConnectedProcesses.Location = new System.Drawing.Point(3, 0);
            this.listViewInternetConnectedProcesses.Name = "listViewInternetConnectedProcesses";
            this.listViewInternetConnectedProcesses.ShowGroups = false;
            this.listViewInternetConnectedProcesses.Size = new System.Drawing.Size(764, 345);
            this.listViewInternetConnectedProcesses.TabIndex = 0;
            this.listViewInternetConnectedProcesses.UseCompatibleStateImageBehavior = false;
            this.listViewInternetConnectedProcesses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderProcessName
            // 
            this.columnHeaderProcessName.Text = "Process name";
            this.columnHeaderProcessName.Width = 250;
            // 
            // columnHeaderLocalAddr
            // 
            this.columnHeaderLocalAddr.Text = "Local";
            this.columnHeaderLocalAddr.Width = 80;
            // 
            // columnHeaderRemote
            // 
            this.columnHeaderRemote.Text = "Remote";
            this.columnHeaderRemote.Width = 80;
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
            this.columnHeaderState.Width = 100;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(770, 653);
            this.Controls.Add(this.splitContainerListviews);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.toolBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "MainWindow";
            this.Text = "ProgCop";
            this.splitContainerListviews.Panel1.ResumeLayout(false);
            this.splitContainerListviews.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerListviews)).EndInit();
            this.splitContainerListviews.ResumeLayout(false);
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
        private System.Windows.Forms.ToolBarButton toolBarButtonBlockApplication;
        private System.Windows.Forms.ToolBarButton toolBarButtonUnblockApplication;
        private System.Windows.Forms.ToolBarButton toolBarButtonAddProg;
        private System.Windows.Forms.ToolBarButton toolBarButtonSeparator;
        private System.Windows.Forms.SplitContainer splitContainerListviews;
        private System.Windows.Forms.ListView listView1BlockedApplications;
        private System.Windows.Forms.ListView listViewInternetConnectedProcesses;
        private System.Windows.Forms.ToolBarButton toolBarButtonDelProg;
        private System.Windows.Forms.ColumnHeader columnHeaderProcessName;
        private System.Windows.Forms.ColumnHeader columnHeaderLocalAddr;
        private System.Windows.Forms.ColumnHeader columnHeaderRemote;
        private System.Windows.Forms.ColumnHeader columnHeaderLocalPort;
        private System.Windows.Forms.ColumnHeader columnHeaderRemotePort;
        private System.Windows.Forms.ColumnHeader columnHeaderState;
    }
}


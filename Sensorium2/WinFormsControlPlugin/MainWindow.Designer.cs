/*	This file is part of Sensorium2 <http://code.google.com/p/sensorium>
 * 
 *	Copyright (C) 2009-2010 Aaron Maslen
 *	This program is free software: you can redistribute it and/or modify it 
 *	under the terms of the GNU General Public License as published by 
 *	the Free Software Foundation, either version 3 of the License, or 
 *	(at your option) any later version. This program is distributed in the 
 *	hope that it will be useful, but WITHOUT ANY WARRANTY; without 
 *	even the implied warranty of MERCHANTABILITY or FITNESS FOR 
 *	A PARTICULAR PURPOSE. See the GNU General Public License 
 *	for more details. You should have received a copy of the GNU General 
 *	Public License along with this program. If not, see <http://www.gnu.org/licenses/>
 */

namespace WinFormsControlPlugin {
	partial class MainWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ColumnHeader columnHeaderEnabled;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.tabLog = new System.Windows.Forms.TabPage();
			this.listBoxLog = new System.Windows.Forms.ListBox();
			this.tabSensors = new System.Windows.Forms.TabPage();
			this.tabsMain = new System.Windows.Forms.TabControl();
			this.tabPlugins = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelPluginsTab = new System.Windows.Forms.TableLayoutPanel();
			this.listViewPlugins = new System.Windows.Forms.ListView();
			this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.imageListPluginView = new System.Windows.Forms.ImageList(this.components);
			this.labelPluginName = new System.Windows.Forms.Label();
			this.labelPluginVersion = new System.Windows.Forms.Label();
			this.labelPluginDescription = new System.Windows.Forms.Label();
			this.toolStripMainWindow = new System.Windows.Forms.ToolStrip();
			this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
			this.buttonAbout = new System.Windows.Forms.ToolStripButton();
			this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
			this.buttonEnable = new System.Windows.Forms.ToolStripButton();
			this.buttonDisable = new System.Windows.Forms.ToolStripButton();
			columnHeaderEnabled = new System.Windows.Forms.ColumnHeader();
			this.tabLog.SuspendLayout();
			this.tabsMain.SuspendLayout();
			this.tabPlugins.SuspendLayout();
			this.tableLayoutPanelPluginsTab.SuspendLayout();
			this.toolStripMainWindow.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabLog
			// 
			this.tabLog.Controls.Add(this.listBoxLog);
			this.tabLog.Location = new System.Drawing.Point(4, 22);
			this.tabLog.Name = "tabLog";
			this.tabLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabLog.Size = new System.Drawing.Size(509, 363);
			this.tabLog.TabIndex = 1;
			this.tabLog.Text = "Application Log";
			this.tabLog.UseVisualStyleBackColor = true;
			// 
			// listBoxLog
			// 
			this.listBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxLog.FormattingEnabled = true;
			this.listBoxLog.HorizontalScrollbar = true;
			this.listBoxLog.Location = new System.Drawing.Point(3, 3);
			this.listBoxLog.Name = "listBoxLog";
			this.listBoxLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBoxLog.Size = new System.Drawing.Size(503, 351);
			this.listBoxLog.TabIndex = 0;
			// 
			// tabSensors
			// 
			this.tabSensors.Location = new System.Drawing.Point(4, 22);
			this.tabSensors.Name = "tabSensors";
			this.tabSensors.Padding = new System.Windows.Forms.Padding(3);
			this.tabSensors.Size = new System.Drawing.Size(509, 363);
			this.tabSensors.TabIndex = 0;
			this.tabSensors.Text = "Sensors";
			this.tabSensors.UseVisualStyleBackColor = true;
			// 
			// tabsMain
			// 
			this.tabsMain.Controls.Add(this.tabSensors);
			this.tabsMain.Controls.Add(this.tabLog);
			this.tabsMain.Controls.Add(this.tabPlugins);
			this.tabsMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabsMain.Location = new System.Drawing.Point(0, 25);
			this.tabsMain.Multiline = true;
			this.tabsMain.Name = "tabsMain";
			this.tabsMain.SelectedIndex = 0;
			this.tabsMain.Size = new System.Drawing.Size(517, 389);
			this.tabsMain.TabIndex = 1;
			this.tabsMain.SelectedIndexChanged += new System.EventHandler(this.tabsMain_SelectedIndexChanged);
			// 
			// tabPlugins
			// 
			this.tabPlugins.Controls.Add(this.tableLayoutPanelPluginsTab);
			this.tabPlugins.Location = new System.Drawing.Point(4, 22);
			this.tabPlugins.Name = "tabPlugins";
			this.tabPlugins.Padding = new System.Windows.Forms.Padding(3);
			this.tabPlugins.Size = new System.Drawing.Size(509, 363);
			this.tabPlugins.TabIndex = 2;
			this.tabPlugins.Text = "Plugins";
			this.tabPlugins.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelPluginsTab
			// 
			this.tableLayoutPanelPluginsTab.ColumnCount = 2;
			this.tableLayoutPanelPluginsTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tableLayoutPanelPluginsTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanelPluginsTab.Controls.Add(this.listViewPlugins, 0, 0);
			this.tableLayoutPanelPluginsTab.Controls.Add(this.labelPluginName, 1, 0);
			this.tableLayoutPanelPluginsTab.Controls.Add(this.labelPluginVersion, 1, 1);
			this.tableLayoutPanelPluginsTab.Controls.Add(this.labelPluginDescription, 1, 2);
			this.tableLayoutPanelPluginsTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelPluginsTab.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanelPluginsTab.Name = "tableLayoutPanelPluginsTab";
			this.tableLayoutPanelPluginsTab.RowCount = 3;
			this.tableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelPluginsTab.Size = new System.Drawing.Size(503, 357);
			this.tableLayoutPanelPluginsTab.TabIndex = 1;
			// 
			// listViewPlugins
			// 
			this.listViewPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            columnHeaderEnabled,
            this.columnHeaderType});
			this.listViewPlugins.FullRowSelect = true;
			this.listViewPlugins.Location = new System.Drawing.Point(3, 3);
			this.listViewPlugins.Name = "listViewPlugins";
			this.tableLayoutPanelPluginsTab.SetRowSpan(this.listViewPlugins, 3);
			this.listViewPlugins.ShowGroups = false;
			this.listViewPlugins.Size = new System.Drawing.Size(295, 351);
			this.listViewPlugins.SmallImageList = this.imageListPluginView;
			this.listViewPlugins.TabIndex = 0;
			this.listViewPlugins.UseCompatibleStateImageBehavior = false;
			this.listViewPlugins.View = System.Windows.Forms.View.Details;
			this.listViewPlugins.SelectedIndexChanged += new System.EventHandler(this.listViewPlugins_SelectedIndexChanged);
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = 160;
			// 
			// columnHeaderEnabled
			// 
			columnHeaderEnabled.Text = "Enabled";
			// 
			// imageListPluginView
			// 
			this.imageListPluginView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPluginView.ImageStream")));
			this.imageListPluginView.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListPluginView.Images.SetKeyName(0, "plugin");
			this.imageListPluginView.Images.SetKeyName(1, "plugin_disabled");
			this.imageListPluginView.Images.SetKeyName(2, "arrow_refresh_small");
			// 
			// labelPluginName
			// 
			this.labelPluginName.AutoSize = true;
			this.labelPluginName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPluginName.Location = new System.Drawing.Point(304, 0);
			this.labelPluginName.Name = "labelPluginName";
			this.labelPluginName.Size = new System.Drawing.Size(196, 20);
			this.labelPluginName.TabIndex = 1;
			this.labelPluginName.Text = "Name:";
			// 
			// labelPluginVersion
			// 
			this.labelPluginVersion.AutoSize = true;
			this.labelPluginVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPluginVersion.Location = new System.Drawing.Point(304, 20);
			this.labelPluginVersion.Name = "labelPluginVersion";
			this.labelPluginVersion.Size = new System.Drawing.Size(196, 20);
			this.labelPluginVersion.TabIndex = 2;
			this.labelPluginVersion.Text = "Version:";
			// 
			// labelPluginDescription
			// 
			this.labelPluginDescription.AutoSize = true;
			this.labelPluginDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPluginDescription.Location = new System.Drawing.Point(304, 40);
			this.labelPluginDescription.Name = "labelPluginDescription";
			this.labelPluginDescription.Size = new System.Drawing.Size(196, 317);
			this.labelPluginDescription.TabIndex = 3;
			this.labelPluginDescription.Text = "Description:";
			// 
			// toolStripMainWindow
			// 
			this.toolStripMainWindow.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripMainWindow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAbout,
            this.buttonRefresh,
            this.buttonEnable,
            this.buttonDisable});
			this.toolStripMainWindow.Location = new System.Drawing.Point(0, 0);
			this.toolStripMainWindow.Name = "toolStripMainWindow";
			this.toolStripMainWindow.Size = new System.Drawing.Size(517, 25);
			this.toolStripMainWindow.TabIndex = 0;
			// 
			// columnHeaderType
			// 
			this.columnHeaderType.Text = "Type";
			// 
			// buttonAbout
			// 
			this.buttonAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.buttonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.buttonAbout.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbout.Image")));
			this.buttonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonAbout.Name = "buttonAbout";
			this.buttonAbout.Size = new System.Drawing.Size(53, 22);
			this.buttonAbout.Text = "About...";
			this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Image = global::WinFormsControlPlugin.Properties.Resources.arrow_refresh;
			this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(66, 22);
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.Visible = false;
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// buttonEnable
			// 
			this.buttonEnable.Image = ((System.Drawing.Image)(resources.GetObject("buttonEnable.Image")));
			this.buttonEnable.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonEnable.Name = "buttonEnable";
			this.buttonEnable.Size = new System.Drawing.Size(62, 22);
			this.buttonEnable.Text = "Enable";
			this.buttonEnable.Visible = false;
			this.buttonEnable.Click += new System.EventHandler(this.buttonEnable_Click);
			// 
			// buttonDisable
			// 
			this.buttonDisable.Image = global::WinFormsControlPlugin.Properties.Resources.plugin_disabled;
			this.buttonDisable.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonDisable.Name = "buttonDisable";
			this.buttonDisable.Size = new System.Drawing.Size(65, 22);
			this.buttonDisable.Text = "Disable";
			this.buttonDisable.Visible = false;
			this.buttonDisable.Click += new System.EventHandler(this.buttonDisable_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(517, 414);
			this.Controls.Add(this.tabsMain);
			this.Controls.Add(this.toolStripMainWindow);
			this.Name = "MainWindow";
			this.Text = "Sensorium2";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.tabLog.ResumeLayout(false);
			this.tabsMain.ResumeLayout(false);
			this.tabPlugins.ResumeLayout(false);
			this.tableLayoutPanelPluginsTab.ResumeLayout(false);
			this.tableLayoutPanelPluginsTab.PerformLayout();
			this.toolStripMainWindow.ResumeLayout(false);
			this.toolStripMainWindow.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabPage tabLog;
		private System.Windows.Forms.ListBox listBoxLog;
		private System.Windows.Forms.TabPage tabSensors;
		private System.Windows.Forms.TabControl tabsMain;
		//private System.Windows.Forms.ToolStripButton refreshButton;
		private System.Windows.Forms.TabPage tabPlugins;
		private System.Windows.Forms.ListView listViewPlugins;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ImageList imageListPluginView;
		private System.Windows.Forms.ToolStrip toolStripMainWindow;
		private System.Windows.Forms.ToolStripButton buttonRefresh;
		private System.Windows.Forms.ToolStripButton buttonAbout;
		private System.Windows.Forms.ToolStripButton buttonEnable;
		private System.Windows.Forms.ToolStripButton buttonDisable;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPluginsTab;
		private System.Windows.Forms.Label labelPluginName;
		private System.Windows.Forms.Label labelPluginVersion;
		private System.Windows.Forms.Label labelPluginDescription;
		private System.Windows.Forms.ColumnHeader columnHeaderType;





	}
}
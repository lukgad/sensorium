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
			this.listViewSensors = new System.Windows.Forms.ListView();
			this.columnHeaderSensorName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSensorType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSensorValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageListSensorsView = new System.Windows.Forms.ImageList(this.components);
			this.imageListPluginView = new System.Windows.Forms.ImageList(this.components);
			this.tabsMain = new System.Windows.Forms.TabControl();
			this.tabPlugins = new System.Windows.Forms.TabPage();
			this.splitContainerPluginsTab = new System.Windows.Forms.SplitContainer();
			this.listViewPlugins = new System.Windows.Forms.ListView();
			this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tableLayoutPanelPluginsTab = new System.Windows.Forms.TableLayoutPanel();
			this.labelPluginName = new System.Windows.Forms.Label();
			this.labelPluginVersion = new System.Windows.Forms.Label();
			this.tableLayoutLegend = new System.Windows.Forms.TableLayoutPanel();
			this.labelRestart = new System.Windows.Forms.Label();
			this.labelDisabled = new System.Windows.Forms.Label();
			this.pictureBoxEnabled = new System.Windows.Forms.PictureBox();
			this.pictureBoxDisabled = new System.Windows.Forms.PictureBox();
			this.labelEnabled = new System.Windows.Forms.Label();
			this.pictureBoxRestart = new System.Windows.Forms.PictureBox();
			this.textBoxPluginDescription = new System.Windows.Forms.TextBox();
			this.toolStripMainWindow = new System.Windows.Forms.ToolStrip();
			this.buttonAbout = new System.Windows.Forms.ToolStripButton();
			this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
			this.buttonEnable = new System.Windows.Forms.ToolStripButton();
			this.buttonDisable = new System.Windows.Forms.ToolStripButton();
			columnHeaderEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabLog.SuspendLayout();
			this.tabSensors.SuspendLayout();
			this.tabsMain.SuspendLayout();
			this.tabPlugins.SuspendLayout();
			this.splitContainerPluginsTab.Panel1.SuspendLayout();
			this.splitContainerPluginsTab.Panel2.SuspendLayout();
			this.splitContainerPluginsTab.SuspendLayout();
			this.tableLayoutPanelPluginsTab.SuspendLayout();
			this.tableLayoutLegend.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnabled)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisabled)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxRestart)).BeginInit();
			this.toolStripMainWindow.SuspendLayout();
			this.SuspendLayout();
			// 
			// columnHeaderEnabled
			// 
			columnHeaderEnabled.Text = "Enabled";
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
			this.listBoxLog.Size = new System.Drawing.Size(503, 357);
			this.listBoxLog.TabIndex = 0;
			// 
			// tabSensors
			// 
			this.tabSensors.Controls.Add(this.listViewSensors);
			this.tabSensors.Location = new System.Drawing.Point(4, 22);
			this.tabSensors.Name = "tabSensors";
			this.tabSensors.Padding = new System.Windows.Forms.Padding(3);
			this.tabSensors.Size = new System.Drawing.Size(509, 363);
			this.tabSensors.TabIndex = 0;
			this.tabSensors.Text = "Sensors";
			this.tabSensors.UseVisualStyleBackColor = true;
			// 
			// listViewSensors
			// 
			this.listViewSensors.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listViewSensors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSensorName,
            this.columnHeaderSensorType,
            this.columnHeaderSensorValue});
			this.listViewSensors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewSensors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewSensors.Location = new System.Drawing.Point(3, 3);
			this.listViewSensors.MultiSelect = false;
			this.listViewSensors.Name = "listViewSensors";
			this.listViewSensors.Size = new System.Drawing.Size(503, 357);
			this.listViewSensors.SmallImageList = this.imageListSensorsView;
			this.listViewSensors.TabIndex = 0;
			this.listViewSensors.UseCompatibleStateImageBehavior = false;
			this.listViewSensors.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderSensorName
			// 
			this.columnHeaderSensorName.Text = "Name";
			this.columnHeaderSensorName.Width = 100;
			// 
			// columnHeaderSensorType
			// 
			this.columnHeaderSensorType.Text = "Type";
			this.columnHeaderSensorType.Width = 109;
			// 
			// columnHeaderSensorValue
			// 
			this.columnHeaderSensorValue.Text = "Value";
			this.columnHeaderSensorValue.Width = 100;
			// 
			// imageListSensorsView
			// 
			this.imageListSensorsView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSensorsView.ImageStream")));
			this.imageListSensorsView.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListSensorsView.Images.SetKeyName(0, "server.png");
			// 
			// imageListPluginView
			// 
			this.imageListPluginView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPluginView.ImageStream")));
			this.imageListPluginView.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListPluginView.Images.SetKeyName(0, "plugin");
			this.imageListPluginView.Images.SetKeyName(1, "plugin_disabled");
			this.imageListPluginView.Images.SetKeyName(2, "arrow_refresh_small");
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
			this.tabPlugins.Controls.Add(this.splitContainerPluginsTab);
			this.tabPlugins.Location = new System.Drawing.Point(4, 22);
			this.tabPlugins.Name = "tabPlugins";
			this.tabPlugins.Padding = new System.Windows.Forms.Padding(3);
			this.tabPlugins.Size = new System.Drawing.Size(509, 363);
			this.tabPlugins.TabIndex = 2;
			this.tabPlugins.Text = "Plugins";
			this.tabPlugins.UseVisualStyleBackColor = true;
			// 
			// splitContainerPluginsTab
			// 
			this.splitContainerPluginsTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerPluginsTab.IsSplitterFixed = true;
			this.splitContainerPluginsTab.Location = new System.Drawing.Point(3, 3);
			this.splitContainerPluginsTab.Name = "splitContainerPluginsTab";
			// 
			// splitContainerPluginsTab.Panel1
			// 
			this.splitContainerPluginsTab.Panel1.Controls.Add(this.listViewPlugins);
			// 
			// splitContainerPluginsTab.Panel2
			// 
			this.splitContainerPluginsTab.Panel2.Controls.Add(this.tableLayoutPanelPluginsTab);
			this.splitContainerPluginsTab.Size = new System.Drawing.Size(503, 357);
			this.splitContainerPluginsTab.SplitterDistance = 290;
			this.splitContainerPluginsTab.TabIndex = 2;
			// 
			// listViewPlugins
			// 
			this.listViewPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            columnHeaderEnabled,
            this.columnHeaderType});
			this.listViewPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewPlugins.FullRowSelect = true;
			this.listViewPlugins.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewPlugins.HideSelection = false;
			this.listViewPlugins.Location = new System.Drawing.Point(0, 0);
			this.listViewPlugins.Name = "listViewPlugins";
			this.listViewPlugins.ShowGroups = false;
			this.listViewPlugins.Size = new System.Drawing.Size(290, 357);
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
			// columnHeaderType
			// 
			this.columnHeaderType.Text = "Type";
			// 
			// tableLayoutPanelPluginsTab
			// 
			this.tableLayoutPanelPluginsTab.ColumnCount = 1;
			this.tableLayoutPanelPluginsTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelPluginsTab.Controls.Add(this.labelPluginName, 0, 0);
			this.tableLayoutPanelPluginsTab.Controls.Add(this.labelPluginVersion, 0, 1);
			this.tableLayoutPanelPluginsTab.Controls.Add(this.tableLayoutLegend, 0, 3);
			this.tableLayoutPanelPluginsTab.Controls.Add(this.textBoxPluginDescription, 0, 2);
			this.tableLayoutPanelPluginsTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelPluginsTab.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelPluginsTab.Name = "tableLayoutPanelPluginsTab";
			this.tableLayoutPanelPluginsTab.RowCount = 4;
			this.tableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
			this.tableLayoutPanelPluginsTab.Size = new System.Drawing.Size(209, 357);
			this.tableLayoutPanelPluginsTab.TabIndex = 1;
			// 
			// labelPluginName
			// 
			this.labelPluginName.AutoSize = true;
			this.labelPluginName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPluginName.Location = new System.Drawing.Point(3, 0);
			this.labelPluginName.Name = "labelPluginName";
			this.labelPluginName.Size = new System.Drawing.Size(203, 20);
			this.labelPluginName.TabIndex = 1;
			this.labelPluginName.Text = "Name:";
			// 
			// labelPluginVersion
			// 
			this.labelPluginVersion.AutoSize = true;
			this.labelPluginVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPluginVersion.Location = new System.Drawing.Point(3, 20);
			this.labelPluginVersion.Name = "labelPluginVersion";
			this.labelPluginVersion.Size = new System.Drawing.Size(203, 20);
			this.labelPluginVersion.TabIndex = 2;
			this.labelPluginVersion.Text = "Version:";
			// 
			// tableLayoutLegend
			// 
			this.tableLayoutLegend.ColumnCount = 2;
			this.tableLayoutLegend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutLegend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutLegend.Controls.Add(this.labelRestart, 1, 2);
			this.tableLayoutLegend.Controls.Add(this.labelDisabled, 1, 1);
			this.tableLayoutLegend.Controls.Add(this.pictureBoxEnabled, 0, 0);
			this.tableLayoutLegend.Controls.Add(this.pictureBoxDisabled, 0, 1);
			this.tableLayoutLegend.Controls.Add(this.labelEnabled, 1, 0);
			this.tableLayoutLegend.Controls.Add(this.pictureBoxRestart, 0, 2);
			this.tableLayoutLegend.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutLegend.Location = new System.Drawing.Point(3, 294);
			this.tableLayoutLegend.Name = "tableLayoutLegend";
			this.tableLayoutLegend.RowCount = 3;
			this.tableLayoutLegend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutLegend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutLegend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutLegend.Size = new System.Drawing.Size(203, 60);
			this.tableLayoutLegend.TabIndex = 4;
			// 
			// labelRestart
			// 
			this.labelRestart.AutoSize = true;
			this.labelRestart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelRestart.Location = new System.Drawing.Point(23, 43);
			this.labelRestart.Margin = new System.Windows.Forms.Padding(3);
			this.labelRestart.Name = "labelRestart";
			this.labelRestart.Size = new System.Drawing.Size(177, 14);
			this.labelRestart.TabIndex = 5;
			this.labelRestart.Text = "Restart for changes to take effect";
			// 
			// labelDisabled
			// 
			this.labelDisabled.AutoSize = true;
			this.labelDisabled.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelDisabled.Location = new System.Drawing.Point(23, 23);
			this.labelDisabled.Margin = new System.Windows.Forms.Padding(3);
			this.labelDisabled.Name = "labelDisabled";
			this.labelDisabled.Size = new System.Drawing.Size(177, 14);
			this.labelDisabled.TabIndex = 3;
			this.labelDisabled.Text = "Disabled";
			// 
			// pictureBoxEnabled
			// 
			this.pictureBoxEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxEnabled.ErrorImage = null;
			this.pictureBoxEnabled.Image = global::WinFormsControlPlugin.Properties.Resources.plugin;
			this.pictureBoxEnabled.InitialImage = null;
			this.pictureBoxEnabled.Location = new System.Drawing.Point(2, 2);
			this.pictureBoxEnabled.Margin = new System.Windows.Forms.Padding(2);
			this.pictureBoxEnabled.Name = "pictureBoxEnabled";
			this.pictureBoxEnabled.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxEnabled.TabIndex = 0;
			this.pictureBoxEnabled.TabStop = false;
			// 
			// pictureBoxDisabled
			// 
			this.pictureBoxDisabled.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxDisabled.ErrorImage = null;
			this.pictureBoxDisabled.Image = global::WinFormsControlPlugin.Properties.Resources.plugin_disabled;
			this.pictureBoxDisabled.InitialImage = null;
			this.pictureBoxDisabled.Location = new System.Drawing.Point(2, 22);
			this.pictureBoxDisabled.Margin = new System.Windows.Forms.Padding(2);
			this.pictureBoxDisabled.Name = "pictureBoxDisabled";
			this.pictureBoxDisabled.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxDisabled.TabIndex = 1;
			this.pictureBoxDisabled.TabStop = false;
			// 
			// labelEnabled
			// 
			this.labelEnabled.AutoSize = true;
			this.labelEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelEnabled.Location = new System.Drawing.Point(23, 3);
			this.labelEnabled.Margin = new System.Windows.Forms.Padding(3);
			this.labelEnabled.Name = "labelEnabled";
			this.labelEnabled.Size = new System.Drawing.Size(177, 14);
			this.labelEnabled.TabIndex = 2;
			this.labelEnabled.Text = "Enabled";
			// 
			// pictureBoxRestart
			// 
			this.pictureBoxRestart.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBoxRestart.ErrorImage = null;
			this.pictureBoxRestart.Image = global::WinFormsControlPlugin.Properties.Resources.arrow_refresh_small;
			this.pictureBoxRestart.InitialImage = null;
			this.pictureBoxRestart.Location = new System.Drawing.Point(2, 42);
			this.pictureBoxRestart.Margin = new System.Windows.Forms.Padding(2);
			this.pictureBoxRestart.Name = "pictureBoxRestart";
			this.pictureBoxRestart.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxRestart.TabIndex = 4;
			this.pictureBoxRestart.TabStop = false;
			// 
			// textBoxPluginDescription
			// 
			this.textBoxPluginDescription.BackColor = System.Drawing.SystemColors.Window;
			this.textBoxPluginDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxPluginDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxPluginDescription.Location = new System.Drawing.Point(3, 43);
			this.textBoxPluginDescription.Multiline = true;
			this.textBoxPluginDescription.Name = "textBoxPluginDescription";
			this.textBoxPluginDescription.ReadOnly = true;
			this.textBoxPluginDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxPluginDescription.Size = new System.Drawing.Size(203, 245);
			this.textBoxPluginDescription.TabIndex = 5;
			this.textBoxPluginDescription.Text = "Description:";
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
			this.tabSensors.ResumeLayout(false);
			this.tabsMain.ResumeLayout(false);
			this.tabPlugins.ResumeLayout(false);
			this.splitContainerPluginsTab.Panel1.ResumeLayout(false);
			this.splitContainerPluginsTab.Panel2.ResumeLayout(false);
			this.splitContainerPluginsTab.ResumeLayout(false);
			this.tableLayoutPanelPluginsTab.ResumeLayout(false);
			this.tableLayoutPanelPluginsTab.PerformLayout();
			this.tableLayoutLegend.ResumeLayout(false);
			this.tableLayoutLegend.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxEnabled)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisabled)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxRestart)).EndInit();
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
		private System.Windows.Forms.ColumnHeader columnHeaderType;
		private System.Windows.Forms.SplitContainer splitContainerPluginsTab;
		private System.Windows.Forms.TableLayoutPanel tableLayoutLegend;
		private System.Windows.Forms.PictureBox pictureBoxEnabled;
		private System.Windows.Forms.PictureBox pictureBoxDisabled;
		private System.Windows.Forms.Label labelDisabled;
		private System.Windows.Forms.Label labelEnabled;
		private System.Windows.Forms.PictureBox pictureBoxRestart;
		private System.Windows.Forms.Label labelRestart;
		private System.Windows.Forms.TextBox textBoxPluginDescription;
		private System.Windows.Forms.ListView listViewSensors;
		private System.Windows.Forms.ColumnHeader columnHeaderSensorName;
		private System.Windows.Forms.ColumnHeader columnHeaderSensorType;
		private System.Windows.Forms.ColumnHeader columnHeaderSensorValue;
		private System.Windows.Forms.ImageList imageListSensorsView;





	}
}
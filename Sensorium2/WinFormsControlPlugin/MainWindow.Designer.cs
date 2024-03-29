﻿/*	This file is part of Sensorium2 <http://code.google.com/p/sensorium>
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
			this.TabLog = new System.Windows.Forms.TabPage();
			this.ListBoxLog = new System.Windows.Forms.ListBox();
			this.TabSensors = new System.Windows.Forms.TabPage();
			this.ListViewSensors = new System.Windows.Forms.ListView();
			this.columnHeaderSensorName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSensorType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSensorValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TabsMain = new System.Windows.Forms.TabControl();
			this.TabPlugins = new System.Windows.Forms.TabPage();
			this.SplitContainerPluginsTab = new System.Windows.Forms.SplitContainer();
			this.ListViewPlugins = new System.Windows.Forms.ListView();
			this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TableLayoutPanelPluginsTab = new System.Windows.Forms.TableLayoutPanel();
			this.LabelPluginName = new System.Windows.Forms.Label();
			this.LabelPluginVersion = new System.Windows.Forms.Label();
			this.TableLayoutLegend = new System.Windows.Forms.TableLayoutPanel();
			this.LabelRestart = new System.Windows.Forms.Label();
			this.LabelDisabled = new System.Windows.Forms.Label();
			this.PictureBoxEnabled = new System.Windows.Forms.PictureBox();
			this.PictureBoxDisabled = new System.Windows.Forms.PictureBox();
			this.LabelEnabled = new System.Windows.Forms.Label();
			this.PictureBoxRestart = new System.Windows.Forms.PictureBox();
			this.TextBoxPluginDescription = new System.Windows.Forms.TextBox();
			this.TabSettings = new System.Windows.Forms.TabPage();
			this.ResetButton = new System.Windows.Forms.Button();
			this.SaveButton = new System.Windows.Forms.Button();
			this.FriendlyNameTextbox = new System.Windows.Forms.TextBox();
			this.FriendlyNameLabel = new System.Windows.Forms.Label();
			this.SettingsDirectoryTextbox = new System.Windows.Forms.TextBox();
			this.SettingsDirectoryLabel = new System.Windows.Forms.Label();
			this.PluginDirectoryTextbox = new System.Windows.Forms.TextBox();
			this.PluginDirectoryLabel = new System.Windows.Forms.Label();
			this.ToolStripMainWindow = new System.Windows.Forms.ToolStrip();
			this.ButtonAbout = new System.Windows.Forms.ToolStripButton();
			this.ButtonRefresh = new System.Windows.Forms.ToolStripButton();
			this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ButtonEnable = new System.Windows.Forms.ToolStripButton();
			this.ButtonDisable = new System.Windows.Forms.ToolStripButton();
			this.ButtonSettings = new System.Windows.Forms.ToolStripButton();
			this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.LogLevelDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.AllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.InfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.WarnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.LogLevelLabel = new System.Windows.Forms.ToolStripLabel();
			this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			columnHeaderEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TabLog.SuspendLayout();
			this.TabSensors.SuspendLayout();
			this.TabsMain.SuspendLayout();
			this.TabPlugins.SuspendLayout();
			this.SplitContainerPluginsTab.Panel1.SuspendLayout();
			this.SplitContainerPluginsTab.Panel2.SuspendLayout();
			this.SplitContainerPluginsTab.SuspendLayout();
			this.TableLayoutPanelPluginsTab.SuspendLayout();
			this.TableLayoutLegend.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxEnabled)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxDisabled)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxRestart)).BeginInit();
			this.TabSettings.SuspendLayout();
			this.ToolStripMainWindow.SuspendLayout();
			this.SuspendLayout();
			// 
			// columnHeaderEnabled
			// 
			columnHeaderEnabled.Text = "Enabled";
			// 
			// TabLog
			// 
			this.TabLog.Controls.Add(this.ListBoxLog);
			this.TabLog.Location = new System.Drawing.Point(4, 22);
			this.TabLog.Name = "TabLog";
			this.TabLog.Padding = new System.Windows.Forms.Padding(3);
			this.TabLog.Size = new System.Drawing.Size(509, 363);
			this.TabLog.TabIndex = 1;
			this.TabLog.Text = "Application Log";
			this.TabLog.UseVisualStyleBackColor = true;
			// 
			// ListBoxLog
			// 
			this.ListBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ListBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListBoxLog.FormattingEnabled = true;
			this.ListBoxLog.HorizontalScrollbar = true;
			this.ListBoxLog.Location = new System.Drawing.Point(3, 3);
			this.ListBoxLog.Name = "ListBoxLog";
			this.ListBoxLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ListBoxLog.Size = new System.Drawing.Size(503, 357);
			this.ListBoxLog.TabIndex = 0;
			// 
			// TabSensors
			// 
			this.TabSensors.Controls.Add(this.ListViewSensors);
			this.TabSensors.Location = new System.Drawing.Point(4, 22);
			this.TabSensors.Name = "TabSensors";
			this.TabSensors.Padding = new System.Windows.Forms.Padding(3);
			this.TabSensors.Size = new System.Drawing.Size(509, 363);
			this.TabSensors.TabIndex = 0;
			this.TabSensors.Text = "Sensors";
			this.TabSensors.UseVisualStyleBackColor = true;
			// 
			// ListViewSensors
			// 
			this.ListViewSensors.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ListViewSensors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSensorName,
            this.columnHeaderSensorType,
            this.columnHeaderSensorValue});
			this.ListViewSensors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListViewSensors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.ListViewSensors.Location = new System.Drawing.Point(3, 3);
			this.ListViewSensors.MultiSelect = false;
			this.ListViewSensors.Name = "ListViewSensors";
			this.ListViewSensors.Size = new System.Drawing.Size(503, 357);
			this.ListViewSensors.TabIndex = 0;
			this.ListViewSensors.UseCompatibleStateImageBehavior = false;
			this.ListViewSensors.View = System.Windows.Forms.View.Details;
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
			// TabsMain
			// 
			this.TabsMain.Controls.Add(this.TabSensors);
			this.TabsMain.Controls.Add(this.TabLog);
			this.TabsMain.Controls.Add(this.TabPlugins);
			this.TabsMain.Controls.Add(this.TabSettings);
			this.TabsMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabsMain.Location = new System.Drawing.Point(0, 25);
			this.TabsMain.Multiline = true;
			this.TabsMain.Name = "TabsMain";
			this.TabsMain.SelectedIndex = 0;
			this.TabsMain.Size = new System.Drawing.Size(517, 389);
			this.TabsMain.TabIndex = 1;
			this.TabsMain.SelectedIndexChanged += new System.EventHandler(this.TabsMainSelectedIndexChanged);
			// 
			// TabPlugins
			// 
			this.TabPlugins.Controls.Add(this.SplitContainerPluginsTab);
			this.TabPlugins.Location = new System.Drawing.Point(4, 22);
			this.TabPlugins.Name = "TabPlugins";
			this.TabPlugins.Padding = new System.Windows.Forms.Padding(3);
			this.TabPlugins.Size = new System.Drawing.Size(509, 363);
			this.TabPlugins.TabIndex = 2;
			this.TabPlugins.Text = "Plugins";
			this.TabPlugins.UseVisualStyleBackColor = true;
			// 
			// SplitContainerPluginsTab
			// 
			this.SplitContainerPluginsTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainerPluginsTab.IsSplitterFixed = true;
			this.SplitContainerPluginsTab.Location = new System.Drawing.Point(3, 3);
			this.SplitContainerPluginsTab.Name = "SplitContainerPluginsTab";
			// 
			// SplitContainerPluginsTab.Panel1
			// 
			this.SplitContainerPluginsTab.Panel1.Controls.Add(this.ListViewPlugins);
			// 
			// SplitContainerPluginsTab.Panel2
			// 
			this.SplitContainerPluginsTab.Panel2.Controls.Add(this.TableLayoutPanelPluginsTab);
			this.SplitContainerPluginsTab.Size = new System.Drawing.Size(503, 357);
			this.SplitContainerPluginsTab.SplitterDistance = 290;
			this.SplitContainerPluginsTab.TabIndex = 2;
			// 
			// ListViewPlugins
			// 
			this.ListViewPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            columnHeaderEnabled,
            this.columnHeaderType});
			this.ListViewPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListViewPlugins.FullRowSelect = true;
			this.ListViewPlugins.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.ListViewPlugins.HideSelection = false;
			this.ListViewPlugins.Location = new System.Drawing.Point(0, 0);
			this.ListViewPlugins.Name = "ListViewPlugins";
			this.ListViewPlugins.ShowGroups = false;
			this.ListViewPlugins.Size = new System.Drawing.Size(290, 357);
			this.ListViewPlugins.TabIndex = 0;
			this.ListViewPlugins.UseCompatibleStateImageBehavior = false;
			this.ListViewPlugins.View = System.Windows.Forms.View.Details;
			this.ListViewPlugins.SelectedIndexChanged += new System.EventHandler(this.ListViewPluginsSelectedIndexChanged);
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
			// TableLayoutPanelPluginsTab
			// 
			this.TableLayoutPanelPluginsTab.ColumnCount = 1;
			this.TableLayoutPanelPluginsTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayoutPanelPluginsTab.Controls.Add(this.LabelPluginName, 0, 0);
			this.TableLayoutPanelPluginsTab.Controls.Add(this.LabelPluginVersion, 0, 1);
			this.TableLayoutPanelPluginsTab.Controls.Add(this.TableLayoutLegend, 0, 3);
			this.TableLayoutPanelPluginsTab.Controls.Add(this.TextBoxPluginDescription, 0, 2);
			this.TableLayoutPanelPluginsTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TableLayoutPanelPluginsTab.Location = new System.Drawing.Point(0, 0);
			this.TableLayoutPanelPluginsTab.Name = "TableLayoutPanelPluginsTab";
			this.TableLayoutPanelPluginsTab.RowCount = 4;
			this.TableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayoutPanelPluginsTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
			this.TableLayoutPanelPluginsTab.Size = new System.Drawing.Size(209, 357);
			this.TableLayoutPanelPluginsTab.TabIndex = 1;
			// 
			// LabelPluginName
			// 
			this.LabelPluginName.AutoSize = true;
			this.LabelPluginName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LabelPluginName.Location = new System.Drawing.Point(3, 0);
			this.LabelPluginName.Name = "LabelPluginName";
			this.LabelPluginName.Size = new System.Drawing.Size(203, 20);
			this.LabelPluginName.TabIndex = 1;
			this.LabelPluginName.Text = "Name:";
			// 
			// LabelPluginVersion
			// 
			this.LabelPluginVersion.AutoSize = true;
			this.LabelPluginVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LabelPluginVersion.Location = new System.Drawing.Point(3, 20);
			this.LabelPluginVersion.Name = "LabelPluginVersion";
			this.LabelPluginVersion.Size = new System.Drawing.Size(203, 20);
			this.LabelPluginVersion.TabIndex = 2;
			this.LabelPluginVersion.Text = "Version:";
			// 
			// TableLayoutLegend
			// 
			this.TableLayoutLegend.ColumnCount = 2;
			this.TableLayoutLegend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TableLayoutLegend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayoutLegend.Controls.Add(this.LabelRestart, 1, 2);
			this.TableLayoutLegend.Controls.Add(this.LabelDisabled, 1, 1);
			this.TableLayoutLegend.Controls.Add(this.PictureBoxEnabled, 0, 0);
			this.TableLayoutLegend.Controls.Add(this.PictureBoxDisabled, 0, 1);
			this.TableLayoutLegend.Controls.Add(this.LabelEnabled, 1, 0);
			this.TableLayoutLegend.Controls.Add(this.PictureBoxRestart, 0, 2);
			this.TableLayoutLegend.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TableLayoutLegend.Location = new System.Drawing.Point(3, 294);
			this.TableLayoutLegend.Name = "TableLayoutLegend";
			this.TableLayoutLegend.RowCount = 3;
			this.TableLayoutLegend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TableLayoutLegend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TableLayoutLegend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TableLayoutLegend.Size = new System.Drawing.Size(203, 60);
			this.TableLayoutLegend.TabIndex = 4;
			// 
			// LabelRestart
			// 
			this.LabelRestart.AutoSize = true;
			this.LabelRestart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LabelRestart.Location = new System.Drawing.Point(23, 43);
			this.LabelRestart.Margin = new System.Windows.Forms.Padding(3);
			this.LabelRestart.Name = "LabelRestart";
			this.LabelRestart.Size = new System.Drawing.Size(177, 14);
			this.LabelRestart.TabIndex = 5;
			this.LabelRestart.Text = "Restart for changes to take effect";
			// 
			// LabelDisabled
			// 
			this.LabelDisabled.AutoSize = true;
			this.LabelDisabled.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LabelDisabled.Location = new System.Drawing.Point(23, 23);
			this.LabelDisabled.Margin = new System.Windows.Forms.Padding(3);
			this.LabelDisabled.Name = "LabelDisabled";
			this.LabelDisabled.Size = new System.Drawing.Size(177, 14);
			this.LabelDisabled.TabIndex = 3;
			this.LabelDisabled.Text = "Disabled";
			// 
			// PictureBoxEnabled
			// 
			this.PictureBoxEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PictureBoxEnabled.ErrorImage = null;
			this.PictureBoxEnabled.Image = global::WinFormsControlPlugin.Properties.Resources.plugin;
			this.PictureBoxEnabled.InitialImage = null;
			this.PictureBoxEnabled.Location = new System.Drawing.Point(2, 2);
			this.PictureBoxEnabled.Margin = new System.Windows.Forms.Padding(2);
			this.PictureBoxEnabled.Name = "PictureBoxEnabled";
			this.PictureBoxEnabled.Size = new System.Drawing.Size(16, 16);
			this.PictureBoxEnabled.TabIndex = 0;
			this.PictureBoxEnabled.TabStop = false;
			// 
			// PictureBoxDisabled
			// 
			this.PictureBoxDisabled.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PictureBoxDisabled.ErrorImage = null;
			this.PictureBoxDisabled.Image = global::WinFormsControlPlugin.Properties.Resources.plugin_disabled;
			this.PictureBoxDisabled.InitialImage = null;
			this.PictureBoxDisabled.Location = new System.Drawing.Point(2, 22);
			this.PictureBoxDisabled.Margin = new System.Windows.Forms.Padding(2);
			this.PictureBoxDisabled.Name = "PictureBoxDisabled";
			this.PictureBoxDisabled.Size = new System.Drawing.Size(16, 16);
			this.PictureBoxDisabled.TabIndex = 1;
			this.PictureBoxDisabled.TabStop = false;
			// 
			// LabelEnabled
			// 
			this.LabelEnabled.AutoSize = true;
			this.LabelEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LabelEnabled.Location = new System.Drawing.Point(23, 3);
			this.LabelEnabled.Margin = new System.Windows.Forms.Padding(3);
			this.LabelEnabled.Name = "LabelEnabled";
			this.LabelEnabled.Size = new System.Drawing.Size(177, 14);
			this.LabelEnabled.TabIndex = 2;
			this.LabelEnabled.Text = "Enabled";
			// 
			// PictureBoxRestart
			// 
			this.PictureBoxRestart.Dock = System.Windows.Forms.DockStyle.Top;
			this.PictureBoxRestart.ErrorImage = null;
			this.PictureBoxRestart.Image = global::WinFormsControlPlugin.Properties.Resources.arrow_refresh_small;
			this.PictureBoxRestart.InitialImage = null;
			this.PictureBoxRestart.Location = new System.Drawing.Point(2, 42);
			this.PictureBoxRestart.Margin = new System.Windows.Forms.Padding(2);
			this.PictureBoxRestart.Name = "PictureBoxRestart";
			this.PictureBoxRestart.Size = new System.Drawing.Size(16, 16);
			this.PictureBoxRestart.TabIndex = 4;
			this.PictureBoxRestart.TabStop = false;
			// 
			// TextBoxPluginDescription
			// 
			this.TextBoxPluginDescription.BackColor = System.Drawing.SystemColors.Window;
			this.TextBoxPluginDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextBoxPluginDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextBoxPluginDescription.Location = new System.Drawing.Point(3, 43);
			this.TextBoxPluginDescription.Multiline = true;
			this.TextBoxPluginDescription.Name = "TextBoxPluginDescription";
			this.TextBoxPluginDescription.ReadOnly = true;
			this.TextBoxPluginDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TextBoxPluginDescription.Size = new System.Drawing.Size(203, 245);
			this.TextBoxPluginDescription.TabIndex = 5;
			this.TextBoxPluginDescription.Text = "Description:";
			// 
			// TabSettings
			// 
			this.TabSettings.Controls.Add(this.ResetButton);
			this.TabSettings.Controls.Add(this.SaveButton);
			this.TabSettings.Controls.Add(this.FriendlyNameTextbox);
			this.TabSettings.Controls.Add(this.FriendlyNameLabel);
			this.TabSettings.Controls.Add(this.SettingsDirectoryTextbox);
			this.TabSettings.Controls.Add(this.SettingsDirectoryLabel);
			this.TabSettings.Controls.Add(this.PluginDirectoryTextbox);
			this.TabSettings.Controls.Add(this.PluginDirectoryLabel);
			this.TabSettings.Location = new System.Drawing.Point(4, 22);
			this.TabSettings.Name = "TabSettings";
			this.TabSettings.Padding = new System.Windows.Forms.Padding(3);
			this.TabSettings.Size = new System.Drawing.Size(509, 363);
			this.TabSettings.TabIndex = 3;
			this.TabSettings.Text = "Program Settings";
			this.TabSettings.UseVisualStyleBackColor = true;
			// 
			// ResetButton
			// 
			this.ResetButton.Location = new System.Drawing.Point(89, 129);
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.Size = new System.Drawing.Size(75, 23);
			this.ResetButton.TabIndex = 7;
			this.ResetButton.Text = "&Reset";
			this.ResetButton.UseVisualStyleBackColor = true;
			this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
			// 
			// SaveButton
			// 
			this.SaveButton.Location = new System.Drawing.Point(8, 129);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(75, 23);
			this.SaveButton.TabIndex = 6;
			this.SaveButton.Text = "&Save";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// FriendlyNameTextbox
			// 
			this.FriendlyNameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.FriendlyNameTextbox.Location = new System.Drawing.Point(8, 101);
			this.FriendlyNameTextbox.Name = "FriendlyNameTextbox";
			this.FriendlyNameTextbox.Size = new System.Drawing.Size(493, 20);
			this.FriendlyNameTextbox.TabIndex = 5;
			// 
			// FriendlyNameLabel
			// 
			this.FriendlyNameLabel.AutoSize = true;
			this.FriendlyNameLabel.Location = new System.Drawing.Point(9, 85);
			this.FriendlyNameLabel.Name = "FriendlyNameLabel";
			this.FriendlyNameLabel.Size = new System.Drawing.Size(77, 13);
			this.FriendlyNameLabel.TabIndex = 4;
			this.FriendlyNameLabel.Text = "Friendly Name:";
			// 
			// SettingsDirectoryTextbox
			// 
			this.SettingsDirectoryTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.SettingsDirectoryTextbox.Location = new System.Drawing.Point(8, 62);
			this.SettingsDirectoryTextbox.Name = "SettingsDirectoryTextbox";
			this.SettingsDirectoryTextbox.Size = new System.Drawing.Size(493, 20);
			this.SettingsDirectoryTextbox.TabIndex = 3;
			// 
			// SettingsDirectoryLabel
			// 
			this.SettingsDirectoryLabel.AutoSize = true;
			this.SettingsDirectoryLabel.Location = new System.Drawing.Point(9, 46);
			this.SettingsDirectoryLabel.Name = "SettingsDirectoryLabel";
			this.SettingsDirectoryLabel.Size = new System.Drawing.Size(93, 13);
			this.SettingsDirectoryLabel.TabIndex = 2;
			this.SettingsDirectoryLabel.Text = "Settings Directory:";
			// 
			// PluginDirectoryTextbox
			// 
			this.PluginDirectoryTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.PluginDirectoryTextbox.Location = new System.Drawing.Point(8, 23);
			this.PluginDirectoryTextbox.Name = "PluginDirectoryTextbox";
			this.PluginDirectoryTextbox.Size = new System.Drawing.Size(493, 20);
			this.PluginDirectoryTextbox.TabIndex = 1;
			// 
			// PluginDirectoryLabel
			// 
			this.PluginDirectoryLabel.AutoSize = true;
			this.PluginDirectoryLabel.Location = new System.Drawing.Point(9, 7);
			this.PluginDirectoryLabel.Name = "PluginDirectoryLabel";
			this.PluginDirectoryLabel.Size = new System.Drawing.Size(84, 13);
			this.PluginDirectoryLabel.TabIndex = 0;
			this.PluginDirectoryLabel.Text = "Plugin Directory:";
			// 
			// ToolStripMainWindow
			// 
			this.ToolStripMainWindow.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ToolStripMainWindow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonAbout,
            this.ButtonRefresh,
            this.ToolStripSeparator2,
            this.ButtonEnable,
            this.ButtonDisable,
            this.ButtonSettings,
            this.ToolStripSeparator1,
            this.LogLevelDropDownButton,
            this.LogLevelLabel});
			this.ToolStripMainWindow.Location = new System.Drawing.Point(0, 0);
			this.ToolStripMainWindow.Name = "ToolStripMainWindow";
			this.ToolStripMainWindow.Size = new System.Drawing.Size(517, 25);
			this.ToolStripMainWindow.TabIndex = 0;
			// 
			// ButtonAbout
			// 
			this.ButtonAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.ButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonAbout.Name = "ButtonAbout";
			this.ButtonAbout.Size = new System.Drawing.Size(53, 22);
			this.ButtonAbout.Text = "About...";
			this.ButtonAbout.Click += new System.EventHandler(this.ButtonAboutClick);
			// 
			// ButtonRefresh
			// 
			this.ButtonRefresh.Image = global::WinFormsControlPlugin.Properties.Resources.arrow_refresh;
			this.ButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonRefresh.Name = "ButtonRefresh";
			this.ButtonRefresh.Size = new System.Drawing.Size(66, 22);
			this.ButtonRefresh.Text = "Refresh";
			this.ButtonRefresh.Visible = false;
			this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefreshClick);
			// 
			// ToolStripSeparator2
			// 
			this.ToolStripSeparator2.Name = "ToolStripSeparator2";
			this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.ToolStripSeparator2.Visible = false;
			// 
			// ButtonEnable
			// 
			this.ButtonEnable.Image = global::WinFormsControlPlugin.Properties.Resources.plugin;
			this.ButtonEnable.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonEnable.Name = "ButtonEnable";
			this.ButtonEnable.Size = new System.Drawing.Size(62, 22);
			this.ButtonEnable.Text = "Enable";
			this.ButtonEnable.Visible = false;
			this.ButtonEnable.Click += new System.EventHandler(this.ButtonEnableClick);
			// 
			// ButtonDisable
			// 
			this.ButtonDisable.Image = global::WinFormsControlPlugin.Properties.Resources.plugin_disabled;
			this.ButtonDisable.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonDisable.Name = "ButtonDisable";
			this.ButtonDisable.Size = new System.Drawing.Size(65, 22);
			this.ButtonDisable.Text = "Disable";
			this.ButtonDisable.Visible = false;
			this.ButtonDisable.Click += new System.EventHandler(this.ButtonDisableClick);
			// 
			// ButtonSettings
			// 
			this.ButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonSettings.Name = "ButtonSettings";
			this.ButtonSettings.Size = new System.Drawing.Size(62, 22);
			this.ButtonSettings.Text = "Settings...";
			this.ButtonSettings.Visible = false;
			this.ButtonSettings.Click += new System.EventHandler(this.ButtonSettings_Click);
			// 
			// ToolStripSeparator1
			// 
			this.ToolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.ToolStripSeparator1.Name = "ToolStripSeparator1";
			this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.ToolStripSeparator1.Visible = false;
			// 
			// LogLevelDropDownButton
			// 
			this.LogLevelDropDownButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.LogLevelDropDownButton.AutoToolTip = false;
			this.LogLevelDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.LogLevelDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AllToolStripMenuItem,
            this.InfoToolStripMenuItem,
            this.WarnToolStripMenuItem,
            this.ErrorToolStripMenuItem});
			this.LogLevelDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.LogLevelDropDownButton.Name = "LogLevelDropDownButton";
			this.LogLevelDropDownButton.Size = new System.Drawing.Size(41, 22);
			this.LogLevelDropDownButton.Text = "Info";
			this.LogLevelDropDownButton.ToolTipText = "Log Level";
			this.LogLevelDropDownButton.Visible = false;
			this.LogLevelDropDownButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.LogLevelDropDownButton_DropDownItemClicked);
			// 
			// AllToolStripMenuItem
			// 
			this.AllToolStripMenuItem.Name = "AllToolStripMenuItem";
			this.AllToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.AllToolStripMenuItem.Text = "All";
			// 
			// InfoToolStripMenuItem
			// 
			this.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem";
			this.InfoToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.InfoToolStripMenuItem.Text = "Info";
			// 
			// WarnToolStripMenuItem
			// 
			this.WarnToolStripMenuItem.Name = "WarnToolStripMenuItem";
			this.WarnToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.WarnToolStripMenuItem.Text = "Warning";
			// 
			// ErrorToolStripMenuItem
			// 
			this.ErrorToolStripMenuItem.Name = "ErrorToolStripMenuItem";
			this.ErrorToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.ErrorToolStripMenuItem.Text = "Error";
			// 
			// LogLevelLabel
			// 
			this.LogLevelLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.LogLevelLabel.Name = "LogLevelLabel";
			this.LogLevelLabel.Size = new System.Drawing.Size(60, 22);
			this.LogLevelLabel.Text = "Log Level:";
			this.LogLevelLabel.Visible = false;
			// 
			// TrayIcon
			// 
			this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
			this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseDoubleClick);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(517, 414);
			this.Controls.Add(this.TabsMain);
			this.Controls.Add(this.ToolStripMainWindow);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainWindow";
			this.Text = "Sensorium2";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.Resize += new System.EventHandler(this.MainWindow_Resize);
			this.TabLog.ResumeLayout(false);
			this.TabSensors.ResumeLayout(false);
			this.TabsMain.ResumeLayout(false);
			this.TabPlugins.ResumeLayout(false);
			this.SplitContainerPluginsTab.Panel1.ResumeLayout(false);
			this.SplitContainerPluginsTab.Panel2.ResumeLayout(false);
			this.SplitContainerPluginsTab.ResumeLayout(false);
			this.TableLayoutPanelPluginsTab.ResumeLayout(false);
			this.TableLayoutPanelPluginsTab.PerformLayout();
			this.TableLayoutLegend.ResumeLayout(false);
			this.TableLayoutLegend.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxEnabled)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxDisabled)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxRestart)).EndInit();
			this.TabSettings.ResumeLayout(false);
			this.TabSettings.PerformLayout();
			this.ToolStripMainWindow.ResumeLayout(false);
			this.ToolStripMainWindow.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabPage TabLog;
		private System.Windows.Forms.ListBox ListBoxLog;
		private System.Windows.Forms.TabPage TabSensors;
		private System.Windows.Forms.TabControl TabsMain;
		//private System.Windows.Forms.ToolStripButton refreshButton;
		private System.Windows.Forms.TabPage TabPlugins;
		private System.Windows.Forms.ListView ListViewPlugins;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ToolStrip ToolStripMainWindow;
		private System.Windows.Forms.ToolStripButton ButtonRefresh;
		private System.Windows.Forms.ToolStripButton ButtonAbout;
		private System.Windows.Forms.ToolStripButton ButtonEnable;
		private System.Windows.Forms.ToolStripButton ButtonDisable;
		private System.Windows.Forms.TableLayoutPanel TableLayoutPanelPluginsTab;
		private System.Windows.Forms.Label LabelPluginName;
		private System.Windows.Forms.Label LabelPluginVersion;
		private System.Windows.Forms.ColumnHeader columnHeaderType;
		private System.Windows.Forms.SplitContainer SplitContainerPluginsTab;
		private System.Windows.Forms.TableLayoutPanel TableLayoutLegend;
		private System.Windows.Forms.PictureBox PictureBoxEnabled;
		private System.Windows.Forms.PictureBox PictureBoxDisabled;
		private System.Windows.Forms.Label LabelDisabled;
		private System.Windows.Forms.Label LabelEnabled;
		private System.Windows.Forms.PictureBox PictureBoxRestart;
		private System.Windows.Forms.Label LabelRestart;
		private System.Windows.Forms.TextBox TextBoxPluginDescription;
		private System.Windows.Forms.ListView ListViewSensors;
		private System.Windows.Forms.ColumnHeader columnHeaderSensorName;
		private System.Windows.Forms.ColumnHeader columnHeaderSensorType;
		private System.Windows.Forms.ColumnHeader columnHeaderSensorValue;
		private System.Windows.Forms.ToolStripButton ButtonSettings;
		private System.Windows.Forms.TabPage TabSettings;
		private System.Windows.Forms.ToolStripDropDownButton LogLevelDropDownButton;
		private System.Windows.Forms.ToolStripMenuItem AllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem InfoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem WarnToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ErrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel LogLevelLabel;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
		private System.Windows.Forms.Label PluginDirectoryLabel;
		private System.Windows.Forms.TextBox PluginDirectoryTextbox;
		private System.Windows.Forms.Label SettingsDirectoryLabel;
		private System.Windows.Forms.TextBox SettingsDirectoryTextbox;
		private System.Windows.Forms.Label FriendlyNameLabel;
		private System.Windows.Forms.TextBox FriendlyNameTextbox;
		private System.Windows.Forms.Button ResetButton;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.NotifyIcon TrayIcon;





	}
}
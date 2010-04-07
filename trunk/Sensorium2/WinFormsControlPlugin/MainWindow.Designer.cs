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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.logTab = new System.Windows.Forms.TabPage();
			this.logListBox = new System.Windows.Forms.ListBox();
			this.sensorsTab = new System.Windows.Forms.TabPage();
			this.mainTabs = new System.Windows.Forms.TabControl();
			this.pluginsTab = new System.Windows.Forms.TabPage();
			this.enableButton = new System.Windows.Forms.Button();
			this.pluginsListView = new System.Windows.Forms.ListView();
			this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.enabledColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.pluginViewImageList = new System.Windows.Forms.ImageList(this.components);
			this.mainWindowToolStrip = new System.Windows.Forms.ToolStrip();
			this.refreshButton = new System.Windows.Forms.ToolStripButton();
			this.disableButton = new System.Windows.Forms.Button();
			this.logTab.SuspendLayout();
			this.mainTabs.SuspendLayout();
			this.pluginsTab.SuspendLayout();
			this.mainWindowToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// logTab
			// 
			this.logTab.Controls.Add(this.logListBox);
			this.logTab.Location = new System.Drawing.Point(4, 22);
			this.logTab.Name = "logTab";
			this.logTab.Padding = new System.Windows.Forms.Padding(3);
			this.logTab.Size = new System.Drawing.Size(509, 363);
			this.logTab.TabIndex = 1;
			this.logTab.Text = "Application Log";
			this.logTab.UseVisualStyleBackColor = true;
			// 
			// logListBox
			// 
			this.logListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.logListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logListBox.FormattingEnabled = true;
			this.logListBox.HorizontalScrollbar = true;
			this.logListBox.Location = new System.Drawing.Point(3, 3);
			this.logListBox.Name = "logListBox";
			this.logListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.logListBox.Size = new System.Drawing.Size(503, 351);
			this.logListBox.TabIndex = 0;
			// 
			// sensorsTab
			// 
			this.sensorsTab.Location = new System.Drawing.Point(4, 22);
			this.sensorsTab.Name = "sensorsTab";
			this.sensorsTab.Padding = new System.Windows.Forms.Padding(3);
			this.sensorsTab.Size = new System.Drawing.Size(509, 363);
			this.sensorsTab.TabIndex = 0;
			this.sensorsTab.Text = "Sensors";
			this.sensorsTab.UseVisualStyleBackColor = true;
			// 
			// mainTabs
			// 
			this.mainTabs.Controls.Add(this.sensorsTab);
			this.mainTabs.Controls.Add(this.logTab);
			this.mainTabs.Controls.Add(this.pluginsTab);
			this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabs.Location = new System.Drawing.Point(0, 25);
			this.mainTabs.Multiline = true;
			this.mainTabs.Name = "mainTabs";
			this.mainTabs.SelectedIndex = 0;
			this.mainTabs.Size = new System.Drawing.Size(517, 389);
			this.mainTabs.TabIndex = 1;
			this.mainTabs.SelectedIndexChanged += new System.EventHandler(this.mainTabs_SelectedIndexChanged);
			// 
			// pluginsTab
			// 
			this.pluginsTab.Controls.Add(this.disableButton);
			this.pluginsTab.Controls.Add(this.enableButton);
			this.pluginsTab.Controls.Add(this.pluginsListView);
			this.pluginsTab.Location = new System.Drawing.Point(4, 22);
			this.pluginsTab.Name = "pluginsTab";
			this.pluginsTab.Padding = new System.Windows.Forms.Padding(3);
			this.pluginsTab.Size = new System.Drawing.Size(509, 363);
			this.pluginsTab.TabIndex = 2;
			this.pluginsTab.Text = "Plugins";
			this.pluginsTab.UseVisualStyleBackColor = true;
			// 
			// enableButton
			// 
			this.enableButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.enableButton.Enabled = false;
			this.enableButton.Location = new System.Drawing.Point(378, 6);
			this.enableButton.Name = "enableButton";
			this.enableButton.Size = new System.Drawing.Size(123, 23);
			this.enableButton.TabIndex = 1;
			this.enableButton.Text = "&Enable";
			this.enableButton.UseVisualStyleBackColor = true;
			this.enableButton.Click += new System.EventHandler(this.enableButton_Click);
			// 
			// pluginsListView
			// 
			this.pluginsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pluginsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.enabledColumnHeader});
			this.pluginsListView.FullRowSelect = true;
			this.pluginsListView.Location = new System.Drawing.Point(8, 6);
			this.pluginsListView.Name = "pluginsListView";
			this.pluginsListView.ShowGroups = false;
			this.pluginsListView.Size = new System.Drawing.Size(364, 349);
			this.pluginsListView.SmallImageList = this.pluginViewImageList;
			this.pluginsListView.TabIndex = 0;
			this.pluginsListView.UseCompatibleStateImageBehavior = false;
			this.pluginsListView.View = System.Windows.Forms.View.Details;
			this.pluginsListView.SelectedIndexChanged += new System.EventHandler(this.pluginsListView_SelectedIndexChanged);
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 104;
			// 
			// enabledColumnHeader
			// 
			this.enabledColumnHeader.Text = "Enabled";
			// 
			// pluginViewImageList
			// 
			this.pluginViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("pluginViewImageList.ImageStream")));
			this.pluginViewImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.pluginViewImageList.Images.SetKeyName(0, "plugin");
			this.pluginViewImageList.Images.SetKeyName(1, "plugin_disabled");
			// 
			// mainWindowToolStrip
			// 
			this.mainWindowToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mainWindowToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshButton});
			this.mainWindowToolStrip.Location = new System.Drawing.Point(0, 0);
			this.mainWindowToolStrip.Name = "mainWindowToolStrip";
			this.mainWindowToolStrip.Size = new System.Drawing.Size(517, 25);
			this.mainWindowToolStrip.TabIndex = 0;
			// 
			// refreshButton
			// 
			this.refreshButton.Image = global::WinFormsControlPlugin.Properties.Resources.arrow_refresh_small;
			this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(66, 22);
			this.refreshButton.Text = "Refresh";
			this.refreshButton.Visible = false;
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// disableButton
			// 
			this.disableButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.disableButton.Enabled = false;
			this.disableButton.Location = new System.Drawing.Point(378, 35);
			this.disableButton.Name = "disableButton";
			this.disableButton.Size = new System.Drawing.Size(123, 23);
			this.disableButton.TabIndex = 2;
			this.disableButton.Text = "&Disable";
			this.disableButton.UseVisualStyleBackColor = true;
			this.disableButton.Click += new System.EventHandler(this.disableButton_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(517, 414);
			this.Controls.Add(this.mainTabs);
			this.Controls.Add(this.mainWindowToolStrip);
			this.Name = "MainWindow";
			this.Text = "Sensorium2";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.logTab.ResumeLayout(false);
			this.mainTabs.ResumeLayout(false);
			this.pluginsTab.ResumeLayout(false);
			this.mainWindowToolStrip.ResumeLayout(false);
			this.mainWindowToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabPage logTab;
		private System.Windows.Forms.ListBox logListBox;
		private System.Windows.Forms.TabPage sensorsTab;
		private System.Windows.Forms.TabControl mainTabs;
		//private System.Windows.Forms.ToolStripButton refreshButton;
		private System.Windows.Forms.TabPage pluginsTab;
		private System.Windows.Forms.ListView pluginsListView;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ImageList pluginViewImageList;
		private System.Windows.Forms.ColumnHeader enabledColumnHeader;
		private System.Windows.Forms.Button enableButton;
		private System.Windows.Forms.ToolStrip mainWindowToolStrip;
		private System.Windows.Forms.ToolStripButton refreshButton;
		private System.Windows.Forms.Button disableButton;





	}
}
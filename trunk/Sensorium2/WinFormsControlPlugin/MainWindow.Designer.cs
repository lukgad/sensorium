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
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.refreshButton = new System.Windows.Forms.ToolStripButton();
			this.logTab = new System.Windows.Forms.TabPage();
			this.logListBox = new System.Windows.Forms.ListBox();
			this.sensorsTab = new System.Windows.Forms.TabPage();
			this.mainTabs = new System.Windows.Forms.TabControl();
			this.toolStrip1.SuspendLayout();
			this.logTab.SuspendLayout();
			this.mainTabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(517, 25);
			this.toolStrip1.TabIndex = 0;
			// 
			// refreshButton
			// 
			this.refreshButton.Image = global::WinFormsControlPlugin.Properties.Resources.arrow_refresh_small;
			this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(66, 22);
			this.refreshButton.Text = "Refresh";
			this.refreshButton.ToolTipText = "Refresh";
			this.refreshButton.Visible = false;
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
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
			this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabs.Location = new System.Drawing.Point(0, 25);
			this.mainTabs.Multiline = true;
			this.mainTabs.Name = "mainTabs";
			this.mainTabs.SelectedIndex = 0;
			this.mainTabs.Size = new System.Drawing.Size(517, 389);
			this.mainTabs.TabIndex = 1;
			this.mainTabs.SelectedIndexChanged += new System.EventHandler(this.mainTabs_SelectedIndexChanged);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(517, 414);
			this.Controls.Add(this.mainTabs);
			this.Controls.Add(this.toolStrip1);
			this.Name = "MainWindow";
			this.Text = "Sensorium2";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.logTab.ResumeLayout(false);
			this.mainTabs.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.TabPage logTab;
		private System.Windows.Forms.ListBox logListBox;
		private System.Windows.Forms.TabPage sensorsTab;
		private System.Windows.Forms.TabControl mainTabs;
		private System.Windows.Forms.ToolStripButton refreshButton;





	}
}
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
			this.mainTabs = new System.Windows.Forms.TabControl();
			this.sensorsTab = new System.Windows.Forms.TabPage();
			this.logTab = new System.Windows.Forms.TabPage();
			this.logListBox = new System.Windows.Forms.ListBox();
			this.refreshLog = new System.Windows.Forms.Button();
			this.mainTabs.SuspendLayout();
			this.logTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTabs
			// 
			this.mainTabs.Controls.Add(this.sensorsTab);
			this.mainTabs.Controls.Add(this.logTab);
			this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabs.Location = new System.Drawing.Point(0, 0);
			this.mainTabs.Name = "mainTabs";
			this.mainTabs.SelectedIndex = 0;
			this.mainTabs.Size = new System.Drawing.Size(517, 391);
			this.mainTabs.TabIndex = 0;
			// 
			// sensorsTab
			// 
			this.sensorsTab.Location = new System.Drawing.Point(4, 22);
			this.sensorsTab.Name = "sensorsTab";
			this.sensorsTab.Padding = new System.Windows.Forms.Padding(3);
			this.sensorsTab.Size = new System.Drawing.Size(376, 236);
			this.sensorsTab.TabIndex = 0;
			this.sensorsTab.Text = "Sensors";
			this.sensorsTab.UseVisualStyleBackColor = true;
			// 
			// logTab
			// 
			this.logTab.Controls.Add(this.refreshLog);
			this.logTab.Controls.Add(this.logListBox);
			this.logTab.Location = new System.Drawing.Point(4, 22);
			this.logTab.Name = "logTab";
			this.logTab.Padding = new System.Windows.Forms.Padding(3);
			this.logTab.Size = new System.Drawing.Size(509, 365);
			this.logTab.TabIndex = 1;
			this.logTab.Text = "Application Log";
			this.logTab.UseVisualStyleBackColor = true;
			// 
			// logListBox
			// 
			this.logListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.logListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.logListBox.FormattingEnabled = true;
			this.logListBox.HorizontalScrollbar = true;
			this.logListBox.Location = new System.Drawing.Point(8, 6);
			this.logListBox.Name = "logListBox";
			this.logListBox.Size = new System.Drawing.Size(493, 327);
			this.logListBox.TabIndex = 0;
			// 
			// refreshLog
			// 
			this.refreshLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.refreshLog.Location = new System.Drawing.Point(439, 337);
			this.refreshLog.Name = "refreshLog";
			this.refreshLog.Size = new System.Drawing.Size(62, 20);
			this.refreshLog.TabIndex = 1;
			this.refreshLog.Text = "Refresh";
			this.refreshLog.UseVisualStyleBackColor = true;
			this.refreshLog.Click += new System.EventHandler(this.refreshLog_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(517, 391);
			this.Controls.Add(this.mainTabs);
			this.Name = "MainWindow";
			this.Text = "Sensorium2";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.mainTabs.ResumeLayout(false);
			this.logTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl mainTabs;
		private System.Windows.Forms.TabPage sensorsTab;
		private System.Windows.Forms.TabPage logTab;
		private System.Windows.Forms.ListBox logListBox;
		private System.Windows.Forms.Button refreshLog;




	}
}
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
	partial class WinFormsSettingsDialog {
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
			this.CheckBoxShowNotificationIcon = new System.Windows.Forms.CheckBox();
			this.CheckBoxMinimizeToTray = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// CheckBoxShowNotificationIcon
			// 
			this.CheckBoxShowNotificationIcon.AutoSize = true;
			this.CheckBoxShowNotificationIcon.Location = new System.Drawing.Point(13, 13);
			this.CheckBoxShowNotificationIcon.Name = "CheckBoxShowNotificationIcon";
			this.CheckBoxShowNotificationIcon.Size = new System.Drawing.Size(96, 17);
			this.CheckBoxShowNotificationIcon.TabIndex = 0;
			this.CheckBoxShowNotificationIcon.Text = "Show tray icon";
			this.CheckBoxShowNotificationIcon.UseVisualStyleBackColor = true;
			this.CheckBoxShowNotificationIcon.CheckedChanged += new System.EventHandler(this.CheckBoxShowNotificationIcon_CheckedChanged);
			// 
			// CheckBoxMinimizeToTray
			// 
			this.CheckBoxMinimizeToTray.AutoSize = true;
			this.CheckBoxMinimizeToTray.Location = new System.Drawing.Point(13, 37);
			this.CheckBoxMinimizeToTray.Name = "CheckBoxMinimizeToTray";
			this.CheckBoxMinimizeToTray.Size = new System.Drawing.Size(98, 17);
			this.CheckBoxMinimizeToTray.TabIndex = 1;
			this.CheckBoxMinimizeToTray.Text = "Minimize to tray";
			this.CheckBoxMinimizeToTray.UseVisualStyleBackColor = true;
			this.CheckBoxMinimizeToTray.CheckedChanged += new System.EventHandler(this.CheckBoxMinimizeToTray_CheckedChanged);
			// 
			// WinFormsSettingsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(124, 66);
			this.Controls.Add(this.CheckBoxMinimizeToTray);
			this.Controls.Add(this.CheckBoxShowNotificationIcon);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(140, 104);
			this.Name = "WinFormsSettingsDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.WinFormsSettingsDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox CheckBoxShowNotificationIcon;
		private System.Windows.Forms.CheckBox CheckBoxMinimizeToTray;

	}
}
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

namespace WinFormsControlPlugin
{
    sealed partial class SettingsDialog
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
			this.SettingsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.SettingsTree = new System.Windows.Forms.TreeView();
			this.SettingsTableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// SettingsTableLayoutPanel
			// 
			this.SettingsTableLayoutPanel.ColumnCount = 2;
			this.SettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.SettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.SettingsTableLayoutPanel.Controls.Add(this.SettingsTree, 0, 0);
			this.SettingsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SettingsTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.SettingsTableLayoutPanel.Name = "SettingsTableLayoutPanel";
			this.SettingsTableLayoutPanel.RowCount = 2;
			this.SettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.SettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.SettingsTableLayoutPanel.Size = new System.Drawing.Size(364, 344);
			this.SettingsTableLayoutPanel.TabIndex = 0;
			// 
			// SettingsTree
			// 
			this.SettingsTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SettingsTree.Location = new System.Drawing.Point(3, 3);
			this.SettingsTree.Name = "SettingsTree";
			this.SettingsTableLayoutPanel.SetRowSpan(this.SettingsTree, 2);
			this.SettingsTree.Size = new System.Drawing.Size(176, 338);
			this.SettingsTree.TabIndex = 0;
			this.SettingsTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SettingsTree_AfterSelect);
			// 
			// SettingsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 344);
			this.Controls.Add(this.SettingsTableLayoutPanel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.SettingsDialog_Load);
			this.Shown += new System.EventHandler(this.SettingsDialog_Shown);
			this.SettingsTableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel SettingsTableLayoutPanel;
		private System.Windows.Forms.TreeView SettingsTree;
    }
}
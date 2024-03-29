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

namespace WinFormsControlPlugin
{
	sealed partial class AboutBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxAppDescription = new System.Windows.Forms.TextBox();
			this.labelAppCopyright = new System.Windows.Forms.Label();
			this.labelAppVersion = new System.Windows.Forms.Label();
			this.labelAppProductName = new System.Windows.Forms.Label();
			this.labelProductName = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.labelCopyright = new System.Windows.Forms.Label();
			this.textBoxDescription = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Controls.Add(this.textBoxAppDescription, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.labelAppCopyright, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.labelAppVersion, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.labelAppProductName, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.okButton, 1, 4);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 5;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(334, 253);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// textBoxAppDescription
			// 
			this.textBoxAppDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxAppDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxAppDescription.Location = new System.Drawing.Point(6, 103);
			this.textBoxAppDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.textBoxAppDescription.Multiline = true;
			this.textBoxAppDescription.Name = "textBoxAppDescription";
			this.textBoxAppDescription.ReadOnly = true;
			this.textBoxAppDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxAppDescription.Size = new System.Drawing.Size(158, 120);
			this.textBoxAppDescription.TabIndex = 28;
			this.textBoxAppDescription.TabStop = false;
			this.textBoxAppDescription.Text = "Description";
			// 
			// labelAppCopyright
			// 
			this.labelAppCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelAppCopyright.Location = new System.Drawing.Point(6, 50);
			this.labelAppCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelAppCopyright.Name = "labelAppCopyright";
			this.labelAppCopyright.Size = new System.Drawing.Size(158, 50);
			this.labelAppCopyright.TabIndex = 27;
			this.labelAppCopyright.Text = "Copyright";
			// 
			// labelAppVersion
			// 
			this.labelAppVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelAppVersion.Location = new System.Drawing.Point(6, 25);
			this.labelAppVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelAppVersion.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelAppVersion.Name = "labelAppVersion";
			this.labelAppVersion.Size = new System.Drawing.Size(158, 17);
			this.labelAppVersion.TabIndex = 26;
			this.labelAppVersion.Text = "Version";
			// 
			// labelAppProductName
			// 
			this.labelAppProductName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelAppProductName.Location = new System.Drawing.Point(6, 0);
			this.labelAppProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelAppProductName.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelAppProductName.Name = "labelAppProductName";
			this.labelAppProductName.Size = new System.Drawing.Size(158, 17);
			this.labelAppProductName.TabIndex = 25;
			this.labelAppProductName.Text = "Product Name";
			// 
			// labelProductName
			// 
			this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelProductName.Location = new System.Drawing.Point(173, 0);
			this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelProductName.Name = "labelProductName";
			this.labelProductName.Size = new System.Drawing.Size(158, 17);
			this.labelProductName.TabIndex = 19;
			this.labelProductName.Text = "Product Name";
			// 
			// labelVersion
			// 
			this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVersion.Location = new System.Drawing.Point(173, 25);
			this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(158, 17);
			this.labelVersion.TabIndex = 0;
			this.labelVersion.Text = "Version";
			// 
			// labelCopyright
			// 
			this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelCopyright.Location = new System.Drawing.Point(173, 50);
			this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new System.Drawing.Size(158, 50);
			this.labelCopyright.TabIndex = 21;
			this.labelCopyright.Text = "Copyright";
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxDescription.Location = new System.Drawing.Point(173, 103);
			this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.textBoxDescription.Multiline = true;
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.ReadOnly = true;
			this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxDescription.Size = new System.Drawing.Size(158, 120);
			this.textBoxDescription.TabIndex = 23;
			this.textBoxDescription.TabStop = false;
			this.textBoxDescription.Text = "Description";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(256, 229);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 21);
			this.okButton.TabIndex = 24;
			this.okButton.Text = "&OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// AboutBox
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 271);
			this.Controls.Add(this.tableLayoutPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutBox";
			this.Padding = new System.Windows.Forms.Padding(9);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AboutBox";
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelProductName;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelCopyright;
		private System.Windows.Forms.TextBox textBoxDescription;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.TextBox textBoxAppDescription;
		private System.Windows.Forms.Label labelAppCopyright;
		private System.Windows.Forms.Label labelAppVersion;
		private System.Windows.Forms.Label labelAppProductName;
	}
}

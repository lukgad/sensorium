namespace WinFormsControlPlugin
{
	partial class SettingsControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.SettingTextBox = new System.Windows.Forms.TextBox();
			this.RemoveButton = new System.Windows.Forms.Button();
			this.AddButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.Controls.Add(this.SettingTextBox, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.RemoveButton, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.AddButton, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 52);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// SettingTextBox
			// 
			this.SettingTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SettingTextBox.Location = new System.Drawing.Point(3, 3);
			this.SettingTextBox.Name = "SettingTextBox";
			this.SettingTextBox.Size = new System.Drawing.Size(118, 20);
			this.SettingTextBox.TabIndex = 3;
			// 
			// RemoveButton
			// 
			this.RemoveButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RemoveButton.Location = new System.Drawing.Point(127, 3);
			this.RemoveButton.Name = "RemoveButton";
			this.RemoveButton.Size = new System.Drawing.Size(20, 20);
			this.RemoveButton.TabIndex = 4;
			this.RemoveButton.Text = "-";
			this.RemoveButton.UseVisualStyleBackColor = true;
			// 
			// AddButton
			// 
			this.AddButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AddButton.Location = new System.Drawing.Point(127, 29);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(20, 20);
			this.AddButton.TabIndex = 5;
			this.AddButton.Text = "+";
			this.AddButton.UseVisualStyleBackColor = true;
			// 
			// UserControl1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "UserControl1";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox SettingTextBox;
		private System.Windows.Forms.Button RemoveButton;
		private System.Windows.Forms.Button AddButton;

	}
}

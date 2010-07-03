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

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sensorium.Common;

namespace WinFormsControlPlugin {
	public partial class SettingsControl : UserControl {
		private readonly PluginSettings.Setting _setting;

		private readonly TableLayoutPanel _tableLayout;
		private readonly Button _addButton;

		private readonly List<Control> _valueControls;
		private readonly List<Button> _removeButtons;

		public SettingsControl() : this(null) {
		}

		public SettingsControl(PluginSettings.Setting setting) {
			InitializeComponent();

			_setting = setting;

			_valueControls = new List<Control>();
			_removeButtons = new List<Button>();

			_tableLayout = new TableLayoutPanel
			               	{
			               		Dock = DockStyle.Fill,
			               		Name = "_tableLayout",
			               		ColumnCount = 2,
			               		RowCount = 0,
			               		AutoScroll = true
							};

			_tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			_tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26));

			_addButton = new Button { Text = "+", Dock = DockStyle.Top, Height = 20 };
			_addButton.Click += AddButton_Click;

			foreach(string s in _setting) {
				AddRow(s);
			}

			if(_setting.Count == 0) {
				_tableLayout.RowCount = 1;
				_tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
				_tableLayout.Controls.Add(_addButton, 1, 0);
			}

			Controls.Add(_tableLayout);
		}

		private void AddButton_Click(object sender, EventArgs e) {
			AddRow();

			_setting.Add("");
		}

		private void RemoveButton_Click(object sender, EventArgs e) {
			int index = _removeButtons.IndexOf((Button) sender);

			RemoveRow(index);

			_setting.RemoveAt(index);
		}

		private void ValueControl_ValueChanged(object sender, EventArgs e) {
			_setting[_valueControls.IndexOf((Control) sender)] = _setting.ValidValues != null && _setting.ValidValues.Count != 0
			                                                     	? ((ComboBox) sender).SelectedItem.ToString()
			                                                     	: ((TextBox) sender).Text;
		}

		private void AddRow() {
			AddRow("");
		}

		private void AddRow(string value) {
			if (_setting.SingleValue && _valueControls.Count == 1)
				return;

			if(_setting.ValidValues != null && _setting.ValidValues.Count != 0) {
				_valueControls.Add(new ComboBox { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList });

				foreach (string s in _setting.ValidValues)
					((ComboBox) _valueControls[_valueControls.Count - 1]).Items.Add(s);

				((ComboBox) _valueControls[_valueControls.Count - 1]).SelectedItem = value;

				((ComboBox) _valueControls[_valueControls.Count - 1]).SelectedIndexChanged += ValueControl_ValueChanged;
			}
			else {
				_valueControls.Add(new TextBox {Text = value, Dock = DockStyle.Top});
				_valueControls[_valueControls.Count - 1].TextChanged += ValueControl_ValueChanged;
			}

			_removeButtons.Add(new Button { Text = "-", Dock = DockStyle.Top, Height = 20 });
			_removeButtons[_removeButtons.Count - 1].Click += RemoveButton_Click;

			RepackTable();
		}

		private void RemoveRow(int index) {
			_valueControls.RemoveAt(index);
			_removeButtons.RemoveAt(index);

			RepackTable();
		}

		private void RepackTable() {
			_tableLayout.Controls.Clear();
			_tableLayout.RowStyles.Clear();
			_tableLayout.RowCount = _valueControls.Count;

			for (int i = 0; i < _valueControls.Count; i++) {
				_tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));

				_tableLayout.Controls.Add(_valueControls[i], 0, i);
				_tableLayout.Controls.Add(_removeButtons[i], 1, i);
			}

			if (_setting.SingleValue && _valueControls.Count == 1)
				return;

			_tableLayout.RowCount++;
			_tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));

			_addButton.TabIndex = _tableLayout.Controls.Count;

			_tableLayout.Controls.Add(_addButton, 1, _tableLayout.RowCount - 1);
		}
	}
}

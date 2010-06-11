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

		public SettingsControl(PluginSettings.Setting setting) {
			InitializeComponent();

			_setting = setting;

			_valueControls = new List<Control>();
			_removeButtons = new List<Button>();

			_tableLayout = new TableLayoutPanel
			               	{
			               		Dock = DockStyle.Fill,
			               		Location = new System.Drawing.Point(0, 0),
			               		Name = "_tableLayout",
			               		ColumnCount = 2,
								RowCount = 1
			               	};

			_tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			_tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26));
			
			_tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));

			foreach(string s in _setting) {
				
			}
		}

		private void AddRow() {
			
		}
	}
}

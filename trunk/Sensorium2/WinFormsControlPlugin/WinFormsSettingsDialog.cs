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
using System.Windows.Forms;
using Sensorium.Common;

namespace WinFormsControlPlugin {
	public partial class WinFormsSettingsDialog : Form {
		private readonly string _pluginName;

		public WinFormsSettingsDialog(string pluginName) : this() {
			_pluginName = pluginName;
		}

		protected WinFormsSettingsDialog() {
			InitializeComponent();
		}

		private void CheckBoxShowNotificationIcon_CheckedChanged(object sender, EventArgs e) {
			SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(_pluginName)["ShowTrayIcon"][0] =
				CheckBoxShowNotificationIcon.Checked.ToString().ToLower();
		}

		private void CheckBoxMinimizeToTray_CheckedChanged(object sender, EventArgs e) {
			SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(_pluginName)["MinimizeToTray"][0] =
				CheckBoxMinimizeToTray.Checked.ToString().ToLower();
		}

		private void WinFormsSettingsDialog_Load(object sender, EventArgs e) {
			CheckBoxShowNotificationIcon.Checked =
				SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(_pluginName)["ShowTrayIcon"][0] == "true";
			CheckBoxMinimizeToTray.Checked =
				SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(_pluginName)["MinimizeToTray"][0] == "true";
		}


	}
}

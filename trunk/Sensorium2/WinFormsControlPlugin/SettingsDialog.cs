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

namespace WinFormsControlPlugin
{
    public sealed partial class SettingsDialog : Form
    {
    	private readonly PluginSettings _pluginSettings;

    	private SettingsControl _settingsControl;

        public SettingsDialog(string pluginName) : this() {
            Text = string.Format("{0} - Settings", pluginName);

        	_pluginSettings = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(pluginName);
        }

        private SettingsDialog() {
            InitializeComponent();
        }

        private void SettingsDialog_Load(object sender, EventArgs e) {
			foreach(string key in _pluginSettings.Keys)
				if(key != "Enabled") //There is already a way to Enable/Disable plugins
					SettingsTree.Nodes.Add(key, key);
        }

		private void SettingsDialog_Shown(object sender, EventArgs e) {
			if (SettingsTree.Nodes.Count != 0) return;

			MessageBox.Show(Parent, "This plugin has no known configurable settings.", "No Settings", MessageBoxButtons.OK,
							MessageBoxIcon.Information);

			Close();
		}

		private void SettingsTree_AfterSelect(object sender, TreeViewEventArgs e) {
			if (_settingsControl != null && SettingsTableLayoutPanel.Controls.Contains(_settingsControl))
				SettingsTableLayoutPanel.Controls.Remove(_settingsControl);

			_settingsControl = new SettingsControl(_pluginSettings[e.Node.Name]);
			_settingsControl.Dock = DockStyle.Fill;

			SettingsTableLayoutPanel.Controls.Add(_settingsControl, 1, 0);
		}
    }
}

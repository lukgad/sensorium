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
using log4net.Core;
using Sensorium.Common;

namespace WinFormsControlPlugin {
	public partial class MainWindow : Form {
		private readonly AboutBox _aboutBox;

		public MainWindow() {
			InitializeComponent();

			_aboutBox = new AboutBox();
		}
		
		private void MainWindow_Load(object sender, EventArgs e) {
			Text = "Sensorium2 v" + SensoriumFactory.GetAppInterface().Version;

            foreach (string p in SensoriumFactory.GetAppInterface().Plugins.Keys)
			{
				ListViewItem listItem = new ListViewItem(SensoriumFactory.GetAppInterface().Plugins[p].Name,
														 SensoriumFactory.GetAppInterface().Plugins[p].Enabled
															? "plugin"
															: "plugin_disabled");
				listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem,
																	   SensoriumFactory.GetAppInterface().Plugins[p].Enabled.
																		ToString()));
				listViewPlugins.Items.Add(listItem);
			}

			RefreshListBoxLog(sender, e);
		}

		public new void Close() {
			if (InvokeRequired) {
				Invoke(new Action(Close));
				return;
			}

			base.Close();
		}

		private void tabsMain_SelectedIndexChanged(object sender, EventArgs e) {
			buttonRefresh.Visible = tabLog.Visible || tabPlugins.Visible;
		}

		private void buttonRefresh_Click(object sender, EventArgs e) {
			if(tabPlugins.Visible)
				ListViewPluginRefresh(sender, e);

			if (!tabLog.Visible) return;

			RefreshListBoxLog(sender, e);
		}

		private void RefreshListBoxLog(object sender, EventArgs e) {
			listBoxLog.BeginUpdate();
			listBoxLog.Items.Clear();

			foreach (LoggingEvent le in SensoriumFactory.GetAppInterface().Log.GetEvents())
			{
				listBoxLog.Items.Add(String.Format("[{0}] {1} {2} - {3}", le.GetLoggingEventData().TimeStamp,
												   le.GetLoggingEventData().Level, le.GetLoggingEventData().LoggerName,
												   le.GetLoggingEventData().Message));
			}

			listBoxLog.EndUpdate();
		}

		private void listViewPlugins_SelectedIndexChanged(object sender, EventArgs e) {
			if (listViewPlugins.SelectedItems.Count == 1) {
				buttonEnable.Enabled = !SensoriumFactory.GetAppInterface().Plugins[listViewPlugins.SelectedItems[0].Text].Enabled;
				buttonDisable.Enabled = SensoriumFactory.GetAppInterface().Plugins[listViewPlugins.SelectedItems[0].Text].Enabled;
			}
			else if (listViewPlugins.SelectedItems.Count == 0) 
				buttonEnable.Enabled = buttonDisable.Enabled = false;
			else buttonEnable.Enabled = buttonDisable.Enabled = true;
		}

		private void buttonEnable_Click(object sender, EventArgs e) {
			foreach(ListViewItem i in listViewPlugins.SelectedItems) {
				if(SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled)
					continue;
			}

			ListViewPluginRefresh(sender, e);
		}

		private void buttonDisable_Click(object sender, EventArgs e) {
			foreach (ListViewItem i in listViewPlugins.SelectedItems) {
				if (!SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled)
					continue;

				SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled = false;
				SensoriumFactory.GetAppInterface().Plugins[i.Text].Stop();
			}

			ListViewPluginRefresh(sender, e);
		}

		private void buttonAbout_Click(object sender, EventArgs e) {
			_aboutBox.ShowDialog();
		}

		private void ListViewPluginRefresh(object sender, EventArgs e) {
			foreach (ListViewItem i in listViewPlugins.Items) {
				i.ImageKey = SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled
				             	? "plugin"
				             	: "plugin_disabled";
			}

			listViewPlugins_SelectedIndexChanged(sender, e);
		}
	}
}

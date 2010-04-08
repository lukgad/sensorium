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
using log4net;
using log4net.Core;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace WinFormsControlPlugin {
	public partial class MainWindow : Form {
		private readonly AboutBox _aboutBox;

		private readonly ILog _log = LogManager.GetLogger(typeof (WinFormsControlPlugin));
		
		public MainWindow() {
			InitializeComponent();

			_aboutBox = new AboutBox();
		}
		
		private void MainWindow_Load(object sender, EventArgs e) {
			Text = "Sensorium2 " + SensoriumFactory.GetAppInterface().Version;

			//Add all plugins to the ListView. Only needs to be done once, as the available plugins probably won't change while running.
            foreach (string p in SensoriumFactory.GetAppInterface().Plugins.Keys)
			{
				ListViewItem listItem = new ListViewItem(SensoriumFactory.GetAppInterface().Plugins[p].Name,
														 SensoriumFactory.GetAppInterface().Plugins[p].Enabled
															? "plugin"
															: "plugin_disabled");
				listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem,
					SensoriumFactory.GetAppInterface().Plugins[p].Enabled.ToString()));
				listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem,
					SensoriumFactory.GetAppInterface().Plugins[p].Type.ToString()));
				listViewPlugins.Items.Add(listItem);
			}

			//Output the log so far to ListBox.
			RefreshListBoxLog(sender, e);
		}

		public new void Close() {
			//Silly hack so I can close the form from another thread <_<
			if (InvokeRequired) {
				Invoke(new Action(Close));
				return;
			}

			base.Close();
		}

		private void tabsMain_SelectedIndexChanged(object sender, EventArgs e) {
			//Show the appropriate toolstrip buttons for the current tab
			buttonRefresh.Visible = tabLog.Visible || tabPlugins.Visible;
			buttonEnable.Visible = buttonDisable.Visible = tabPlugins.Visible;
		}

		private void buttonRefresh_Click(object sender, EventArgs e) {
			//Refresh the current tab
			if(tabPlugins.Visible)
				ListViewPluginRefresh(sender, e);

			if (!tabLog.Visible) return;

			RefreshListBoxLog(sender, e);
		}

		private void RefreshListBoxLog(object sender, EventArgs e) {
			listBoxLog.BeginUpdate();
			listBoxLog.Items.Clear();

			//Write all log events to the listbox
			foreach (LoggingEvent le in SensoriumFactory.GetAppInterface().Log.GetEvents())
			{
				listBoxLog.Items.Add(String.Format("[{0}] {1} {2} - {3}", le.GetLoggingEventData().TimeStamp,
												   le.GetLoggingEventData().Level, le.GetLoggingEventData().LoggerName,
												   le.GetLoggingEventData().Message));
			}

			listBoxLog.EndUpdate();
		}

		private void listViewPlugins_SelectedIndexChanged(object sender, EventArgs e) {
			//Enabled the appropriate buttons for the currently selected plugin/s
			if (listViewPlugins.SelectedItems.Count == 1) {
				buttonEnable.Enabled = !SensoriumFactory.GetAppInterface().Plugins[listViewPlugins.SelectedItems[0].Text].Enabled;
				buttonDisable.Enabled = SensoriumFactory.GetAppInterface().Plugins[listViewPlugins.SelectedItems[0].Text].Enabled;

				labelPluginName.Text = String.Format("Name: {0}", 
					SensoriumFactory.GetAppInterface().Plugins[listViewPlugins.SelectedItems[0].Text].Name);
				labelPluginVersion.Text = String.Format("Version: {0}",
					(double) SensoriumFactory.GetAppInterface().Plugins[listViewPlugins.SelectedItems[0].Text].Version / 10);
			} else {
				if (listViewPlugins.SelectedItems.Count == 0)
					buttonEnable.Enabled = buttonDisable.Enabled = false;
				else buttonEnable.Enabled = buttonDisable.Enabled = true;

				labelPluginName.Text = "Name:";
				labelPluginVersion.Text = "Version:";
			}
		}

		private void buttonEnable_Click(object sender, EventArgs e) {
			foreach(ListViewItem i in listViewPlugins.SelectedItems) {
				if(SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled)
					continue;

				SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled = true;

				if (SensoriumFactory.GetAppInterface().Plugins[i.Text].Type == PluginType.Control) {
					i.ImageKey = "arrow_refresh_small";
					continue;
				}
                
				_log.Info(String.Format("Initializing plugin {0}...", 
					SensoriumFactory.GetAppInterface().Plugins[i.Text].Name));

				SensoriumFactory.GetAppInterface().Plugins[i.Text].ReInit();

				_log.Info(String.Format("Starting {0}", 
					SensoriumFactory.GetAppInterface().Plugins[i.Text].Name));
				SensoriumFactory.GetAppInterface().Plugins[i.Text].Start();
			}

			ListViewPluginRefresh(sender, e);
		}

		private void buttonDisable_Click(object sender, EventArgs e) {
			foreach (ListViewItem i in listViewPlugins.SelectedItems) {
				if (!SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled)
					continue;

				SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled = false;

				if (SensoriumFactory.GetAppInterface().Plugins[i.Text].Type == PluginType.Control) {
					i.ImageKey = "arrow_refresh_small";
					continue;
				}

				_log.Info(String.Format("Stopping {0}",
					SensoriumFactory.GetAppInterface().Plugins[i.Text].Name));
				SensoriumFactory.GetAppInterface().Plugins[i.Text].Stop();
			}

			ListViewPluginRefresh(sender, e);
		}

		private void buttonAbout_Click(object sender, EventArgs e) {
			_aboutBox.ShowDialog();
		}

		private void ListViewPluginRefresh(object sender, EventArgs e) {
			//Update the status' in the plugin list
			foreach (ListViewItem i in listViewPlugins.Items) {
				i.SubItems[1].Text = SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled.ToString();

				if (SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled != SensoriumFactory.GetAppInterface().Plugins[i.Text].Running)
					continue;

				i.ImageKey = SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled
				             	? "plugin"
				             	: "plugin_disabled";
			}

			listViewPlugins_SelectedIndexChanged(sender, e);
		}
	}
}

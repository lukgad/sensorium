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
using log4net;
using log4net.Core;
using Sensorium.Core;
using Sensorium.Core.Plugins;

namespace WinFormsControlPlugin {
	public partial class MainWindow : Form {
		private readonly AboutBox _aboutBox;

		private readonly ILog _log = LogManager.GetLogger(typeof (MainWindow));

		private Level _logLevel = Level.Info;
		
		public MainWindow() {
			InitializeComponent();

			ImageList iconImageList = new ImageList();

			iconImageList.Images.Add("arrow_refresh_small", Properties.Resources.arrow_refresh_small);
			iconImageList.Images.Add("plugin", Properties.Resources.plugin);
			iconImageList.Images.Add("plugin_disabled", Properties.Resources.plugin_disabled);

			ListViewPlugins.SmallImageList = iconImageList;


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
				ListViewPlugins.Items.Add(listItem);
			}

			//Output the log so far to ListBox.
			RefreshListBoxLog();

			RefreshListViewSensors(sender, e);

			Timer refreshTimer = new Timer { Interval = 1000 };
			refreshTimer.Tick += RefreshListViewSensors;
			refreshTimer.Start();

			ListViewSensors.SetDoubleBuffered(true);

			//Populate app settings boxes
			PluginDirectoryTextbox.Text = SensoriumFactory.GetAppInterface().GetSetting("PluginDirectory");
			SettingsDirectoryTextbox.Text = SensoriumFactory.GetAppInterface().GetSetting("SettingsDirectory");
			FriendlyNameTextbox.Text = SensoriumFactory.GetAppInterface().GetSetting("FriendlyName");

			TrayIcon.Visible =
				SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(WinFormsControlPlugin.PluginName)["ShowTrayIcon"][0] == "true";

			SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(WinFormsControlPlugin.PluginName)["ShowTrayIcon"].ValueChanged += TrayIconVisibleChanged;
		}

		private delegate void CloseAction();

		public new void Close() {
			//Silly hack so I can close the form from another thread <_<
			if (InvokeRequired) {
				Invoke(new CloseAction(Close));
				return;
			}

			base.Close();
		}

		private void TabsMainSelectedIndexChanged(object sender, EventArgs e) {
			//Show the appropriate toolstrip buttons for the current tab
			ButtonRefresh.Visible = TabLog.Visible || TabPlugins.Visible;
			ToolStripSeparator2.Visible = ButtonSettings.Visible = ButtonEnable.Visible = ButtonDisable.Visible = TabPlugins.Visible;

			ToolStripSeparator1.Visible = LogLevelLabel.Visible = LogLevelDropDownButton.Visible = TabLog.Visible;

			if (TabPlugins.Visible)
				ListViewPluginsSelectedIndexChanged(sender, e);

			if (ButtonRefresh.Visible)
				ButtonRefreshClick(sender, e);
		}

		private void ButtonRefreshClick(object sender, EventArgs e) {
			//Refresh the current tab
			if(TabPlugins.Visible)
				ListViewPluginRefresh(sender, e);

			if (TabLog.Visible)
				RefreshListBoxLog();
		}

		private void RefreshListBoxLog() {
			ListBoxLog.BeginUpdate();
			ListBoxLog.Items.Clear();

			//Write all log events to the listbox
			foreach (LoggingEvent le in SensoriumFactory.GetAppInterface().Log.GetEvents()) {
				if (le.GetLoggingEventData().Level < _logLevel) continue;

				ListBoxLog.Items.Add(String.Format("[{0}] {1} {2} - {3}", le.GetLoggingEventData().TimeStamp,
												   le.GetLoggingEventData().Level, le.GetLoggingEventData().LoggerName,
												   le.GetLoggingEventData().Message));
			}

			ListBoxLog.EndUpdate();
		}

		private void ListViewPluginsSelectedIndexChanged(object sender, EventArgs e) {
			//Enabled the appropriate buttons for the currently selected plugin/s
			if (ListViewPlugins.SelectedItems.Count == 1) {
				ButtonSettings.Enabled = true;

				ButtonEnable.Enabled = !SensoriumFactory.GetAppInterface().Plugins[ListViewPlugins.SelectedItems[0].Text].Enabled;
				ButtonDisable.Enabled = SensoriumFactory.GetAppInterface().Plugins[ListViewPlugins.SelectedItems[0].Text].Enabled;

				LabelPluginName.Text = String.Format("Name: {0}", 
					SensoriumFactory.GetAppInterface().Plugins[ListViewPlugins.SelectedItems[0].Text].Name);
				LabelPluginVersion.Text = String.Format("Version: {0}",
					(double) SensoriumFactory.GetAppInterface().Plugins[ListViewPlugins.SelectedItems[0].Text].Version / 10);
				TextBoxPluginDescription.Text = String.Format("Description: {0}{1}", Environment.NewLine,
					SensoriumFactory.GetAppInterface().Plugins[ListViewPlugins.SelectedItems[0].Text].Description);
			} else {
				ButtonSettings.Enabled = false;

				if (ListViewPlugins.SelectedItems.Count == 0)
					ButtonEnable.Enabled = ButtonDisable.Enabled = false;
				else ButtonEnable.Enabled = ButtonDisable.Enabled = true;

				LabelPluginName.Text = "Name:";
				LabelPluginVersion.Text = "Version:";
				TextBoxPluginDescription.Text = "Description:";
			}
		}

		private void ButtonEnableClick(object sender, EventArgs e) {
			foreach(ListViewItem i in ListViewPlugins.SelectedItems) {
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

		private void ButtonDisableClick(object sender, EventArgs e) {
			foreach (ListViewItem i in ListViewPlugins.SelectedItems) {
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

		private void ButtonAboutClick(object sender, EventArgs e) {
			_aboutBox.ShowDialog();
		}

		private void ListViewPluginRefresh(object sender, EventArgs e)
		{
			//Update the status' in the plugin list
			foreach (ListViewItem i in ListViewPlugins.Items)
			{
				i.SubItems[1].Text = SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled.ToString();

				if (SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled !=
				    SensoriumFactory.GetAppInterface().Plugins[i.Text].Running)
					continue;

				i.ImageKey = SensoriumFactory.GetAppInterface().Plugins[i.Text].Enabled
				             	? "plugin"
				             	: "plugin_disabled";
			}

			ListViewPluginsSelectedIndexChanged(sender, e);
		}

		private void RefreshListViewSensors(object sender, EventArgs e) {
			Dictionary<String, List<Sensor>> sortedSensors = new Dictionary<string, List<Sensor>>();

			//Special case where sortedList will have 0 keys (and the foreach blocks won't be executed)
			if (SensoriumFactory.GetAppInterface().Sensors.Count == 0){
				ListViewSensors.Items.Clear();
				return;
			}

			//Split the sensors up by hostID
			foreach(Sensor s in SensoriumFactory.GetAppInterface().Sensors) {
				if(!sortedSensors.ContainsKey(s.HostGuid))
					sortedSensors.Add(s.HostGuid, new List<Sensor>());
				
				sortedSensors[s.HostGuid].Add(s);
			}

			foreach (string k in sortedSensors.Keys) {
				//Make sure the Group exists before adding items to it :P
				bool containsGroup = false;
				foreach (ListViewGroup g in ListViewSensors.Groups) {
					if (g.Name != k) continue;
					containsGroup = true;
					break;
				}

				if (!containsGroup)
					ListViewSensors.Groups.Add(k, sortedSensors[k][0].HostFriendlyName);

				//Speed up the refresh (and reduce flickering) when the number of items is the same as last time
				if (ListViewSensors.Groups[k].Items.Count == sortedSensors[k].Count) {
					foreach (ListViewItem i in ListViewSensors.Groups[k].Items) {
						if (i.SubItems[0].Text == sortedSensors[k][i.Index].Name && i.SubItems[1].Text == sortedSensors[k][i.Index].Type &&
						    i.SubItems[2].Text ==
						    ((DataPlugin) SensoriumFactory.GetAppInterface().Plugins[sortedSensors[k][i.Index].SourcePlugin]).
						    	SensorToString(
						    		sortedSensors[k][i.Index])) continue;

						i.SubItems[0].Text = sortedSensors[k][i.Index].Name;
						i.SubItems[1].Text = sortedSensors[k][i.Index].Type;

						i.SubItems[2].Text =
							((DataPlugin) SensoriumFactory.GetAppInterface().Plugins[sortedSensors[k][i.Index].SourcePlugin]).SensorToString
								(
									sortedSensors[k][i.Index]);
					}
				} else {
					ListViewSensors.Groups[k].Items.Clear();
					foreach (Sensor s in sortedSensors[k]) {
						ListViewItem listItem = new ListViewItem(new string[] {
						s.Name, s.Type, 
						((DataPlugin) SensoriumFactory.GetAppInterface().Plugins[s.SourcePlugin]).SensorToString(s)}, 0,
							 ListViewSensors.Groups[k]);

						ListViewSensors.Items.Add(listItem);
					}
				}
			}
		}

        private void ButtonSettings_Click(object sender, EventArgs e) {
			if (ListViewPlugins.SelectedItems[0].Text == WinFormsControlPlugin.PluginName) {
				(new WinFormsSettingsDialog(WinFormsControlPlugin.PluginName)).ShowDialog();
				return;
			}

            (new SettingsDialog(ListViewPlugins.SelectedItems[0].Text)).ShowDialog();
        }

		private void LogLevelDropDownButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
			LogLevelDropDownButton.Text = e.ClickedItem.Text;

			switch(e.ClickedItem.Text) {
				case "All":
					_logLevel = Level.All;
					break;
				case "Info":
					_logLevel = Level.Info;
					break;
				case "Warning":
					_logLevel = Level.Warn;
					break;
				case "Error":
					_logLevel = Level.Error;
					break;
			}

			RefreshListBoxLog();
		}

		private void SaveButton_Click(object sender, EventArgs e) {
			SensoriumFactory.GetAppInterface().SetSetting("PluginDirectory", PluginDirectoryTextbox.Text);
			SensoriumFactory.GetAppInterface().SetSetting("SettingsDirectory", SettingsDirectoryTextbox.Text);
			SensoriumFactory.GetAppInterface().SetSetting("FriendlyName", FriendlyNameTextbox.Text);
		}

		private void ResetButton_Click(object sender, EventArgs e) {
			PluginDirectoryTextbox.Text = SensoriumFactory.GetAppInterface().GetSetting("PluginDirectory");
			SettingsDirectoryTextbox.Text = SensoriumFactory.GetAppInterface().GetSetting("SettingsDirectory");
			FriendlyNameTextbox.Text = SensoriumFactory.GetAppInterface().GetSetting("FriendlyName");
		}

		private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
			WindowState = FormWindowState.Normal;
			ShowInTaskbar = true;
			BringToFront();
			Focus();
		}

		private void MainWindow_Resize(object sender, EventArgs e) {
			if(WindowState == FormWindowState.Minimized && 
				SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(WinFormsControlPlugin.PluginName)["MinimizeToTray"][0] == "true" &&
				SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(WinFormsControlPlugin.PluginName)["ShowTrayIcon"][0] == "true")
					ShowInTaskbar = false;
		}

		private void TrayIconVisibleChanged(object sender, EventArgs e) {
			TrayIcon.Visible =
				SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(WinFormsControlPlugin.PluginName)["ShowTrayIcon"][0] == "true";
		}
	}
}

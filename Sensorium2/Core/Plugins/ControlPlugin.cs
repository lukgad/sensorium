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
using System.ComponentModel;

namespace Sensorium.Core.Plugins {
	public abstract class ControlPlugin : IPluginInterface {
		protected List<CommPlugin> CommPlugins;
		protected List<ControlPlugin> ControlPlugins;
		protected List<DataPlugin> DataPlugins;
		protected List<SettingsPlugin> SettingsPlugins;
		
		protected PluginSettings Settings;

		protected Type[] PluginTypes { get {
			return new Type[] {
	       		typeof (CommPlugin),
	       		typeof (ControlPlugin),
	       		typeof (DataPlugin),
	       		typeof (SettingsPlugin)
			};
		} }
		/// <summary>
		/// Gets the plugin's name
		/// </summary>
		public abstract string Name { get; }
		/// <summary>
		/// Gets the plugin's version
		/// </summary>
		public abstract int Version { get; }
		/// <summary>
		/// Gets the plugin's description
		/// </summary>
		public abstract string Description { get; }

		/// <summary>
		/// Gets or sets the plugin's enabled state.
		/// </summary>
		public virtual bool Enabled {
			get {
				return bool.Parse(Settings["Enabled"][0]);
			}
			set {
				Settings["Enabled"][0] = value.ToString();
			}
		}
		/// <summary>
		/// Starts the plugin
		/// </summary>
		public virtual void Start() {
			Running = true;
			SensoriumFactory.GetAppInterface().HideConsoleEventHandler += HandleHideConsole;
		}
		/// <summary>
		/// Stops the plugin
		/// </summary>
		public virtual void Stop() {
			Running = false;
			SensoriumFactory.GetAppInterface().HideConsoleEventHandler -= HandleHideConsole;
		}
		/// <summary>
		/// Gets the plugin's running state
		/// </summary>
		public bool Running { get; private set; }
		/// <summary>
		/// Gets the plugin's type
		/// </summary>
		public virtual PluginType Type
		{
			get { return PluginType.Control; }
		}
		/// <summary>
		/// (Re)Initializes the plugin
		/// </summary>
		public virtual void ReInit() {
			Init();
		}

		private readonly PluginSettings _defaultSettings = new PluginSettings { { "Enabled", new PluginSettings.Setting(true, new List<string> { "True", "False" }) { "True" } } };

		/// <summary>
		/// Gets the plugin's default settings
		/// </summary>
		public PluginSettings DefaultSettings
		{
			get { return _defaultSettings; }
		}

		protected virtual void HandleHideConsole(object sender, CancelEventArgs e) {
			
		}

		/// <summary>
		/// EventHandler for exiting the app.
		/// </summary>
		public event EventHandler<EventArgs> ExitEventHandler;

		protected virtual void OnExit() {
			EventHandler<EventArgs> handler = ExitEventHandler;
			if (handler != null) {
				handler(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Initialize the plugin
		/// </summary>
		public virtual void Init(){
			CommPlugins = new List<CommPlugin>();
			ControlPlugins = new List<ControlPlugin>();
			DataPlugins = new List<DataPlugin>();
			SettingsPlugins = new List<SettingsPlugin>();

			Settings = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(Name);

			if (!(Enabled = Boolean.Parse(Settings["Enabled"][0])))
				return;

			foreach(string s in SensoriumFactory.GetAppInterface().Plugins.Keys) {
				if (SensoriumFactory.GetAppInterface().Plugins[s] is CommPlugin)
					CommPlugins.Add((CommPlugin) SensoriumFactory.GetAppInterface().Plugins[s]);
				else if (SensoriumFactory.GetAppInterface().Plugins[s] is ControlPlugin)
					ControlPlugins.Add((ControlPlugin)SensoriumFactory.GetAppInterface().Plugins[s]);
				else if (SensoriumFactory.GetAppInterface().Plugins[s] is DataPlugin)
					DataPlugins.Add((DataPlugin) SensoriumFactory.GetAppInterface().Plugins[s]);
				else if (SensoriumFactory.GetAppInterface().Plugins[s] is SettingsPlugin)
					SettingsPlugins.Add((SettingsPlugin)SensoriumFactory.GetAppInterface().Plugins[s]);
			}
		}
	}
}

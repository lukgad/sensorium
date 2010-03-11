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

namespace Sensorium.Common.Plugins {
	public abstract class ControlPlugin : IPluginInterface {
		protected List<CommPlugin> CommPlugins;
		protected List<ControlPlugin> ControlPlugins;
		protected List<DataPlugin> DataPlugins;
		protected List<SettingsPlugin> SettingsPlugins;
		
		protected Dictionary<string, List<string>> Settings;

		protected Type[] PluginTypes { get {
			return new Type[] {
	       		typeof (CommPlugin),
	       		typeof (ControlPlugin),
	       		typeof (DataPlugin),
	       		typeof (SettingsPlugin)
			};
		} }
		public abstract string Name { get; }
		public abstract int Version { get; }

		private bool _enabled = true;
		public virtual bool Enabled {
			get {
				return _enabled;
			}
			set {
				Settings["Enabled"][0] = value.ToString();
				_enabled = value;
			}
		}

		public virtual void Start() {}
		public virtual void Stop() {}

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

			Settings = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(Name);
		}
	}
}

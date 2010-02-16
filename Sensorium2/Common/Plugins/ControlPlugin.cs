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

		public abstract void Start();
		public abstract void Stop();

		public event EventHandler<EventArgs> Exit;

		protected virtual void OnExit() {
			EventHandler<EventArgs> handler = Exit;
			if (handler != null) {
				handler(this, EventArgs.Empty);
			}
		}

		public virtual void Init(){
			CommPlugins = new List<CommPlugin>();
			ControlPlugins = new List<ControlPlugin>();
			DataPlugins = new List<DataPlugin>();
			SettingsPlugins = new List<SettingsPlugin>();

			foreach(IPluginInterface i in SensoriumFactory.GetAppInterface().Plugins) {
				if (i is CommPlugin)
					CommPlugins.Add((CommPlugin) i);
				else if (i is ControlPlugin)
					ControlPlugins.Add((ControlPlugin) i);
				else if (i is DataPlugin)
					DataPlugins.Add((DataPlugin) i);
				else if (i is SettingsPlugin)
					SettingsPlugins.Add((SettingsPlugin) i);
			}

			Settings = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(Name);
		}
	}
}

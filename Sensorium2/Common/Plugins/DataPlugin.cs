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

namespace Sensorium.Common.Plugins
{
	public abstract class DataPlugin : IPluginInterface
	{
		protected List<Sensor> _Sensors;
		public List<Sensor> Sensors
		{
			get { return new List<Sensor>(_Sensors); }
			protected set { _Sensors = value; }
		}

		protected PluginSettings Settings;
		
		protected DataPlugin() {
			Sensors = new List<Sensor>();
		}

		/// <summary>
		/// Initialize the plugin.
		/// </summary>
		/// <param name="mode"></param>
		public virtual void Init(PluginMode mode) {
			Settings = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(Name);

			if (!Settings.ContainsKey("Enabled"))
				Settings.Add("Enabled", new Setting {_Enabled.ToString()});
		}

		public abstract string Name { get; }
		public abstract int Version { get; }
		public abstract string Description { get; }

		protected bool _Enabled = true;
		public virtual bool Enabled
		{
			get {
				return _Enabled;
			}
			set {
				Settings["Enabled"][0] = value.ToString();
				_Enabled = value;
			}
		}

		public virtual void Start() {
			Running = true;
		}

		public virtual void Stop() {
			Running = false;
			_Sensors = new List<Sensor>();
		}

		public bool Running { get; private set; }

		public virtual PluginType Type
		{
			get { return PluginType.Data; }
		}

		
		public virtual void ReInit() {
			Init(Enabled ? PluginMode.Server : PluginMode.Client);
		}

		private readonly PluginSettings _defaultSettings = new PluginSettings { { "Enabled", new Setting { "True" } } };

		public PluginSettings DefaultSettings
		{
			get { return _defaultSettings; }
		}

		public abstract string SensorToString(Sensor sensor);
	}
}
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

namespace Sensorium.Core.Plugins
{
	public abstract class DataPlugin : IPluginInterface
	{
		/// <summary>
		/// Initialize the plugin.
		/// </summary>
		/// <param name="mode">Plugin Mode</param>
		public virtual void Init(PluginMode mode) {
			Settings = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(Name);
		}

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
		/// Gets a string representation of a sensor provided by this plugin, locally or remotely
		/// </summary>
		/// <param name="sensor"></param>
		/// <returns>A string representation of the sensor</returns>
		public abstract string SensorToString(Sensor sensor);

		protected List<Sensor> _Sensors;
		/// <summary>
		/// Gets the sensors provided by this plugin
		/// </summary>
		public List<Sensor> Sensors
		{
			get { return new List<Sensor>(_Sensors); }
			protected set { _Sensors = value; }
		}

		protected PluginSettings Settings;

		protected DataPlugin()
		{
			Sensors = new List<Sensor>();
		}
		/// <summary>
		/// Gets or sets the plugin's enabled state.
		/// </summary>
		public virtual bool Enabled
		{
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
		}
		/// <summary>
		/// Stops the plugin
		/// </summary>
		public virtual void Stop() {
			Running = false;
			_Sensors = new List<Sensor>();
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
			get { return PluginType.Data; }
		}
		/// <summary>
		/// (Re)Initializes the plugin
		/// </summary>
		public virtual void ReInit() {
			Init(Enabled ? PluginMode.Server : PluginMode.Client);
		}

		private readonly PluginSettings _defaultSettings = 
			new PluginSettings { { "Enabled", new PluginSettings.Setting(true, 
				new List<string> { "True", "False" }) { "True" } } };
		/// <summary>
		/// Gets the plugin's default settings
		/// </summary>
		public PluginSettings DefaultSettings
		{
			get { return _defaultSettings; }
		}
	}
}
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

using System.Collections.Generic;

namespace Sensorium.Core.Plugins
{
	public abstract class CommPlugin : IPluginInterface
	{
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


		private PluginMode _mode;
		/// <summary>
		/// Gets the plugin's mode (Client or Server)
		/// </summary>
		public virtual PluginMode Mode
		{
			get { return _mode; }
			protected set {
				_mode = value;
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
			get { return PluginType.Comm; }
		}

		/// <summary>
		/// (Re)Initializes the plugin
		/// </summary>
		public virtual void ReInit() {
			Init(PluginMode.Default);
		}

		private readonly PluginSettings _defaultSettings = 
			new PluginSettings {
					{ "Enabled", new PluginSettings.Setting(true, new List<string> { "True", "False" }) { "True" } },
					{ "Mode", new PluginSettings.Setting(true, new List<string> {"Client", "Server"}) { "Client" } }
				};

		/// <summary>
		/// Gets the plugin's default settings
		/// </summary>
		public PluginSettings DefaultSettings { 
			get {
				return _defaultSettings;
			}
		}

		protected CommPlugin() {
			_Sensors = new List<Sensor>();
		}

		/// <summary>
		/// Initialize the plugin.
		/// </summary>
		/// <param name="mode">Plugin mode</param>
		public virtual void Init(PluginMode mode) {
			Settings = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(Name);

			Mode = mode;

			if (Settings["Mode"].Count == 0)
				Settings["Mode"].Add(_defaultSettings["Mode"][0]);

			if (Mode != PluginMode.Default) return;

			//If in "default" mode, load default mode from config);
			if (Settings["Mode"][0].ToLower().Equals("client"))
				Mode = PluginMode.Client;
			else if (Settings["Mode"][0].ToLower().Equals("server"))
				Mode = PluginMode.Server;
		}
	}
}
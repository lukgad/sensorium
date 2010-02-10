﻿/*	This file is part of Sensorium2 <http://code.google.com/p/sensorium>
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
	public abstract class CommPlugin : IPluginInterface
	{
		protected List<Sensor> _sensors;
		public List<Sensor> Sensors
		{
			get { return new List<Sensor>(_sensors); }
			protected set { _sensors = value; }
		}

		private Dictionary<string, string> _settings;

		public abstract string Name { get; }
		public abstract int Version { get; }

		protected string HostId;

		private bool _enabled = true;

		public virtual bool Enabled {
			get {
				return _enabled;
			}
			set {
				_settings["Enabled"] = value.ToString();
				_enabled = value;
			}
		}

		public PluginMode Mode { get; protected set; }

		//public abstract void Init(Dictionary<string, string> settings);
		public abstract void Stop();
		public abstract void Start();

		public CommPlugin()
		{
			Sensors = new List<Sensor>();
		}

		public virtual void Init(Dictionary<string, string> settings, PluginMode mode, List<Sensor> sensors, string hostId) {
			if (sensors == null)
				throw new NullReferenceException();

			Mode = mode;

			HostId = hostId;

			_settings = settings;

			if (!_settings.ContainsKey("Enabled"))
				_settings.Add("Enabled", _enabled.ToString());
		}
	}
}
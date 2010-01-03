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

namespace Common.Plugins
{
	public abstract class CommPlugin : IPluginInterface
	{
		public List<Sensor> Sensors { get; protected set; }

		public abstract string Name { get; }
		public abstract int Version { get; }

		private bool _enabled = true;

		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}

		public PluginMode Mode { get; protected set; }

		public abstract void Init(Dictionary<string, string> settings);
		public virtual void Init(Dictionary<string, string> settings, PluginMode mode, List<DataPlugin> dataPlugins) {
			if(dataPlugins == null)
				throw new NullReferenceException();

			Mode = mode;

			if (!settings.ContainsKey("Enabled"))
				settings.Add("Enabled", Enabled.ToString());
		}
	}
}
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

namespace Sensorium.Common.Plugins
{
	public abstract class SettingsPlugin : IPluginInterface
	{
		public abstract string Name { get; }
		public abstract int Version { get; }

		public virtual bool Enabled { get; set; }

		/// <summary>
		/// Initialize the plugin
		/// </summary>
		/// <param name="settingsDir">Prefered settings file directory</param>
		public abstract void Init(string settingsDir);
		public virtual void Start() {}
        public virtual void Stop() {}

		/// <summary>
		/// Retrieves settings for specified plugin.
		/// </summary>
		/// <param name="pluginName">Name of plugin</param>
		/// <returns>Specified plugin's settings</returns>
		public abstract Dictionary<string,List<string>> GetSettings(string pluginName);

		/// <summary>
		/// Save all settings
		/// </summary>
		public abstract void Save();
	}
}
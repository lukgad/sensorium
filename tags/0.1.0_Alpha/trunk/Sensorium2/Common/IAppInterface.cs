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
using log4net.Appender;
using Sensorium.Common.Plugins;

namespace Sensorium.Common {
	public interface IAppInterface {
		/// <summary>
		/// All sensors exported by all plugins
		/// </summary>
		List<Sensor> Sensors { get; }

		/// <summary>
		/// All plugins, key is the plugin's name
		/// </summary>
		Dictionary<string, IPluginInterface> Plugins { get; }

		/// <summary>
		/// The enabled settings plugin
		/// </summary>
		SettingsPlugin EnabledSettingsPlugin { get; }

		/// <summary>
		/// This app instance's unique ID
		/// </summary>
		string HostGuid { get; }

		/// <summary>
		/// Current app version
		/// </summary>
		string Version { get; }

		/// <summary>
		/// Current app file version
		/// </summary>
		string FileVersion { get; }

		/// <summary>
		/// App friendly name
		/// </summary>
		string FriendlyName { get; }

		/// <summary>
		/// App host ID
		/// </summary>
		string HostId { get; }

		/// <summary>
		/// App copyright
		/// </summary>
		string Copyright { get; }

		/// <summary>
		/// App description
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Contains every logged event since program start
		/// </summary>
		MemoryAppender Log { get; }

		/// <summary>
		/// Event is triggered after all plugins are loaded. 
		/// If no plugin cancels it, the Console window will be hidden
		/// </summary>
		event EventHandler<CancelEventArgs> HideConsoleEventHandler;

		AppData.GetSettingDelegate GetSetting { get; }
		AppData.SetSettingDelegate SetSetting { get; }
	}
}

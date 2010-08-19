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

namespace Sensorium.Core.Plugins
{
	public enum PluginMode { Default, Server, Client }

	public enum PluginType { Data, Settings, Control, Comm }

	public interface IPluginInterface
	{
		/// <summary>
		/// Gets the plugin name
		/// </summary>
		string Name { get; }
		
		/// <summary>
		/// Gets the plugin version
		/// </summary>
		int Version { get; }

		/// <summary>
		/// Gets the plugin description
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Gets or sets the enabled state of the plugin
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		/// Starts the plugin
		/// </summary>
		void Start();

		/// <summary>
		/// Stops the plugin
		/// </summary>
		void Stop();

		/// <summary>
		/// Gets the running state of the plugin
		/// </summary>
		bool Running { get; }

		/// <summary>
		/// Gets the plugin type
		/// </summary>
		PluginType Type { get; }

		/// <summary>
		/// (Re)Initializes the plugin
		/// </summary>
		void ReInit();

		/// <summary>
		/// Gets the default settings for the plugin
		/// </summary>
		PluginSettings DefaultSettings { get; }
	}
}
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

namespace Sensorium.Core.Plugins
{
	public enum PluginMode { Default, Server, Client }

	public enum PluginType { Data, Settings, Control, Comm }

	public interface IPluginInterface
	{
		string Name { get; }
		
		int Version { get; }

		string Description { get; }

		bool Enabled { get; set; }

		void Start();
		void Stop();

		bool Running { get; }

		PluginType Type { get; }

		void ReInit();

		PluginSettings DefaultSettings { get; }
	}
}
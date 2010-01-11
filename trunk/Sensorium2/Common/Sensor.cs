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

namespace Common
{
	public class Sensor
	{
		public string Type { get; protected set; }
		public virtual byte[] Data { get; set; }
		public string Name { get; protected set; }
		public string HostId { get; protected set; }
		public string SourcePlugin { get; protected set; }

		public Sensor(string name, string type, string source, string sourcePlugin) {
			Type = type;
			Name = name;
			HostId = source;
			SourcePlugin = sourcePlugin;
		}
	}
}

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
using Sensorium.Core.Plugins;

namespace Sensorium.Core
{
	public class Sensor
	{
		/// <summary>
		/// Gets the sensor type
		/// </summary>
		public string Type { get; protected set; }
		private byte[] _data;

		/// <summary>
		/// Encoded sensor data. Use creating data plugin's SensorToString method to decode.
		/// </summary>
		public virtual byte[] Data
		{
			get { return _data; }
			protected set { _data = value; }
		}

		/// <summary>
		/// Gets the sensor names
		/// </summary>
		public string Name { get; protected set; }
		
		/// <summary>
		/// ID of the app instance that created this sensor
		/// </summary>
		public string HostId { get; protected set; }
		
		/// <summary>
		/// Data plugin associated with this sensor
		/// </summary>
		public string SourcePlugin { get; protected set; }

		public Sensor(string name, string type, string hostId, string sourcePlugin) {
			Type = type;
			Name = name;
			HostId = hostId;
			SourcePlugin = sourcePlugin;
		}

		/// <summary>
		/// Sets the data field
		/// </summary>
		/// <param name="data"></param>
		public virtual void SetData(byte[] data) {
			_data = data;
		}
        
		/// <summary>
		/// Gets the GUID of this sensor's host
		/// </summary>
		public string HostGuid {
			get { return HostId.Split(new char[] {'{', '}'}, StringSplitOptions.RemoveEmptyEntries)[0]; }
		}

		/// <summary>
		/// Gets the friendly name of this sensor's host
		/// </summary>
		public string HostFriendlyName
		{
			get { return HostId.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries)[1]; }
		}

		/// <summary>
		/// Gets a textual representation of this sensor's data
		/// </summary>
		/// <returns>Textual representation of the sensor's data</returns>
		public override string ToString() {
			return string.Format("{0} - {1}", Name, 
				((DataPlugin)SensoriumFactory.GetAppInterface().Plugins[SourcePlugin]).SensorToString(this));
		}
	}
}

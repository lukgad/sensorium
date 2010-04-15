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
using System.Text;

namespace Sensorium.Common {
	/// <summary>
	/// Methods for use by server components of comm plugins
	/// 
	/// Data Format
	/// -------------
	/// byte	PV		= Protocol version (currently 3)
	/// int	Length	= Total length (in bytes) of the data
	/// byte	RT		= Request type (Host ID, Plugin Name, Name, Type or Data) 
	/// int	Sensor	= Sensor no.
	/// 
	/// Request packet:
	/// Byte no.	|0 |1 |2 |3 |4 |5 |6 |7 |8 |9 |
	/// Data		|PV|Length     |RT|Sensor     |
	/// 
	/// Response:
	/// Byte no.	|0 |1 |2 |3 |4 |5 |6 |7 |8 |9 |10 ... end|
	/// Data		|PV|Length     |RT|Sensor     |Data      |
	/// 
	/// Data: String for Host ID, Plugin Name, Name, Type requests; byte[] for Data requests
	/// </summary>
	public abstract class SensoriumServer {
		
		/// <summary>
		/// Get the response for a recieved request
		/// </summary>
		/// <param name="requestPacket">Request data</param>
		/// <returns>Response data</returns>
		protected static byte[] GetResponse(byte[] requestPacket) {
			if (requestPacket[0] != 3)
				return null;

			int totalLength = BitConverter.ToInt32(requestPacket, 1);
            if (requestPacket.Length < totalLength) //Make sure we've got all the data
				return null;

			int requestedSensor = -1;

			List<byte> data = new List<byte> {3, requestPacket[5]}; //Common response data

			if (requestPacket[5] != ((byte) RequestType.NumSensors))
			{
				requestedSensor = BitConverter.ToInt32(requestPacket, 6);
				data.AddRange(BitConverter.GetBytes(requestedSensor));
			}

			switch ((RequestType) requestPacket[5]) //Add the requested data
			{
				case RequestType.NumSensors:
					data.AddRange(BitConverter.GetBytes(SensoriumFactory.GetAppInterface().Sensors.Count));
					break;

				case RequestType.HostId:
					data.AddRange(Encoding.UTF8.GetBytes(SensoriumFactory.GetAppInterface().HostId));
					break;

				case RequestType.SourcePlugin:
					data.AddRange(Encoding.UTF8.GetBytes(
						SensoriumFactory.GetAppInterface().Sensors[requestedSensor].SourcePlugin));
					break;

				case RequestType.Name:
					data.AddRange(Encoding.UTF8.GetBytes(
						SensoriumFactory.GetAppInterface().Sensors[requestedSensor].Name));
					break;

				case RequestType.Type:
					data.AddRange(Encoding.UTF8.GetBytes(
						SensoriumFactory.GetAppInterface().Sensors[requestedSensor].Type));
					break;

				case RequestType.Data:
					data.AddRange(SensoriumFactory.GetAppInterface().Sensors[requestedSensor].Data);
					break;
			}
			//Add the total size to the packet, now that we know how big it is
			data.InsertRange(1, BitConverter.GetBytes(data.Count + 4));

			return data.ToArray();
		}
	}
}

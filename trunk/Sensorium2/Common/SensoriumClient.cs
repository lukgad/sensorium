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
	public static class SensoriumClient {
		/// <summary>
		/// Generates a request for a sensor
		/// </summary>
		/// <param name="requestType"></param>
		/// <param name="sensorId">ID of sensor to request</param>
		/// <returns>Sensor request packet payload</returns>
		private static byte[] Request(RequestType requestType, int sensorId)
		{
			List<byte> request = new List<byte> {3, (byte) requestType};

			switch (requestType)
			{
				case RequestType.NumSensors:
					break;
				case RequestType.HostId:
				case RequestType.SourcePlugin:
				case RequestType.Name:
				case RequestType.Type:
				case RequestType.Data:
					request.AddRange(BitConverter.GetBytes(sensorId));
					break;
				default:
					throw new ArgumentException();
			}

			request.InsertRange(1, BitConverter.GetBytes(request.Count));

			return request.ToArray();
		}

		public static Sensor GetSensor(SendRequest requestSend, int sensorId) {
			Sensor sensor = null;
			string hostId = null, sourcePlugin = null, name = null, type = null;
			byte[] data = null;

			for (byte i = 1; i <= 5; i++)
			{
				byte[] response = requestSend(Request((RequestType) i, sensorId));

				if (response[0] != 3 || response[5] != i || BitConverter.ToInt32(response, 1) != response.Length)
					break;

				switch ((RequestType) i)
				{
					case RequestType.HostId:
						hostId = Encoding.UTF8.GetString(response, 10, response.Length - 10);
						break;
					case RequestType.SourcePlugin:
						sourcePlugin = Encoding.UTF8.GetString(response, 10, response.Length - 10);
						break;
					case RequestType.Name:
						name = Encoding.UTF8.GetString(response, 10, response.Length - 10);
						break;
					case RequestType.Type:
						type = Encoding.UTF8.GetString(response, 10, response.Length - 10);
						break;
					case RequestType.Data:
						data = new byte[response.Length - 10];
						for (int p = 10; p < response.Length; p++)
							data[p - 10] = response[p];
						break;
				}

				if (hostId == null || sourcePlugin == null || name == null || type == null || data == null)
					continue;

				sensor = new Sensor(name, type, hostId, sourcePlugin);
				sensor.SetData(data);
			}

			return sensor;
		}

		public static byte[] GetSensorData(SendRequest requestSend, int sensorId) {
			byte[] response = requestSend(Request(RequestType.Data, sensorId));

			if (response[0] != 3 || response[5] != (byte) RequestType.Data || BitConverter.ToInt32(response, 1) != response.Length)
				return null;

			byte[] data = new byte[response.Length - 10];
			for (int p = 10; p < response.Length; p++)
				data[p - 10] = response[p];

			return data;
		}

		public static int GetNumSensors(SendRequest requestSend) {
			byte[] request = requestSend(Request(RequestType.NumSensors, -1));

			if (request.Length == 0 || request[0] != 3 || request[5] != ((byte) RequestType.NumSensors) || 
				BitConverter.ToInt32(request, 1) != request.Length)
				return 0;

			return BitConverter.ToInt32(request, 6);
		}

		/// <summary>
		/// Sends a request
		/// </summary>
		/// <param name="request">Request data</param>
		/// <returns>Response data</returns>
		public delegate byte[] SendRequest(byte[] request);

		/// <summary>
		/// Gets a list of Sensors using delegate.
		/// </summary>
		/// <param name="requestSend">SensorRequest delegate function</param>
		/// <returns>List of Sensors retrieved</returns>
		public static List<Sensor> GetSensors(SendRequest requestSend) {
			int numSensors = GetNumSensors(requestSend);

			if (numSensors <= 0)
				return new List<Sensor>();

			List<Sensor> sensorList = new List<Sensor>();
			
			for (int i = 0; i < numSensors; i++) {
					Sensor newSensor = GetSensor(requestSend, i);
					
					if(newSensor == null) {
						sensorList.Add(new Sensor("Unknown", "Unknown", "Unknown", "Unknown"));
					}

				sensorList.Add(newSensor);
			}

			return sensorList;
		}
	}
}

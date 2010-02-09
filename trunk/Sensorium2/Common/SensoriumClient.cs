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

namespace Sensorium.Common {
	public static class SensoriumClient {
		
		private static byte[] Request(RequestType requestType, int sensorId) {
			List<byte> request = new List<byte> { 3, (byte) requestType };

			switch (requestType) {
				case RequestType.NumSensors: //Not strictly necessary
					break;
				case RequestType.HostId:
				case RequestType.SourcePlugin:
				case RequestType.Name:
				case RequestType.Type:
				case RequestType.Data:
					request.AddRange(BitConverter.GetBytes(sensorId));
					break;
			}

			request.InsertRange(1, BitConverter.GetBytes(request.Count));

			return request.ToArray();
		}

		public delegate byte[] SensorRequest(byte[] request);

		/// <summary>
		/// Gets a list of Sensors using delegate.
		/// </summary>
		/// <param name="requestSensor">SensorRequest delegate function</param>
		/// <returns>List of Sensors retrieved</returns>
		public static List<Sensor> GetSensors(SensorRequest requestSensor, string localHostId) {
			byte[] request = requestSensor(Request(RequestType.NumSensors, -1));

			if (request[0] != 3 || request[5] != ((byte) RequestType.NumSensors) || BitConverter.ToInt32(request, 1) != request.Length)
				return null;

			List<Sensor> sensorList = new List<Sensor>();

			int numSensors = BitConverter.ToInt32(request, 6);

			for (int i = 0; i < numSensors; i++) {
				string hostId = null, sourcePlugin = null, name = null, type = null;
				byte[] data = null;

				for (byte j = 1; j <= 5; j++) {
					byte[] response = requestSensor(Request((RequestType)j, i));

					if (request[0] != 3 || request[5] != j || BitConverter.ToInt32(request, 1) != request.Length)
						break;

                    switch ((RequestType) j) {
						case RequestType.HostId:
                    		hostId = BitConverter.ToString(response, 10);
							break;
						case RequestType.SourcePlugin:
							sourcePlugin = BitConverter.ToString(response, 10);
							break;
						case RequestType.Name:
							name = BitConverter.ToString(response, 10);
							break;
						case RequestType.Type:
							type = BitConverter.ToString(response, 10);
							break;
						case RequestType.Data:
                    		data = new byte[request.Length - 10];
							for (int p = 10; p < request.Length; p++)
								data[p - 10] = request[p];
							break;
					}

					if(hostId == null || sourcePlugin == null || name == null || type == null || data == null)
						continue;

					Sensor newSensor = new Sensor(name, type, sourcePlugin, hostId);
					newSensor.SetData(data);
					sensorList.Add(newSensor);
				}
			}

			return sensorList;
		}
	}
}

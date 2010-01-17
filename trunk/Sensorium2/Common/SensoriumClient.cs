﻿using System;
using System.Collections.Generic;

namespace Common {
	public static class SensoriumClient {
		
		private static byte[] Request(RequestType requestType, int sensorId) {
			List<byte> request = new List<byte> { 3, (byte) requestType };

			switch (requestType) {
				case RequestType.NumSensors:
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
		/// 
		/// </summary>
		/// <param name="requestSensor"></param>
		/// <returns></returns>
		public static List<Sensor> GetSensors(SensorRequest requestSensor) {
			byte[] request = requestSensor(Request(RequestType.NumSensors, -1));

			if (request[0] != 3 || request[5] != ((byte) RequestType.NumSensors) || BitConverter.ToInt32(request, 1) != request.Length)
				return null;

			int numSensors = BitConverter.ToInt32(request, 6);

			for (int i = 0; i < numSensors; i++) {
				string hostId, sourcePlugin, name, type;
				byte[] data;

				for (byte j = 1; j <= 5; j++) {
					

					switch ((RequestType) j) {
						case RequestType.HostId:
							break;
						case RequestType.SourcePlugin:
							break;
						case RequestType.Name:
							break;
						case RequestType.Type:
							break;
						case RequestType.Data:
							break;
					}
				}
			}

			return null;
		}
	}
}

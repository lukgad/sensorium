using System;
using System.Collections.Generic;

namespace Common {
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
		public static List<Sensor> GetSensors(SensorRequest requestSensor) {
			byte[] request = requestSensor(Request(RequestType.NumSensors, -1));

			if (request[0] != 3 || request[5] != ((byte) RequestType.NumSensors) || BitConverter.ToInt32(request, 1) != request.Length)
				return null;

			int numSensors = BitConverter.ToInt32(request, 6);

			for (int i = 0; i < numSensors; i++) {
				string hostId, sourcePlugin, name, type;
				byte[] data;

				for (byte j = 1; j <= 5; j++) {
					byte[] response = requestSensor(Request((RequestType) j, i));

					if (request[0] != 3 || request[5] != j || BitConverter.ToInt32(request, 1) != request.Length)
						continue;

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
							
							break;
					}

				}
			}

			return null;
		}
	}
}

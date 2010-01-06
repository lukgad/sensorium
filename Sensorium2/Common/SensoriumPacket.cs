using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Common {
	public static class SensoriumPacket {
		public static class RequestType {
			public const byte NumSensors = 0;
			public const byte HostId = 1;
			public const byte SourcePlugin = 2;
			public const byte Name = 3;
			public const byte Type = 4;
			public const byte Data = 5;
		}

		public static byte[] GetResponse(byte[] requestPacket, List<Sensor> sensors) {
			try {
				if (requestPacket[0] != 3)
					return null;

				int totalLength = BitConverter.ToInt32(requestPacket, 1);
                if (requestPacket.Length < totalLength) //Make sure we've got all the data
					return null;

				int requestedSensor = -1;

				List<byte> data = new List<byte> {3, requestPacket[5]}; //Common response data

				if (requestPacket[5] != RequestType.NumSensors)
				{
					requestedSensor = BitConverter.ToInt32(requestPacket, 6);
					data.AddRange(BitConverter.GetBytes(requestedSensor));
				}

				switch (requestPacket[5]) //Add the requested data
				{
					case RequestType.NumSensors:
						data.AddRange(BitConverter.GetBytes(sensors.Count));
						break;

					case RequestType.HostId:
						//TODO: Get HostId here. Used to make sure we don't request data from ourselves
						break;

					case RequestType.SourcePlugin:
						data.AddRange(Encoding.UTF8.GetBytes(sensors[requestedSensor].SourcePlugin));
						break;

					case RequestType.Name:
						data.AddRange(Encoding.UTF8.GetBytes(sensors[requestedSensor].Name));
						break;

					case RequestType.Type:
						data.AddRange(Encoding.UTF8.GetBytes(sensors[requestedSensor].Type));
						break;

					case RequestType.Data:
						data.AddRange(sensors[requestedSensor].Data);
						break;
				}
				//Add the total size to the packet, now that we know how big it is
				data.InsertRange(1, BitConverter.GetBytes(data.Count + 4));

				return data.ToArray();
			} catch (Exception e) {
				Debug.WriteLine(e.GetType() + ": " + e.Message);
				Debug.WriteLine(e.StackTrace);
				return null;
			}
		}

		private static byte[] Request(byte requestType, int sensor) {
			List<byte> request = new List<byte> {3, requestType};

			if (requestType != RequestType.NumSensors)
				request.AddRange(BitConverter.GetBytes(sensor));

			request.InsertRange(1, BitConverter.GetBytes(request.Count + 4));

			return request.ToArray();
		}

		public delegate byte[] PacketDelegate(byte[] request);

		public static List<Sensor> GetSensors(PacketDelegate pDelegate) {
			int numSensors = BitConverter.ToInt32(pDelegate(Request(RequestType.NumSensors, -1)), 0);
			List<Sensor> sensors = new List<Sensor>();

			for (int i = 0; i < numSensors; i++) {
				string[] packetData = new string[5];

				for (byte n = 1; n < 5; n++) {
					byte[] response = pDelegate(Request(n, i));
					if (response[0] == 3 && BitConverter.ToInt32(response, 1) == response.Length &&
						response[5] == n && BitConverter.ToInt32(response, 6) == i)
						packetData[n] = Encoding.UTF8.GetString(response, 10, response.Length - 10);
					else
						throw new Exception("Unexpected packet recieved");
				}
				sensors.Add(new Sensor(packetData[RequestType.Name], packetData[RequestType.Type], packetData[RequestType.HostId], packetData[RequestType.SourcePlugin]));
			}
			return sensors;
		}
	}
}

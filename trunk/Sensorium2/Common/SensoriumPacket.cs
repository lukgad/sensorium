using System;
using System.Collections.Generic;
using System.Text;

namespace Common {
	public static class SensoriumPacket {
		private static class RequestType {
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

				data.InsertRange(1, BitConverter.GetBytes(data.Count + 4)); //Add the total size to the packet, now that we know how big it is

				return data.ToArray();
			} catch (Exception e) {
				return null;
			}
		}
	}
}

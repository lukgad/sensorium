using System;
using System.Collections.Generic;
using System.Text;

namespace Common {
	public static class SensoriumServer {
		/* Data Format
		 * -------------
		 * byte	PV		= Protocol version (currently 3)
		 * int	Length	= Total length (in bytes) of the data
		 * byte	RT		= Request type (Host ID, Plugin Name, Name, Type or Data) 
		 * int	Sensor	= Sensor no.
		 * 
		 * Request packet:
		 * Byte no.	|0 |1 |2 |3 |4 |5 |6 |7 |8 |9 |
		 * Data		|PV|Length     |RT|Sensor     |
		 * 
		 * Response:
		 * Byte no.	|0 |1 |2 |3 |4 |5 |6 |7 |8 |9 |10 ... end|
		 * Data		|PV|Length     |RT|Sensor     |Data      |
		 * 
		 * Data: String for Host ID, Plugin Name, Name, Type requests; byte[] for Data requests
		 */
		public static byte[] GetResponse(byte[] requestPacket, List<Sensor> sensors) {
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
		}
	}
}

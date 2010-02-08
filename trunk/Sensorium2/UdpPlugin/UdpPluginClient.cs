using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Common;

namespace UdpPlugin {
	class UdpPluginClient {
		private UdpClient _udpClient;

		private List<Sensor> _sensors;
		public List<Sensor> Sensors
		{
			get { return new List<Sensor>(_sensors); }
		}

		public UdpPluginClient(string hostName, int port) {
			_udpClient = new UdpClient(hostName, port);
			UpdateSensors();
		}

		public void UpdateSensors() {
			_sensors = SensoriumClient.GetSensors(GetResponse);
		}

		private byte[] GetResponse(byte[] request) {
			_udpClient.Send(request, request.Length);
			IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
			
			return _udpClient.Receive(ref remoteEndPoint);
		}
	}
}

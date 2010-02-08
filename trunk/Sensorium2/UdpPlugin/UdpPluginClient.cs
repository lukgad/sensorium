using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common;

namespace UdpPlugin {
	class UdpPluginClient {
		private UdpClient _udpClient;
        private bool _running;
		private string _localHostId;
		private int _delay;

		private List<Sensor> _sensors;
		public List<Sensor> Sensors
		{
			get { return new List<Sensor>(_sensors); }
		}

		public void Start() {
			_running = true;

			ThreadStart callback = UpdateSensors;

			new Thread(callback).Start();
		}

		public void Stop() {
			_running = false;
		}

		public UdpPluginClient(string hostName, int port, string localHostId, int delay) {
			_udpClient = new UdpClient(hostName, port);
			_running = false;
			_localHostId = localHostId;
			_delay = delay;
		}

		public void UpdateSensors() {

			while (_running) {
				_sensors = SensoriumClient.GetSensors(GetResponse, _localHostId);
				Thread.Sleep(_delay);
			}

			_udpClient.Close();
		}

		private byte[] GetResponse(byte[] request) {
			_udpClient.Send(request, request.Length);
			IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
			
			return _udpClient.Receive(ref remoteEndPoint);
		}
	}
}

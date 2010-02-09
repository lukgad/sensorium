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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Sensorium.Common;

namespace UdpPlugin {
	class UdpPluginClient {
		private UdpClient _udpClient;
        private bool _running;
		private string _localHostId;
		private int _delay;
		private string _hostName;
		private int _port;

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
			_udpClient = new UdpClient();
			_port = port;
			_hostName = hostName;
			_running = false;
			_localHostId = localHostId;
			_delay = delay;
			_sensors = new List<Sensor>();

			_udpClient.Client.ReceiveTimeout = delay;
		}

		private void UpdateSensors() {

			while (_running) {
				_sensors = SensoriumClient.GetSensors(GetResponse, _localHostId);

				Thread.Sleep(_delay);
			}

			_udpClient.Close();
		}

		private byte[] GetResponse(byte[] request) {
			_udpClient.Send(request, request.Length, _hostName, _port);
			IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

			return _udpClient.Receive(ref remoteEndPoint);
		}
	}
}

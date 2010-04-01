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

using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using log4net;
using Sensorium.Common;

namespace UDPPlugin {
	class UDPPluginClient {
		private UdpClient _udpClient;
        private bool _running;
		private int _delay;
		public string HostName { get; private set; }
		public int Port { get; private set; }
		
		private readonly ILog _log = LogManager.GetLogger(typeof (UDPPluginClient));

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

		public UDPPluginClient(string hostName, int port, int delay) {
			_udpClient = new UdpClient();
			Port = port;
			HostName = hostName;
			_running = false;
			_delay = delay;
			_sensors = new List<Sensor>();

			_udpClient.Client.ReceiveTimeout = delay;
		}

		private void UpdateSensors() {

			while (_running) {
				if(_sensors.Count != SensoriumClient.GetNumSensors(GetResponse))
                    _sensors = SensoriumClient.GetSensors(GetResponse);
				else
					for(int i = 0; i < _sensors.Count; i++)
						_sensors[i].SetData(SensoriumClient.GetSensorData(GetResponse,i));

				Thread.Sleep(_delay);
			}

			_udpClient.Close();
		}

		private byte[] GetResponse(byte[] request) {
			_udpClient.Send(request, request.Length, HostName, Port);
			IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
			try {
				return _udpClient.Receive(ref remoteEndPoint);
			} catch (SocketException se) {
				_log.Error(HostName + ":" + Port + " - Socket Exception, " + se.SocketErrorCode + ": " + se.Message);
				
				return new byte[0];
			}
		}
	}
}

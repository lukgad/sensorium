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

using System.Net;
using System.Net.Sockets;
using System.Threading;
using log4net;
using Sensorium.Common;

namespace UDPPlugin {
	class UDPPluginServer {
	 	public IPAddress Address { get; private set;}
	 	public int Port { get; private set; }
		private bool _running;
		private int _timeout;

		private readonly ILog _log = LogManager.GetLogger(typeof (UDPPluginServer));
        
		public UDPPluginServer(IPAddress address, int port, int timeout) {
			Address = address;
			Port = port;
			_timeout = timeout;
		}

		public void Start() {
			_running = true;

			ThreadStart callBack = Listener;
			
            //Start thread
			new Thread(callBack).Start();
		}

		private void Listener() {
			WaitCallback callBack = Responder;

			IPAddress address = Address;
			byte[] data = new byte[1024];

			Socket listener = new Socket(address.AddressFamily, SocketType.Dgram, 
				ProtocolType.Udp) {ReceiveTimeout = _timeout};

			try {
				listener.Bind(new IPEndPoint(address, Port));
			} catch (SocketException se) {
				_log.Error(address + ":" + Port + " - Socket Exception: " + se.SocketErrorCode);
				return;
			}

			while (_running) {
				EndPoint sender = new IPEndPoint(IPAddress.Any, 0);
				//SocketFlags flags = new SocketFlags();
				//IPPacketInformation packetInfo;

				//Wait for a packet
				try {
					//listener.ReceiveMessageFrom(data, 0, data.Length, ref flags, ref sender, out packetInfo);
					listener.ReceiveFrom(data, ref sender);
				} catch (SocketException se) {
					if(se.SocketErrorCode == SocketError.TimedOut) {
						continue;
					}

					throw;
				}

				//Queue a new responder task
				ThreadPool.QueueUserWorkItem(callBack, new UDPPluginPacket( data, (IPEndPoint) sender));
			}

			listener.Close();
		}
		
		private void Responder(object packet) {
			UDPPluginPacket ipPacket = (UDPPluginPacket) packet;

			byte[] response = SensoriumServer.GetResponse(ipPacket.Data);

			Socket responseSocket = new Socket(ipPacket.EndPoint.AddressFamily, 
				SocketType.Dgram, ProtocolType.Udp);

			if (response != null)
				responseSocket.SendTo(response, 0, response.Length, 
					SocketFlags.None, ipPacket.EndPoint); //Send the response
		}

		public void Stop() {
			_running = false;
		}
	}
}

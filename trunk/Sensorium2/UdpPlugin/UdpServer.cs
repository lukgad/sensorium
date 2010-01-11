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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using Common;

namespace UdpPlugin {
	static class UdpServer {
		private static Dictionary<IPAddress, int> _listenAddresses;
		private static bool _running;
		private static List<Sensor> _sensors;

		public static void Start(Dictionary<IPAddress, int> addresses, List<Sensor> sensors) {
			_listenAddresses = addresses;
			_sensors = sensors;
			_running = true;

			ParameterizedThreadStart callBack = Listener;
			
            //Start threads for each IPAddress.
			//TODO: Change the dictionary to have an array of port numbers, not just one
			foreach(IPAddress i in addresses.Keys) {
				Thread newThread = new Thread(callBack);
				newThread.Start(i);
			}
		}

		private static void Listener(object listenAddress) {
			WaitCallback callBack = Responder;

			IPAddress address = (IPAddress) listenAddress;
			byte[] data = new byte[1024];

			Socket listener = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp) {ReceiveTimeout = 1000};

			listener.Bind(new IPEndPoint(address, _listenAddresses[address]));

			while (_running) {
				EndPoint sender = new IPEndPoint(IPAddress.Any, 0);
				SocketFlags flags = new SocketFlags();
				IPPacketInformation packetInfo;

				//Wait for a packet
				try {
					listener.ReceiveMessageFrom(data, 0, data.Length, ref flags, ref sender, out packetInfo);
				} catch (SocketException se) {
					if(se.ErrorCode == 10060)
						continue;

					throw;
				}

				//Queue a new responder task
				ThreadPool.QueueUserWorkItem(callBack, new UdpPluginPacket(packetInfo, data, (IPEndPoint) sender));
			}

			listener.Close();
		}
		
		private static void Responder(object packet) {
			UdpPluginPacket ipPacket = (UdpPluginPacket) packet;
			byte[] response;

			try {
				response = SensoriumServer.GetResponse(ipPacket.Data, _sensors);
			} catch(Exception e) { //TODO: Is any more exception handling necessary here?
				response = null;
			}

			Socket responseSocket = new Socket(ipPacket.PacketInfo.Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

			if (response != null)
				responseSocket.SendTo(response, 0, response.Length, SocketFlags.None, ipPacket.EndPoint); //Send the response
		}

		public static void Stop() {
			_running = false;
		}
	}
}

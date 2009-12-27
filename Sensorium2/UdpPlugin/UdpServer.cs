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
using Common.Plugins;

namespace UdpPlugin {
	static class UdpServer {
		private static Dictionary<IPAddress, int> _listenAddress;
		private static List<DataPlugin> _dataPlugins;

		public static void Start(Dictionary<IPAddress, int> addresses, List<DataPlugin> plugins) {
			 _listenAddress = addresses;
			 _dataPlugins = plugins;

			WaitCallback callBack = Listener;

			foreach(IPAddress i in _listenAddress.Keys)
				ThreadPool.QueueUserWorkItem(callBack, i);
		}

		private static void Listener(object listenAddress) {
			WaitCallback callBack = Responder;

			IPAddress address = (IPAddress) listenAddress;

			Socket listener = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
			listener.Bind(new IPEndPoint(address, _listenAddress[address]));

            while(true) {
            	Socket handler = listener.Accept();
            	List<byte> data = new List<byte>();

				//do
				//{
				byte[] bytes = new byte[1024];
				/*int bytesRec =*/ handler.Receive(bytes);
				data.AddRange(bytes);
				//} while ()

            	ThreadPool.QueueUserWorkItem(callBack, data);

				handler.Close();
            }
		}
		
		private static void Responder(object data) {
			Console.Write("Recieved");
			
			foreach(byte i in ((List<byte>) data))
				Console.Write(i + " ");

			Console.WriteLine();
		}
	}
}

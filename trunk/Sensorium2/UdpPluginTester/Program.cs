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
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpPluginTester {
	static class Program {
		static void Main(string[] args) {
			foreach(string s in args)
				Debug.WriteLine(s);

			IPEndPoint endPoint = null;
			IPAddress address;

			if ((address = IPAddress.Parse(args[0])) != null)
				endPoint = new IPEndPoint(address, Int16.Parse(args[1]));

			if(address == null)
				return;
			
			Socket socket = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

			byte[] data = Encoding.UTF8.GetBytes(args[2]);

			socket.SendTo(data, endPoint);
		}
	}
}

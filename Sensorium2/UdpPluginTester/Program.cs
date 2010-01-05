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
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpPluginTester {
	static class Program {
		static void Main(string[] args) {
			foreach(string s in args)
				Debug.WriteLine(s);

			EndPoint endPoint;
			IPAddress address;

			if ((address = IPAddress.Parse(args[0])) != null)
				endPoint = new IPEndPoint(address, Int16.Parse(args[1]));
			else
				return;
			
			Socket socket = new Socket(address.AddressFamily, SocketType.Dgram, ProtocolType.Udp) {ReceiveTimeout = 1000};

			List<byte> request = new List<byte> {3, 0};

			request.InsertRange(1,BitConverter.GetBytes(request.Count + 4));

			byte[] data = request.ToArray();

			socket.SendTo(data, endPoint);

			data = new byte[1024];

			socket.ReceiveFrom(data, ref endPoint);

			for (int i = 0; i < BitConverter.ToInt32(data, 1) ; i++) {
				Console.Write(data[i].ToString("X") + " ");

				if (i == 0 || i == 4 || i == 5)
					Console.WriteLine();
			}
			Console.WriteLine();
			Console.ReadKey();

			Console.WriteLine("Requesting {0} sensors", BitConverter.ToInt32(data, 6));

			int numSensors = BitConverter.ToInt32(data, 6);

			for(int i = 0; i < numSensors; i++) {
				Console.WriteLine("Sensor {0}", i);
				
				request = new List<byte> {3,3};
				request.AddRange(BitConverter.GetBytes(i));
				request.InsertRange(1, BitConverter.GetBytes(request.Count + 4));

				data = request.ToArray();
				endPoint = new IPEndPoint(address, Int16.Parse(args[1]));

				socket.SendTo(data, endPoint);

				data = new byte[1024];

				socket.ReceiveFrom(data, ref endPoint);
				
				Console.Write("Name: ");
				foreach (char c in Encoding.UTF8.GetChars(data, 10, BitConverter.ToInt32(data, 1) - 10))
					Console.Write(c);
				Console.WriteLine();
			}

			Console.ReadKey();
		}
	}
}

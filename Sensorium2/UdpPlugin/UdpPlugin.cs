﻿/*	This file is part of Sensorium2 <http://code.google.com/p/sensorium>
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
using Common;
using Common.Plugins;

namespace UdpPlugin{
	public class UdpPlugin : CommPlugin {
		private List<UdpServer> _servers;
		public override string Name {
			get { return "UDP Plugin"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override void Stop() {
			foreach(UdpServer s in _servers)
				s.Stop();
		}

		public override void Start() {
			foreach (UdpServer s in _servers)
				s.Start();
		}

		public override void Init(Dictionary<string, string> settings, PluginMode mode, List<Sensor> sensors, string hostId)
		{
			base.Init(settings, mode, sensors, hostId);

			_servers = new List<UdpServer>();

			Mode = mode;

			if (settings.ContainsKey("Enabled") && settings["Enabled"].ToLower().Equals("false")) {
				Enabled = false;
				return;
			}

			//If in "default" mode, load default mode from config
			if(Mode == PluginMode.Default && settings.ContainsKey("Mode")) {
				if (settings["Mode"].ToLower().Equals("client"))
					Mode = PluginMode.Client;
				else if (settings["Mode"].ToLower().Equals("server"))
					Mode = PluginMode.Server;
			}

			//If in server mode, start the server
			if (Mode == PluginMode.Server && settings.ContainsKey("Listen"))
			{
				Dictionary<IPAddress, int> listenAddresses = new Dictionary<IPAddress, int>();

				string[] splitListen = settings["Listen"].Trim().Split(' ');

				if ((splitListen.Length%2) != 0)
					throw new Exception();

				for (int i = 0; i < splitListen.Length; i += 2)
				{
					IPAddress address;

					if ((address = IPAddress.Parse(splitListen[i])) != null)
						listenAddresses.Add(address, int.Parse(splitListen[i + 1]));
					else
						throw new Exception();
				}

				Console.WriteLine("Listening on:");
				foreach (IPAddress i in listenAddresses.Keys) {
					Console.WriteLine(i + ":" + listenAddresses[i]);
					_servers.Add(new UdpServer(i, listenAddresses[i], sensors));
				}

				foreach(UdpServer s in _servers)
					s.Start();

			} else { //Otherwise, start in client mode (default)
				Console.WriteLine("Starting in client mode");
				Mode = PluginMode.Client;
			}

		}
	}
}

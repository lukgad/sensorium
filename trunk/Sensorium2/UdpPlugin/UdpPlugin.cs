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
using System.Threading;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace UdpPlugin{
	public class UdpPlugin : CommPlugin {
		private List<UdpPluginServer> _servers;
		private List<UdpPluginClient> _clients;
		private bool _running;
		private int _delay;

		public override string Name {
			get { return "UDP Plugin"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override void Stop() {
			foreach(UdpPluginServer s in _servers)
				s.Stop();

			foreach(UdpPluginClient c in _clients)
				c.Stop();

			_running = false;
		}

		public override void Start() {
			foreach (UdpPluginServer s in _servers)
				s.Start();

			foreach (UdpPluginClient c in _clients)
				c.Start();

			_running = true;
			ThreadStart callback = UpdateSensors;
            new Thread(callback).Start();
		}

		private void UpdateSensors() {
			while(_running) {
				Sensors = new List<Sensor>();
				foreach(UdpPluginClient c in _clients) {
					Sensors.AddRange(c.Sensors);
				}
				Thread.Sleep(_delay);
			}
		}

		public override void Init(Dictionary<string, string> settings, PluginMode mode, List<Sensor> sensors, string hostId)
		{
			base.Init(settings, mode, sensors, hostId);

			_servers = new List<UdpPluginServer>();
			_clients = new List<UdpPluginClient>();

			Mode = mode;

			if (settings.ContainsKey("Enabled") && settings["Enabled"].ToLower().Equals("false")) {
				Enabled = false;
				return;
			}

			if (settings.ContainsKey("UpdateDelay") && Int32.Parse(settings["UpdateDelay"]) > 300)
				_delay = Int32.Parse(settings["UpdateDelay"]);
			else
				_delay = 1000;

			//If in "default" mode, load default mode from config
			if(Mode == PluginMode.Default && settings.ContainsKey("Mode")) {
				if (settings["Mode"].ToLower().Equals("client"))
					Mode = PluginMode.Client;
				else if (settings["Mode"].ToLower().Equals("server"))
					Mode = PluginMode.Server;
			}

			//If in server mode, start the server
			if (Mode == PluginMode.Server && settings.ContainsKey("Server"))
			{
				Dictionary<IPAddress, int> listenAddresses = new Dictionary<IPAddress, int>();

				string[] servers = settings["Server"].Trim().Split(' ');

				if ((servers.Length%2) != 0)
					throw new Exception("Malformed address/port pair");

				for (int i = 0; i < servers.Length; i += 2)
				{
					IPAddress address;

					if ((address = IPAddress.Parse(servers[i])) != null)
						listenAddresses.Add(address, int.Parse(servers[i + 1]));
					else
						throw new Exception();
				}

				Console.WriteLine("Server listening on:");
				foreach (IPAddress i in listenAddresses.Keys) {
					Console.WriteLine(i + ":" + listenAddresses[i]);
					_servers.Add(new UdpPluginServer(i, listenAddresses[i], sensors, _delay));
				}

			} else { //Otherwise, start in client mode (default)
				Console.WriteLine("Starting in client mode");
				Mode = PluginMode.Client;
			}

			if (settings.ContainsKey("Client")) {
				string[] clients = settings["Client"].Trim().Split(' ');

				if ((clients.Length % 2) != 0)
					throw new Exception("Malformed address/port pair");

				for (int i = 0; i < clients.Length; i += 2) {
					_clients.Add(new UdpPluginClient(clients[i], Int32.Parse(clients[i + 1]), HostId, 1000));
				}
			}

			Start();
		}
	}
}

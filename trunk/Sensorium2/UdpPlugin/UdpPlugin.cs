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
using log4net;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace UdpPlugin{
	public class UDPPlugin : CommPlugin {
		private List<UDPPluginServer> _servers;
		private List<UDPPluginClient> _clients;
		private bool _running;
		private int _delay;

		private readonly ILog _log = LogManager.GetLogger(typeof(CommPlugin));

		public override string Name {
			get { return "UDP Communications"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override void Stop() {
			foreach(UDPPluginServer s in _servers)
				s.Stop();

			foreach(UDPPluginClient c in _clients)
				c.Stop();

			_running = false;
		}

		public override void Start() {
			foreach (UDPPluginServer s in _servers) {
				s.Start();
				_log.Info("Starting server on " + s.Address + ":" + s.Port);
			}

			foreach (UDPPluginClient c in _clients) {
				c.Start();
				_log.Info("Starting client on " + c.HostName + ":" + c.Port);
			}

			_running = true;
			ThreadStart callback = UpdateSensors;
            new Thread(callback).Start();
		}

		private void UpdateSensors() {
			while(_running) {
				List<Sensor> s = new List<Sensor>();
				foreach(UDPPluginClient c in _clients) {
					s.AddRange(c.Sensors);
				}
				Sensors = s;
				Thread.Sleep(_delay);
			}
		}

		public override void Init(PluginMode mode)
		{
			base.Init(mode);

			_servers = new List<UDPPluginServer>();
			_clients = new List<UDPPluginClient>();

			Mode = mode;

			if (Settings.ContainsKey("Enabled") && Settings["Enabled"][0].ToLower().Equals("false")) {
				Enabled = false;
				return;
			}

			if (Settings.ContainsKey("UpdateDelay") && Int32.Parse(Settings["UpdateDelay"][0]) > 300)
				_delay = Int32.Parse(Settings["UpdateDelay"][0]);
			else
				_delay = 1000;

			//If in "default" mode, load default mode from config
			if(Mode == PluginMode.Default && Settings.ContainsKey("Mode")) {
				if (Settings["Mode"][0].ToLower().Equals("client"))
					Mode = PluginMode.Client;
				else if (Settings["Mode"][0].ToLower().Equals("server"))
					Mode = PluginMode.Server;
			}

			//If in server mode, start the server
			if (Mode == PluginMode.Server && Settings.ContainsKey("Server"))
			{
				//Dictionary<IPAddress, int> listenAddresses = new Dictionary<IPAddress, int>();

				//string[] servers = Settings["Server"][0].Trim().Split(' ');

				//if ((servers.Length%2) != 0)
				//    throw new Exception("Malformed address/port pair");

				//for (int i = 0; i < servers.Length; i += 2)
				//{
				//    IPAddress address;

				//    if ((address = IPAddress.Parse(servers[i])) != null)
				//        listenAddresses.Add(address, int.Parse(servers[i + 1]));
				//    else
				//        throw new Exception();
				//}

				foreach (string s in Settings["Server"]) {
					string[] servers = s.Trim().Split(new char[] { ' ', '	' });
					_servers.Add(new UDPPluginServer(IPAddress.Parse(servers[0]), Int32.Parse(servers[1]), _delay));
				}

			} else { //Otherwise, start in client mode (default)
				_log.Info("Starting in client mode");
				Mode = PluginMode.Client;
			}

			if (Settings.ContainsKey("Client")) {
				string[] clients = Settings["Client"][0].Trim().Split(' ');

				if ((clients.Length % 2) != 0)
					throw new Exception("Malformed address/port pair");

				for (int i = 0; i < clients.Length; i += 2) {
					_clients.Add(new UDPPluginClient(clients[i], Int32.Parse(clients[i + 1]), 1000));
				}
			}
		}
	}
}

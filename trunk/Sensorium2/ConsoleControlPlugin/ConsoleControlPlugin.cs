﻿using System;
using System.Collections.Generic;
using System.Threading;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace ConsoleControlPlugin {
	public class ConsoleControlPlugin : ControlPlugin {

		private bool _running;

		public override string Name {
			get { return "Console Control Plugin"; }
		}

		public override int Version {
			get { return 1; }
		}
	
		public override void Start() {
			_running = true;
			new Thread(WaitKey).Start();
			new Thread(Update).Start();
		}

		public override void Stop() {
			_running = false;
		}

		private void WaitKey() {
			while (_running) {
				UpdateConsole(Console.ReadKey(true));
			}
		}

		private void Update() {
			while(_running) {
				UpdateConsole(new ConsoleKeyInfo());

				int delay;
				if(Settings.ContainsKey("Delay") && (delay = Int32.Parse(Settings["Delay"])) >= 200)
					Thread.Sleep(delay);
				else 
					Thread.Sleep(1000);
			}
		}
        
		private void UpdateConsole(ConsoleKeyInfo keyPress) {
			Console.Clear();
			Console.WriteLine("Host ID: {0}", App.HostId);
			Console.WriteLine("Plugins:");

			List<Sensor> tempSensors = new List<Sensor>(App.Sensors);

			foreach (Type t in PluginTypes) {
				Console.WriteLine(t);
				foreach (IPluginInterface i in App.Plugins) {
					if (t.IsAssignableFrom(i.GetType())) {
						Console.WriteLine(" {0}", i.Name);
						foreach (Sensor s in tempSensors)
							if(s.SourcePlugin.Equals(i.Name))
								Console.WriteLine("	{0}", s.Name);
					}
				}
			}

			if (keyPress.KeyChar != '\0') {
				Console.Write(keyPress.KeyChar);
				if (keyPress.KeyChar.Equals('q'))
					OnExit();
			}
		}
	}
}

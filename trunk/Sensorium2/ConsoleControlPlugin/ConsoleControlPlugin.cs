using System;
using System.Collections.Generic;
using Common.Plugins;
using Sensorium.Common;
using Sensorium.Common.Plugins;
using System.Reflection;

namespace ConsoleControlPlugin {
	public class ConsoleControlPlugin : ControlPlugin {

		public override string Name {
			get { return "Console Control Plugin"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override void Start() {
			Console.Clear();
			Console.WriteLine("Host ID: {0}", HostId);
			Console.WriteLine("Plugins:");

			foreach (Type t in PluginTypes) {
				Console.WriteLine(t);
				foreach (IPluginInterface i in Plugins) {
					if (t.IsAssignableFrom(i.GetType())) {
						Console.WriteLine(" " + i.Name);
					}
				}
			}

			Console.WriteLine("Sensors");

			Console.ReadKey();
		}

		public override void Stop() {
			
		}

		public override void Init(List<Sensor> sensors, List<IPluginInterface> plugins, SettingsPlugin settingsPLugin, string hostId) {
			base.Init(sensors, plugins, settingsPLugin, hostId);
		}
	}
}

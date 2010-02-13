using System;
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
			int cursorTop = Console.CursorTop;
			int cursorLeft = Console.CursorLeft;

			Console.Clear();
			Console.WriteLine("Host ID: {0}", SensoriumFactory.GetAppInterface().HostId);
			Console.WriteLine("Plugins:");

			List<Sensor> tempSensors = new List<Sensor>(SensoriumFactory.GetAppInterface().Sensors);

			foreach (Type t in PluginTypes) {
				foreach (IPluginInterface i in SensoriumFactory.GetAppInterface().Plugins) {
					if (t.IsAssignableFrom(i.GetType())) {
						Console.WriteLine(" {0}", i.Name);
						foreach (Sensor s in tempSensors)
							if(s.SourcePlugin.Equals(i.Name))
								Console.WriteLine("	{0}	{1}", s.Name, ((DataPlugin) i).SensorToString(s));
					}
				}
			}

			Console.CursorTop = cursorTop;
			Console.CursorLeft = cursorLeft;

        	switch(keyPress.Key) {
				case ConsoleKey.Q:
					OnExit();
					break;
				case ConsoleKey.UpArrow:
					if (Console.CursorTop > 0)
						Console.CursorTop--;
					break;
				case ConsoleKey.DownArrow:
					if (Console.CursorTop < (Console.WindowHeight - 1))
						Console.CursorTop++;
					break;
				case ConsoleKey.LeftArrow:
					if (Console.CursorLeft > 0)
						Console.CursorLeft--;
					break;
				case ConsoleKey.RightArrow:
					if (Console.CursorLeft < (Console.WindowWidth - 1))
						Console.CursorLeft++;
					break;

			}
		}
	}
}

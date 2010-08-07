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
using System.ComponentModel;
using System.Threading;
using log4net;
using log4net.Appender;
using log4net.Core;
using Sensorium.Core;
using Sensorium.Core.Plugins;

namespace ConsoleControlPlugin {
	public class ConsoleControlPlugin : ControlPlugin {

		private bool _running;
		private bool _paused;

		public ConsoleControlPlugin() {
			DefaultSettings.Add("Delay", new PluginSettings.Setting(true, null));
		}

		public override string Name {
			get { return "Console Control"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override string Description
		{
			get { return "A very basic console UI"; }
		}

		public override void Start() {
			_running = true;
			_paused = false;
			new Thread(WaitKey).Start();
			new Thread(Update).Start();
			Console.CursorVisible = false;
			Console.WriteLine("Press K to see control keys");

			base.Start();
		}

		public override void Stop() {
			_running = false;
			Console.WriteLine("Press any key to exit...");

			base.Stop();
		}

		protected override void HandleHideConsole(object sender, CancelEventArgs e) {
			e.Cancel = true;
		}

		private void WaitKey() {
			while (_running) {
				UpdateConsole(Console.ReadKey(true));
			}
			Console.CursorVisible = true;
		}

		private void Update() {
			while(_running) {
				if (_paused) {
					Thread.Sleep(1000);
					continue;
				}

				UpdateConsole(new ConsoleKeyInfo());

				int delay;
				Thread.Sleep((Settings["Delay"].Count != 0 && (delay = Int32.Parse(Settings["Delay"][0])) >= 200) ? delay : 1000);
			}
			Console.CursorVisible = true;
		}

        private enum State {Idle, DisplayMenu, DisplaySensors, Loggers, Appenders, DisplayLog}

		private State _state = State.Idle;

		private void UpdateConsole(ConsoleKeyInfo keyPress) {
			if (!_running)
				return;

			//Handle global keypresses
			switch (keyPress.Key) {
				case ConsoleKey.Q:
					OnExit();
					break;
				case ConsoleKey.S:
					_state = State.DisplaySensors;
					break;
				case ConsoleKey.K:
					_state = State.DisplayMenu;
					break;
				case ConsoleKey.L:
					_state = (keyPress.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift ? State.Loggers : State.DisplayLog;
					break;
				case ConsoleKey.A:
					_state = State.Appenders;
					break;
				case ConsoleKey.P:
					_paused = !_paused;
					break;
			}

			//Do the correct action for the current state
			switch (_state) {
				case State.DisplayMenu:
					Console.Clear();
					Console.WriteLine("Key".PadRight(5) + "Function");
					Console.WriteLine("a".PadRight(5) + "(Debug) List of log4net appenders");
					Console.WriteLine("k".PadRight(5) + "Display keys help (this list)");
					Console.WriteLine("l".PadRight(5) + "Display current logs");
					Console.WriteLine("L".PadRight(5) + "(Debug) List of log4net loggers");
					Console.WriteLine("p".PadRight(5) + "Pause screen updates");
					Console.WriteLine("s".PadRight(5) + "Sensors");
					Console.WriteLine("q".PadRight(5) + "Quit");
					_state = State.Idle;
					break;

				case State.DisplayLog:
					Console.Clear();

					foreach (LoggingEvent le in SensoriumFactory.GetAppInterface().Log.GetEvents()) {
						Console.WriteLine("[{0}] {1} ({2}) - {3}", le.GetLoggingEventData().TimeStamp, 
							le.GetLoggingEventData().LoggerName, le.GetLoggingEventData().Level, 
							le.GetLoggingEventData().Message);
					}

					_state = State.Idle;
					break;

				case State.DisplaySensors:
					Console.Clear();
					Console.WriteLine(SensoriumFactory.GetAppInterface().Sensors.Count + " sensors");
					
					//Get all data plugins, print all sensor data
					foreach (string p in SensoriumFactory.GetAppInterface().Plugins.Keys) {
						if (/*!typeof (CommPlugin).IsAssignableFrom(SensoriumFactory.GetAppInterface().Plugins[p].GetType()) &&*/
							!typeof (DataPlugin).IsAssignableFrom(SensoriumFactory.GetAppInterface().Plugins[p].GetType()))
							continue;

						Console.WriteLine(" {0}", SensoriumFactory.GetAppInterface().Plugins[p].Name);

						foreach (Sensor s in SensoriumFactory.GetAppInterface().Sensors)
							if (s.SourcePlugin.Equals(SensoriumFactory.GetAppInterface().Plugins[p].Name))
								Console.WriteLine("  " + s.Name.PadRight(6) +
								                  ((DataPlugin)SensoriumFactory.GetAppInterface().Plugins[p]).SensorToString(s).PadLeft(8));
					}
					break;
				case State.Loggers:
					Console.Clear();
					foreach (ILog l in LogManager.GetCurrentLoggers()) {
						Console.WriteLine(l.Logger.Name);
					}
					_state = State.Idle;
					break;
				case State.Appenders:
					Console.Clear();
					foreach(IAppender a in LogManager.GetRepository().GetAppenders())
						Console.WriteLine(a.Name + " " + a.GetType());
					_state = State.Idle;
					break;
			}
		}
	}
}

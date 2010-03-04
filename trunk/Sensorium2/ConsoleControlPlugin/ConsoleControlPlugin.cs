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
using System.Threading;
using log4net;
using log4net.Appender;
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
			Console.WriteLine("Press K to see control keys");
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
				if(Settings.ContainsKey("Delay") && (delay = Int32.Parse(Settings["Delay"][0])) >= 200)
					Thread.Sleep(delay);
				else 
					Thread.Sleep(1000);
			}
		}

        private enum State {Idle, DisplayMenu, DisplaySensors, Loggers, Appenders}

		private State _state;

		private void UpdateConsole(ConsoleKeyInfo keyPress) {
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
					if ((keyPress.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift)
						_state = State.Loggers;
					break;
				case ConsoleKey.A:
					_state = State.Appenders;
					break;
			}

			switch (_state) {
				case State.DisplayMenu:
					Console.Clear();
					Console.WriteLine("Key".PadRight(5) + "Function");
					Console.WriteLine("a".PadRight(5) + "(Debug) List of log4net appenders");
					Console.WriteLine("k".PadRight(5) + "Display keys help (this list)");
					Console.WriteLine("L".PadRight(5) + "(Debug) List of log4net loggers");
					Console.WriteLine("s".PadRight(5) + "Sensors");
					Console.WriteLine("q".PadRight(5) + "Quit");
					_state = State.Idle;
					break;
				case State.DisplaySensors:
					Console.Clear();
					Console.WriteLine(SensoriumFactory.GetAppInterface().Sensors.Count + " sensors");
					
					foreach (IPluginInterface i in SensoriumFactory.GetAppInterface().Plugins) {
						if (typeof(CommPlugin).IsAssignableFrom(i.GetType()) || 
							typeof(DataPlugin).IsAssignableFrom(i.GetType())) {
							Console.WriteLine(" {0}", i.Name);

							foreach (Sensor s in SensoriumFactory.GetAppInterface().Sensors)
								if (s.SourcePlugin.Equals(i.Name))
									Console.WriteLine("  " + s.Name.PadRight(6) + ((DataPlugin)i).SensorToString(s).PadLeft(8));
						}
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

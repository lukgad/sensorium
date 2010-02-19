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
using System.IO;
using System.Threading;

using log4net.Appender;
using log4net.Core;
using log4net.Layout;

using Sensorium.Common;
using Sensorium.Common.Plugins;

using log4net;

namespace Sensorium2
{
	static class Program
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
		private static readonly List<MemoryAppender> Logs = new List<MemoryAppender>();
		
		private static string _pluginDir = Environment.CurrentDirectory;
		private static string _settingsDir;

		private static bool _client;
		private static bool _recursive;
		private static bool _running;

		private static Dictionary<String, IPluginInterface> _allPluginsD;
		private static List<SettingsPlugin> _settingsPlugins;
		private static List<DataPlugin> _dataPlugins;
		private static List<CommPlugin> _commPlugins;
		private static List<ControlPlugin> _controlPlugins;
		private static List<IPluginInterface> _genericPlugins;
        
		static readonly AppData Me = (AppData) SensoriumFactory.GetAppInterface(); //TODO: Better variable name

        static void Main(string[] args)
		{
        	Me.Logs = Logs;

        	IAppender[] appenders = LogManager.GetRepository().GetAppenders();
        	IAppenderAttachable debugMessageApp = null;
			
			foreach(IAppender app in appenders) 
				if(app.Name == "DebugForwardingAppender")
					debugMessageApp = (IAppenderAttachable)app;
						
        	ColoredConsoleAppender.LevelColors[] consoleColors = new ColoredConsoleAppender.LevelColors[5];
			for (int i = 0; i < 5; i++)
				consoleColors[i] = new ColoredConsoleAppender.LevelColors();

        	consoleColors[0].Level = Level.Info;
			consoleColors[0].ForeColor = ColoredConsoleAppender.Colors.White;
			consoleColors[0].ActivateOptions();

			consoleColors[1].Level = Level.Debug;
			consoleColors[1].ForeColor = ColoredConsoleAppender.Colors.Green;
			consoleColors[1].ActivateOptions();

        	consoleColors[2].Level = Level.Warn;
			consoleColors[2].ForeColor = ColoredConsoleAppender.Colors.Yellow | ColoredConsoleAppender.Colors.HighIntensity;
			consoleColors[2].ActivateOptions();

			consoleColors[3].Level = Level.Error;
			consoleColors[3].ForeColor = ColoredConsoleAppender.Colors.Red | ColoredConsoleAppender.Colors.HighIntensity;
			consoleColors[3].ActivateOptions();

			consoleColors[4].Level = Level.Fatal;
			consoleColors[4].ForeColor = ColoredConsoleAppender.Colors.White | ColoredConsoleAppender.Colors.HighIntensity;
        	consoleColors[4].BackColor = ColoredConsoleAppender.Colors.Red;
			consoleColors[4].ActivateOptions();

			if (debugMessageApp != null) {
				ColoredConsoleAppender debugConsoleMessageApp = new ColoredConsoleAppender();

				//PatternLayout debugLayout = new PatternLayout("%date [%thread] %-5level %logger [%property{NDC}] - %message%newline");
				PatternLayout debugLayout = new PatternLayout("%logger - %message%newline");
				debugLayout.ActivateOptions();
				debugConsoleMessageApp.Layout = debugLayout;

				foreach (ColoredConsoleAppender.LevelColors lc in consoleColors)
					debugConsoleMessageApp.AddMapping(lc);

				debugConsoleMessageApp.ActivateOptions();

				debugMessageApp.AddAppender(debugConsoleMessageApp);

				MemoryAppender debugMemoryAppender = new MemoryAppender();
                debugMessageApp.AddAppender(debugMemoryAppender);
				Logs.Add(debugMemoryAppender);
			}

        	Log.Debug("Host ID: " + Me.HostId);
        	Log.Debug("OS: " + Environment.OSVersion + "(" + Environment.OSVersion.Platform + ")");

			ArgHandler(args);

			InitPlugins();

			//Start enabled plugins
			Log.Info("Starting enabled plugins...");
			foreach(IPluginInterface i in Me.Plugins)
				if (i.Enabled) {
					i.Start();
				}

        	_running = true;

			new Thread(UpdateSensors).Start();
        }

		static void HandleExit(object sender, EventArgs e) {
			_running = false;

			foreach (IPluginInterface i in Me.Plugins) {
				if (i.Enabled)
					i.Stop();
			}
		}


		static void ArgHandler(string[] args) { //Processes command-line arguments
			for (int i = 0; i < args.Length; i++) {
				if (args[i].Equals("-p")) {
					i++;
					if (i < args.Length || args[i][0].Equals('-'))
						_pluginDir = args[i];
					else {
						HelpMessage();
						Environment.Exit(1);
					}
				}
				else if (args[i].Equals("-c"))
					_client = true;

				else if (args[i].Equals("-R"))
					_recursive = true;

				else if (args[i].Equals("-s")) {
					i++;
					if (i < args.Length || args[i][0].Equals('-'))
						_settingsDir = args[i];
					else {
						HelpMessage();
						Environment.Exit(1);
					}
				}
				else if (args[i].Equals("-h")) {
					HelpMessage();
					Environment.Exit(0);
				}
			}
			if (_settingsPlugins == null || _settingsPlugins.Equals(""))
				_settingsDir = Path.Combine(_pluginDir, "settings");
		}

		static void HelpMessage()
		{
			Console.WriteLine("Usage: Sensorium2.exe [options]");
			Console.WriteLine("Option		Description");
			Console.WriteLine("-p dir		Set plugin directory to dir. Default: current directory");
			Console.WriteLine("-R			Search plugin directory recursively");
			Console.WriteLine("-c			All plugins will operate in client mode only");
			Console.WriteLine("-h			Display this message");
		}

		static void InitPlugins()
		{
			//Get all plugins
			Me.Plugins = PluginLoader.GetPlugins(_pluginDir, _recursive);
			_allPluginsD = new Dictionary<string, IPluginInterface>();

			_settingsPlugins = new List<SettingsPlugin>();
			_dataPlugins = new List<DataPlugin>();
			_commPlugins = new List<CommPlugin>();
			_controlPlugins = new List<ControlPlugin>();
			_genericPlugins = new List<IPluginInterface>();

			Me.Sensors = new List<Sensor>();

			Log.Info("Initializing Plugins...");

			//Add plugins to correct lists
			foreach (IPluginInterface i in Me.Plugins) {
				_allPluginsD.Add(i.Name, i);

				if (i is DataPlugin) {
					_dataPlugins.Add((DataPlugin) i);
				} else if (i is CommPlugin) {
					_commPlugins.Add((CommPlugin) i);
				} else if (i is ControlPlugin) {
					_controlPlugins.Add((ControlPlugin) i);
				} else if (i is SettingsPlugin) {
					_settingsPlugins.Add((SettingsPlugin)i);

					//Settings plugins have to init first
					Log.Info(i.Name + ", Ver. " + i.Version + " initializing...");
					((SettingsPlugin) i).Init(_settingsDir);

					if (!i.Enabled)
						Log.Info("Disabled");

					if (i.Enabled) //Only 1 settings plugin can be enabled
						if (Me.EnabledSettingsPlugin == null)
							Me.EnabledSettingsPlugin = (SettingsPlugin)i;
						else
							i.Enabled = false;
				} else if (i.Enabled) {
					_genericPlugins.Add(i);
				}
			}

			if (Me.EnabledSettingsPlugin == null) {
				Log.Warn("No settings plugin is enabled, enabling 1st in list");
				_settingsPlugins[0].Enabled = true;
				Me.EnabledSettingsPlugin = _settingsPlugins[0];
			}

			//Init plugins (in correct order)
			foreach (DataPlugin p in _dataPlugins) {
				Log.Info(p.Name + ", Ver. " + p.Version + " initializing...");
				p.Init(_client ? PluginMode.Client : PluginMode.Default);
				if (!p.Enabled)
					Log.Info(p.Name + " Started in client mode");
			}

			foreach (CommPlugin p in _commPlugins) {
				Log.Info(p.Name + ", Ver. " + p.Version + " initializing...");
				p.Init(_client ? PluginMode.Client : PluginMode.Default);
				if (!p.Enabled)
					Log.Info(p.Name + " Disabled");
			}

			foreach (ControlPlugin p in _controlPlugins) {
				Log.Info(p.Name + ", Ver. " + p.Version + " initializing...");
				p.Init();
				if (!p.Enabled)
					Log.Info(p.Name + " Disabled");
				else
					p.ExitEventHandler += HandleExit;
			}

			foreach (IPluginInterface p in _genericPlugins) {
				Log.Info(p.Name + ", Ver. " + p.Version + " initializing...");
				//i.Init(_EnabledSettingsPlugin.GetSettings(i.Name));
				if (!p.Enabled)
					Log.Info(p.Name + " Disabled");
			}

			Log.Info("Plugins initialized");
		}

		static void UpdateSensors() {
			while (_running) {
				List<Sensor> s = new List<Sensor>();

				foreach (DataPlugin d in _dataPlugins)
					s.AddRange(d.Sensors);
				foreach (CommPlugin c in _commPlugins)
					s.AddRange(c.Sensors);

				Me.Sensors = s;
				Thread.Sleep(1000);
			}
		}
	}
}

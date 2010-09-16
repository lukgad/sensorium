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
using System.Reflection;
using System.Threading;

using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;

using Sensorium.Core;
using Sensorium.Core.Plugins;

using Sensorium2.Properties;

namespace Sensorium2
{
	public static class Program
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
		private static MemoryAppender _memLog;
		private static IAppenderAttachable _forwardingApp;
		private static Level _logLevel;
		private static string _logFile = "";

		private static string _pluginDir;
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

        static void Main(string[] args) {
#if !DEBUG
			try {
#endif
				object[] copyrightAttributes =
					Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
				Me.Copyright = copyrightAttributes.Length != 0
				               	? ((AssemblyCopyrightAttribute) copyrightAttributes[0]).Copyright
				               	: "";
				object[] descriptionAttributes =
					Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
				Me.Description = descriptionAttributes.Length != 0
				                 	? ((AssemblyDescriptionAttribute) descriptionAttributes[0]).Description
				                 	: "";
				Me.Version = String.Format("{0} Trunk", Assembly.GetExecutingAssembly().GetName().Version);
				object[] fileVersionAttributes =
					Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyFileVersionAttribute), false);
				string fileVersionNo = fileVersionAttributes.Length != 0
				                       	? ((AssemblyFileVersionAttribute) fileVersionAttributes[0]).Version
				                       	: "";
				Me.FileVersion = fileVersionNo;

				_memLog = new MemoryAppender();
				Me.Log = _memLog;

				try {
					if (!Directory.Exists(Settings.Default.PluginDirectory))
						Directory.CreateDirectory(Settings.Default.PluginDirectory);
				} catch (Exception e) {
					Log.Error("Invalid or inaccessible plugin directory. Using default.");
					Settings.Default.PluginDirectory = @"./";
				}

				try {
					if (!Directory.Exists(Settings.Default.SettingsDirectory))
						Directory.CreateDirectory(Settings.Default.SettingsDirectory);
				} catch (Exception e) {
					Log.Error("Invalid or inaccessible settings directory. Using default.");
					Settings.Default.PluginDirectory = @"./settings";
				}

				_pluginDir = Settings.Default.PluginDirectory;
				_settingsDir = Settings.Default.SettingsDirectory;
				Me.FriendlyName = Settings.Default.FriendlyName;

				ArgHandler(args);

				SetUpLog();

				Me.GetSetting = GetSetting;
				Me.SetSetting = SetSetting;

				Log.Debug("Host ID: " + Me.HostGuid);
				Log.Debug("OS: " + Environment.OSVersion + "(" + Environment.OSVersion.Platform + ")");
                if (_logFile != "")
                    Log.Debug("Log file: " + _logFile);

				InitPlugins();

				//Start enabled plugins
				Log.Info("Starting enabled plugins...");
				foreach (string p in Me.Plugins.Keys)
					if (Me.Plugins[p].Enabled)
					{
						Me.Plugins[p].Start();
					}

				_running = true;

				Thread sensorUpdater = new Thread(UpdateSensors);
				sensorUpdater.Start();

				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
					Me.OnHideConsole();

				//Workaround for log4net on mono
				sensorUpdater.Join();
#if !DEBUG
			} catch (Exception e) {
				Log.Fatal("Unhandled Exception: " + e.Message + Environment.NewLine + e.StackTrace);

				HandleExit(e, new EventArgs());

				throw;
			}
#endif
        }

		private static void SetSetting(string key, string value) {
			switch (key) {
				case "PluginDirectory":
					Settings.Default.PluginDirectory = value;
					break;
				case "SettingsDirectory":
					Settings.Default.SettingsDirectory = value;
					break;
				case "FriendlyName":
					Settings.Default.FriendlyName = value;
					break;
			}
		}

		private static string GetSetting(string key) {
			switch(key) {
				case "PluginDirectory":
					return Settings.Default.PluginDirectory;
				case "SettingsDirectory":
					return Settings.Default.SettingsDirectory;
				case "FriendlyName":
					return Settings.Default.FriendlyName;
			}
			return null;
		}

		private static void HandleExit(object sender, EventArgs e) {
			_running = false;

			foreach (string p in Me.Plugins.Keys) {
				if (Me.Plugins[p].Running)
					Me.Plugins[p].Stop();
			}
		}

		/// <summary>
		/// Handles command-line arguments
		/// </summary>
		/// <param name="args">Command-line arguments</param>
		private static void ArgHandler(string[] args) { //Processes command-line arguments
			try {
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
					else if (args[i].Equals("-l")) {
						i++;
						switch (args[i].ToLower()) {
							case "info":
								_logLevel = Level.Info;
								break;
							case "debug":
								_logLevel = Level.Debug;
								break;
							case "warn":
								_logLevel = Level.Warn;
								break;
							case "error":
								_logLevel = Level.Error;
								break;
							case "fatal":
								_logLevel = Level.Fatal;
								break;
							case "all":
								_logLevel = Level.All;
								break;
							case "off":
								_logLevel = Level.Off;
								break;
						}

						i++;
						_logFile = args[i];
					}
				}
			} catch (ArgumentOutOfRangeException aoore) {
				Console.WriteLine(aoore.Message);
				HelpMessage();
				Environment.Exit(1);
			}
		}

		/// <summary>
		/// Prints the command-line help message
		/// </summary>
		private static void HelpMessage()
		{
			Console.WriteLine("Usage: Sensorium2.exe [options]");
			Console.WriteLine("Option			Description");
			Console.WriteLine("-p dir			Set plugin directory to 'dir'. Default: current directory");
			Console.WriteLine("-R				Search plugin directory recursively");
			Console.WriteLine("-c				All plugins will operate in client mode only");
			Console.WriteLine("-h				Display this message");
			Console.WriteLine("-l level file	Log events of 'level' or higher will be written to 'file'.");
		}

		/// <summary>
		/// Initialises all plugins
		/// </summary>
		private static void InitPlugins()
		{
			//Get all plugins
			//Me.Plugins = PluginLoader.GetPlugins(_pluginDir, _recursive);
			Me.Plugins = new Dictionary<string, IPluginInterface>();
			foreach (IPluginInterface i in PluginLoader.GetPlugins(_pluginDir, _recursive))
				Me.Plugins.Add(i.Name, i);

			_allPluginsD = new Dictionary<string, IPluginInterface>();

			_settingsPlugins = new List<SettingsPlugin>();
			_dataPlugins = new List<DataPlugin>();
			_commPlugins = new List<CommPlugin>();
			_controlPlugins = new List<ControlPlugin>();
			_genericPlugins = new List<IPluginInterface>();

			Me.Sensors = new List<Sensor>();

			Log.Info("Initializing Plugins...");

			//Add plugins to correct lists
			foreach (string p in Me.Plugins.Keys) {
				_allPluginsD.Add(Me.Plugins[p].Name, Me.Plugins[p]);

				if (Me.Plugins[p] is DataPlugin) {
					_dataPlugins.Add((DataPlugin)Me.Plugins[p]);
				} else if (Me.Plugins[p] is CommPlugin) {
					_commPlugins.Add((CommPlugin)Me.Plugins[p]);
				} else if (Me.Plugins[p] is ControlPlugin) {
					_controlPlugins.Add((ControlPlugin)Me.Plugins[p]);
				} else if (Me.Plugins[p] is SettingsPlugin) {
					_settingsPlugins.Add((SettingsPlugin)Me.Plugins[p]);

					//Settings plugins have to init first
					Log.Info(Me.Plugins[p].Name + ", Ver. " + Me.Plugins[p].Version + " initializing...");
					((SettingsPlugin)Me.Plugins[p]).Init(_settingsDir);

					if (!Me.Plugins[p].Enabled)
						Log.Info("Disabled");

					if (Me.Plugins[p].Enabled) //Only 1 settings plugin can be enabled
						if (Me.EnabledSettingsPlugin == null)
							Me.EnabledSettingsPlugin = (SettingsPlugin)Me.Plugins[p];
						else
							Me.Plugins[p].Enabled = false;
				} else if (Me.Plugins[p].Enabled) {
					_genericPlugins.Add(Me.Plugins[p]);
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
				if (!p.Enabled)
					Log.Info(p.Name + " Disabled");
			}

			Log.Info("Plugins initialized");
		}

		/// <summary>
		/// Thread-safe (I think) updater for sensors list
		/// </summary>
		private static void UpdateSensors() {
			while (_running) {
				List<Sensor> s = new List<Sensor>();

				foreach (DataPlugin d in _dataPlugins)
					s.AddRange(d.Sensors);
				foreach (CommPlugin c in _commPlugins) {
					//s.AddRange(c.Sensors);
					//continue;

					foreach (Sensor cs in c.Sensors) {
						if (cs.HostId == Me.HostGuid)
							continue;

						s.Add(cs);
					}
				}

				Me.Sensors = s;
				Thread.Sleep(1000);
			}
		}
        
		/// <summary>
		/// Sets up log4net appenders
		/// </summary>
		private static void SetUpLog() {
			//Create default log4net config if a config file doesn't already exist
			if (!File.Exists("Sensorium2.exe.log4net")) {
				Console.WriteLine("log4net config file not found.");
				using(StreamWriter sw = new StreamWriter("Sensorium2.exe.log4net")) {
					sw.Write(Resources.ResourceManager.GetString("DefaultLogConfig"));
					sw.Close();
				}
				Console.WriteLine("Default config generated.");
				Console.WriteLine("Exiting.");
				Environment.Exit(2);
			}

			//Get all appenders
			IAppender[] appenders = LogManager.GetRepository().GetAppenders();
			
			//Find the appender we need
			foreach (IAppender app in appenders)
				if (app.Name == "ForwardingAppender") {
					_forwardingApp = (IAppenderAttachable) app;
					break;
				}

			if (_forwardingApp == null) return;

			PatternLayout layout = new PatternLayout("%message%newline");
			layout.ActivateOptions();

			//Add a memory appender to the forwarder
			_forwardingApp.AddAppender(_memLog);

			//Set an log file appender
			if(_logFile != "") {
				FileAppender fileAppender = new FileAppender {File = _logFile};
				
				PatternLayout logFileLayout = new PatternLayout("%-6timestamp %-5level %logger - %message%newline");

				LevelRangeFilter levelRangeFilter = new LevelRangeFilter {LevelMax = Level.Off, LevelMin = _logLevel};
				levelRangeFilter.ActivateOptions();

			    fileAppender.Layout = logFileLayout;
				fileAppender.AddFilter(levelRangeFilter);
                fileAppender.ActivateOptions();

				_forwardingApp.AddAppender(fileAppender);
			}

			//Workaround for mono (no colored console support)
			if (Environment.OSVersion.Platform != PlatformID.Win32NT) {
				ConsoleAppender consoleAppender = new ConsoleAppender {Layout = layout};
				consoleAppender.ActivateOptions();

				_forwardingApp.AddAppender(consoleAppender);
				return;
			}

			//Set up level colors
			List<ColoredConsoleAppender.LevelColors> consoleColors =
				new List<ColoredConsoleAppender.LevelColors> {
				                                             	new ColoredConsoleAppender.LevelColors {
				                                             	                                       	Level = Level.Info,
				                                             	                                       	ForeColor = ColoredConsoleAppender.Colors.White
				                                             	                                       },
				                                             	new ColoredConsoleAppender.LevelColors {
				                                             	                                       	Level = Level.Debug,
				                                             	                                       	ForeColor = ColoredConsoleAppender.Colors.Green
				                                             	                                       },
				                                             	new ColoredConsoleAppender.LevelColors {
				                                             	                                       	Level = Level.Warn,
				                                             	                                       	ForeColor = ColoredConsoleAppender.Colors.Yellow |
																										ColoredConsoleAppender.Colors.HighIntensity
				                                             	                                       },
				                                             	new ColoredConsoleAppender.LevelColors {
				                                             	                                       	Level = Level.Error,
				                                             	                                       	ForeColor = ColoredConsoleAppender.Colors.Red |
																										ColoredConsoleAppender.Colors.HighIntensity
				                                             	                                       },
				                                             	new ColoredConsoleAppender.LevelColors {
				                                             	                                       	Level = Level.Fatal,
				                                             	                                       	ForeColor = ColoredConsoleAppender.Colors.White |
																										ColoredConsoleAppender.Colors.HighIntensity,
				                                             	                                       	BackColor = ColoredConsoleAppender.Colors.Red,
				                                             	                                       }
				                                             };

			ColoredConsoleAppender colorConsoleAppender = new ColoredConsoleAppender();

			//Add the color mappings to the console appender
			foreach (ColoredConsoleAppender.LevelColors lc in consoleColors) {
				lc.ActivateOptions();
				colorConsoleAppender.AddMapping(lc);
			}

			colorConsoleAppender.Layout = layout;
		        
			colorConsoleAppender.ActivateOptions();

			//Add the appender to the forwarder
			_forwardingApp.AddAppender(colorConsoleAppender);
		}
	}
}

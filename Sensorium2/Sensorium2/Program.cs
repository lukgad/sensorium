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
using System.IO;
using System.Threading;
using Common.Plugins;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace Sensorium2
{
	static class Program
	{
		private static string _pluginDir = Environment.CurrentDirectory;
        private static string _settingsDir;

		private static bool _client;
		private static bool _recursive;

	    private static List<IPluginInterface> _allPlugins;
		private static Dictionary<String, IPluginInterface> _allPluginsD;

		private static SettingsPlugin _enabledSettingsPlugin;
    	
		private static List<SettingsPlugin> _settingsPlugins;
        private static List<DataPlugin> _dataPlugins;
		private static List<CommPlugin> _commPlugins;
		private static List<ControlPlugin> _controlPlugins;
		private static List<IPluginInterface> _genericPlugins;

		private static List<Sensor> _sensors; //TODO: make _sensors thread-safe

		private static readonly string HostId = Guid.NewGuid().ToString();

		private static bool _running;
		
        static void Main(string[] args)
		{
			Console.WriteLine("Host ID: {0}", HostId);
        	Console.WriteLine("OS: {0} ({1})", Environment.OSVersion, Environment.OSVersion.Platform);
        	Console.WriteLine();

			ArgHandler(args);

			InitPlugins();

			//Start enabled plugins
			Console.WriteLine("Starting enabled plugins...");
			foreach(IPluginInterface i in _allPlugins)
				if (i.Enabled) {
					Console.WriteLine(i.Name);
					i.Start();
				}

        	_running = true;

			new Thread(UpdateSensors).Start();

			Thread.Sleep(200);

			//Output data plugins' sensor data
			Console.WriteLine();
			Console.WriteLine("Sensors:");
			Console.WriteLine("Host ID					Plugin		Type		Name	Value");
			Monitor.Enter(_sensors);
			foreach (Sensor s in (new List<Sensor>(_sensors))) {
				Console.WriteLine("{0}	{1}	{2}		{3}	{4}",
					s.HostId, s.SourcePlugin, s.Type, s.Name,
					((DataPlugin) _allPluginsD[s.SourcePlugin]).SensorToString(s));
			}
			Monitor.Exit(_sensors);

        	Console.WriteLine();
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();

        	_running = false;

			foreach (IPluginInterface i in _allPlugins) {
				if(i.Enabled)
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
			_allPlugins = PluginLoader.GetPlugins(_pluginDir, _recursive);
			_allPluginsD = new Dictionary<string, IPluginInterface>();

			_settingsPlugins = new List<SettingsPlugin>();
			_dataPlugins = new List<DataPlugin>();
			_commPlugins = new List<CommPlugin>();
			_controlPlugins = new List<ControlPlugin>();
			_genericPlugins = new List<IPluginInterface>();

			_sensors = new List<Sensor>();

			Console.WriteLine("Initializing Plugins...");

			//Add plugins to correct lists
			foreach (IPluginInterface i in _allPlugins) {
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
					Console.WriteLine("{0}, Ver. {1} initializing...", i.Name, i.Version);
					((SettingsPlugin) i).Init(_settingsDir);

					if (!i.Enabled)
						Console.WriteLine("Disabled");

					if (i.Enabled) //Only 1 settings plugin can be enabled
						if (_enabledSettingsPlugin == null)
							_enabledSettingsPlugin = (SettingsPlugin)i;
						else
							i.Enabled = false;
				} else if (i.Enabled) {
					_genericPlugins.Add(i);
				}
			}

			if (_enabledSettingsPlugin == null) {
				Console.WriteLine("No settings plugin is enabled, enabling 1st in list");
				_settingsPlugins[0].Enabled = true;
				_enabledSettingsPlugin = _settingsPlugins[0];
			}

			//Init plugins (in correct order)
			foreach (DataPlugin d in _dataPlugins) {
				Console.WriteLine("{0}, Ver. {1} initializing...", d.Name, d.Version);
				d.Init(_enabledSettingsPlugin.GetSettings(d.Name), 
					(_client ? PluginMode.Client : PluginMode.Default), HostId);
				if (!d.Enabled)
					Console.WriteLine("Started in client mode");
			}

			foreach (CommPlugin c in _commPlugins) {
				Console.WriteLine("{0}, Ver. {1} initializing...", c.Name, c.Version);
				c.Init(_enabledSettingsPlugin.GetSettings(c.Name), 
					(_client ? PluginMode.Client : PluginMode.Default), _sensors, HostId);
				if (!c.Enabled)
					Console.WriteLine("Disabled");
			}

			foreach (ControlPlugin c in _controlPlugins) {
				Console.WriteLine("{0}, Ver. {1} initializing...", c.Name, c.Version);
				c.Init(_sensors, _allPlugins, _enabledSettingsPlugin);
				if (!c.Enabled)
					Console.WriteLine("Disabled");
			}

			foreach (IPluginInterface i in _genericPlugins) {
				Console.WriteLine("{0}, Ver. {1} initializing...", i.Name, i.Version);
				//i.Init(_enabledSettingsPlugin.GetSettings(i.Name));
				if (!i.Enabled)
					Console.WriteLine("Disabled");
			}

			Console.WriteLine("Plugins initialized");
			Console.WriteLine();
		}

		static void UpdateSensors() {
			while (_running) {
				List<Sensor> s = new List<Sensor>();

				foreach (DataPlugin d in _dataPlugins)
					s.AddRange(d.Sensors);
				foreach (CommPlugin c in _commPlugins)
					s.AddRange(c.Sensors);

				_sensors = s;
				Thread.Sleep(1000);
			}
		}
	}
}

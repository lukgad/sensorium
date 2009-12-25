﻿using System;
using System.Collections.Generic;
using Common;

namespace Sensorium2
{
	static class Program
	{
		private static string _pluginDir =  Environment.CurrentDirectory;
		private static string _settingsDir = (_pluginDir + @"\settings");

		private static bool _client;
		private static bool _recursive;

	    private static List<IPluginInterface> _allPlugins;

		private static SettingsPlugin _enabledSettingsPlugin;
    	
		private static List<SettingsPlugin> _settingsPlugins;
        private static List<DataPlugin> _dataPlugins;
		private static List<CommPlugin> _commPlugins;
		private static List<IPluginInterface> _genericPlugins;
		
        static void Main(string[] args)
		{
			Console.WriteLine("OS: {0} ({1})", Environment.OSVersion, Environment.OSVersion.Platform);

			ArgHandler(args);

			InitPlugins();

			//Output data plugins' sensor data
			Console.WriteLine();
			Console.WriteLine("Sensors:");
			Console.WriteLine("Source		Plugin		Type		Name		Value");
        	foreach (DataPlugin d in _dataPlugins)
        		foreach (Sensor s in d.Sensors)
        			Console.WriteLine("{0}		{1}	{2}		{3}		{4}",
        			                  s.Source, s.SourcePlugin, s.Type, s.Name, d.SensorToString(s));

        	Console.WriteLine();
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		static void ArgHandler(string[] args) { //Processes command-line args
			for (int i = 0; i < args.Length; i++) {
				if (args[i].Equals("-p")) {
					i++;
					if (i < args.Length)
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
					if (i < args.Length)
						_settingsDir = args[i];
					else {
						HelpMessage();
						Environment.Exit(1);
					}
				}
				else if (args[i].Equals("-h"))
				{
					HelpMessage();
					Environment.Exit(0);
				}
			}
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
			Console.WriteLine("Initializing Plugins...");

			_allPlugins = PluginManager.GetPlugins(_pluginDir, _recursive);

			_settingsPlugins = new List<SettingsPlugin>();
			_dataPlugins = new List<DataPlugin>();
			_commPlugins = new List<CommPlugin>();
			_genericPlugins = new List<IPluginInterface>();

			//Add plugins to correct lists
			foreach (IPluginInterface i in _allPlugins) {
				if (i is DataPlugin)
					_dataPlugins.Add((DataPlugin)i);
				else if (i is CommPlugin)
					_commPlugins.Add((CommPlugin)i);
				else if (i is SettingsPlugin) {
					_settingsPlugins.Add((SettingsPlugin)i);

					//Settings plugins have to init first
					Console.WriteLine("{0}, Ver. {1} initializing...", i.Name, i.Version);
					i.Init(new Dictionary<string, string> { { "settingsDir", _settingsDir } });

					if (!i.Enabled)
						Console.WriteLine("Disabled");

					if (i.Enabled && _enabledSettingsPlugin == null) //Only 1 settings plugin can be enabled
						_enabledSettingsPlugin = (SettingsPlugin)i;
				}
				else {
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
				d.Init(_enabledSettingsPlugin.GetSettings(d.Name), (_client ? PluginMode.Client : PluginMode.Default));
			}

			foreach (CommPlugin c in _commPlugins) {
				Console.WriteLine("{0}, Ver. {1} initializing...", c.Name, c.Version);
				c.Init(_enabledSettingsPlugin.GetSettings(c.Name), (_client ? PluginMode.Client : PluginMode.Default), _dataPlugins);
			}

			foreach (IPluginInterface i in _genericPlugins) {
				Console.WriteLine("{0}, Ver. {1} initializing...", i.Name, i.Version);
				i.Init(_enabledSettingsPlugin.GetSettings(i.Name));
			}

			Console.WriteLine("Plugins initialized");
		}
	}
}
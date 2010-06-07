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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace TextSettingsPlugin
{
	public class TextSettingsPlugin : SettingsPlugin {
		private Dictionary<string, PluginSettings> _settings;

		private string _settingsFile;

		private readonly ILog _log = LogManager.GetLogger(typeof (SettingsPlugin));

		public TextSettingsPlugin(){
			_settings = new Dictionary<string, PluginSettings>();
		}

		public override string Name {
			get { return "Text File Settings"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override string Description
		{
			get { return "Basic settings plugin using a user-editable text file"; }
		}

		public override bool Enabled {
			get { return base.Enabled; }
			set {
				base.Enabled = value;

				Dictionary<string, Setting> mySettings = GetSettings(Name);

				if (!mySettings.ContainsKey("Enabled"))
					mySettings.Add("Enabled", new Setting { value.ToString() });
				else
					mySettings["Enabled"][0] = value.ToString();
			}
		}

		public override void Init(string settingsDir) {
		    base.Init(settingsDir);

			if (SettingsDir == null || SettingsDir.Equals(""))
				throw new DirectoryNotFoundException("Invalid settings directory"); //TODO: May no longer be needed

			string pluginSettingsDir = Path.Combine(settingsDir, Name);
			_settingsFile = Path.Combine(pluginSettingsDir, "config.ini");

			if (!Directory.Exists(pluginSettingsDir)) //Create settings directory if it does not exist
				Directory.CreateDirectory(pluginSettingsDir);
			
			_log.Info("Loading settings...");

			if (!File.Exists(_settingsFile))
				return;

			//Read and parse config file
			using (StreamReader sr = new StreamReader(_settingsFile)){
				string line;
				string currentPlugin = null;

				while((line = sr.ReadLine()) != null)
				{
					line = line.Trim();
					char[] splitChars = {'|'};


                    string[] splitLine = line.Split(splitChars);

					if (currentPlugin == null) {
						if (splitLine[0].Trim().Equals("Plugin")) {
							currentPlugin = splitLine[1].Trim();

							if (!_settings.ContainsKey(currentPlugin))
								_settings.Add(currentPlugin, new PluginSettings());

							_log.Debug("Loaded settings for " + currentPlugin);

							continue;
						}
						continue;
					}

					if (line.Equals("EndPlugin")) {
						currentPlugin = null;
						continue;
					}

					if(_settings[currentPlugin].ContainsKey(splitLine[0]))
						_settings[currentPlugin][splitLine[0]].Add(splitLine[1]);
					else
						_settings[currentPlugin].Add(splitLine[0].Trim(), new Setting { splitLine[1].Trim() });
				}
			}

			if(_settings.ContainsKey(Name) &&_settings[Name].ContainsKey("Enabled") 
				&& _settings[Name]["Enabled"][0].ToLower().Equals("true"))
				Enabled = true;

			foreach(string pluginName in _settings.Keys)
				foreach(string key in SensoriumFactory.GetAppInterface().Plugins[pluginName].DefaultSettings.Keys)
					if (!_settings[pluginName].Keys.Contains(key))
						_settings[pluginName].Add(key, SensoriumFactory.GetAppInterface().Plugins[pluginName].DefaultSettings[key]);
		}

		public override PluginSettings GetSettings(string pluginName) {
			if (!_settings.ContainsKey(pluginName)) {
				_settings.Add(pluginName, new PluginSettings());

				foreach(string key in SensoriumFactory.GetAppInterface().Plugins[pluginName].DefaultSettings.Keys)
					_settings[pluginName].Add(key, SensoriumFactory.GetAppInterface().Plugins[pluginName].DefaultSettings[key]);
			}

			return _settings[pluginName];
		}

		public override void Save()
		{
			//Write data to settings file.
			using (StreamWriter sw = new StreamWriter(_settingsFile))
			{
				foreach (string p in _settings.Keys)
				{
					//if (_settings[p].Count == 0)
					//	continue;

					sw.WriteLine("Plugin|" + p);
					foreach (string settingName in _settings[p].Keys)
					{
						foreach(string setting in _settings[p][settingName])
							sw.WriteLine("	" + settingName + "|" + setting);
					}
					sw.WriteLine("EndPlugin");
					sw.WriteLine();
				}
				sw.Close();
			}
		}

		~TextSettingsPlugin()
		{
			Save();
		}
	}
}

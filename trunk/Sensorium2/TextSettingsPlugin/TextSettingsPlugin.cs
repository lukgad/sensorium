/*	Copyright (C) 2009-2010 Aaron Maslen
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
using Common;

namespace TextSettingsPlugin
{
	public class TextSettingsPlugin : SettingsPlugin {
		private Dictionary<string, Dictionary<string, string>> _settings;
		
		public override string Name {
			get { return "TextFileSettings"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override void Init(Dictionary<string, string> settings) {
			if (settings["settingsDir"] == null || settings["settingsDir"].Equals(""))
				throw new DirectoryNotFoundException("Invalid settings directory"); //TODO: May no longer be needed

			string pluginSettingsDir = settings["settingsDir"] + @"\" + Name;

			if (!Directory.Exists(pluginSettingsDir)) //Create settings directory if it does not exist
				Directory.CreateDirectory(pluginSettingsDir);

			using (FileStream fs = new FileStream(pluginSettingsDir + @"\config.ini",FileMode.OpenOrCreate,FileAccess.Read)) {
				
			}
		}

		public override Dictionary<string, string> GetSettings(string pluginName) {
			return null; //TODO: Actually return something
		}
	}
}

using System;
using System.Collections.Generic;
using Common;

namespace TextSettingsPlugin
{
	public class TextSettingsPlugin : SettingsPlugin {
		public override string Name {
			get { return "TextFileSettings"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override void Init(Dictionary<string, string> settings) {
			if (settings["settingsDir"] == null || settings["settingsDir"].Equals(""))
				throw new Exception("Directory does not exist"); //TODO: use a proper exception

			
		}

		public override Dictionary<string, string> GetSettings(string pluginName) {
			return null;
		}
	}
}

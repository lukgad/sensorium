using System.Collections.Generic;

namespace Common
{
	public abstract class SettingsPlugin : IPluginInterface
	{
		public abstract string Name { get; }
		public abstract int Version { get; }

		public bool Enabled { get; set; }

		public abstract void Init(Dictionary<string, string> settings);
		public abstract Dictionary<string, string> GetSettings(string pluginName);
	}
}

using System.Collections.Generic;

namespace Common
{
	public enum PluginMode { Server, Client, Default }

	public interface IPluginInterface
	{
		//string GetName();
		string Name { get; }
		//int GetVersion();
		int Version { get; }

		bool Enabled { get; set; }

		void Init(Dictionary<string, string> settings);
	}
}

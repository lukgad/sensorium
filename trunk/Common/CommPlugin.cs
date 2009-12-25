using System;
using System.Collections.Generic;

namespace Common
{
	public abstract class CommPlugin : IPluginInterface
	{
		public List<Sensor> Sensors { get; protected set; }
		private List<DataPlugin> _dataPlugins;

		public abstract string Name { get; }
		public abstract int Version { get; }

		private bool _enabled = true;

		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}

		public abstract void Init(Dictionary<string, string> settings);
		public virtual void Init(Dictionary<string, string> settings, PluginMode mode, List<DataPlugin> dataPlugins) {
			if(dataPlugins == null)
				throw new NullReferenceException();

			_dataPlugins = dataPlugins;
		}
	}
}

using System.Collections.Generic;

namespace Common
{

	public abstract class DataPlugin : IPluginInterface
	{
		public List<Sensor> Sensors { get; protected set; } //TODO: This may be bad. Can other plugins add sensors?
															//TODO: If so, I'll need a getter >_>
		protected DataPlugin() {
			Sensors = new List<Sensor>();
		}

		//public abstract string GetName();
        //public abstract int GetVersion();

		public abstract void Init(Dictionary<string, string> settings, PluginMode mode);

		public abstract string Name { get; }
		public abstract int Version { get; }

		private bool _enabled = true;
		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}

		public virtual void Init(Dictionary<string, string> settings) {
			Init(null, PluginMode.Server);
		}

		public abstract string SensorToString(Sensor sensor);
	}
}

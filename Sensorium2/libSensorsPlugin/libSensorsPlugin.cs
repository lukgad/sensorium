using System;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace libSensorsPlugin {
	public class LibSensorsPlugin : DataPlugin {
		public override string Name {
			get { return "libSensorsPlugin"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override string SensorToString(Sensor sensor) {
			throw new NotImplementedException();
		}

		private LibSensorsWrapper _wrapper;

		public override void Init(PluginMode mode) {
			base.Init(mode);

			_wrapper = new LibSensorsWrapper();
		}
	}
}

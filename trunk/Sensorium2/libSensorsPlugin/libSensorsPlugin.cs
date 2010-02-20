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

		public override void Stop() {
			
		}

		public override void Start() {
			
		}

		public override string SensorToString(Sensor sensor) {
			throw new NotImplementedException();
		}
	}
}

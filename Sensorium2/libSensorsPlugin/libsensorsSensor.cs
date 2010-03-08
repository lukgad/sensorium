using System.Collections.Generic;
using System.Text;
using Sensorium.Common;

namespace libSensorsPlugin
{
	class LibsensorsSensor : Sensor
	{
		private LibSensorsTreeNode _feature;

		public readonly string ChipPrefix;

		public LibsensorsSensor(string name, string type, string hostId, string sourcePlugin,
			LibSensorsTreeNode feature) : base(name, type, hostId, sourcePlugin) {
			_feature = feature;

			ChipPrefix = LibSensorsWrapper.GetChipNameStruct((LibSensorsTreeNode) _feature.Parent).prefix;
		}

		public override byte[] Data {
			get {
				List<byte> data = new List<byte>();

				foreach(LibSensorsTreeNode sf in _feature.Children) {
					LibSensorsWrapper.sensors_subfeature subfeature = LibSensorsWrapper.GetSubfeatureStruct(sf);
					data.AddRange(Encoding.UTF8.GetBytes(subfeature.name));
				}


				return null;
			}
		}
	}
}

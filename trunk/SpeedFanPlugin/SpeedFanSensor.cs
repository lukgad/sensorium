using System;
using Common;

namespace SpeedFanPlugin
{
	class SpeedFanSensor : Sensor
	{
		private readonly int _speedFanId;
		public SpeedFanSensor(string name, string type, string source, string sourcePlugin, int speedFanId) : base(name, type, source, sourcePlugin)
		{
			_speedFanId = speedFanId;
		}

		public override byte[] Data {
			get {
				if (Type.Equals("Fan"))
					return BitConverter.GetBytes(SpeedFanWrapper.GetFan(_speedFanId));
				if (Type.Equals("Temp"))
					return BitConverter.GetBytes(SpeedFanWrapper.GetTemp(_speedFanId));
				if (Type.Equals("Volt"))
					return BitConverter.GetBytes(SpeedFanWrapper.GetVolt(_speedFanId));
				
				return null;
			}
		}
	}
}
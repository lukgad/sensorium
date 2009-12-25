using System;
using System.Collections.Generic;
using Common;

namespace SpeedFanPlugin
{
	public class SpeedFanPlugin : DataPlugin
	{

		public override void Init(Dictionary<string, string> settings, PluginMode mode){
			if (mode == PluginMode.Client || Environment.OSVersion.Platform.ToString() != "Win32NT") {
				//TODO: Client logic
				return;
			}

			try {
				SpeedFanWrapper.OpenSharedMemory();
			} catch (NullReferenceException e) { //TODO: Use dedicated exception here
				if (e.Message.Equals("Unable to read shared memory."))
				{
					Console.WriteLine(e.Message + " Restarting in client mode.");
					Init(settings, PluginMode.Client);
				}
				else
					throw;
				
				return;
			}

			Console.WriteLine("SpeedFan shared memory ver. " + SpeedFanWrapper.GetVersion());
			if (SpeedFanWrapper.GetVersion() != 1) {
				Console.WriteLine("Uknown shared memory version. Is SpeedFan running?");
				return;
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumFans(); i++) {
				Sensors.Add(new SpeedFanSensor("Fan" + i, "Fan", "local", Name, i));
				Console.WriteLine("Found Fan{0}: {1}", i, SpeedFanWrapper.GetFan(i));
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumTemps(); i++) {
				Sensors.Add(new SpeedFanSensor("Temp" + i, "Temp", "local", Name, i));
				Console.WriteLine("Found Temp{0}: {1}", i, ((float) SpeedFanWrapper.GetTemp(i))/100);
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumVolts(); i++) {
				Sensors.Add(new SpeedFanSensor("Volt" + i, "Volt", "local", Name, i));
				Console.WriteLine("Found Voltage{0}: {1}", i, ((float) SpeedFanWrapper.GetVolt(i))/100);
			}
		}

		public override string Name {
			get { return "SpeedFan"; }
		}

		public override int Version
		{
			get { return 1; }
		}

		public override void Init(Dictionary<string, string> settings) {
			if (Environment.OSVersion.Platform.ToString() == "Win32NT") {
				Init(null, PluginMode.Server);
			}
			else {
				Init(null, PluginMode.Client);
			}
		}

		public override string SensorToString(Sensor sensor) {
			if (sensor.Type == "Temp" || sensor.Type == "Volt")
				return ((float) BitConverter.ToInt32(sensor.Data, 0) / 100).ToString();
			
			return BitConverter.ToInt32(sensor.Data, 0).ToString();

		}
	}
}

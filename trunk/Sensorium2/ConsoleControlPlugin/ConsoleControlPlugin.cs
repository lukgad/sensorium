using System;
using System.Collections.Generic;
using Common.Plugins;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace ConsoleControlPlugin {
	public class ConsoleControlPlugin : ControlPlugin {
		public override string Name
		{
			get { return "Console Control Plugin"; }
		}

		public override int Version
		{
			get { return 1; }
		}

		public override void Start()
		{
			
		}

		public override void Stop()
		{
			
		}

		public override void Init(List<Sensor> sensors, List<IPluginInterface> plugins, SettingsPlugin settingsPLugin)
		{
			
		}
	}
}

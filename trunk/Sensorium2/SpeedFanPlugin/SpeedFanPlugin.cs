/*	This file is part of Sensorium2 <http://code.google.com/p/sensorium>
 * 
 *	Copyright (C) 2009-2010 Aaron Maslen
 *	This program is free software: you can redistribute it and/or modify it 
 *	under the terms of the GNU General Public License as published by 
 *	the Free Software Foundation, either version 3 of the License, or 
 *	(at your option) any later version. This program is distributed in the 
 *	hope that it will be useful, but WITHOUT ANY WARRANTY; without 
 *	even the implied warranty of MERCHANTABILITY or FITNESS FOR 
 *	A PARTICULAR PURPOSE. See the GNU General Public License 
 *	for more details. You should have received a copy of the GNU General 
 *	Public License along with this program. If not, see <http://www.gnu.org/licenses/>
 */

using System;
using log4net;
using Sensorium.Core;
using Sensorium.Core.Plugins;

namespace SpeedFanPlugin {
	public class SpeedFanPlugin : DataPlugin {
		private const double FanMult = 1;
		private const double TempMult = .01;
		private const double VoltMult = .01;

		private readonly ILog _log = LogManager.GetLogger(typeof (SpeedFanPlugin));

		public SpeedFanPlugin() {
			DefaultSettings.Add("TempNames", new PluginSettings.Setting(false, null));
			DefaultSettings.Add("FanNames", new PluginSettings.Setting(false, null));
			DefaultSettings.Add("VoltNames", new PluginSettings.Setting(false, null));
		}

		public override void Init(PluginMode mode) {
			base.Init(mode);

			if (mode == PluginMode.Client || Environment.OSVersion.Platform != PlatformID.Win32NT) {
				Enabled = false;
			}

			if(!Enabled)
				return;
            
			try {
				SpeedFanWrapper.OpenSharedMemory();
			} catch (NullReferenceException e) {
				if (e.Message.Equals("Unable to read shared memory.")) {
					_log.Warn(e.Message + " Restarting in client mode.");
					Init(PluginMode.Client);
				}
				else
					throw;

				return;
			}

			_log.Debug("SpeedFan shared memory ver. " + SpeedFanWrapper.GetVersion());

			if (SpeedFanWrapper.GetVersion() != 1) {
				_log.Error("Uknown shared memory version. Is SpeedFan running?");
				return;
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumFans(); i++) {
				if (Settings["FanNames"].Count < i + 1)
					Settings["FanNames"].Insert(i, "Fan" + i);

				string name = Settings["FanNames"][i];

				_Sensors.Add(new SpeedFanSensor(name, "Fan", SensoriumFactory.GetAppInterface().HostId, Name, i));
				

				_log.Debug("Found Fan" + i + ": " + (SpeedFanWrapper.GetFan(i) * FanMult));
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumTemps(); i++) {
				if (Settings["TempNames"].Count < i + 1)
					Settings["TempNames"].Insert(i, "Temp" + i);

				string name = Settings["TempNames"][i];

				_Sensors.Add(new SpeedFanSensor(name, "Temp", SensoriumFactory.GetAppInterface().HostId, Name, i));

				_log.Debug("Found Temp" + i + ": " + (SpeedFanWrapper.GetTemp(i) * TempMult));
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumVolts(); i++) {
				if (Settings["VoltNames"].Count < i + 1)
					Settings["VoltNames"].Insert(i, "Volt" + i);

				string name = Settings["VoltNames"][i];

				_Sensors.Add(new SpeedFanSensor(name, "Volt", SensoriumFactory.GetAppInterface().HostId, Name, i));

				_log.Debug("Found Voltage" + i + ": " + (SpeedFanWrapper.GetVolt(i) * VoltMult));
			}

			Settings["FanNames"].ValueChanged += SensorNameChanged;
			Settings["TempNames"].ValueChanged += SensorNameChanged;
			Settings["VoltNames"].ValueChanged += SensorNameChanged;

		}

		~SpeedFanPlugin() {
			SpeedFanWrapper.CloseSharedMemory();
		}

		public override string Name {
			get { return "SpeedFan"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override string Description {
			get { return "Provides SpeedFan support for Windows hosts"; }
		}

		public override string SensorToString(Sensor sensor) {
			if (!sensor.SourcePlugin.Equals(Name))
				throw new ArgumentException("Invalid Sensor");

			if(sensor.Type.Equals("Temp"))
				return (BitConverter.ToInt32(sensor.Data, 0) * TempMult) + "°";
			if (sensor.Type.Equals("Fan"))
				return (BitConverter.ToInt32(sensor.Data, 0) * FanMult) + "rpm";
			if (sensor.Type.Equals("Volt"))
				return (BitConverter.ToInt32(sensor.Data, 0) * VoltMult) + "V";
			
			throw new Exception("Unknown Sensor Type");
		}

		private void SensorNameChanged(object sender, EventArgs e) {
			int fanIndex = 0;
			int tempIndex = 0;
			int voltIndex = 0;
			foreach(SpeedFanSensor s in _Sensors) {
				switch(s.Type) {
					case "Fan":
						s.Name = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(Name)["FanNames"][fanIndex];
						fanIndex++;
						break;

					case "Temp":
						s.Name = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(Name)["TempNames"][tempIndex];
						tempIndex++;
						break;

					case "Volt":
						s.Name = SensoriumFactory.GetAppInterface().EnabledSettingsPlugin.GetSettings(Name)["VoltNames"][voltIndex];
						voltIndex++;
						break;
				}
			}
		}
	}
}

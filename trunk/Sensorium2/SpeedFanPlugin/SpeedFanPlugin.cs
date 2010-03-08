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
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace SpeedFanPlugin
{
	public class SpeedFanPlugin : DataPlugin
	{
		private const double FanMult = 1;
		private const double TempMult = .01;
		private const double VoltMult = .01;

		private readonly ILog _log = LogManager.GetLogger(typeof (SpeedFanPlugin));

		public override void Init(PluginMode mode){
			base.Init(mode);

			if (Settings.ContainsKey("Enabled"))
				Enabled = Settings["Enabled"][0].ToLower().Equals("true");

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

			if (_log.IsDebugEnabled)
				_log.Debug("SpeedFan shared memory ver. " + SpeedFanWrapper.GetVersion());

			if (SpeedFanWrapper.GetVersion() != 1) {
				_log.Error("Uknown shared memory version. Is SpeedFan running?");
				return;
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumFans(); i++) {
				_Sensors.Add(new SpeedFanSensor("Fan" + i, "Fan", 
					SensoriumFactory.GetAppInterface().HostId, Name, i));
				if (_log.IsDebugEnabled)
					_log.Debug("Found Fan" + i + ": " + (SpeedFanWrapper.GetFan(i) * FanMult));
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumTemps(); i++) {
				_Sensors.Add(new SpeedFanSensor("Temp" + i, "Temp", 
					SensoriumFactory.GetAppInterface().HostId, Name, i));
				if (_log.IsDebugEnabled)
					_log.Debug("Found Temp" + i + ": " + (SpeedFanWrapper.GetTemp(i) * TempMult));
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumVolts(); i++) {
				_Sensors.Add(new SpeedFanSensor("Volt" + i, "Volt", 
					SensoriumFactory.GetAppInterface().HostId, Name, i));
				if (_log.IsDebugEnabled)
					_log.Debug("Found Voltage" + i + ": " + (SpeedFanWrapper.GetVolt(i) * VoltMult));
			}
		}

		~SpeedFanPlugin() {
			SpeedFanWrapper.CloseSharedMemory();
		}

		public override string Name {
			get { return "SpeedFan"; }
		}

		public override int Version
		{
			get { return 1; }
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
	}
}

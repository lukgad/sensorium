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
using System.Collections.Generic;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace SpeedFanPlugin
{
	public class SpeedFanPlugin : DataPlugin
	{
		private const double FanMult = 1;
		private const double TempMult = .01;
		private const double VoltMult = .01;

		public override void Init(IAppInterface app, PluginMode mode){
			base.Init(app, mode);

			foreach (string s in Settings.Keys)
				if (s.Equals("Enabled")) {
					Enabled = Settings[s].ToLower().Equals("true");
					break;
				}

			if (mode == PluginMode.Client || Environment.OSVersion.Platform.ToString() != "Win32NT") {
				Enabled = false;
			}

			if(!Enabled)
				return;
            
			try {
				SpeedFanWrapper.OpenSharedMemory();
			} catch (NullReferenceException e) {
				if (e.Message.Equals("Unable to read shared memory.")) {
					Console.WriteLine(e.Message + " Restarting in client mode.");
					Init(app, PluginMode.Client);
				}
				else
					throw;

				return;
			}

			Console.WriteLine("SpeedFan shared memory ver. {0}", SpeedFanWrapper.GetVersion());
			if (SpeedFanWrapper.GetVersion() != 1) {
				Console.WriteLine("Uknown shared memory version. Is SpeedFan running?");
				return;
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumFans(); i++) {
				_Sensors.Add(new SpeedFanSensor("Fan" + i, "Fan", app.HostId, Name, i));
				Console.WriteLine("Found Fan{0}: {1}", i, SpeedFanWrapper.GetFan(i) * FanMult);
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumTemps(); i++) {
				_Sensors.Add(new SpeedFanSensor("Temp" + i, "Temp", app.HostId, Name, i));
				Console.WriteLine("Found Temp{0}: {1}", i, SpeedFanWrapper.GetTemp(i) * TempMult);
			}

			for (int i = 0; i < SpeedFanWrapper.GetNumVolts(); i++) {
				_Sensors.Add(new SpeedFanSensor("Volt" + i, "Volt", app.HostId, Name, i));
				Console.WriteLine("Found Voltage{0}: {1}", i, SpeedFanWrapper.GetVolt(i) * VoltMult);
			}
		}

		public override string Name {
			get { return "SpeedFan"; }
		}

		public override int Version
		{
			get { return 1; }
		}

		public override void Start() {
			
		}

		public override void Stop() {

		}

		public override string SensorToString(Sensor sensor) {
			if (!sensor.SourcePlugin.Equals("SpeedFan"))
				throw new Exception("Invalid Sensor");

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

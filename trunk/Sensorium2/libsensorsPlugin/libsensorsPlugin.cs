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
using System.Text;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace libSensorsPlugin {
	public class LibSensorsPlugin : DataPlugin {
		public override string Name {
			get { return "libsensors"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override string Description
		{
			get { return "Provides libsensors support on Linux hosts"; }
		}

		private LibSensorsWrapper _wrapper;

		public override void Init(PluginMode mode) {
			base.Init(mode);

			if (mode == PluginMode.Client || Environment.OSVersion.Platform != PlatformID.Unix ||
				!Settings.ContainsKey("Enabled")) {
				if (Settings.ContainsKey("Enabled"))
					Settings["Enabled"][0] = "False";
				else
					Settings.Add("Enabled",new Setting {"False"});

				Enabled = false;

                return;
			}

			Enabled = true;
            
			_wrapper = new LibSensorsWrapper();
			
			foreach(LibSensorsTreeNode cn in _wrapper.Chips.Children)
				foreach(LibSensorsTreeNode f in cn.Children)
					_Sensors.Add(new LibsensorsSensor(LibSensorsWrapper.GetFeatureName(f), 
						LibSensorsWrapper.GetFeatureType(f).ToString(), SensoriumFactory.GetAppInterface().HostId, 
						Name, f));
		}

		public override string SensorToString(Sensor sensor) {
			if (sensor.SourcePlugin != Name)
				throw new ArgumentException("Invalid Sensor");

			List<string> subfeatureNames = new List<string>();
			List<double> subfeatureValues = new List<double>();

			for (int i = 0; i < sensor.Data.Length; i += 8) {
				//Get the name (string)
				List<byte> nameBytes = new List<byte>();
				
				while (sensor.Data[i] != 0x00) {
					nameBytes.Add(sensor.Data[i]);
					i++;
				}
				subfeatureNames.Add(Encoding.UTF8.GetString(nameBytes.ToArray()));

				i++;
				//Get the value (double)
				subfeatureValues.Add(BitConverter.ToDouble(sensor.Data, i));
			}
			
			if(subfeatureNames.Count != subfeatureValues.Count)
				throw new Exception();
			
			string output = "(";
			
			for (int i = 0; i < subfeatureNames.Count; i++) {
				output += subfeatureNames[i] + ": " + subfeatureValues[i];
				if (i < subfeatureNames.Count -1)
					output += " ";
			}
			
			output += ")";
			
			return output;
		}
	}
}

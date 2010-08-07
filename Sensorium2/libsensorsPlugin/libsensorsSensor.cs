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
using Sensorium.Core;

namespace libSensorsPlugin
{
	class LibsensorsSensor : Sensor
	{
		private LibSensorsTreeNode _feature;

		public readonly string ChipPrefix;

		public LibsensorsSensor(string name, string type, string hostId, string sourcePlugin,
			LibSensorsTreeNode feature) : base(name, type, hostId, sourcePlugin) {
			if(feature.NodeType != NodeType.Feature)
				throw new ArgumentException();

			_feature = feature;

			ChipPrefix = LibSensorsWrapper.GetChipNameStruct((LibSensorsTreeNode) _feature.Parent).prefix;
		}

		public override byte[] Data {
			get {
				List<byte> data = new List<byte>();

				//Add all subfeatures and values to data array
				foreach(LibSensorsTreeNode sf in _feature.Children) {
					LibSensorsWrapper.sensors_subfeature subfeature = LibSensorsWrapper.GetSubfeatureStruct(sf);
					data.AddRange(Encoding.UTF8.GetBytes(subfeature.name));
					data.Add(0x00);
					data.AddRange(BitConverter.GetBytes(LibSensorsWrapper.GetSubfeatureValue(sf)));
				}

				return data.ToArray();
			}
		}
	}
}

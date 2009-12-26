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
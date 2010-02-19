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
using log4net;
using log4net.Appender;
using Sensorium.Common.Plugins;

namespace Sensorium.Common {
	public class AppData : IAppInterface {
		private List<Sensor> _sensors;
		public List<Sensor> Sensors {
			get { return new List<Sensor>(_sensors); }
			set { _sensors = value; }
		}

		public List<IPluginInterface> Plugins { get; set; }

		public SettingsPlugin EnabledSettingsPlugin { get; set; }

		private readonly string _hostId = Guid.NewGuid().ToString();
		public string HostId {
			get { return _hostId; }
		}
		public List<MemoryAppender> Logs { get; set; }
	}
}

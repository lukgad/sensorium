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

namespace Sensorium.Common {
	public class PluginSettings : Dictionary<string, PluginSettings.Setting> {
		public class Setting : List<string> {
			public bool SingleValue { get; protected set; }
			public List<string> ValidValues { get; protected set; }

			public string SettingsGroup { get; protected set; }

			public Setting(bool singleValue, List<string> validValues)
				: this(singleValue, validValues, "")
			{
			}

		    public Setting(bool singleValue, List<string> validValues, string settingsGroup) {
				SingleValue = singleValue;
				ValidValues = validValues;
				SettingsGroup = settingsGroup;
			}

			public new void Add(string s) {
				if (SingleValue && Count == 1)
					throw new ArgumentException("Only one value is allowed for this setting");

				if (ValidValues != null && !ValidValues.Contains(s))
					throw new ArgumentException(String.Format("{0} is not a valid value for this setting", s));
				
				base.Add(s);
			}
		}
	}
}
/*	Copyright (C) 2009-2010 Aaron Maslen
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

using System.Collections.Generic;
using Common;

namespace DotNetSettingsPlugin {
	public class DotNetSettingsPlugin : SettingsPlugin {
		public override string Name
		{
			get { return ".NET Settings Plugin"; }
		}

		public override int Version
		{
			get { return 1; }
		}

		public override void Init(Dictionary<string, string> settings)
		{

		}

		public override Dictionary<string, string> GetSettings(string pluginName)
		{
			return null;
		}
	}
}

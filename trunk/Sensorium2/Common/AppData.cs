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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using log4net.Appender;
using Sensorium.Common.Plugins;

namespace Sensorium.Common {
	public class AppData : IAppInterface {
		private List<Sensor> _sensors;
		public List<Sensor> Sensors {
			get { return new List<Sensor>(_sensors); }
			set { _sensors = value; }
		}

		public Dictionary<string, IPluginInterface> Plugins { get; set; }

		public SettingsPlugin EnabledSettingsPlugin { get; set; }

		public string Version { get; set; }
		public string FileVersion { get; set; }
		public string FriendlyName { get; set; }
		public string HostId
		{
			get { return '{' + HostGuid + '}' + FriendlyName; }
		}

		public string Copyright { get; set; }
		public string Description { get; set; }

		private readonly string _hostGuid = Guid.NewGuid().ToString();
        public string HostGuid {
			get { return _hostGuid; }
		}

		public MemoryAppender Log { get; set; }

		public event EventHandler<CancelEventArgs> HideConsoleEventHandler;

		public void OnHideConsole() {
			EventHandler<CancelEventArgs> handler = HideConsoleEventHandler;
			
			CancelEventArgs e = new CancelEventArgs(false);

			if (handler != null)
				handler(this, e);

			if(e.Cancel)
				return;

			Console.Title = _hostGuid;
			//Wait for the new title to be applied to the window
			Thread.Sleep(200);
			IntPtr hWnd = FindWindow(null, Console.Title);

			if (hWnd != IntPtr.Zero)
				//Hide the window
				ShowWindow(hWnd, 0); // 0 = SW_HIDE
		}

		[DllImport("user32.dll")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
	}
}

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
using System.Threading;
using System.Windows.Forms;
using Sensorium.Common.Plugins;

namespace WinFormsControlPlugin {
	public class WinFormsControlPlugin : ControlPlugin {
		public override string Name {
			get { return "WinForms Control"; }
		}

		public override int Version {
			get { return 1; }
		}

		public override string Description
		{
			get { return "Provides a WinForms-based GUI. Has more functionality than the basic console control plugin."; }
		}

		private static MainWindow _mainWindow;
		private bool _closing;

		public override void Init() {
			base.Init();
			_mainWindow = new MainWindow();
			_mainWindow.Closed += MainWindowClosedHandler;
		}

		private void MainWindowClosedHandler(object sender, EventArgs e) {
			if(_closing)
				return;

			OnExit();
		}

		protected override void OnExit() {
			_closing = true;
			base.OnExit();
		}

		private static void ShowWindow() {
			Application.EnableVisualStyles();
			Application.Run(_mainWindow);
		}

		public override void Start() {
			_closing = false;
			(new Thread(ShowWindow)).Start();
			
			base.Start();
		}

		public override void Stop() {
			_closing = true;

			_mainWindow.Close();

			base.Stop();
		}
	}
}

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

		private static MainWindow _mainWindow;
		private bool _closing = false;

		public override void Init() {
			base.Init();
			_mainWindow = new MainWindow();
			_mainWindow.Closed += MainWindowClosedHandler;
		}

		private void MainWindowClosedHandler(object sender, EventArgs e) {
			_closing = true;
			OnExit();
		}

		private static void ShowWindow() {
			Application.Run(_mainWindow);
		}

		public override void Start() {
			_closing = false;
			(new Thread(ShowWindow)).Start();
		}

		public override void Stop() {
			if(_closing)
				return;

			_mainWindow.Close();
		}
	}
}

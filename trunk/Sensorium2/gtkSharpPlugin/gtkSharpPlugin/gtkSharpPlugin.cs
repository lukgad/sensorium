using System;
using Gtk;
using Sensorium.Common;
using Sensorium.Common.Plugins;

namespace gtkSharpPlugin {
	class gtkSharpPlugin : DataPlugin {
		public override string Name { get { return "GTK Sharp"; } }
		public override int Version { get { return 1; } }
		public override string Description { get { return ""; } }
		
		public override string SensorToString(Sensor sensor) {
			return "";
		}
		
		private MainWindow _mainWindow;
		private bool closing;
		
		public override void Init (PluginMode mode) {
			base.Init (mode);
			
			_mainWindow = new MainWindow();
			
		}
		
		public override void Start() {
			_closing = false;
			(new Thread(ShowWindow)).Start();
			
			base.Start();
		}
		
		private void ShowWindow() {
			Application.Run(_mainWindow);
		}
		
		public override void Stop() {
			_closing = true;
			
			_mainWindow.Close();
			
			base.Stop();
		}
 
		
//		public static void Main (string[] args) {
//			Application.Init ();
//			MainWindow win = new MainWindow ();
//			win.Show ();
//			Application.Run ();
//		}
	}
}

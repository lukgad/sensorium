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

		public override void Init() {
			base.Init();
			(new Thread(ShowWindow)).Start();
		}

		private static void ShowWindow() {
			Application.Run(new MainWindow());
		}
	}
}

using System.Threading;
using Sensorium.Core.Plugins;

namespace WxWidgetsControlPlugin
{
	public class WxWidgetsControlPlugin : ControlPlugin
	{
		public override string Name
		{
			get { return "WxWdigets Control Plugin"; }
		}

		public override int Version
		{
			get { return 1; }
		}

		public override string Description
		{
			get { return "Basic WxWidgets-based Control Plugin. Cross-platform"; }
		}

		private WxApp _wxApp;

		public override void Init() {
			base.Init();

			_wxApp = new WxApp();

			(new Thread(_wxApp.Start)).Start();
		}
	}
}

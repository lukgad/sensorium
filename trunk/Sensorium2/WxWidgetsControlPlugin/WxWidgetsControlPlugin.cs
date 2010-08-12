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


	}
}

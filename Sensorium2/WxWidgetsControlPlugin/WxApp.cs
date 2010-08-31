using System;

namespace WxWidgetsControlPlugin {
	class WxApp : wx.App {
		public override bool OnInit() {
			WxControlFrame frame = new WxControlFrame();
			frame.Show(true);

			return true;
		}

		[STAThread]
		public void Start() {
			Run();
		}
	}
}

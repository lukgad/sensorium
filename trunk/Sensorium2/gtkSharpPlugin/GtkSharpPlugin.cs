using System;
using Sensorium.Common.Plugins;

namespace gtkSharpPlugin {
	class GtkSharpPlugin : ControlPlugin {
		public override string Name
		{
			get { return "GTK Sharp Control"; }
		}

		public override int Version
		{
			get { return 1; }
		}
	}
}

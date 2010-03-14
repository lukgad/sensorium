using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sensorium.Common;

namespace WinFormsControlPlugin {
	public partial class MainWindow : Form {
		public MainWindow() {
			InitializeComponent();
		}

		private void MainWindow_Load(object sender, EventArgs e) {
			Text = "Sensorium2 " + SensoriumFactory.GetAppInterface().Version;
		}

		public new void Close() {
			if (InvokeRequired) {
				Invoke(new Action(Close));
				return;
			}

			base.Close();
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {

		}
	}
}

using System.Windows.Forms;

namespace WinFormsControlPlugin
{
    public sealed partial class SettingsDialog : Form
    {
        public SettingsDialog(string pluginName) : this() {
            Text = string.Format("{0} Settings", pluginName);
        }

        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void SettingsDialog_Load(object sender, System.EventArgs e)
        {

        }
    }
}

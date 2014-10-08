using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EmbSysRegView
{
    public partial class OpenChipFileDialog : Form
    {
        private string basePath;
        private string archPath;
        private string vendorPath;
        private string chipPath;

        public string ChipFile { get; private set; }

        public OpenChipFileDialog()
        {
            InitializeComponent();
            basePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "data");
        }

        private void OpenChipFileDialog_Load(object sender, EventArgs e)
        {
            comboBoxArch.Items.AddRange(Directory.GetDirectories(basePath).Select(dir => dir.Substring(basePath.Length + 1)).ToArray());
        }

        private void comboBoxArch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var str = comboBoxArch.SelectedItem as string;
            if (str != null)
            {
                archPath = Path.Combine(basePath, str);
                comboBoxVendor.Items.AddRange(Directory.GetDirectories(archPath).Select(dir => dir.Substring(archPath.Length + 1)).ToArray());
            }
        }

        private void comboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var str = comboBoxVendor.SelectedItem as string;
            if (str != null)
            {
                vendorPath = Path.Combine(archPath, str);
                comboBoxChip.Items.AddRange(Directory.GetFiles(vendorPath, "*.xml").Select(dir => dir.Substring(vendorPath.Length + 1)).ToArray());
            }
        }

        private void comboBoxChip_SelectedIndexChanged(object sender, EventArgs e)
        {
            var str = comboBoxChip.SelectedItem as string;
            if (str != null)
            {
                chipPath = Path.Combine(vendorPath, str);
                buttonOk.Enabled = true;
            }
            else
            {
                buttonOk.Enabled = false;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            ChipFile = chipPath;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}

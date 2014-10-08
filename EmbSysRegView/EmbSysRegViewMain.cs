using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;

namespace EmbSysRegView
{
    public partial class EmbSysRegViewMain : Form
    {
        private class ToolTipProvider: IToolTipProvider
        {
            public string GetToolTip(TreeNodeAdv node, NodeControl nodeControl)
            {
                var item = node.Tag as BaseItem;
                if (item != null)
                    return item.Description;
                return null;
            }
        }

        public EmbSysRegViewMain()
        {
            InitializeComponent();
            nodeTextBoxDescription.ToolTipProvider = new ToolTipProvider();
            foreach (var node in tree.AllNodes)
            {
            }
        }

        public void LoadChipFile(string filename)
        {
            // Create a schema validating XmlReader.
            var reader = XmlReader.Create(filename, new XmlReaderSettings() { ValidationType = ValidationType.DTD, DtdProcessing = DtdProcessing.Parse });

            try
            {
                var doc = XDocument.Load(reader);
                tree.Model = new RegViewModel(doc);
            }
            catch (XmlSchemaValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoadChipFile(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "data", "cortex-m3", "STMicro", "stm32f20x.xml"));
        }

        private void tree_DrawControl(object sender, Aga.Controls.Tree.NodeControls.DrawEventArgs e)
        {
            var item = e.Node.Tag as BaseItem;
            var groupItem = item as GroupItem;
            var regGroupItem = item as RegisterGroupItem;
            var registerItem = item as RegisterItem;
            var fieldItem = item as FieldItem;
            var control = e.Control as BindableControl;

            if (item == null || control == null) return;

            switch (control.DataPropertyName)
            {
                case "Bin":
                    e.Text = item.FormatBin();
                    break;
                case "Hex":
                    e.Text = item.FormatHex();
                    break;
                case "Reset":
                    e.Text = item.FormatReset();
                    break;
                case "Address":
                    e.Text = item.FormatAddress();
                    break;
            }
        }

        private void tree_NodeMouseClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
            // only handle right click
            if (e.Button != System.Windows.Forms.MouseButtons.Right) return;

            // handle register groups
            if (e.Node.Tag is RegisterGroupItem)
            {
                var regGroup = e.Node.Tag as RegisterGroupItem;
                var enable = !regGroup.Children.All(reg => reg.Enabled);
                foreach (var reg in regGroup.Children)
                    reg.Enabled = enable;
            }

            // handle registers
            if (e.Node.Tag is RegisterItem)
            {
                var register = e.Node.Tag as RegisterItem;
                register.Enabled = !register.Enabled;
            }

            // handle fields
            if (e.Node.Tag is FieldItem)
            {
                var register = (e.Node.Tag as FieldItem).Parent;
                register.Enabled = !register.Enabled;
            }
        }

        private void hideDisabledToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            (tree.Model as RegViewModel).ShowDisabled = !hideDisabledToolStripMenuItem.Checked;
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using System.Collections.Generic;

namespace EmbSysRegView
{
    public partial class EmbSysRegViewMain : Form
    {
        private OpenOcdTclClient client;

        private class DescriptionToolTipProvider: IToolTipProvider
        {
            public string GetToolTip(TreeNodeAdv node, NodeControl nodeControl)
            {
                var item = node.Tag as BaseItem;
                if (item != null)
                    return item.Description;
                return null;
            }
        }

        private class InterpretationToolTipProvider : IToolTipProvider
        {
            public string GetToolTip(TreeNodeAdv node, NodeControl nodeControl)
            {
                var item = node.Tag as FieldItem;
                if (item == null) return null;
                var interp = item.Interpretation();
                if (interp == item.Description) return null;
                return interp;
            }
        }

        public EmbSysRegViewMain()
        {
            InitializeComponent();
            nodeTextBoxDescription.ToolTipProvider = new DescriptionToolTipProvider();
            nodeTextBoxBin.ToolTipProvider = new InterpretationToolTipProvider();
            nodeTextBoxHex.ToolTipProvider = new InterpretationToolTipProvider();
            client = new OpenOcdTclClient(this);
            client.ConnectionChanged += ClientOnConnectionChanged;
            client.TargetEvent += ClientOnTargetEvent;
        }

        private void ClientOnConnectionChanged(object sender, EventArgs e)
        {
            if (client.Connected)
                toolStripStatusLabelConnected.Text = "Connected";
            else
                toolStripStatusLabelConnected.Text = "Disconnected";
        }

        private void ClientOnTargetEvent(object sender, OpenOcdTclClient.TargetEventArgs e)
        {
            switch (e.EventType)
            {
                case OpenOcdTclClient.TargetEventType.GdbHalt:
                    RefreshRegisters();
                    break;
            }
        }

        private void RefreshRegisters()
        {
            foreach (var node in tree.AllNodes)
            {
                var register = node.Tag as RegisterItem;
                if (register != null && register.Enabled)
                {
                    register.Value = client.ReadMemory(register.Address);
                }
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
                    e.TextColor = (item.Changed) ? System.Drawing.Color.Red : System.Drawing.SystemColors.ControlText;
                    break;
                case "Hex":
                    e.Text = item.FormatHex();
                    e.TextColor = (item.Changed) ? System.Drawing.Color.Red : System.Drawing.SystemColors.ControlText;
                    break;
                case "Reset":
                    e.Text = item.FormatReset();
                    break;
                case "Address":
                    e.Text = item.FormatAddress();
                    break;
                case "Description":
                    if (fieldItem != null)
                        e.Text = fieldItem.Interpretation();
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

        private void EmbSysRegViewMain_Load(object sender, EventArgs e)
        {
            client.Start();
        }

        private void toolStripMenuItemLoadChipFile_Click(object sender, EventArgs e)
        {
            var dialog = new OpenChipFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                LoadChipFile(dialog.ChipFile);
        }

        private void dumpAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = "";

            foreach (var node in tree.AllNodes)
            {
                var register = node.Tag as RegisterItem;
                if (register != null && register.Enabled)
                {
                    var name = register.ItemPath;
                    var val = client.ReadMemory(register.Address);
                    text += String.Format("{0}: {1:X8}\r\n", name, val);
                }
            }

            if (saveFileDialogDump.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialogDump.FileName, text);
            }
        }

        private void loadDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogDump.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var values = new Dictionary<string, uint>();
                var text = File.ReadAllText(openFileDialogDump.FileName);
                foreach (var line in text.Split('\n'))
                {
                    if (line.Trim().Length == 0) continue;
                    var parts = line.Split(':');
                    var name = parts[0].Trim();
                    var val = Convert.ToUInt32(parts[1].Trim(), 16);
                    values.Add(name, val);
                }
                foreach (var node in tree.AllNodes)
                {
                    var register = node.Tag as RegisterItem;
                    if (register != null && values.ContainsKey(register.ItemPath))
                    {
                        register.Value = values[register.ItemPath];
                        register.Enabled = true;
                        register.Owner.OnNodesChanged(register);
                    }
                }
            }
        }
    }
}

namespace EmbSysRegView
{
    partial class EmbSysRegViewMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmbSysRegViewMain));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tree = new Aga.Controls.Tree.TreeViewAdv();
            this.register = new Aga.Controls.Tree.TreeColumn();
            this.hex = new Aga.Controls.Tree.TreeColumn();
            this.bin = new Aga.Controls.Tree.TreeColumn();
            this.reset = new Aga.Controls.Tree.TreeColumn();
            this.access = new Aga.Controls.Tree.TreeColumn();
            this.address = new Aga.Controls.Tree.TreeColumn();
            this.description = new Aga.Controls.Tree.TreeColumn();
            this.nodeStateIcon = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            this.nodeTextBoxName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxHex = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxBin = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxReset = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxAccess = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxAddress = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBoxDescription = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.hideDisabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(824, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(125, 20);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // tree
            // 
            this.tree.BackColor = System.Drawing.SystemColors.Window;
            this.tree.Columns.Add(this.register);
            this.tree.Columns.Add(this.hex);
            this.tree.Columns.Add(this.bin);
            this.tree.Columns.Add(this.reset);
            this.tree.Columns.Add(this.access);
            this.tree.Columns.Add(this.address);
            this.tree.Columns.Add(this.description);
            this.tree.DefaultToolTipProvider = null;
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.DragDropMarkColor = System.Drawing.Color.Black;
            this.tree.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.tree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tree.Location = new System.Drawing.Point(0, 24);
            this.tree.Model = null;
            this.tree.Name = "tree";
            this.tree.NodeControls.Add(this.nodeStateIcon);
            this.tree.NodeControls.Add(this.nodeTextBoxName);
            this.tree.NodeControls.Add(this.nodeTextBoxHex);
            this.tree.NodeControls.Add(this.nodeTextBoxBin);
            this.tree.NodeControls.Add(this.nodeTextBoxReset);
            this.tree.NodeControls.Add(this.nodeTextBoxAccess);
            this.tree.NodeControls.Add(this.nodeTextBoxAddress);
            this.tree.NodeControls.Add(this.nodeTextBoxDescription);
            this.tree.SelectedNode = null;
            this.tree.ShowNodeToolTips = true;
            this.tree.Size = new System.Drawing.Size(824, 443);
            this.tree.TabIndex = 1;
            this.tree.UseColumns = true;
            this.tree.NodeMouseClick += new System.EventHandler<Aga.Controls.Tree.TreeNodeAdvMouseEventArgs>(this.tree_NodeMouseClick);
            this.tree.DrawControl += new System.EventHandler<Aga.Controls.Tree.NodeControls.DrawEventArgs>(this.tree_DrawControl);
            // 
            // register
            // 
            this.register.Header = "Register";
            this.register.SortOrder = System.Windows.Forms.SortOrder.None;
            this.register.TooltipText = null;
            this.register.Width = 100;
            // 
            // hex
            // 
            this.hex.Header = "Hex";
            this.hex.SortOrder = System.Windows.Forms.SortOrder.None;
            this.hex.TooltipText = null;
            this.hex.Width = 80;
            // 
            // bin
            // 
            this.bin.Header = "Bin";
            this.bin.SortOrder = System.Windows.Forms.SortOrder.None;
            this.bin.TooltipText = null;
            this.bin.Width = 200;
            // 
            // reset
            // 
            this.reset.Header = "Reset";
            this.reset.SortOrder = System.Windows.Forms.SortOrder.None;
            this.reset.TooltipText = null;
            this.reset.Width = 80;
            // 
            // access
            // 
            this.access.Header = "Access";
            this.access.SortOrder = System.Windows.Forms.SortOrder.None;
            this.access.TooltipText = null;
            // 
            // address
            // 
            this.address.Header = "Address";
            this.address.SortOrder = System.Windows.Forms.SortOrder.None;
            this.address.TooltipText = null;
            this.address.Width = 100;
            // 
            // description
            // 
            this.description.Header = "Description";
            this.description.SortOrder = System.Windows.Forms.SortOrder.None;
            this.description.TooltipText = null;
            this.description.Width = 200;
            // 
            // nodeStateIcon
            // 
            this.nodeStateIcon.DataPropertyName = "Icon";
            this.nodeStateIcon.LeftMargin = 1;
            this.nodeStateIcon.ParentColumn = this.register;
            this.nodeStateIcon.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // nodeTextBoxName
            // 
            this.nodeTextBoxName.DataPropertyName = "Name";
            this.nodeTextBoxName.IncrementalSearchEnabled = true;
            this.nodeTextBoxName.LeftMargin = 3;
            this.nodeTextBoxName.ParentColumn = this.register;
            // 
            // nodeTextBoxHex
            // 
            this.nodeTextBoxHex.DataPropertyName = "Hex";
            this.nodeTextBoxHex.IncrementalSearchEnabled = true;
            this.nodeTextBoxHex.LeftMargin = 3;
            this.nodeTextBoxHex.ParentColumn = this.hex;
            // 
            // nodeTextBoxBin
            // 
            this.nodeTextBoxBin.DataPropertyName = "Bin";
            this.nodeTextBoxBin.IncrementalSearchEnabled = true;
            this.nodeTextBoxBin.LeftMargin = 3;
            this.nodeTextBoxBin.ParentColumn = this.bin;
            // 
            // nodeTextBoxReset
            // 
            this.nodeTextBoxReset.DataPropertyName = "Reset";
            this.nodeTextBoxReset.IncrementalSearchEnabled = true;
            this.nodeTextBoxReset.LeftMargin = 3;
            this.nodeTextBoxReset.ParentColumn = this.reset;
            // 
            // nodeTextBoxAccess
            // 
            this.nodeTextBoxAccess.DataPropertyName = "Access";
            this.nodeTextBoxAccess.IncrementalSearchEnabled = true;
            this.nodeTextBoxAccess.LeftMargin = 3;
            this.nodeTextBoxAccess.ParentColumn = this.access;
            // 
            // nodeTextBoxAddress
            // 
            this.nodeTextBoxAddress.DataPropertyName = "Address";
            this.nodeTextBoxAddress.IncrementalSearchEnabled = true;
            this.nodeTextBoxAddress.LeftMargin = 3;
            this.nodeTextBoxAddress.ParentColumn = this.address;
            // 
            // nodeTextBoxDescription
            // 
            this.nodeTextBoxDescription.DataPropertyName = "Description";
            this.nodeTextBoxDescription.EditEnabled = true;
            this.nodeTextBoxDescription.IncrementalSearchEnabled = true;
            this.nodeTextBoxDescription.LeftMargin = 3;
            this.nodeTextBoxDescription.ParentColumn = this.description;
            this.nodeTextBoxDescription.Trimming = System.Drawing.StringTrimming.EllipsisWord;
            this.nodeTextBoxDescription.TrimMultiLine = true;
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideDisabledToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItem2.Text = "Settings";
            // 
            // hideDisabledToolStripMenuItem
            // 
            this.hideDisabledToolStripMenuItem.CheckOnClick = true;
            this.hideDisabledToolStripMenuItem.Name = "hideDisabledToolStripMenuItem";
            this.hideDisabledToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hideDisabledToolStripMenuItem.Text = "Hide Disabled";
            this.hideDisabledToolStripMenuItem.CheckedChanged += new System.EventHandler(this.hideDisabledToolStripMenuItem_CheckedChanged);
            // 
            // EmbSysRegViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 467);
            this.Controls.Add(this.tree);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "EmbSysRegViewMain";
            this.Text = "EmbSysRegView";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private Aga.Controls.Tree.TreeViewAdv tree;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private Aga.Controls.Tree.TreeColumn register;
        private Aga.Controls.Tree.TreeColumn hex;
        private Aga.Controls.Tree.TreeColumn bin;
        private Aga.Controls.Tree.TreeColumn reset;
        private Aga.Controls.Tree.TreeColumn access;
        private Aga.Controls.Tree.TreeColumn address;
        private Aga.Controls.Tree.TreeColumn description;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxName;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxDescription;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxReset;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxAccess;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxAddress;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxHex;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxBin;
        private Aga.Controls.Tree.NodeControls.NodeStateIcon nodeStateIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem hideDisabledToolStripMenuItem;
    }
}


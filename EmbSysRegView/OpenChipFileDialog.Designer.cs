namespace EmbSysRegView
{
    partial class OpenChipFileDialog
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
            this.comboBoxArch = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxVendor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxChip = new System.Windows.Forms.ComboBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxArch
            // 
            this.comboBoxArch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArch.FormattingEnabled = true;
            this.comboBoxArch.Location = new System.Drawing.Point(12, 25);
            this.comboBoxArch.Name = "comboBoxArch";
            this.comboBoxArch.Size = new System.Drawing.Size(207, 21);
            this.comboBoxArch.TabIndex = 0;
            this.comboBoxArch.SelectedIndexChanged += new System.EventHandler(this.comboBoxArch_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Architecture";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vendor";
            // 
            // comboBoxVendor
            // 
            this.comboBoxVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVendor.FormattingEnabled = true;
            this.comboBoxVendor.Location = new System.Drawing.Point(12, 79);
            this.comboBoxVendor.Name = "comboBoxVendor";
            this.comboBoxVendor.Size = new System.Drawing.Size(207, 21);
            this.comboBoxVendor.TabIndex = 2;
            this.comboBoxVendor.SelectedIndexChanged += new System.EventHandler(this.comboBoxVendor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Chip";
            // 
            // comboBoxChip
            // 
            this.comboBoxChip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChip.FormattingEnabled = true;
            this.comboBoxChip.Location = new System.Drawing.Point(12, 133);
            this.comboBoxChip.Name = "comboBoxChip";
            this.comboBoxChip.Size = new System.Drawing.Size(207, 21);
            this.comboBoxChip.TabIndex = 6;
            this.comboBoxChip.SelectedIndexChanged += new System.EventHandler(this.comboBoxChip_SelectedIndexChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(12, 187);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(207, 25);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // OpenChipFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 224);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.comboBoxChip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxVendor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxArch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenChipFileDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Open Chip File";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OpenChipFileDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxArch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxVendor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxChip;
        private System.Windows.Forms.Button buttonOk;
    }
}
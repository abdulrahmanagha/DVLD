namespace DVLD.Licenses.Controls
{
    partial class ctrlDriverLicenses
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbDriverLicenses = new System.Windows.Forms.GroupBox();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tpLocalLicenses = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLocalNumberOfRecords = new System.Windows.Forms.Label();
            this.dgvLocalLicenses = new System.Windows.Forms.DataGridView();
            this.cmServiceLocalLicenses = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmShowLicenseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.tbInternationalLicenses = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInternationalNumberOfRecords = new System.Windows.Forms.Label();
            this.dgvInternationalList = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbDriverLicenses.SuspendLayout();
            this.tabCtrl.SuspendLayout();
            this.tpLocalLicenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenses)).BeginInit();
            this.cmServiceLocalLicenses.SuspendLayout();
            this.tbInternationalLicenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalList)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDriverLicenses
            // 
            this.gbDriverLicenses.Controls.Add(this.tabCtrl);
            this.gbDriverLicenses.Controls.Add(this.panel1);
            this.gbDriverLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDriverLicenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDriverLicenses.Location = new System.Drawing.Point(10, 10);
            this.gbDriverLicenses.Name = "gbDriverLicenses";
            this.gbDriverLicenses.Size = new System.Drawing.Size(1163, 394);
            this.gbDriverLicenses.TabIndex = 0;
            this.gbDriverLicenses.TabStop = false;
            this.gbDriverLicenses.Text = "Driver Licenses";
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tpLocalLicenses);
            this.tabCtrl.Controls.Add(this.tbInternationalLicenses);
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabCtrl.ItemSize = new System.Drawing.Size(30, 30);
            this.tabCtrl.Location = new System.Drawing.Point(3, 44);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.Padding = new System.Drawing.Point(30, 3);
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(1157, 347);
            this.tabCtrl.TabIndex = 0;
            // 
            // tpLocalLicenses
            // 
            this.tpLocalLicenses.Controls.Add(this.label3);
            this.tpLocalLicenses.Controls.Add(this.lblLocalNumberOfRecords);
            this.tpLocalLicenses.Controls.Add(this.dgvLocalLicenses);
            this.tpLocalLicenses.Controls.Add(this.label1);
            this.tpLocalLicenses.Location = new System.Drawing.Point(4, 34);
            this.tpLocalLicenses.Name = "tpLocalLicenses";
            this.tpLocalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocalLicenses.Size = new System.Drawing.Size(1149, 309);
            this.tpLocalLicenses.TabIndex = 0;
            this.tpLocalLicenses.Text = "Local";
            this.tpLocalLicenses.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Total Records:";
            // 
            // lblLocalNumberOfRecords
            // 
            this.lblLocalNumberOfRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocalNumberOfRecords.AutoSize = true;
            this.lblLocalNumberOfRecords.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalNumberOfRecords.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblLocalNumberOfRecords.Location = new System.Drawing.Point(139, 285);
            this.lblLocalNumberOfRecords.Name = "lblLocalNumberOfRecords";
            this.lblLocalNumberOfRecords.Size = new System.Drawing.Size(19, 20);
            this.lblLocalNumberOfRecords.TabIndex = 12;
            this.lblLocalNumberOfRecords.Text = "0";
            // 
            // dgvLocalLicenses
            // 
            this.dgvLocalLicenses.AllowUserToAddRows = false;
            this.dgvLocalLicenses.AllowUserToDeleteRows = false;
            this.dgvLocalLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicenses.ContextMenuStrip = this.cmServiceLocalLicenses;
            this.dgvLocalLicenses.Location = new System.Drawing.Point(3, 48);
            this.dgvLocalLicenses.Name = "dgvLocalLicenses";
            this.dgvLocalLicenses.ReadOnly = true;
            this.dgvLocalLicenses.RowHeadersWidth = 51;
            this.dgvLocalLicenses.RowTemplate.Height = 24;
            this.dgvLocalLicenses.Size = new System.Drawing.Size(1140, 230);
            this.dgvLocalLicenses.TabIndex = 1;
            // 
            // cmServiceLocalLicenses
            // 
            this.cmServiceLocalLicenses.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmServiceLocalLicenses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowLicenseInfo});
            this.cmServiceLocalLicenses.Name = "contextMenuStrip1";
            this.cmServiceLocalLicenses.Size = new System.Drawing.Size(213, 42);
            // 
            // tsmShowLicenseInfo
            // 
            this.tsmShowLicenseInfo.Image = global::DVLD.Properties.Resources.License_View_32;
            this.tsmShowLicenseInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowLicenseInfo.Name = "tsmShowLicenseInfo";
            this.tsmShowLicenseInfo.Size = new System.Drawing.Size(212, 38);
            this.tsmShowLicenseInfo.Text = "Show License Info";
            this.tsmShowLicenseInfo.Click += new System.EventHandler(this.tsmShowLicenseInfo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local Licenses History";
            // 
            // tbInternationalLicenses
            // 
            this.tbInternationalLicenses.Controls.Add(this.label2);
            this.tbInternationalLicenses.Controls.Add(this.lblInternationalNumberOfRecords);
            this.tbInternationalLicenses.Controls.Add(this.dgvInternationalList);
            this.tbInternationalLicenses.Controls.Add(this.label5);
            this.tbInternationalLicenses.Location = new System.Drawing.Point(4, 34);
            this.tbInternationalLicenses.Name = "tbInternationalLicenses";
            this.tbInternationalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tbInternationalLicenses.Size = new System.Drawing.Size(1149, 309);
            this.tbInternationalLicenses.TabIndex = 1;
            this.tbInternationalLicenses.Text = "International";
            this.tbInternationalLicenses.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Total Records:";
            // 
            // lblInternationalNumberOfRecords
            // 
            this.lblInternationalNumberOfRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInternationalNumberOfRecords.AutoSize = true;
            this.lblInternationalNumberOfRecords.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternationalNumberOfRecords.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblInternationalNumberOfRecords.Location = new System.Drawing.Point(139, 285);
            this.lblInternationalNumberOfRecords.Name = "lblInternationalNumberOfRecords";
            this.lblInternationalNumberOfRecords.Size = new System.Drawing.Size(19, 20);
            this.lblInternationalNumberOfRecords.TabIndex = 16;
            this.lblInternationalNumberOfRecords.Text = "0";
            // 
            // dgvInternationalList
            // 
            this.dgvInternationalList.AllowUserToAddRows = false;
            this.dgvInternationalList.AllowUserToDeleteRows = false;
            this.dgvInternationalList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalList.ContextMenuStrip = this.cmServiceLocalLicenses;
            this.dgvInternationalList.Location = new System.Drawing.Point(3, 48);
            this.dgvInternationalList.Name = "dgvInternationalList";
            this.dgvInternationalList.ReadOnly = true;
            this.dgvInternationalList.RowHeadersWidth = 51;
            this.dgvInternationalList.RowTemplate.Height = 24;
            this.dgvInternationalList.Size = new System.Drawing.Size(1140, 230);
            this.dgvInternationalList.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(231, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "International Licenses History";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1157, 24);
            this.panel1.TabIndex = 0;
            // 
            // ctrlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbDriverLicenses);
            this.Name = "ctrlDriverLicenses";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(1183, 414);
            this.gbDriverLicenses.ResumeLayout(false);
            this.tabCtrl.ResumeLayout(false);
            this.tpLocalLicenses.ResumeLayout(false);
            this.tpLocalLicenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenses)).EndInit();
            this.cmServiceLocalLicenses.ResumeLayout(false);
            this.tbInternationalLicenses.ResumeLayout(false);
            this.tbInternationalLicenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDriverLicenses;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tpLocalLicenses;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tbInternationalLicenses;
        private System.Windows.Forms.DataGridView dgvLocalLicenses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLocalNumberOfRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInternationalNumberOfRecords;
        private System.Windows.Forms.DataGridView dgvInternationalList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip cmServiceLocalLicenses;
        private System.Windows.Forms.ToolStripMenuItem tsmShowLicenseInfo;
    }
}

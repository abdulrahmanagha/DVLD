namespace DVLD.Applications.DetainAndReleaseLicenses
{
    partial class frmListDetainedLicenses
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnReleaseLicense = new System.Windows.Forms.Button();
            this.cbApplicationStatus = new System.Windows.Forms.ComboBox();
            this.btnDetainLicense = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDetainedLicenseList = new System.Windows.Forms.DataGridView();
            this.cmServiceDetainLincense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmShowPersonDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmReleaseDetainedLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenseList)).BeginInit();
            this.cmServiceDetainLincense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(20, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1509, 73);
            this.label1.TabIndex = 9;
            this.label1.Text = "List Detained Licenses";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblNumberOfRecords);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 769);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1509, 76);
            this.panel2.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Total Records:";
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.lblNumberOfRecords.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNumberOfRecords.Location = new System.Drawing.Point(162, 53);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(22, 23);
            this.lblNumberOfRecords.TabIndex = 8;
            this.lblNumberOfRecords.Text = "0";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.Location = new System.Drawing.Point(1364, 27);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(145, 49);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnReleaseLicense);
            this.panel3.Controls.Add(this.cbApplicationStatus);
            this.panel3.Controls.Add(this.btnDetainLicense);
            this.panel3.Controls.Add(this.txtFilter);
            this.panel3.Controls.Add(this.cbFilterBy);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(20, 309);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1509, 74);
            this.panel3.TabIndex = 13;
            // 
            // btnReleaseLicense
            // 
            this.btnReleaseLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReleaseLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReleaseLicense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Tan;
            this.btnReleaseLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReleaseLicense.Image = global::DVLD.Properties.Resources.Release_Detained_License_40;
            this.btnReleaseLicense.Location = new System.Drawing.Point(1324, 7);
            this.btnReleaseLicense.Name = "btnReleaseLicense";
            this.btnReleaseLicense.Size = new System.Drawing.Size(74, 54);
            this.btnReleaseLicense.TabIndex = 7;
            this.btnReleaseLicense.UseVisualStyleBackColor = true;
            this.btnReleaseLicense.Click += new System.EventHandler(this.btnReleaseLicense_Click);
            // 
            // cbApplicationStatus
            // 
            this.cbApplicationStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbApplicationStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbApplicationStatus.FormattingEnabled = true;
            this.cbApplicationStatus.Location = new System.Drawing.Point(378, 19);
            this.cbApplicationStatus.Name = "cbApplicationStatus";
            this.cbApplicationStatus.Size = new System.Drawing.Size(151, 28);
            this.cbApplicationStatus.TabIndex = 6;
            this.cbApplicationStatus.SelectedIndexChanged += new System.EventHandler(this.cbApplicationStatus_SelectedIndexChanged);
            // 
            // btnDetainLicense
            // 
            this.btnDetainLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetainLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetainLicense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Tan;
            this.btnDetainLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetainLicense.Image = global::DVLD.Properties.Resources.Detain_40;
            this.btnDetainLicense.Location = new System.Drawing.Point(1432, 7);
            this.btnDetainLicense.Name = "btnDetainLicense";
            this.btnDetainLicense.Size = new System.Drawing.Size(74, 54);
            this.btnDetainLicense.TabIndex = 3;
            this.btnDetainLicense.UseVisualStyleBackColor = true;
            this.btnDetainLicense.Click += new System.EventHandler(this.btnDetainLicense_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.AcceptsReturn = true;
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(378, 20);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(198, 27);
            this.txtFilter.TabIndex = 2;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(123, 19);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(223, 28);
            this.cbFilterBy.TabIndex = 0;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filter By";
            // 
            // dgvDetainedLicenseList
            // 
            this.dgvDetainedLicenseList.AllowUserToAddRows = false;
            this.dgvDetainedLicenseList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetainedLicenseList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetainedLicenseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedLicenseList.ContextMenuStrip = this.cmServiceDetainLincense;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetainedLicenseList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetainedLicenseList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDetainedLicenseList.Location = new System.Drawing.Point(20, 383);
            this.dgvDetainedLicenseList.MultiSelect = false;
            this.dgvDetainedLicenseList.Name = "dgvDetainedLicenseList";
            this.dgvDetainedLicenseList.ReadOnly = true;
            this.dgvDetainedLicenseList.RowHeadersWidth = 51;
            this.dgvDetainedLicenseList.RowTemplate.Height = 24;
            this.dgvDetainedLicenseList.Size = new System.Drawing.Size(1509, 386);
            this.dgvDetainedLicenseList.TabIndex = 12;
            // 
            // cmServiceDetainLincense
            // 
            this.cmServiceDetainLincense.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmServiceDetainLincense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowPersonDetails,
            this.tsmShowLicense,
            this.tsmShowPersonLicenseHistory,
            this.toolStripMenuItem1,
            this.tsmReleaseDetainedLicense});
            this.cmServiceDetainLincense.Name = "contextMenuStrip1";
            this.cmServiceDetainLincense.Size = new System.Drawing.Size(281, 162);
            this.cmServiceDetainLincense.Opening += new System.ComponentModel.CancelEventHandler(this.cmServiceDetainLincense_Opening);
            // 
            // tsmShowPersonDetails
            // 
            this.tsmShowPersonDetails.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.tsmShowPersonDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowPersonDetails.Name = "tsmShowPersonDetails";
            this.tsmShowPersonDetails.Size = new System.Drawing.Size(280, 38);
            this.tsmShowPersonDetails.Text = "Show Person Details";
            this.tsmShowPersonDetails.Click += new System.EventHandler(this.tsmShowPersonDetails_Click);
            // 
            // tsmShowLicense
            // 
            this.tsmShowLicense.Image = global::DVLD.Properties.Resources.License_View_32;
            this.tsmShowLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowLicense.Name = "tsmShowLicense";
            this.tsmShowLicense.Size = new System.Drawing.Size(280, 38);
            this.tsmShowLicense.Text = "Show License";
            this.tsmShowLicense.Click += new System.EventHandler(this.tsmShowLicense_Click);
            // 
            // tsmShowPersonLicenseHistory
            // 
            this.tsmShowPersonLicenseHistory.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.tsmShowPersonLicenseHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowPersonLicenseHistory.Name = "tsmShowPersonLicenseHistory";
            this.tsmShowPersonLicenseHistory.Size = new System.Drawing.Size(280, 38);
            this.tsmShowPersonLicenseHistory.Text = "Show Person License History";
            this.tsmShowPersonLicenseHistory.Click += new System.EventHandler(this.tsmShowPersonLicenseHistory_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(277, 6);
            // 
            // tsmReleaseDetainedLicense
            // 
            this.tsmReleaseDetainedLicense.Image = global::DVLD.Properties.Resources.Release_Detained_License_32;
            this.tsmReleaseDetainedLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmReleaseDetainedLicense.Name = "tsmReleaseDetainedLicense";
            this.tsmReleaseDetainedLicense.Size = new System.Drawing.Size(280, 38);
            this.tsmReleaseDetainedLicense.Text = "Release Detained License";
            this.tsmReleaseDetainedLicense.Click += new System.EventHandler(this.tsmReleaseDetainedLicense_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Detain_512;
            this.pictureBox1.Location = new System.Drawing.Point(20, 40);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(10, 13, 13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pictureBox1.Size = new System.Drawing.Size(1509, 171);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // frmListDetainedLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1549, 885);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dgvDetainedLicenseList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmListDetainedLicenses";
            this.Padding = new System.Windows.Forms.Padding(20, 40, 20, 40);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "List Detained Licenses";
            this.Load += new System.EventHandler(this.frmListDetained_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenseList)).EndInit();
            this.cmServiceDetainLincense.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbApplicationStatus;
        private System.Windows.Forms.Button btnDetainLicense;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDetainedLicenseList;
        private System.Windows.Forms.Button btnReleaseLicense;
        private System.Windows.Forms.ContextMenuStrip cmServiceDetainLincense;
        private System.Windows.Forms.ToolStripMenuItem tsmShowPersonDetails;
        private System.Windows.Forms.ToolStripMenuItem tsmShowLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmShowPersonLicenseHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmReleaseDetainedLicense;
    }
}
namespace DVLD.LocalDrivingLicenseApplications
{
    partial class frmListLocalDrivingLicenseApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListLocalDrivingLicenseApplication));
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbApplicationStatus = new System.Windows.Forms.ComboBox();
            this.btnAddNewLocalLicenseApplication = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLocalLicenseApplicationList = new System.Windows.Forms.DataGridView();
            this.cmServiceLocalLincese = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmShowApplicationDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmEditApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmCancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmScheduleTests = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScheduleVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScheduleWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScheduleStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmIssueDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenseApplicationList)).BeginInit();
            this.cmServiceLocalLincese.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(1410, 73);
            this.label1.TabIndex = 7;
            this.label1.Text = "Local Driving License Application";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbApplicationStatus);
            this.panel3.Controls.Add(this.btnAddNewLocalLicenseApplication);
            this.panel3.Controls.Add(this.txtFilter);
            this.panel3.Controls.Add(this.cbFilterBy);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(20, 309);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1410, 74);
            this.panel3.TabIndex = 11;
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
            // btnAddNewLocalLicenseApplication
            // 
            this.btnAddNewLocalLicenseApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewLocalLicenseApplication.Image = global::DVLD.Properties.Resources.New_Application_40;
            this.btnAddNewLocalLicenseApplication.Location = new System.Drawing.Point(1322, 6);
            this.btnAddNewLocalLicenseApplication.Name = "btnAddNewLocalLicenseApplication";
            this.btnAddNewLocalLicenseApplication.Size = new System.Drawing.Size(88, 54);
            this.btnAddNewLocalLicenseApplication.TabIndex = 3;
            this.btnAddNewLocalLicenseApplication.UseVisualStyleBackColor = true;
            this.btnAddNewLocalLicenseApplication.Click += new System.EventHandler(this.btnAddNewLocalLicenseApplication_Click);
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
            // dgvLocalLicenseApplicationList
            // 
            this.dgvLocalLicenseApplicationList.AllowUserToAddRows = false;
            this.dgvLocalLicenseApplicationList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalLicenseApplicationList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLocalLicenseApplicationList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicenseApplicationList.ContextMenuStrip = this.cmServiceLocalLincese;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLocalLicenseApplicationList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLocalLicenseApplicationList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvLocalLicenseApplicationList.Location = new System.Drawing.Point(20, 383);
            this.dgvLocalLicenseApplicationList.MultiSelect = false;
            this.dgvLocalLicenseApplicationList.Name = "dgvLocalLicenseApplicationList";
            this.dgvLocalLicenseApplicationList.ReadOnly = true;
            this.dgvLocalLicenseApplicationList.RowHeadersWidth = 51;
            this.dgvLocalLicenseApplicationList.RowTemplate.Height = 24;
            this.dgvLocalLicenseApplicationList.Size = new System.Drawing.Size(1410, 386);
            this.dgvLocalLicenseApplicationList.TabIndex = 10;
            // 
            // cmServiceLocalLincese
            // 
            this.cmServiceLocalLincese.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmServiceLocalLincese.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowApplicationDetails,
            this.toolStripMenuItem2,
            this.tsmEditApplication,
            this.tsmDeleteApplication,
            this.toolStripSeparator1,
            this.tsmCancelApplication,
            this.toolStripMenuItem1,
            this.tsmScheduleTests,
            this.toolStripSeparator2,
            this.tsmIssueDrivingLicense,
            this.toolStripMenuItem3,
            this.tsmShowLicense,
            this.toolStripMenuItem4,
            this.tsmShowPersonLicenseHistory});
            this.cmServiceLocalLincese.Name = "contextMenuStrip1";
            this.cmServiceLocalLincese.Size = new System.Drawing.Size(309, 344);
            this.cmServiceLocalLincese.Opening += new System.ComponentModel.CancelEventHandler(this.cmServiceLocalLincese_Opening);
            // 
            // tsmShowApplicationDetails
            // 
            this.tsmShowApplicationDetails.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.tsmShowApplicationDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowApplicationDetails.Name = "tsmShowApplicationDetails";
            this.tsmShowApplicationDetails.Size = new System.Drawing.Size(308, 38);
            this.tsmShowApplicationDetails.Text = "Show Application Details";
            this.tsmShowApplicationDetails.Click += new System.EventHandler(this.tsmShowApplicationDetails_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(305, 6);
            // 
            // tsmEditApplication
            // 
            this.tsmEditApplication.Image = global::DVLD.Properties.Resources.edit_32;
            this.tsmEditApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmEditApplication.Name = "tsmEditApplication";
            this.tsmEditApplication.Size = new System.Drawing.Size(308, 38);
            this.tsmEditApplication.Text = "Edit Application";
            this.tsmEditApplication.Click += new System.EventHandler(this.tsmEditApplication_Click);
            // 
            // tsmDeleteApplication
            // 
            this.tsmDeleteApplication.Image = global::DVLD.Properties.Resources.Delete_32_2;
            this.tsmDeleteApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmDeleteApplication.Name = "tsmDeleteApplication";
            this.tsmDeleteApplication.Size = new System.Drawing.Size(308, 38);
            this.tsmDeleteApplication.Text = "Delete Application";
            this.tsmDeleteApplication.Click += new System.EventHandler(this.tsmDeleteApplication_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(305, 6);
            // 
            // tsmCancelApplication
            // 
            this.tsmCancelApplication.Image = global::DVLD.Properties.Resources.Delete_32;
            this.tsmCancelApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmCancelApplication.Name = "tsmCancelApplication";
            this.tsmCancelApplication.Size = new System.Drawing.Size(308, 38);
            this.tsmCancelApplication.Text = "Cancel Application";
            this.tsmCancelApplication.Click += new System.EventHandler(this.tsmCancelApplication_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(305, 6);
            // 
            // tsmScheduleTests
            // 
            this.tsmScheduleTests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmScheduleVisionTest,
            this.tsmScheduleWrittenTest,
            this.tsmScheduleStreetTest});
            this.tsmScheduleTests.Image = global::DVLD.Properties.Resources.Schedule_Test_32;
            this.tsmScheduleTests.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmScheduleTests.Name = "tsmScheduleTests";
            this.tsmScheduleTests.Size = new System.Drawing.Size(308, 38);
            this.tsmScheduleTests.Text = "Schedule Tests";
            // 
            // tsmScheduleVisionTest
            // 
            this.tsmScheduleVisionTest.Image = global::DVLD.Properties.Resources.Vision_Test_32;
            this.tsmScheduleVisionTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmScheduleVisionTest.Name = "tsmScheduleVisionTest";
            this.tsmScheduleVisionTest.Size = new System.Drawing.Size(247, 38);
            this.tsmScheduleVisionTest.Text = "Schedule Vision Test";
            this.tsmScheduleVisionTest.Click += new System.EventHandler(this.tsmScheduleVisionTest_Click);
            // 
            // tsmScheduleWrittenTest
            // 
            this.tsmScheduleWrittenTest.Image = global::DVLD.Properties.Resources.Written_Test_32;
            this.tsmScheduleWrittenTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmScheduleWrittenTest.Name = "tsmScheduleWrittenTest";
            this.tsmScheduleWrittenTest.Size = new System.Drawing.Size(247, 38);
            this.tsmScheduleWrittenTest.Text = "Schedule Written Test";
            this.tsmScheduleWrittenTest.Click += new System.EventHandler(this.tsmScheduleWrittenTest_Click);
            // 
            // tsmScheduleStreetTest
            // 
            this.tsmScheduleStreetTest.Image = global::DVLD.Properties.Resources.Street_Test_32;
            this.tsmScheduleStreetTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmScheduleStreetTest.Name = "tsmScheduleStreetTest";
            this.tsmScheduleStreetTest.Size = new System.Drawing.Size(247, 38);
            this.tsmScheduleStreetTest.Text = "Schedule Street Test";
            this.tsmScheduleStreetTest.Click += new System.EventHandler(this.tsmScheduleStreetTest_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(305, 6);
            // 
            // tsmIssueDrivingLicense
            // 
            this.tsmIssueDrivingLicense.Image = global::DVLD.Properties.Resources.IssueDrivingLicense_32;
            this.tsmIssueDrivingLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmIssueDrivingLicense.Name = "tsmIssueDrivingLicense";
            this.tsmIssueDrivingLicense.Size = new System.Drawing.Size(308, 38);
            this.tsmIssueDrivingLicense.Text = "Issue Driving License (First Time)";
            this.tsmIssueDrivingLicense.Click += new System.EventHandler(this.tsmIssueDrivingLicense_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(305, 6);
            // 
            // tsmShowLicense
            // 
            this.tsmShowLicense.Image = global::DVLD.Properties.Resources.License_View_32;
            this.tsmShowLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowLicense.Name = "tsmShowLicense";
            this.tsmShowLicense.Size = new System.Drawing.Size(308, 38);
            this.tsmShowLicense.Text = "Show License";
            this.tsmShowLicense.Click += new System.EventHandler(this.tsmShowLicense_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(305, 6);
            // 
            // tsmShowPersonLicenseHistory
            // 
            this.tsmShowPersonLicenseHistory.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.tsmShowPersonLicenseHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowPersonLicenseHistory.Name = "tsmShowPersonLicenseHistory";
            this.tsmShowPersonLicenseHistory.Size = new System.Drawing.Size(308, 38);
            this.tsmShowPersonLicenseHistory.Text = "Show Person License History";
            this.tsmShowPersonLicenseHistory.Click += new System.EventHandler(this.tsmShowPersonLicenseHistory_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblNumberOfRecords);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 769);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1410, 76);
            this.panel2.TabIndex = 9;
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
            this.btnClose.Location = new System.Drawing.Point(1265, 27);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(145, 49);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Application_Types_512;
            this.pictureBox1.Location = new System.Drawing.Point(20, 40);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(10, 13, 13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pictureBox1.Size = new System.Drawing.Size(1410, 171);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // frmListLocalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1450, 885);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dgvLocalLicenseApplicationList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmListLocalDrivingLicenseApplication";
            this.Padding = new System.Windows.Forms.Padding(20, 40, 20, 40);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Local Driving License Application";
            this.Load += new System.EventHandler(this.frmListLocalDrivingLicenseApplication_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenseApplicationList)).EndInit();
            this.cmServiceLocalLincese.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnAddNewLocalLicenseApplication;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvLocalLicenseApplicationList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbApplicationStatus;
        private System.Windows.Forms.ContextMenuStrip cmServiceLocalLincese;
        private System.Windows.Forms.ToolStripMenuItem tsmShowApplicationDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmEditApplication;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteApplication;
        private System.Windows.Forms.ToolStripMenuItem tsmCancelApplication;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmScheduleTests;
        private System.Windows.Forms.ToolStripMenuItem tsmIssueDrivingLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmShowLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmShowPersonLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmScheduleVisionTest;
        private System.Windows.Forms.ToolStripMenuItem tsmScheduleWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem tsmScheduleStreetTest;
    }
}
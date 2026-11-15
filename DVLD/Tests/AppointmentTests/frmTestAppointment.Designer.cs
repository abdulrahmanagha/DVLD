namespace DVLD.AppointmentTests
{
    partial class frmTestAppointment
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvAppointmentsList = new System.Windows.Forms.DataGridView();
            this.cmServiceAppointments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmEditAppointment = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmTakeTest = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddNewAppointment = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlDrivingLicenseApplicationInfo1 = new DVLD.AppointmentTests.Controls.ctrlDrivingLicenseApplicationInfo();
            this.ctrlTestTitle1 = new DVLD.Tests.ctrlTestTitle();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsList)).BeginInit();
            this.cmServiceAppointments.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(946, 25);
            this.panel1.TabIndex = 12;
            // 
            // dgvAppointmentsList
            // 
            this.dgvAppointmentsList.AllowUserToAddRows = false;
            this.dgvAppointmentsList.AllowUserToDeleteRows = false;
            this.dgvAppointmentsList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAppointmentsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointmentsList.ContextMenuStrip = this.cmServiceAppointments;
            this.dgvAppointmentsList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvAppointmentsList.Location = new System.Drawing.Point(20, 639);
            this.dgvAppointmentsList.Name = "dgvAppointmentsList";
            this.dgvAppointmentsList.ReadOnly = true;
            this.dgvAppointmentsList.RowHeadersWidth = 51;
            this.dgvAppointmentsList.RowTemplate.Height = 24;
            this.dgvAppointmentsList.Size = new System.Drawing.Size(946, 166);
            this.dgvAppointmentsList.TabIndex = 25;
            // 
            // cmServiceAppointments
            // 
            this.cmServiceAppointments.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmServiceAppointments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEditAppointment,
            this.toolStripMenuItem2,
            this.tsmTakeTest});
            this.cmServiceAppointments.Name = "contextMenuStrip1";
            this.cmServiceAppointments.Size = new System.Drawing.Size(213, 86);
            // 
            // tsmEditAppointment
            // 
            this.tsmEditAppointment.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.tsmEditAppointment.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmEditAppointment.Name = "tsmEditAppointment";
            this.tsmEditAppointment.Size = new System.Drawing.Size(212, 38);
            this.tsmEditAppointment.Text = "Edit Appointment";
            this.tsmEditAppointment.Click += new System.EventHandler(this.tsmEditAppointment_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(209, 6);
            // 
            // tsmTakeTest
            // 
            this.tsmTakeTest.Image = global::DVLD.Properties.Resources.AddPerson_32;
            this.tsmTakeTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmTakeTest.Name = "tsmTakeTest";
            this.tsmTakeTest.Size = new System.Drawing.Size(212, 38);
            this.tsmTakeTest.Text = "Take Test";
            this.tsmTakeTest.Click += new System.EventHandler(this.tsmTakeTest_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblNumberOfRecords);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 805);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(946, 72);
            this.panel2.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 35);
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
            this.lblNumberOfRecords.Location = new System.Drawing.Point(162, 35);
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
            this.btnClose.Location = new System.Drawing.Point(801, 22);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(145, 49);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnAddNewAppointment
            // 
            this.btnAddNewAppointment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewAppointment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewAppointment.FlatAppearance.BorderSize = 0;
            this.btnAddNewAppointment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddNewAppointment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddNewAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewAppointment.Image = global::DVLD.Properties.Resources.my_add_Appointment_48;
            this.btnAddNewAppointment.Location = new System.Drawing.Point(905, 580);
            this.btnAddNewAppointment.Name = "btnAddNewAppointment";
            this.btnAddNewAppointment.Size = new System.Drawing.Size(53, 48);
            this.btnAddNewAppointment.TabIndex = 27;
            this.btnAddNewAppointment.UseVisualStyleBackColor = true;
            this.btnAddNewAppointment.Click += new System.EventHandler(this.btnAddNewAppointment_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 598);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "Appointments:";
            // 
            // ctrlDrivingLicenseApplicationInfo1
            // 
            this.ctrlDrivingLicenseApplicationInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlDrivingLicenseApplicationInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(20, 147);
            this.ctrlDrivingLicenseApplicationInfo1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlDrivingLicenseApplicationInfo1.Name = "ctrlDrivingLicenseApplicationInfo1";
            this.ctrlDrivingLicenseApplicationInfo1.SetPassedTests = 0;
            this.ctrlDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(946, 419);
            this.ctrlDrivingLicenseApplicationInfo1.TabIndex = 22;
            // 
            // ctrlTestTitle1
            // 
            this.ctrlTestTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlTestTitle1.Location = new System.Drawing.Point(20, 10);
            this.ctrlTestTitle1.Name = "ctrlTestTitle1";
            this.ctrlTestTitle1.Size = new System.Drawing.Size(946, 112);
            this.ctrlTestTitle1.TabIndex = 0;
            this.ctrlTestTitle1.Title = "Schedule Test";
            // 
            // frmTestAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(986, 897);
            this.Controls.Add(this.btnAddNewAppointment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvAppointmentsList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ctrlDrivingLicenseApplicationInfo1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctrlTestTitle1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmTestAppointment";
            this.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vision Test Appointment";
            this.Load += new System.EventHandler(this.frmVisionTestAppointment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentsList)).EndInit();
            this.cmServiceAppointments.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tests.ctrlTestTitle ctrlTestTitle1;
        private System.Windows.Forms.Panel panel1;
        private Controls.ctrlDrivingLicenseApplicationInfo ctrlDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.DataGridView dgvAppointmentsList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddNewAppointment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmServiceAppointments;
        private System.Windows.Forms.ToolStripMenuItem tsmEditAppointment;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmTakeTest;
    }
}
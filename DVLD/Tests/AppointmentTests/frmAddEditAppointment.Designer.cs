namespace DVLD.Tests
{
    partial class frmAddEditAppointment
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLocked = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbRetakeApplication = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblRetakeApplicationId = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.lblTotalFees = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.lblRetakeAppFees = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ctrlScheduledTest1 = new DVLD.Tests.Controls.ctrlScheduledTest();
            this.ctrlTestTitle1 = new DVLD.Tests.ctrlTestTitle();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbRetakeApplication.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblLocked);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 166);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 46);
            this.panel1.TabIndex = 27;
            // 
            // lblLocked
            // 
            this.lblLocked.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocked.ForeColor = System.Drawing.Color.Red;
            this.lblLocked.Location = new System.Drawing.Point(0, 0);
            this.lblLocked.Name = "lblLocked";
            this.lblLocked.Size = new System.Drawing.Size(711, 46);
            this.lblLocked.TabIndex = 168;
            this.lblLocked.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(20, 724);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(711, 88);
            this.panel3.TabIndex = 31;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DVLD.Properties.Resources.Save_32;
            this.btnSave.Location = new System.Drawing.Point(547, 26);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 49);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.Location = new System.Drawing.Point(357, 26);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(145, 49);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 546);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(711, 26);
            this.panel2.TabIndex = 33;
            // 
            // gbRetakeApplication
            // 
            this.gbRetakeApplication.Controls.Add(this.pictureBox1);
            this.gbRetakeApplication.Controls.Add(this.lblRetakeApplicationId);
            this.gbRetakeApplication.Controls.Add(this.label12);
            this.gbRetakeApplication.Controls.Add(this.pictureBox8);
            this.gbRetakeApplication.Controls.Add(this.lblTotalFees);
            this.gbRetakeApplication.Controls.Add(this.label10);
            this.gbRetakeApplication.Controls.Add(this.pictureBox12);
            this.gbRetakeApplication.Controls.Add(this.lblRetakeAppFees);
            this.gbRetakeApplication.Controls.Add(this.label7);
            this.gbRetakeApplication.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRetakeApplication.Enabled = false;
            this.gbRetakeApplication.Location = new System.Drawing.Point(20, 572);
            this.gbRetakeApplication.Name = "gbRetakeApplication";
            this.gbRetakeApplication.Size = new System.Drawing.Size(711, 142);
            this.gbRetakeApplication.TabIndex = 34;
            this.gbRetakeApplication.TabStop = false;
            this.gbRetakeApplication.Text = "Retake Test Info";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Number_32;
            this.pictureBox1.Location = new System.Drawing.Point(174, 90);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 152;
            this.pictureBox1.TabStop = false;
            // 
            // lblRetakeApplicationId
            // 
            this.lblRetakeApplicationId.AutoSize = true;
            this.lblRetakeApplicationId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetakeApplicationId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblRetakeApplicationId.Location = new System.Drawing.Point(221, 95);
            this.lblRetakeApplicationId.Name = "lblRetakeApplicationId";
            this.lblRetakeApplicationId.Size = new System.Drawing.Size(40, 20);
            this.lblRetakeApplicationId.TabIndex = 151;
            this.lblRetakeApplicationId.Text = "N\\A";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(27, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 20);
            this.label12.TabIndex = 150;
            this.label12.Text = "R.Test.App. ID";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD.Properties.Resources.money_32;
            this.pictureBox8.Location = new System.Drawing.Point(534, 38);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(36, 30);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 149;
            this.pictureBox8.TabStop = false;
            // 
            // lblTotalFees
            // 
            this.lblTotalFees.AutoSize = true;
            this.lblTotalFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFees.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTotalFees.Location = new System.Drawing.Point(581, 43);
            this.lblTotalFees.Name = "lblTotalFees";
            this.lblTotalFees.Size = new System.Drawing.Size(37, 20);
            this.lblTotalFees.TabIndex = 148;
            this.lblTotalFees.Text = "N\\A";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(420, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 20);
            this.label10.TabIndex = 147;
            this.label10.Text = "Total Fees";
            // 
            // pictureBox12
            // 
            this.pictureBox12.Image = global::DVLD.Properties.Resources.money_32;
            this.pictureBox12.Location = new System.Drawing.Point(174, 38);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(36, 30);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 146;
            this.pictureBox12.TabStop = false;
            // 
            // lblRetakeAppFees
            // 
            this.lblRetakeAppFees.AutoSize = true;
            this.lblRetakeAppFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetakeAppFees.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRetakeAppFees.Location = new System.Drawing.Point(221, 43);
            this.lblRetakeAppFees.Name = "lblRetakeAppFees";
            this.lblRetakeAppFees.Size = new System.Drawing.Size(37, 20);
            this.lblRetakeAppFees.TabIndex = 76;
            this.lblRetakeAppFees.Text = "N\\A";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(47, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "R.App. Fees";
            // 
            // ctrlScheduledTest1
            // 
            this.ctrlScheduledTest1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlScheduledTest1.Location = new System.Drawing.Point(20, 212);
            this.ctrlScheduledTest1.Name = "ctrlScheduledTest1";
            this.ctrlScheduledTest1.Padding = new System.Windows.Forms.Padding(10);
            this.ctrlScheduledTest1.Size = new System.Drawing.Size(711, 334);
            this.ctrlScheduledTest1.TabIndex = 32;
            // 
            // ctrlTestTitle1
            // 
            this.ctrlTestTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlTestTitle1.Location = new System.Drawing.Point(20, 10);
            this.ctrlTestTitle1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlTestTitle1.Name = "ctrlTestTitle1";
            this.ctrlTestTitle1.Size = new System.Drawing.Size(711, 156);
            this.ctrlTestTitle1.TabIndex = 0;
            this.ctrlTestTitle1.Title = "Schedule Test";
            // 
            // frmAddEditAppointment
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(751, 822);
            this.Controls.Add(this.gbRetakeApplication);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ctrlScheduledTest1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctrlTestTitle1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmAddEditAppointment";
            this.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Take Test";
            this.Load += new System.EventHandler(this.frmScheduleTests_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.gbRetakeApplication.ResumeLayout(false);
            this.gbRetakeApplication.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlTestTitle ctrlTestTitle1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private Controls.ctrlScheduledTest ctrlScheduledTest1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbRetakeApplication;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblRetakeApplicationId;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.Label lblRetakeAppFees;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblLocked;
    }
}
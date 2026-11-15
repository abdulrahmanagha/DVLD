namespace DVLD.People
{
    partial class frmFindPerson
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
            this.lblTitleForm = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlPersonCardWithFilter1 = new DVLD.People.Controls.ctrlPersonCardWithFilter();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitleForm
            // 
            this.lblTitleForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleForm.Font = new System.Drawing.Font("Arial Rounded MT Bold", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleForm.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTitleForm.Location = new System.Drawing.Point(20, 20);
            this.lblTitleForm.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblTitleForm.Name = "lblTitleForm";
            this.lblTitleForm.Padding = new System.Windows.Forms.Padding(15);
            this.lblTitleForm.Size = new System.Drawing.Size(1159, 112);
            this.lblTitleForm.TabIndex = 5;
            this.lblTitleForm.Text = "Find Person";
            this.lblTitleForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 584);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1159, 76);
            this.panel2.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.Location = new System.Drawing.Point(994, 17);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(145, 49);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.CausesValidation = false;
            this.ctrlPersonCardWithFilter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlPersonCardWithFilter1.FilterEnable = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(20, 134);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.Padding = new System.Windows.Forms.Padding(10);
            this.ctrlPersonCardWithFilter1.ShowAddPerson = true;
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(1159, 450);
            this.ctrlPersonCardWithFilter1.TabIndex = 7;
            // 
            // frmFindPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1199, 680);
            this.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblTitleForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmFindPerson";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find Person";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFindPerson_FormClosed);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblTitleForm;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
    }
}
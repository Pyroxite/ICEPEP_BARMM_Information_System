namespace InformationManagementSystem
{
    partial class FrmReportsViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportsViewer));
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.StudentsReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel4.Controls.Add(this.lblReportTitle);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1062, 23);
            this.panel4.TabIndex = 157;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.AutoSize = true;
            this.lblReportTitle.ForeColor = System.Drawing.Color.White;
            this.lblReportTitle.Location = new System.Drawing.Point(450, 5);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(95, 13);
            this.lblReportTitle.TabIndex = 5;
            this.lblReportTitle.Text = "PRINT REPORTS";
            // 
            // StudentsReportViewer
            // 
            this.StudentsReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StudentsReportViewer.LocalReport.ReportEmbeddedResource = "InformationManagementSystem.Reports.ReportStudents.rdlc";
            this.StudentsReportViewer.Location = new System.Drawing.Point(0, 23);
            this.StudentsReportViewer.Name = "StudentsReportViewer";
            this.StudentsReportViewer.ServerReport.BearerToken = null;
            this.StudentsReportViewer.Size = new System.Drawing.Size(1062, 686);
            this.StudentsReportViewer.TabIndex = 158;
            // 
            // FrmReportsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 709);
            this.Controls.Add(this.StudentsReportViewer);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReportsViewer";
            this.Text = "Report Viewer";
            this.Load += new System.EventHandler(this.FrmReportsViewer_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblReportTitle;
        private Microsoft.Reporting.WinForms.ReportViewer StudentsReportViewer;
    }
}
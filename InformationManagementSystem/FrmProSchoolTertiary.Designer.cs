
namespace InformationManagementSystem
{
    partial class FrmProSchoolTertiary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProSchoolTertiary));
            this.btnCurrentAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxCurrentSchoolAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxCurrentSchool = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCurrentAdd
            // 
            this.btnCurrentAdd.Location = new System.Drawing.Point(175, 127);
            this.btnCurrentAdd.Name = "btnCurrentAdd";
            this.btnCurrentAdd.Size = new System.Drawing.Size(75, 23);
            this.btnCurrentAdd.TabIndex = 21;
            this.btnCurrentAdd.Text = "Add";
            this.btnCurrentAdd.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Degree:";
            // 
            // tbxCurrentSchoolAddress
            // 
            this.tbxCurrentSchoolAddress.Location = new System.Drawing.Point(29, 97);
            this.tbxCurrentSchoolAddress.Name = "tbxCurrentSchoolAddress";
            this.tbxCurrentSchoolAddress.Size = new System.Drawing.Size(221, 22);
            this.tbxCurrentSchoolAddress.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "School Name:";
            // 
            // tbxCurrentSchool
            // 
            this.tbxCurrentSchool.Location = new System.Drawing.Point(29, 46);
            this.tbxCurrentSchool.Name = "tbxCurrentSchool";
            this.tbxCurrentSchool.Size = new System.Drawing.Size(221, 22);
            this.tbxCurrentSchool.TabIndex = 17;
            // 
            // FrmProSchoolTertiary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 176);
            this.Controls.Add(this.btnCurrentAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxCurrentSchoolAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxCurrentSchool);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProSchoolTertiary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "School Tertiary and Degree";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCurrentAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxCurrentSchoolAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxCurrentSchool;
    }
}
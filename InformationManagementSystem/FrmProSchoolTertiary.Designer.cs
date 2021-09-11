
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
            this.btnTertiaryAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxTertiarySchool = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTertiaryAdd
            // 
            this.btnTertiaryAdd.Location = new System.Drawing.Point(175, 88);
            this.btnTertiaryAdd.Name = "btnTertiaryAdd";
            this.btnTertiaryAdd.Size = new System.Drawing.Size(75, 23);
            this.btnTertiaryAdd.TabIndex = 21;
            this.btnTertiaryAdd.Text = "Add";
            this.btnTertiaryAdd.UseVisualStyleBackColor = true;
            this.btnTertiaryAdd.Click += new System.EventHandler(this.BtnCurrentAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "School Name (Required):";
            // 
            // tbxTertiarySchool
            // 
            this.tbxTertiarySchool.Location = new System.Drawing.Point(29, 46);
            this.tbxTertiarySchool.Name = "tbxTertiarySchool";
            this.tbxTertiarySchool.Size = new System.Drawing.Size(221, 22);
            this.tbxTertiarySchool.TabIndex = 17;
            // 
            // FrmProSchoolTertiary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 140);
            this.Controls.Add(this.btnTertiaryAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxTertiarySchool);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProSchoolTertiary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New School ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTertiaryAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxTertiarySchool;
    }
}
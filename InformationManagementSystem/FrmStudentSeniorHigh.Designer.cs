
namespace InformationManagementSystem
{
    partial class FrmStudentSeniorHigh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStudentSeniorHigh));
            this.btnCurrentAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxSeniorHighSchool = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCurrentAdd
            // 
            this.btnCurrentAdd.Location = new System.Drawing.Point(176, 79);
            this.btnCurrentAdd.Name = "btnCurrentAdd";
            this.btnCurrentAdd.Size = new System.Drawing.Size(75, 23);
            this.btnCurrentAdd.TabIndex = 21;
            this.btnCurrentAdd.Text = "Add";
            this.btnCurrentAdd.UseVisualStyleBackColor = true;
            this.btnCurrentAdd.Click += new System.EventHandler(this.btnCurrentAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Senior High School:";
            // 
            // tbxSeniorHighSchool
            // 
            this.tbxSeniorHighSchool.Location = new System.Drawing.Point(30, 39);
            this.tbxSeniorHighSchool.Name = "tbxSeniorHighSchool";
            this.tbxSeniorHighSchool.Size = new System.Drawing.Size(221, 22);
            this.tbxSeniorHighSchool.TabIndex = 17;
            // 
            // FrmSeniorHigh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 124);
            this.Controls.Add(this.btnCurrentAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxSeniorHighSchool);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSeniorHigh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Senior High";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCurrentAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxSeniorHighSchool;
    }
}
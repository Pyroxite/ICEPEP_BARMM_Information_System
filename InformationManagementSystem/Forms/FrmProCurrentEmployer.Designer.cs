
namespace InformationManagementSystem
{
    partial class FrmProCurrentEmployer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProCurrentEmployer));
            this.tbxProCurrentEmployer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProCEAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxProCurrentAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbxProCurrentEmployer
            // 
            this.tbxProCurrentEmployer.Location = new System.Drawing.Point(23, 42);
            this.tbxProCurrentEmployer.Name = "tbxProCurrentEmployer";
            this.tbxProCurrentEmployer.Size = new System.Drawing.Size(223, 22);
            this.tbxProCurrentEmployer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 157;
            this.label1.Text = "Current Employer (Required):";
            // 
            // btnProCEAdd
            // 
            this.btnProCEAdd.Location = new System.Drawing.Point(181, 128);
            this.btnProCEAdd.Name = "btnProCEAdd";
            this.btnProCEAdd.Size = new System.Drawing.Size(65, 23);
            this.btnProCEAdd.TabIndex = 158;
            this.btnProCEAdd.Text = "Add";
            this.btnProCEAdd.UseVisualStyleBackColor = true;
            this.btnProCEAdd.Click += new System.EventHandler(this.BtnProCEAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 162;
            this.label3.Text = "Employee Address (Required):";
            // 
            // tbxProCurrentAddress
            // 
            this.tbxProCurrentAddress.Location = new System.Drawing.Point(23, 95);
            this.tbxProCurrentAddress.Name = "tbxProCurrentAddress";
            this.tbxProCurrentAddress.Size = new System.Drawing.Size(223, 22);
            this.tbxProCurrentAddress.TabIndex = 161;
            // 
            // FrmProCurrentEmployer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 177);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxProCurrentAddress);
            this.Controls.Add(this.btnProCEAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxProCurrentEmployer);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProCurrentEmployer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Employee Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxProCurrentEmployer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProCEAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxProCurrentAddress;
    }
}
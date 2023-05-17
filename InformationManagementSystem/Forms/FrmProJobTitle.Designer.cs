namespace InformationManagementSystem
{
    partial class FrmProJobTitle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProJobTitle));
            this.label1 = new System.Windows.Forms.Label();
            this.tbxProJobTitle = new System.Windows.Forms.TextBox();
            this.btnProJobTitleAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 159;
            this.label1.Text = "Job Title:";
            // 
            // tbxProJobTitle
            // 
            this.tbxProJobTitle.Location = new System.Drawing.Point(22, 38);
            this.tbxProJobTitle.Name = "tbxProJobTitle";
            this.tbxProJobTitle.Size = new System.Drawing.Size(187, 22);
            this.tbxProJobTitle.TabIndex = 158;
            // 
            // btnProJobTitleAdd
            // 
            this.btnProJobTitleAdd.Location = new System.Drawing.Point(144, 72);
            this.btnProJobTitleAdd.Name = "btnProJobTitleAdd";
            this.btnProJobTitleAdd.Size = new System.Drawing.Size(65, 23);
            this.btnProJobTitleAdd.TabIndex = 160;
            this.btnProJobTitleAdd.Text = "Add";
            this.btnProJobTitleAdd.UseVisualStyleBackColor = true;
            this.btnProJobTitleAdd.Click += new System.EventHandler(this.BtnProJobTitleAdd_Click);
            // 
            // FrmProJobTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 127);
            this.Controls.Add(this.btnProJobTitleAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxProJobTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProJobTitle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Job Title";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxProJobTitle;
        private System.Windows.Forms.Button btnProJobTitleAdd;
    }
}
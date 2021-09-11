namespace InformationManagementSystem
{
    partial class FrmProTertiary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProTertiary));
            this.btnProTertiaryAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxProTertiary = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnProTertiaryAdd
            // 
            this.btnProTertiaryAdd.Location = new System.Drawing.Point(147, 74);
            this.btnProTertiaryAdd.Name = "btnProTertiaryAdd";
            this.btnProTertiaryAdd.Size = new System.Drawing.Size(65, 23);
            this.btnProTertiaryAdd.TabIndex = 163;
            this.btnProTertiaryAdd.Text = "Add";
            this.btnProTertiaryAdd.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 162;
            this.label1.Text = "School (Tertiary):";
            // 
            // tbxProTertiary
            // 
            this.tbxProTertiary.Location = new System.Drawing.Point(25, 40);
            this.tbxProTertiary.Name = "tbxProTertiary";
            this.tbxProTertiary.Size = new System.Drawing.Size(187, 22);
            this.tbxProTertiary.TabIndex = 161;
            // 
            // FrmProTertiary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 129);
            this.Controls.Add(this.btnProTertiaryAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxProTertiary);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProTertiary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New School (Tertiary)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProTertiaryAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxProTertiary;
    }
}
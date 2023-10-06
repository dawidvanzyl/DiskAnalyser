namespace DiskAnalyser
{
    partial class analyse
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
            this.gbxAnalysis = new System.Windows.Forms.GroupBox();
            this.lblCurrentDirectory = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.panel1.SuspendLayout();
            this.gbxAnalysis.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.gbxAnalysis);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 109);
            this.panel1.TabIndex = 2;
            // 
            // gbxAnalysis
            // 
            this.gbxAnalysis.Controls.Add(this.lblCurrentDirectory);
            this.gbxAnalysis.Controls.Add(this.btnCancel);
            this.gbxAnalysis.Location = new System.Drawing.Point(3, -1);
            this.gbxAnalysis.Name = "gbxAnalysis";
            this.gbxAnalysis.Size = new System.Drawing.Size(992, 105);
            this.gbxAnalysis.TabIndex = 2;
            this.gbxAnalysis.TabStop = false;
            this.gbxAnalysis.Text = "Directory being analysed";
            // 
            // lblCurrentDirectory
            // 
            this.lblCurrentDirectory.Location = new System.Drawing.Point(6, 19);
            this.lblCurrentDirectory.Name = "lblCurrentDirectory";
            this.lblCurrentDirectory.Size = new System.Drawing.Size(980, 52);
            this.lblCurrentDirectory.TabIndex = 2;
            this.lblCurrentDirectory.Text = "label1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(819, 74);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(165, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel Analysis";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // analyse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1000, 109);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "analyse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "analyse";
            this.Load += new System.EventHandler(this.analyse_Load);
            this.Shown += new System.EventHandler(this.analyse_Shown);
            this.panel1.ResumeLayout(false);
            this.gbxAnalysis.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbxAnalysis;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label lblCurrentDirectory;
    }
}
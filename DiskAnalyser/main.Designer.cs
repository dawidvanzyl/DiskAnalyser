namespace DiskAnalyser
{
    partial class main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbDrives = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAnalyseDrive = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbxSnapshot = new System.Windows.Forms.GroupBox();
            this.tvTreeView = new System.Windows.Forms.TreeView();
            this.toolStripSnapshot = new System.Windows.Forms.ToolStrip();
            this.btnSaveSnapshot = new System.Windows.Forms.ToolStripButton();
            this.toolStripProcessing = new System.Windows.Forms.ToolStrip();
            this.lblProcessingDirectories = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pbProcessingDirectories = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblEstimatedTimeLeft = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbxSnapshot.SuspendLayout();
            this.toolStripSnapshot.SuspendLayout();
            this.toolStripProcessing.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbDrives,
            this.toolStripSeparator1,
            this.btnAnalyseDrive});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1057, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Drives";
            // 
            // cmbDrives
            // 
            this.cmbDrives.Name = "cmbDrives";
            this.cmbDrives.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAnalyseDrive
            // 
            this.btnAnalyseDrive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAnalyseDrive.Image = ((System.Drawing.Image)(resources.GetObject("btnAnalyseDrive.Image")));
            this.btnAnalyseDrive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAnalyseDrive.Name = "btnAnalyseDrive";
            this.btnAnalyseDrive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnAnalyseDrive.Size = new System.Drawing.Size(82, 22);
            this.btnAnalyseDrive.Text = "Analyse Drive";
            this.btnAnalyseDrive.Click += new System.EventHandler(this.btnAnalyseDrive_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbxSnapshot);
            this.panel1.Controls.Add(this.toolStripSnapshot);
            this.panel1.Controls.Add(this.toolStripProcessing);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1057, 563);
            this.panel1.TabIndex = 11;
            // 
            // gbxSnapshot
            // 
            this.gbxSnapshot.Controls.Add(this.tvTreeView);
            this.gbxSnapshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxSnapshot.Location = new System.Drawing.Point(0, 0);
            this.gbxSnapshot.Name = "gbxSnapshot";
            this.gbxSnapshot.Size = new System.Drawing.Size(1057, 513);
            this.gbxSnapshot.TabIndex = 3;
            this.gbxSnapshot.TabStop = false;
            this.gbxSnapshot.Text = "No Snapshot";
            // 
            // tvTreeView
            // 
            this.tvTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTreeView.Location = new System.Drawing.Point(3, 19);
            this.tvTreeView.Name = "tvTreeView";
            this.tvTreeView.Size = new System.Drawing.Size(1051, 491);
            this.tvTreeView.TabIndex = 0;
            // 
            // toolStripSnapshot
            // 
            this.toolStripSnapshot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripSnapshot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveSnapshot});
            this.toolStripSnapshot.Location = new System.Drawing.Point(0, 513);
            this.toolStripSnapshot.Name = "toolStripSnapshot";
            this.toolStripSnapshot.Size = new System.Drawing.Size(1057, 25);
            this.toolStripSnapshot.TabIndex = 2;
            this.toolStripSnapshot.Text = "toolStrip2";
            // 
            // btnSaveSnapshot
            // 
            this.btnSaveSnapshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSaveSnapshot.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveSnapshot.Image")));
            this.btnSaveSnapshot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveSnapshot.Name = "btnSaveSnapshot";
            this.btnSaveSnapshot.Size = new System.Drawing.Size(87, 22);
            this.btnSaveSnapshot.Text = "Save Snapshot";
            // 
            // toolStripProcessing
            // 
            this.toolStripProcessing.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripProcessing.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblProcessingDirectories,
            this.toolStripSeparator2,
            this.pbProcessingDirectories,
            this.toolStripSeparator3,
            this.lblEstimatedTimeLeft});
            this.toolStripProcessing.Location = new System.Drawing.Point(0, 538);
            this.toolStripProcessing.Name = "toolStripProcessing";
            this.toolStripProcessing.Size = new System.Drawing.Size(1057, 25);
            this.toolStripProcessing.TabIndex = 1;
            this.toolStripProcessing.Text = "toolStrip2";
            // 
            // lblProcessingDirectories
            // 
            this.lblProcessingDirectories.Name = "lblProcessingDirectories";
            this.lblProcessingDirectories.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblProcessingDirectories.Size = new System.Drawing.Size(104, 22);
            this.lblProcessingDirectories.Text = "Creating Snapshot";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // pbProcessingDirectories
            // 
            this.pbProcessingDirectories.Name = "pbProcessingDirectories";
            this.pbProcessingDirectories.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.pbProcessingDirectories.Size = new System.Drawing.Size(750, 22);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // lblEstimatedTimeLeft
            // 
            this.lblEstimatedTimeLeft.Name = "lblEstimatedTimeLeft";
            this.lblEstimatedTimeLeft.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblEstimatedTimeLeft.Size = new System.Drawing.Size(136, 22);
            this.lblEstimatedTimeLeft.Text = "Estimated time left 00:00";
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 588);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "main";
            this.Text = "Disk Analyser";
            this.Load += new System.EventHandler(this.main_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbxSnapshot.ResumeLayout(false);
            this.toolStripSnapshot.ResumeLayout(false);
            this.toolStripSnapshot.PerformLayout();
            this.toolStripProcessing.ResumeLayout(false);
            this.toolStripProcessing.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbDrives;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAnalyseDrive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvTreeView;
        private System.Windows.Forms.ToolStrip toolStripProcessing;
        private System.Windows.Forms.ToolStripLabel lblProcessingDirectories;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripProgressBar pbProcessingDirectories;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel lblEstimatedTimeLeft;
        private System.Windows.Forms.ToolStrip toolStripSnapshot;
        private System.Windows.Forms.ToolStripButton btnSaveSnapshot;
        private System.Windows.Forms.GroupBox gbxSnapshot;
    }
}


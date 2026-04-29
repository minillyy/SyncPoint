namespace SyncPoint.Forms
{
    partial class SidebarControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlNav = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDashboard = new System.Windows.Forms.Label();
            this.lblAddTask = new System.Windows.Forms.Label();
            this.lblMembers = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblReports = new System.Windows.Forms.Label();
            this.lblLogout = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlNav.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNav
            // 
            this.pnlNav.BackColor = System.Drawing.Color.Transparent;
            this.pnlNav.Controls.Add(this.lblDashboard);
            this.pnlNav.Controls.Add(this.lblAddTask);
            this.pnlNav.Controls.Add(this.lblMembers);
            this.pnlNav.Controls.Add(this.lblProgress);
            this.pnlNav.Controls.Add(this.lblReports);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNav.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlNav.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(163)))), ((int)(((byte)(196)))));
            this.pnlNav.Location = new System.Drawing.Point(0, 0);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(120, 500);
            this.pnlNav.TabIndex = 1;
            this.pnlNav.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlNav_Paint_1);
            // 
            // lblDashboard
            // 
            this.lblDashboard.AutoSize = true;
            this.lblDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDashboard.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(163)))), ((int)(((byte)(196)))));
            this.lblDashboard.Location = new System.Drawing.Point(3, 0);
            this.lblDashboard.Name = "lblDashboard";
            this.lblDashboard.Padding = new System.Windows.Forms.Padding(12, 8, 0, 10);
            this.lblDashboard.Size = new System.Drawing.Size(101, 37);
            this.lblDashboard.TabIndex = 1;
            this.lblDashboard.Text = "Dashboard";
            this.lblDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDashboard.Click += new System.EventHandler(this.lblDashboard_Click);
            // 
            // lblAddTask
            // 
            this.lblAddTask.AutoSize = true;
            this.lblAddTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAddTask.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAddTask.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddTask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(163)))), ((int)(((byte)(196)))));
            this.lblAddTask.Location = new System.Drawing.Point(3, 37);
            this.lblAddTask.Name = "lblAddTask";
            this.lblAddTask.Padding = new System.Windows.Forms.Padding(12, 8, 0, 10);
            this.lblAddTask.Size = new System.Drawing.Size(87, 37);
            this.lblAddTask.TabIndex = 2;
            this.lblAddTask.Text = "Add Task";
            this.lblAddTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAddTask.Click += new System.EventHandler(this.lblAddTask_Click);
            // 
            // lblMembers
            // 
            this.lblMembers.AutoSize = true;
            this.lblMembers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMembers.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMembers.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMembers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(163)))), ((int)(((byte)(196)))));
            this.lblMembers.Location = new System.Drawing.Point(3, 74);
            this.lblMembers.Name = "lblMembers";
            this.lblMembers.Padding = new System.Windows.Forms.Padding(12, 8, 0, 10);
            this.lblMembers.Size = new System.Drawing.Size(88, 37);
            this.lblMembers.TabIndex = 3;
            this.lblMembers.Text = "Members";
            this.lblMembers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMembers.Click += new System.EventHandler(this.lblMembers_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProgress.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(163)))), ((int)(((byte)(196)))));
            this.lblProgress.Location = new System.Drawing.Point(3, 111);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Padding = new System.Windows.Forms.Padding(12, 8, 0, 10);
            this.lblProgress.Size = new System.Drawing.Size(87, 37);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "Progress";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProgress.Click += new System.EventHandler(this.lblProgress_Click);
            // 
            // lblReports
            // 
            this.lblReports.AutoSize = true;
            this.lblReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReports.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(163)))), ((int)(((byte)(196)))));
            this.lblReports.Location = new System.Drawing.Point(3, 148);
            this.lblReports.Name = "lblReports";
            this.lblReports.Padding = new System.Windows.Forms.Padding(12, 8, 0, 10);
            this.lblReports.Size = new System.Drawing.Size(77, 37);
            this.lblReports.TabIndex = 5;
            this.lblReports.Text = "Reports";
            this.lblReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblReports.Click += new System.EventHandler(this.lblReports_Click);
            // 
            // lblLogout
            // 
            this.lblLogout.AutoSize = true;
            this.lblLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLogout.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(123)))), ((int)(((byte)(106)))));
            this.lblLogout.Location = new System.Drawing.Point(17, 13);
            this.lblLogout.Name = "lblLogout";
            this.lblLogout.Size = new System.Drawing.Size(74, 19);
            this.lblLogout.TabIndex = 0;
            this.lblLogout.Text = "↩ Logout";
            this.lblLogout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogout.Click += new System.EventHandler(this.lblLogout_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.Transparent;
            this.pnlFooter.Controls.Add(this.lblLogout);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 455);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(120, 45);
            this.pnlFooter.TabIndex = 7;
            this.pnlFooter.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFooter_Paint_1);
            // 
            // SidebarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(45)))), ((int)(((byte)(74)))));
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlNav);
            this.Name = "SidebarControl";
            this.Size = new System.Drawing.Size(120, 500);
            this.Load += new System.EventHandler(this.SidebarControl_Load);
            this.pnlNav.ResumeLayout(false);
            this.pnlNav.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlNav;
        private System.Windows.Forms.Label lblDashboard;
        private System.Windows.Forms.Label lblAddTask;
        private System.Windows.Forms.Label lblMembers;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblReports;
        private System.Windows.Forms.Label lblLogout;
        private System.Windows.Forms.Panel pnlFooter;
    }
}

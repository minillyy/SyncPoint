namespace SyncPoint.Forms.Dashboards
{
    partial class MemberDashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTopbar = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.lblTasks = new System.Windows.Forms.Label();
            this.pnlLogout = new System.Windows.Forms.Panel();
            this.lblLogout = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.flpStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStatTotal = new System.Windows.Forms.Panel();
            this.lblTotalNum = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.pnlStatCompleted = new System.Windows.Forms.Panel();
            this.lblCompletedNum = new System.Windows.Forms.Label();
            this.lblCompletedLabel = new System.Windows.Forms.Label();
            this.pnlStatProgress = new System.Windows.Forms.Panel();
            this.lblInProgressNum = new System.Windows.Forms.Label();
            this.lblInProgressLabel = new System.Windows.Forms.Label();
            this.pnlTasksCard = new System.Windows.Forms.Panel();
            this.lblTasksTitle = new System.Windows.Forms.Label();
            this.dgvMyTasks = new System.Windows.Forms.DataGridView();
            this.pnlTopbar.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlLogout.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tableLayoutMain.SuspendLayout();
            this.flpStats.SuspendLayout();
            this.pnlStatTotal.SuspendLayout();
            this.pnlStatCompleted.SuspendLayout();
            this.pnlStatProgress.SuspendLayout();
            this.pnlTasksCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopbar
            // 
            this.pnlTopbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlTopbar.Controls.Add(this.lblUserName);
            this.pnlTopbar.Controls.Add(this.lblUserRole);
            this.pnlTopbar.Controls.Add(this.lblTitle);
            this.pnlTopbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopbar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopbar.Name = "pnlTopbar";
            this.pnlTopbar.Size = new System.Drawing.Size(1035, 65);
            this.pnlTopbar.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.lblUserName.Location = new System.Drawing.Point(765, 12);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(245, 25);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Welcome, Member";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserRole
            // 
            this.lblUserRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblUserRole.Location = new System.Drawing.Point(765, 40);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(245, 18);
            this.lblUserRole.TabIndex = 3;
            this.lblUserRole.Text = "Team Member";
            this.lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.lblTitle.Location = new System.Drawing.Point(25, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(418, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 SyncPoint — My Workspace";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.pnlSidebar.Controls.Add(this.lblTasks);
            this.pnlSidebar.Controls.Add(this.pnlLogout);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 65);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(112, 585);
            this.pnlSidebar.TabIndex = 1;
            // 
            // lblTasks
            // 
            this.lblTasks.BackColor = System.Drawing.Color.Transparent;
            this.lblTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTasks.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTasks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(163)))), ((int)(((byte)(196)))));
            this.lblTasks.Location = new System.Drawing.Point(3, 3);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.lblTasks.Size = new System.Drawing.Size(109, 41);
            this.lblTasks.TabIndex = 1;
            this.lblTasks.Text = "Tasks";
            this.lblTasks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTasks.Click += new System.EventHandler(this.lblTasks_Click);
            // 
            // pnlLogout
            // 
            this.pnlLogout.Controls.Add(this.lblLogout);
            this.pnlLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLogout.Location = new System.Drawing.Point(0, 540);
            this.pnlLogout.Name = "pnlLogout";
            this.pnlLogout.Size = new System.Drawing.Size(112, 45);
            this.pnlLogout.TabIndex = 0;
            // 
            // lblLogout
            // 
            this.lblLogout.AutoSize = true;
            this.lblLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLogout.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblLogout.Location = new System.Drawing.Point(14, 14);
            this.lblLogout.Name = "lblLogout";
            this.lblLogout.Size = new System.Drawing.Size(88, 23);
            this.lblLogout.TabIndex = 0;
            this.lblLogout.Text = "↩ Logout";
            this.lblLogout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlContent.Controls.Add(this.tableLayoutMain);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(112, 65);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30);
            this.pnlContent.Size = new System.Drawing.Size(923, 585);
            this.pnlContent.TabIndex = 2;
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.flpStats, 0, 0);
            this.tableLayoutMain.Controls.Add(this.pnlTasksCard, 0, 2);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(30, 30);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 3;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(863, 525);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // flpStats
            // 
            this.flpStats.Controls.Add(this.pnlStatTotal);
            this.flpStats.Controls.Add(this.pnlStatCompleted);
            this.flpStats.Controls.Add(this.pnlStatProgress);
            this.flpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpStats.Location = new System.Drawing.Point(0, 0);
            this.flpStats.Margin = new System.Windows.Forms.Padding(0);
            this.flpStats.Name = "flpStats";
            this.flpStats.Size = new System.Drawing.Size(863, 130);
            this.flpStats.TabIndex = 0;
            // 
            // pnlStatTotal
            // 
            this.pnlStatTotal.BackColor = System.Drawing.Color.White;
            this.pnlStatTotal.Controls.Add(this.lblTotalNum);
            this.pnlStatTotal.Controls.Add(this.lblTotalLabel);
            this.pnlStatTotal.Location = new System.Drawing.Point(3, 3);
            this.pnlStatTotal.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.pnlStatTotal.Name = "pnlStatTotal";
            this.pnlStatTotal.Size = new System.Drawing.Size(270, 120);
            this.pnlStatTotal.TabIndex = 0;
            this.pnlStatTotal.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatTotal_Paint);
            // 
            // lblTotalNum
            // 
            this.lblTotalNum.AutoSize = true;
            this.lblTotalNum.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblTotalNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTotalNum.Location = new System.Drawing.Point(25, 20);
            this.lblTotalNum.Name = "lblTotalNum";
            this.lblTotalNum.Size = new System.Drawing.Size(70, 81);
            this.lblTotalNum.TabIndex = 0;
            this.lblTotalNum.Text = "0";
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblTotalLabel.Location = new System.Drawing.Point(25, 85);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(55, 25);
            this.lblTotalLabel.TabIndex = 1;
            this.lblTotalLabel.Text = "Tasks";
            // 
            // pnlStatCompleted
            // 
            this.pnlStatCompleted.BackColor = System.Drawing.Color.White;
            this.pnlStatCompleted.Controls.Add(this.lblCompletedNum);
            this.pnlStatCompleted.Controls.Add(this.lblCompletedLabel);
            this.pnlStatCompleted.Location = new System.Drawing.Point(293, 3);
            this.pnlStatCompleted.Margin = new System.Windows.Forms.Padding(0, 3, 20, 3);
            this.pnlStatCompleted.Name = "pnlStatCompleted";
            this.pnlStatCompleted.Size = new System.Drawing.Size(270, 120);
            this.pnlStatCompleted.TabIndex = 1;
            this.pnlStatCompleted.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatCompleted_Paint);
            // 
            // lblCompletedNum
            // 
            this.lblCompletedNum.AutoSize = true;
            this.lblCompletedNum.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblCompletedNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblCompletedNum.Location = new System.Drawing.Point(25, 20);
            this.lblCompletedNum.Name = "lblCompletedNum";
            this.lblCompletedNum.Size = new System.Drawing.Size(70, 81);
            this.lblCompletedNum.TabIndex = 0;
            this.lblCompletedNum.Text = "0";
            // 
            // lblCompletedLabel
            // 
            this.lblCompletedLabel.AutoSize = true;
            this.lblCompletedLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCompletedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblCompletedLabel.Location = new System.Drawing.Point(25, 85);
            this.lblCompletedLabel.Name = "lblCompletedLabel";
            this.lblCompletedLabel.Size = new System.Drawing.Size(104, 25);
            this.lblCompletedLabel.TabIndex = 1;
            this.lblCompletedLabel.Text = "Completed";
            // 
            // pnlStatProgress
            // 
            this.pnlStatProgress.BackColor = System.Drawing.Color.White;
            this.pnlStatProgress.Controls.Add(this.lblInProgressNum);
            this.pnlStatProgress.Controls.Add(this.lblInProgressLabel);
            this.pnlStatProgress.Location = new System.Drawing.Point(583, 3);
            this.pnlStatProgress.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlStatProgress.Name = "pnlStatProgress";
            this.pnlStatProgress.Size = new System.Drawing.Size(270, 120);
            this.pnlStatProgress.TabIndex = 2;
            this.pnlStatProgress.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatProgress_Paint);
            // 
            // lblInProgressNum
            // 
            this.lblInProgressNum.AutoSize = true;
            this.lblInProgressNum.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblInProgressNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblInProgressNum.Location = new System.Drawing.Point(25, 20);
            this.lblInProgressNum.Name = "lblInProgressNum";
            this.lblInProgressNum.Size = new System.Drawing.Size(70, 81);
            this.lblInProgressNum.TabIndex = 0;
            this.lblInProgressNum.Text = "0";
            // 
            // lblInProgressLabel
            // 
            this.lblInProgressLabel.AutoSize = true;
            this.lblInProgressLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblInProgressLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblInProgressLabel.Location = new System.Drawing.Point(25, 85);
            this.lblInProgressLabel.Name = "lblInProgressLabel";
            this.lblInProgressLabel.Size = new System.Drawing.Size(81, 25);
            this.lblInProgressLabel.TabIndex = 1;
            this.lblInProgressLabel.Text = "Pending";
            // 
            // pnlTasksCard
            // 
            this.pnlTasksCard.BackColor = System.Drawing.Color.White;
            this.pnlTasksCard.Controls.Add(this.lblTasksTitle);
            this.pnlTasksCard.Controls.Add(this.dgvMyTasks);
            this.pnlTasksCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTasksCard.Location = new System.Drawing.Point(3, 144);
            this.pnlTasksCard.Name = "pnlTasksCard";
            this.pnlTasksCard.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.pnlTasksCard.Size = new System.Drawing.Size(857, 378);
            this.pnlTasksCard.TabIndex = 2;
            // 
            // lblTasksTitle
            // 
            this.lblTasksTitle.AutoSize = true;
            this.lblTasksTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTasksTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTasksTitle.Location = new System.Drawing.Point(25, 18);
            this.lblTasksTitle.Name = "lblTasksTitle";
            this.lblTasksTitle.Size = new System.Drawing.Size(223, 28);
            this.lblTasksTitle.TabIndex = 0;
            this.lblTasksTitle.Text = "📝 My Assigned Tasks";
            // 
            // dgvMyTasks
            // 
            this.dgvMyTasks.AllowUserToAddRows = false;
            this.dgvMyTasks.AllowUserToDeleteRows = false;
            this.dgvMyTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyTasks.BackgroundColor = System.Drawing.Color.White;
            this.dgvMyTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMyTasks.ColumnHeadersHeight = 29;
            this.dgvMyTasks.Location = new System.Drawing.Point(29, 55);
            this.dgvMyTasks.Name = "dgvMyTasks";
            this.dgvMyTasks.ReadOnly = true;
            this.dgvMyTasks.RowHeadersVisible = false;
            this.dgvMyTasks.RowHeadersWidth = 51;
            this.dgvMyTasks.RowTemplate.Height = 42;
            this.dgvMyTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMyTasks.Size = new System.Drawing.Size(816, 140);
            this.dgvMyTasks.TabIndex = 2;
            // 
            // MemberDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1035, 650);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlTopbar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MemberDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SyncPoint - My Workspace";
            this.pnlTopbar.ResumeLayout(false);
            this.pnlTopbar.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.pnlLogout.ResumeLayout(false);
            this.pnlLogout.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.tableLayoutMain.ResumeLayout(false);
            this.flpStats.ResumeLayout(false);
            this.pnlStatTotal.ResumeLayout(false);
            this.pnlStatTotal.PerformLayout();
            this.pnlStatCompleted.ResumeLayout(false);
            this.pnlStatCompleted.PerformLayout();
            this.pnlStatProgress.ResumeLayout(false);
            this.pnlStatProgress.PerformLayout();
            this.pnlTasksCard.ResumeLayout(false);
            this.pnlTasksCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyTasks)).EndInit();
            this.ResumeLayout(false);

        }

        // Designer-friendly paint handlers
        private void pnlStatTotal_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            using (var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(220, 220, 220), 1))
            using (var fillBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
            {
                e.Graphics.FillRectangle(fillBrush, 0, 0, 269, 119);
                e.Graphics.DrawRectangle(pen, 0, 0, 269, 119);
            }
        }

        private void pnlStatCompleted_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            using (var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(220, 220, 220), 1))
            using (var fillBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
            {
                e.Graphics.FillRectangle(fillBrush, 0, 0, 269, 119);
                e.Graphics.DrawRectangle(pen, 0, 0, 269, 119);
            }
        }

        private void pnlStatProgress_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            using (var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(220, 220, 220), 1))
            using (var fillBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
            {
                e.Graphics.FillRectangle(fillBrush, 0, 0, 269, 119);
                e.Graphics.DrawRectangle(pen, 0, 0, 269, 119);
            }
        }

        // ========== Control Declarations ==========
        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlLogout;
        private System.Windows.Forms.Label lblLogout;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.FlowLayoutPanel flpStats;
        private System.Windows.Forms.Panel pnlStatTotal;
        private System.Windows.Forms.Label lblTotalNum;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Panel pnlStatCompleted;
        private System.Windows.Forms.Label lblCompletedNum;
        private System.Windows.Forms.Label lblCompletedLabel;
        private System.Windows.Forms.Panel pnlStatProgress;
        private System.Windows.Forms.Label lblInProgressNum;
        private System.Windows.Forms.Label lblInProgressLabel;
        private System.Windows.Forms.Panel pnlTasksCard;
        private System.Windows.Forms.Label lblTasksTitle;
        private System.Windows.Forms.DataGridView dgvMyTasks;
        private System.Windows.Forms.Label lblTasks;
    }
}
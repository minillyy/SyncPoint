namespace SyncPoint.Forms
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
            // Topbar Panel
            this.pnlTopbar = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();

            // Sidebar Panel
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlLogout = new System.Windows.Forms.Panel();
            this.lblLogout = new System.Windows.Forms.Label();

            // Main Content Panel
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();

            // Stats Row
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

            // Progress Card
            this.pnlProgressCard = new System.Windows.Forms.Panel();
            this.lblProgressTitle = new System.Windows.Forms.Label();
            this.pbMyProgress = new System.Windows.Forms.ProgressBar();
            this.lblProgressPercent = new System.Windows.Forms.Label();

            // Tasks Card
            this.pnlTasksCard = new System.Windows.Forms.Panel();
            this.lblTasksTitle = new System.Windows.Forms.Label();
            this.lblTaskCount = new System.Windows.Forms.Label();
            this.dgvMyTasks = new System.Windows.Forms.DataGridView();
            this.pnlUpdateStatus = new System.Windows.Forms.Panel();
            this.lblSelectStatus = new System.Windows.Forms.Label();
            this.cmbNewStatus = new System.Windows.Forms.ComboBox();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            // Topbar Suspend
            this.pnlTopbar.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlLogout.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tableLayoutMain.SuspendLayout();
            this.flpStats.SuspendLayout();
            this.pnlStatTotal.SuspendLayout();
            this.pnlStatCompleted.SuspendLayout();
            this.pnlStatProgress.SuspendLayout();
            this.pnlProgressCard.SuspendLayout();
            this.pnlTasksCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyTasks)).BeginInit();
            this.pnlUpdateStatus.SuspendLayout();
            this.SuspendLayout();

            // ========== pnlTopbar ==========
            this.pnlTopbar.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.pnlTopbar.Controls.Add(this.lblUserName);
            this.pnlTopbar.Controls.Add(this.lblUserRole);
            this.pnlTopbar.Controls.Add(this.lblTitle);
            this.pnlTopbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopbar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopbar.Name = "pnlTopbar";
            this.pnlTopbar.Size = new System.Drawing.Size(1000, 65);
            this.pnlTopbar.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.lblTitle.Location = new System.Drawing.Point(25, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(315, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 SyncPoint — My Workspace";

            // lblUserName
            this.lblUserName.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.lblUserName.Location = new System.Drawing.Point(730, 12);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(245, 25);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Welcome, Member";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // lblUserRole
            this.lblUserRole.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.lblUserRole.Location = new System.Drawing.Point(730, 40);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(245, 18);
            this.lblUserRole.TabIndex = 3;
            this.lblUserRole.Text = "Team Member";
            this.lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ========== pnlSidebar ==========
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.pnlSidebar.Controls.Add(this.pnlLogout);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 65);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(60, 585);
            this.pnlSidebar.TabIndex = 1;

            // pnlLogout
            this.pnlLogout.Controls.Add(this.lblLogout);
            this.pnlLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLogout.Location = new System.Drawing.Point(0, 540);
            this.pnlLogout.Name = "pnlLogout";
            this.pnlLogout.Size = new System.Drawing.Size(60, 45);
            this.pnlLogout.TabIndex = 0;

            // lblLogout - EXIT BUTTON (UPDATED)
            this.lblLogout.AutoSize = true;
            this.lblLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLogout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLogout.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblLogout.Location = new System.Drawing.Point(14, 14);
            this.lblLogout.Name = "lblLogout";
            this.lblLogout.Size = new System.Drawing.Size(32, 20);
            this.lblLogout.TabIndex = 0;
            this.lblLogout.Text = "Exit";
            this.lblLogout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ========== pnlContent ==========
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.pnlContent.Controls.Add(this.tableLayoutMain);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(60, 65);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30);
            this.pnlContent.Size = new System.Drawing.Size(940, 585);
            this.pnlContent.TabIndex = 2;

            // ========== tableLayoutMain ==========
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.flpStats, 0, 0);
            this.tableLayoutMain.Controls.Add(this.pnlProgressCard, 0, 1);
            this.tableLayoutMain.Controls.Add(this.pnlTasksCard, 0, 2);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(30, 30);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 3;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(880, 525);
            this.tableLayoutMain.TabIndex = 0;

            // ========== flpStats ==========
            this.flpStats.Controls.Add(this.pnlStatTotal);
            this.flpStats.Controls.Add(this.pnlStatCompleted);
            this.flpStats.Controls.Add(this.pnlStatProgress);
            this.flpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpStats.Location = new System.Drawing.Point(0, 0);
            this.flpStats.Margin = new System.Windows.Forms.Padding(0);
            this.flpStats.Name = "flpStats";
            this.flpStats.Size = new System.Drawing.Size(880, 130);
            this.flpStats.TabIndex = 0;

            // pnlStatTotal
            this.pnlStatTotal.BackColor = System.Drawing.Color.White;
            this.pnlStatTotal.Controls.Add(this.lblTotalNum);
            this.pnlStatTotal.Controls.Add(this.lblTotalLabel);
            this.pnlStatTotal.Location = new System.Drawing.Point(3, 3);
            this.pnlStatTotal.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.pnlStatTotal.Name = "pnlStatTotal";
            this.pnlStatTotal.Size = new System.Drawing.Size(270, 120);
            this.pnlStatTotal.TabIndex = 0;
            this.pnlStatTotal.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatTotal_Paint);

            this.lblTotalNum.AutoSize = true;
            this.lblTotalNum.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblTotalNum.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblTotalNum.Location = new System.Drawing.Point(25, 20);
            this.lblTotalNum.Name = "lblTotalNum";
            this.lblTotalNum.Size = new System.Drawing.Size(57, 65);
            this.lblTotalNum.Text = "0";

            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalLabel.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblTotalLabel.Location = new System.Drawing.Point(25, 85);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(46, 20);
            this.lblTotalLabel.Text = "Tasks";

            // pnlStatCompleted
            this.pnlStatCompleted.BackColor = System.Drawing.Color.White;
            this.pnlStatCompleted.Controls.Add(this.lblCompletedNum);
            this.pnlStatCompleted.Controls.Add(this.lblCompletedLabel);
            this.pnlStatCompleted.Location = new System.Drawing.Point(293, 3);
            this.pnlStatCompleted.Margin = new System.Windows.Forms.Padding(0, 3, 20, 3);
            this.pnlStatCompleted.Name = "pnlStatCompleted";
            this.pnlStatCompleted.Size = new System.Drawing.Size(270, 120);
            this.pnlStatCompleted.TabIndex = 1;
            this.pnlStatCompleted.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatCompleted_Paint);

            this.lblCompletedNum.AutoSize = true;
            this.lblCompletedNum.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblCompletedNum.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.lblCompletedNum.Location = new System.Drawing.Point(25, 20);
            this.lblCompletedNum.Name = "lblCompletedNum";
            this.lblCompletedNum.Size = new System.Drawing.Size(57, 65);
            this.lblCompletedNum.Text = "0";

            this.lblCompletedLabel.AutoSize = true;
            this.lblCompletedLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCompletedLabel.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblCompletedLabel.Location = new System.Drawing.Point(25, 85);
            this.lblCompletedLabel.Name = "lblCompletedLabel";
            this.lblCompletedLabel.Size = new System.Drawing.Size(81, 20);
            this.lblCompletedLabel.Text = "Completed";

            // pnlStatProgress (In Progress)
            this.pnlStatProgress.BackColor = System.Drawing.Color.White;
            this.pnlStatProgress.Controls.Add(this.lblInProgressNum);
            this.pnlStatProgress.Controls.Add(this.lblInProgressLabel);
            this.pnlStatProgress.Location = new System.Drawing.Point(583, 3);
            this.pnlStatProgress.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlStatProgress.Name = "pnlStatProgress";
            this.pnlStatProgress.Size = new System.Drawing.Size(270, 120);
            this.pnlStatProgress.TabIndex = 2;
            this.pnlStatProgress.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatProgress_Paint);

            this.lblInProgressNum.AutoSize = true;
            this.lblInProgressNum.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblInProgressNum.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblInProgressNum.Location = new System.Drawing.Point(25, 20);
            this.lblInProgressNum.Name = "lblInProgressNum";
            this.lblInProgressNum.Size = new System.Drawing.Size(57, 65);
            this.lblInProgressNum.Text = "0";

            this.lblInProgressLabel.AutoSize = true;
            this.lblInProgressLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblInProgressLabel.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblInProgressLabel.Location = new System.Drawing.Point(25, 85);
            this.lblInProgressLabel.Name = "lblInProgressLabel";
            this.lblInProgressLabel.Size = new System.Drawing.Size(92, 20);
            this.lblInProgressLabel.Text = "In Progress";

            // ========== pnlProgressCard ==========
            this.pnlProgressCard.BackColor = System.Drawing.Color.White;
            this.pnlProgressCard.Controls.Add(this.lblProgressTitle);
            this.pnlProgressCard.Controls.Add(this.pbMyProgress);
            this.pnlProgressCard.Controls.Add(this.lblProgressPercent);
            this.pnlProgressCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProgressCard.Location = new System.Drawing.Point(3, 133);
            this.pnlProgressCard.Name = "pnlProgressCard";
            this.pnlProgressCard.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.pnlProgressCard.Size = new System.Drawing.Size(874, 94);
            this.pnlProgressCard.TabIndex = 1;

            // lblProgressTitle
            this.lblProgressTitle.AutoSize = true;
            this.lblProgressTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblProgressTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblProgressTitle.Location = new System.Drawing.Point(25, 18);
            this.lblProgressTitle.Name = "lblProgressTitle";
            this.lblProgressTitle.Size = new System.Drawing.Size(179, 21);
            this.lblProgressTitle.Text = "📊 Completion Progress";

            // lblProgressPercent
            this.lblProgressPercent.AutoSize = true;
            this.lblProgressPercent.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblProgressPercent.ForeColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.lblProgressPercent.Location = new System.Drawing.Point(775, 15);
            this.lblProgressPercent.Name = "lblProgressPercent";
            this.lblProgressPercent.Size = new System.Drawing.Size(74, 25);
            this.lblProgressPercent.Text = "0%";
            this.lblProgressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // pbMyProgress
            this.pbMyProgress.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.pbMyProgress.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.pbMyProgress.Location = new System.Drawing.Point(29, 55);
            this.pbMyProgress.Name = "pbMyProgress";
            this.pbMyProgress.Size = new System.Drawing.Size(816, 14);
            this.pbMyProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            // ========== pnlTasksCard ==========
            this.pnlTasksCard.BackColor = System.Drawing.Color.White;
            this.pnlTasksCard.Controls.Add(this.lblTasksTitle);
            this.pnlTasksCard.Controls.Add(this.lblTaskCount);
            this.pnlTasksCard.Controls.Add(this.dgvMyTasks);
            this.pnlTasksCard.Controls.Add(this.pnlUpdateStatus);
            this.pnlTasksCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTasksCard.Location = new System.Drawing.Point(3, 233);
            this.pnlTasksCard.Name = "pnlTasksCard";
            this.pnlTasksCard.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.pnlTasksCard.Size = new System.Drawing.Size(874, 289);
            this.pnlTasksCard.TabIndex = 2;

            // lblTasksTitle
            this.lblTasksTitle.AutoSize = true;
            this.lblTasksTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTasksTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblTasksTitle.Location = new System.Drawing.Point(25, 18);
            this.lblTasksTitle.Name = "lblTasksTitle";
            this.lblTasksTitle.Size = new System.Drawing.Size(169, 21);
            this.lblTasksTitle.Text = "📝 My Assigned Tasks";

            // lblTaskCount
            this.lblTaskCount.AutoSize = true;
            this.lblTaskCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTaskCount.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblTaskCount.Location = new System.Drawing.Point(195, 21);
            this.lblTaskCount.Name = "lblTaskCount";
            this.lblTaskCount.Size = new System.Drawing.Size(79, 15);
            this.lblTaskCount.Text = "0 tasks assigned";

            // dgvMyTasks
            this.dgvMyTasks.AllowUserToAddRows = false;
            this.dgvMyTasks.AllowUserToDeleteRows = false;
            this.dgvMyTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyTasks.BackgroundColor = System.Drawing.Color.White;
            this.dgvMyTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMyTasks.Location = new System.Drawing.Point(29, 55);
            this.dgvMyTasks.Name = "dgvMyTasks";
            this.dgvMyTasks.ReadOnly = true;
            this.dgvMyTasks.RowHeadersVisible = false;
            this.dgvMyTasks.RowTemplate.Height = 42;
            this.dgvMyTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMyTasks.Size = new System.Drawing.Size(816, 140);
            this.dgvMyTasks.TabIndex = 2;

            // pnlUpdateStatus
            this.pnlUpdateStatus.Controls.Add(this.lblSelectStatus);
            this.pnlUpdateStatus.Controls.Add(this.cmbNewStatus);
            this.pnlUpdateStatus.Controls.Add(this.btnUpdateStatus);
            this.pnlUpdateStatus.Controls.Add(this.btnRefresh);
            this.pnlUpdateStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUpdateStatus.Location = new System.Drawing.Point(25, 210);
            this.pnlUpdateStatus.Name = "pnlUpdateStatus";
            this.pnlUpdateStatus.Size = new System.Drawing.Size(824, 64);
            this.pnlUpdateStatus.TabIndex = 3;

            // lblSelectStatus
            this.lblSelectStatus.AutoSize = true;
            this.lblSelectStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSelectStatus.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.lblSelectStatus.Location = new System.Drawing.Point(4, 22);
            this.lblSelectStatus.Name = "lblSelectStatus";
            this.lblSelectStatus.Size = new System.Drawing.Size(121, 19);
            this.lblSelectStatus.Text = "Update status to:";

            // cmbNewStatus
            this.cmbNewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNewStatus.Location = new System.Drawing.Point(131, 18);
            this.cmbNewStatus.Name = "cmbNewStatus";
            this.cmbNewStatus.Size = new System.Drawing.Size(150, 25);
            this.cmbNewStatus.TabIndex = 1;
            this.cmbNewStatus.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            // btnUpdateStatus
            this.btnUpdateStatus.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStatus.Location = new System.Drawing.Point(297, 14);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(150, 35);
            this.btnUpdateStatus.TabIndex = 2;
            this.btnUpdateStatus.Text = "✓ Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = false;
            this.btnUpdateStatus.FlatAppearance.BorderSize = 0;

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.btnRefresh.Location = new System.Drawing.Point(463, 14);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "⟳ Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnRefresh.FlatAppearance.BorderSize = 1;

            // ========== MemberDashboardForm ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlTopbar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MemberDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SyncPoint - My Workspace";

            // Resume Layouts
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
            this.pnlProgressCard.ResumeLayout(false);
            this.pnlProgressCard.PerformLayout();
            this.pnlTasksCard.ResumeLayout(false);
            this.pnlTasksCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyTasks)).EndInit();
            this.pnlUpdateStatus.ResumeLayout(false);
            this.pnlUpdateStatus.PerformLayout();
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
        private System.Windows.Forms.Panel pnlProgressCard;
        private System.Windows.Forms.Label lblProgressTitle;
        private System.Windows.Forms.ProgressBar pbMyProgress;
        private System.Windows.Forms.Label lblProgressPercent;
        private System.Windows.Forms.Panel pnlTasksCard;
        private System.Windows.Forms.Label lblTasksTitle;
        private System.Windows.Forms.Label lblTaskCount;
        private System.Windows.Forms.DataGridView dgvMyTasks;
        private System.Windows.Forms.Panel pnlUpdateStatus;
        private System.Windows.Forms.Label lblSelectStatus;
        private System.Windows.Forms.ComboBox cmbNewStatus;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnRefresh;
    }
}
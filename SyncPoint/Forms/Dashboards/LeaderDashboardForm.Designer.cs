namespace SyncPoint.Forms
{
    partial class LeaderDashboardForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTopbar = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.pnlMembersCard = new System.Windows.Forms.Panel();
            this.dgvMembers = new System.Windows.Forms.DataGridView();
            this.lblMembersTitle = new System.Windows.Forms.Label();
            this.flpStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStatTotal1 = new System.Windows.Forms.Panel();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblTotalNum = new System.Windows.Forms.Label();
            this.pnlStatTotal2 = new System.Windows.Forms.Panel();
            this.lblCompletedLabel = new System.Windows.Forms.Label();
            this.lblCompletedNum = new System.Windows.Forms.Label();
            this.pnlStatTotal3 = new System.Windows.Forms.Panel();
            this.lblPendingLabel = new System.Windows.Forms.Label();
            this.lblPendingNum = new System.Windows.Forms.Label();
            this.sidebarControl1 = new SyncPoint.Forms.SidebarControl();
            this.pnlTopbar.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMembersCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).BeginInit();
            this.flpStats.SuspendLayout();
            this.pnlStatTotal1.SuspendLayout();
            this.pnlStatTotal2.SuspendLayout();
            this.pnlStatTotal3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopbar
            // 
            this.pnlTopbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlTopbar.Controls.Add(this.lblUserName);
            this.pnlTopbar.Controls.Add(this.lblTitle);
            this.pnlTopbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopbar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopbar.Name = "pnlTopbar";
            this.pnlTopbar.Size = new System.Drawing.Size(763, 50);
            this.pnlTopbar.TabIndex = 0;
            this.pnlTopbar.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTopbar_Paint);
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(168)))), ((int)(((byte)(76)))));
            this.lblUserName.Location = new System.Drawing.Point(599, 16);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(126, 23);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Leader: Juan D.";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(240)))), ((int)(((byte)(232)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(387, 31);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "📋 SyncPoint — Leader Dashboard";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(39)))), ((int)(((byte)(68)))));
            this.pnlSidebar.Controls.Add(this.sidebarControl1);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 50);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(90, 618);
            this.pnlSidebar.TabIndex = 1;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlContent.Controls.Add(this.btnAddMember);
            this.pnlContent.Controls.Add(this.pnlMembersCard);
            this.pnlContent.Controls.Add(this.flpStats);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(90, 50);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(14);
            this.pnlContent.Size = new System.Drawing.Size(673, 618);
            this.pnlContent.TabIndex = 2;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // btnAddMember
            // 
            this.btnAddMember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnAddMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMember.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMember.ForeColor = System.Drawing.Color.White;
            this.btnAddMember.Location = new System.Drawing.Point(447, 560);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(170, 41);
            this.btnAddMember.TabIndex = 3;
            this.btnAddMember.Text = "Add Member";
            this.btnAddMember.UseVisualStyleBackColor = false;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // pnlMembersCard
            // 
            this.pnlMembersCard.BackColor = System.Drawing.Color.White;
            this.pnlMembersCard.Controls.Add(this.dgvMembers);
            this.pnlMembersCard.Controls.Add(this.lblMembersTitle);
            this.pnlMembersCard.Location = new System.Drawing.Point(14, 115);
            this.pnlMembersCard.Name = "pnlMembersCard";
            this.pnlMembersCard.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlMembersCard.Size = new System.Drawing.Size(615, 426);
            this.pnlMembersCard.TabIndex = 2;
            // 
            // dgvMembers
            // 
            this.dgvMembers.AllowUserToAddRows = false;
            this.dgvMembers.AllowUserToDeleteRows = false;
            this.dgvMembers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMembers.BackgroundColor = System.Drawing.Color.White;
            this.dgvMembers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMembers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(224)))), ((int)(((byte)(208)))));
            this.dgvMembers.Location = new System.Drawing.Point(12, 38);
            this.dgvMembers.Name = "dgvMembers";
            this.dgvMembers.ReadOnly = true;
            this.dgvMembers.RowHeadersVisible = false;
            this.dgvMembers.RowHeadersWidth = 51;
            this.dgvMembers.RowTemplate.Height = 24;
            this.dgvMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMembers.Size = new System.Drawing.Size(591, 378);
            this.dgvMembers.TabIndex = 1;
            this.dgvMembers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMembers_CellContentClick);
            // 
            // lblMembersTitle
            // 
            this.lblMembersTitle.AutoSize = true;
            this.lblMembersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMembersTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMembersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(39)))), ((int)(((byte)(68)))));
            this.lblMembersTitle.Location = new System.Drawing.Point(12, 10);
            this.lblMembersTitle.Name = "lblMembersTitle";
            this.lblMembersTitle.Size = new System.Drawing.Size(184, 28);
            this.lblMembersTitle.TabIndex = 0;
            this.lblMembersTitle.Text = "Member overview";
            // 
            // flpStats
            // 
            this.flpStats.BackColor = System.Drawing.Color.Transparent;
            this.flpStats.Controls.Add(this.pnlStatTotal1);
            this.flpStats.Controls.Add(this.pnlStatTotal2);
            this.flpStats.Controls.Add(this.pnlStatTotal3);
            this.flpStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpStats.Location = new System.Drawing.Point(14, 14);
            this.flpStats.Name = "flpStats";
            this.flpStats.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.flpStats.Size = new System.Drawing.Size(645, 80);
            this.flpStats.TabIndex = 0;
            this.flpStats.WrapContents = false;
            // 
            // pnlStatTotal1
            // 
            this.pnlStatTotal1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlStatTotal1.Controls.Add(this.lblTotalLabel);
            this.pnlStatTotal1.Controls.Add(this.lblTotalNum);
            this.pnlStatTotal1.Location = new System.Drawing.Point(0, 0);
            this.pnlStatTotal1.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnlStatTotal1.Name = "pnlStatTotal1";
            this.pnlStatTotal1.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlStatTotal1.Size = new System.Drawing.Size(200, 80);
            this.pnlStatTotal1.TabIndex = 0;
            this.pnlStatTotal1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(111)))), ((int)(((byte)(90)))));
            this.lblTotalLabel.Location = new System.Drawing.Point(91, 31);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(94, 25);
            this.lblTotalLabel.TabIndex = 1;
            this.lblTotalLabel.Text = "Total tasks";
            this.lblTotalLabel.Click += new System.EventHandler(this.lblTotalLabel_Click);
            // 
            // lblTotalNum
            // 
            this.lblTotalNum.AutoSize = true;
            this.lblTotalNum.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(39)))), ((int)(((byte)(68)))));
            this.lblTotalNum.Location = new System.Drawing.Point(18, 11);
            this.lblTotalNum.Name = "lblTotalNum";
            this.lblTotalNum.Size = new System.Drawing.Size(50, 59);
            this.lblTotalNum.TabIndex = 0;
            this.lblTotalNum.Text = "0";
            this.lblTotalNum.Click += new System.EventHandler(this.lblTotalNum_Click);
            // 
            // pnlStatTotal2
            // 
            this.pnlStatTotal2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlStatTotal2.Controls.Add(this.lblCompletedLabel);
            this.pnlStatTotal2.Controls.Add(this.lblCompletedNum);
            this.pnlStatTotal2.Location = new System.Drawing.Point(210, 0);
            this.pnlStatTotal2.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnlStatTotal2.Name = "pnlStatTotal2";
            this.pnlStatTotal2.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlStatTotal2.Size = new System.Drawing.Size(196, 80);
            this.pnlStatTotal2.TabIndex = 1;
            this.pnlStatTotal2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lblCompletedLabel
            // 
            this.lblCompletedLabel.AutoSize = true;
            this.lblCompletedLabel.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompletedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(111)))), ((int)(((byte)(90)))));
            this.lblCompletedLabel.Location = new System.Drawing.Point(83, 31);
            this.lblCompletedLabel.Name = "lblCompletedLabel";
            this.lblCompletedLabel.Size = new System.Drawing.Size(100, 25);
            this.lblCompletedLabel.TabIndex = 1;
            this.lblCompletedLabel.Text = "Completed";
            // 
            // lblCompletedNum
            // 
            this.lblCompletedNum.AutoSize = true;
            this.lblCompletedNum.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompletedNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(107)))), ((int)(((byte)(58)))));
            this.lblCompletedNum.Location = new System.Drawing.Point(19, 11);
            this.lblCompletedNum.Name = "lblCompletedNum";
            this.lblCompletedNum.Size = new System.Drawing.Size(50, 59);
            this.lblCompletedNum.TabIndex = 0;
            this.lblCompletedNum.Text = "0";
            this.lblCompletedNum.Click += new System.EventHandler(this.lblCompletedNum_Click);
            // 
            // pnlStatTotal3
            // 
            this.pnlStatTotal3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlStatTotal3.Controls.Add(this.lblPendingLabel);
            this.pnlStatTotal3.Controls.Add(this.lblPendingNum);
            this.pnlStatTotal3.Location = new System.Drawing.Point(416, 0);
            this.pnlStatTotal3.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnlStatTotal3.Name = "pnlStatTotal3";
            this.pnlStatTotal3.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlStatTotal3.Size = new System.Drawing.Size(199, 80);
            this.pnlStatTotal3.TabIndex = 2;
            this.pnlStatTotal3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // lblPendingLabel
            // 
            this.lblPendingLabel.AutoSize = true;
            this.lblPendingLabel.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(111)))), ((int)(((byte)(90)))));
            this.lblPendingLabel.Location = new System.Drawing.Point(95, 31);
            this.lblPendingLabel.Name = "lblPendingLabel";
            this.lblPendingLabel.Size = new System.Drawing.Size(76, 25);
            this.lblPendingLabel.TabIndex = 1;
            this.lblPendingLabel.Text = "Pending";
            // 
            // lblPendingNum
            // 
            this.lblPendingNum.AutoSize = true;
            this.lblPendingNum.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(92)))), ((int)(((byte)(0)))));
            this.lblPendingNum.Location = new System.Drawing.Point(21, 14);
            this.lblPendingNum.Name = "lblPendingNum";
            this.lblPendingNum.Size = new System.Drawing.Size(46, 54);
            this.lblPendingNum.TabIndex = 0;
            this.lblPendingNum.Text = "0";
            // 
            // sidebarControl1
            // 
            this.sidebarControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.sidebarControl1.Location = new System.Drawing.Point(0, 0);
            this.sidebarControl1.Name = "sidebarControl1";
            this.sidebarControl1.Size = new System.Drawing.Size(116, 618);
            this.sidebarControl1.TabIndex = 0;
            // 
            // LeaderDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(763, 668);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlTopbar);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LeaderDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SyncPoint";
            this.Load += new System.EventHandler(this.LeaderDashboardForm_Load);
            this.pnlTopbar.ResumeLayout(false);
            this.pnlTopbar.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMembersCard.ResumeLayout(false);
            this.pnlMembersCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).EndInit();
            this.flpStats.ResumeLayout(false);
            this.pnlStatTotal1.ResumeLayout(false);
            this.pnlStatTotal1.PerformLayout();
            this.pnlStatTotal2.ResumeLayout(false);
            this.pnlStatTotal2.PerformLayout();
            this.pnlStatTotal3.ResumeLayout(false);
            this.pnlStatTotal3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpStats;
        private System.Windows.Forms.Panel pnlStatTotal1;
        private System.Windows.Forms.Panel pnlStatTotal2;
        private System.Windows.Forms.Panel pnlStatTotal3;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotalNum;
        private System.Windows.Forms.Label lblCompletedLabel;
        private System.Windows.Forms.Label lblCompletedNum;
        private System.Windows.Forms.Label lblPendingLabel;
        private System.Windows.Forms.Label lblPendingNum;
        private System.Windows.Forms.Panel pnlMembersCard;
        private System.Windows.Forms.Label lblMembersTitle;
        private System.Windows.Forms.DataGridView dgvMembers;
        private SidebarControl sidebarControl1;
        private System.Windows.Forms.Button btnAddMember;
    }
}
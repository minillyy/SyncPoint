namespace SyncPoint
{
    partial class AddMemberForm
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblStatusCount = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.lblSelectedUser = new System.Windows.Forms.Label();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Controls.Add(this.lblGroupName);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(439, 85);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(17, 10);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(190, 38);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Add Member";
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(168)))), ((int)(((byte)(76)))));
            this.lblGroupName.Location = new System.Drawing.Point(19, 50);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(140, 23);
            this.lblGroupName.TabIndex = 1;
            this.lblGroupName.Text = "Group: Sample1";
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(111)))), ((int)(((byte)(90)))));
            this.lblInstructions.Location = new System.Drawing.Point(13, 101);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(403, 20);
            this.lblInstructions.TabIndex = 1;
            this.lblInstructions.Text = "⚠ Search and select a registered user to add to this group.";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSearch.Location = new System.Drawing.Point(17, 126);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(404, 29);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblStatusCount
            // 
            this.lblStatusCount.AutoSize = true;
            this.lblStatusCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(111)))), ((int)(((byte)(90)))));
            this.lblStatusCount.Location = new System.Drawing.Point(17, 160);
            this.lblStatusCount.Name = "lblStatusCount";
            this.lblStatusCount.Size = new System.Drawing.Size(150, 20);
            this.lblStatusCount.TabIndex = 3;
            this.lblStatusCount.Text = "0 student(s) available";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToResizeRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(17, 183);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 25;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(405, 192);
            this.dgvUsers.TabIndex = 4;
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);
            // 
            // lblSelectedUser
            // 
            this.lblSelectedUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.lblSelectedUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblSelectedUser.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblSelectedUser.Location = new System.Drawing.Point(17, 389);
            this.lblSelectedUser.Name = "lblSelectedUser";
            this.lblSelectedUser.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblSelectedUser.Size = new System.Drawing.Size(405, 27);
            this.lblSelectedUser.TabIndex = 5;
            this.lblSelectedUser.Text = "Selected: None";
            this.lblSelectedUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAddMember
            // 
            this.btnAddMember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnAddMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMember.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddMember.ForeColor = System.Drawing.Color.White;
            this.btnAddMember.Location = new System.Drawing.Point(17, 432);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(194, 37);
            this.btnAddMember.TabIndex = 6;
            this.btnAddMember.Text = "Add Member";
            this.btnAddMember.UseVisualStyleBackColor = false;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(30)))), ((int)(((byte)(49)))));
            this.btnCancel.Location = new System.Drawing.Point(227, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(194, 37);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddMemberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(439, 492);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddMember);
            this.Controls.Add(this.lblSelectedUser);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.lblStatusCount);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddMemberForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Member";
            this.Load += new System.EventHandler(this.AddMemberForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblStatusCount;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Label lblSelectedUser;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnCancel;
    }
}
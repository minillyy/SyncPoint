// ===============================
// TaskProgress.Designer.cs
// ===============================

using System.Windows.Forms;

namespace SyncPoint.Forms
{
    partial class TaskProgress
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.goldLine = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnViewProgress = new System.Windows.Forms.Button();
            this.lblMembers = new System.Windows.Forms.Label();
            this.flowMembers = new System.Windows.Forms.FlowLayoutPanel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblMemberHeader = new System.Windows.Forms.Label();
            this.lblTaskHeader = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panelTop.Controls.Add(this.goldLine);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblSubtitle);
            this.panelTop.Controls.Add(this.btnViewProgress);
            this.panelTop.Location = new System.Drawing.Point(20, 20);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(678, 220);
            this.panelTop.TabIndex = 3;
            // 
            // goldLine
            // 
            this.goldLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(155)))), ((int)(((byte)(60)))));
            this.goldLine.Location = new System.Drawing.Point(0, 216);
            this.goldLine.Name = "goldLine";
            this.goldLine.Size = new System.Drawing.Size(700, 4);
            this.goldLine.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 23F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(25, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(384, 45);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Team Transparency";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Arial", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(168)))), ((int)(((byte)(76)))));
            this.lblSubtitle.Location = new System.Drawing.Point(30, 80);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(560, 50);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Real-time updates of team performance and completed tasks.";
            // 
            // btnViewProgress
            // 
            this.btnViewProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnViewProgress.FlatAppearance.BorderSize = 0;
            this.btnViewProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewProgress.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnViewProgress.ForeColor = System.Drawing.Color.White;
            this.btnViewProgress.Location = new System.Drawing.Point(35, 150);
            this.btnViewProgress.Name = "btnViewProgress";
            this.btnViewProgress.Size = new System.Drawing.Size(599, 45);
            this.btnViewProgress.TabIndex = 3;
            this.btnViewProgress.Text = "View Team Progress";
            this.btnViewProgress.UseVisualStyleBackColor = false;
            this.btnViewProgress.Click += new System.EventHandler(this.btnViewProgress_Click);
            // 
            // lblMembers
            // 
            this.lblMembers.AutoSize = true;
            this.lblMembers.Font = new System.Drawing.Font("Arial", 1F, System.Drawing.FontStyle.Bold);
            this.lblMembers.ForeColor = System.Drawing.Color.Transparent;
            this.lblMembers.Location = new System.Drawing.Point(0, 0);
            this.lblMembers.Name = "lblMembers";
            this.lblMembers.Size = new System.Drawing.Size(0, 2);
            this.lblMembers.TabIndex = 2;
            // 
            // flowMembers
            // 
            this.flowMembers.AutoScroll = true;
            this.flowMembers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.flowMembers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMembers.Location = new System.Drawing.Point(30, 300);
            this.flowMembers.Name = "flowMembers";
            this.flowMembers.Padding = new System.Windows.Forms.Padding(10);
            this.flowMembers.Size = new System.Drawing.Size(646, 300);
            this.flowMembers.TabIndex = 0;
            this.flowMembers.WrapContents = false;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.headerPanel.Controls.Add(this.lblMemberHeader);
            this.headerPanel.Controls.Add(this.lblTaskHeader);
            this.headerPanel.Location = new System.Drawing.Point(30, 259);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(658, 40);
            this.headerPanel.TabIndex = 1;
            // 
            // lblMemberHeader
            // 
            this.lblMemberHeader.AutoSize = true;
            this.lblMemberHeader.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblMemberHeader.ForeColor = System.Drawing.Color.White;
            this.lblMemberHeader.Location = new System.Drawing.Point(25, 11);
            this.lblMemberHeader.Name = "lblMemberHeader";
            this.lblMemberHeader.Size = new System.Drawing.Size(80, 19);
            this.lblMemberHeader.TabIndex = 0;
            this.lblMemberHeader.Text = "Members";
            // 
            // lblTaskHeader
            // 
            this.lblTaskHeader.AutoSize = true;
            this.lblTaskHeader.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblTaskHeader.ForeColor = System.Drawing.Color.White;
            this.lblTaskHeader.Location = new System.Drawing.Point(483, 11);
            this.lblTaskHeader.Name = "lblTaskHeader";
            this.lblTaskHeader.Size = new System.Drawing.Size(143, 19);
            this.lblTaskHeader.TabIndex = 1;
            this.lblTaskHeader.Text = "Tasks Completed";
            // 
            // TaskProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(717, 625);
            this.Controls.Add(this.flowMembers);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.lblMembers);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TaskProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SyncPoint - Team Dashboard";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel goldLine;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnViewProgress;
        private System.Windows.Forms.Label lblMembers;
        private System.Windows.Forms.FlowLayoutPanel flowMembers;
        private Panel headerPanel;
        private Label lblMemberHeader;
        private Label lblTaskHeader;
    }
}
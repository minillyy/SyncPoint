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

            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelTop
            // 
            this.panelTop.BackColor =
                System.Drawing.Color.FromArgb(18, 35, 70);

            this.panelTop.Controls.Add(this.goldLine);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblSubtitle);
            this.panelTop.Controls.Add(this.btnViewProgress);

            this.panelTop.Location =
                new System.Drawing.Point(20, 20);

            this.panelTop.Name = "panelTop";

            this.panelTop.Size =
                new System.Drawing.Size(700, 220);

            // 
            // goldLine
            // 
            this.goldLine.BackColor =
                System.Drawing.Color.FromArgb(196, 155, 60);

            this.goldLine.Location =
                new System.Drawing.Point(0, 216);

            this.goldLine.Name = "goldLine";

            this.goldLine.Size =
                new System.Drawing.Size(700, 4);

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;

            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Arial",
                    23F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.Color.White;

            this.lblTitle.Location =
                new System.Drawing.Point(25, 25);

            this.lblTitle.Name = "lblTitle";

            this.lblTitle.Size =
                new System.Drawing.Size(430, 36);

            this.lblTitle.Text =
                "Team Transparency";

            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font =
                new System.Drawing.Font("Arial", 12F);

            this.lblSubtitle.ForeColor =
                System.Drawing.Color.FromArgb(52, 72, 94);

            this.lblSubtitle.Location =
                new System.Drawing.Point(30, 80);

            this.lblSubtitle.Name = "lblSubtitle";

            this.lblSubtitle.Size =
                new System.Drawing.Size(560, 50);

            this.lblSubtitle.Text =
                "Real-time updates of team performance and completed tasks.";

            // 
            // btnViewProgress
            // 
            this.btnViewProgress.BackColor =
                System.Drawing.Color.FromArgb(52, 73, 94);

            this.btnViewProgress.FlatAppearance.BorderSize = 0;

            this.btnViewProgress.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnViewProgress.Font =
                new System.Drawing.Font(
                    "Arial",
                    12F,
                    System.Drawing.FontStyle.Bold);

            this.btnViewProgress.ForeColor =
                System.Drawing.Color.White;

            this.btnViewProgress.Location =
                new System.Drawing.Point(35, 150);

            this.btnViewProgress.Name =
                "btnViewProgress";

            this.btnViewProgress.Size =
                new System.Drawing.Size(630, 45);

            this.btnViewProgress.Text =
                "View Team Progress";

            this.btnViewProgress.UseVisualStyleBackColor = false;

            this.btnViewProgress.Click +=
                new System.EventHandler(this.btnViewProgress_Click);

            // 
            // lblMembers
            // 
            // 
            // lblMembers
            // 
            this.lblMembers.AutoSize = true;

            this.lblMembers.Font =
                new System.Drawing.Font(
                    "Arial",
                    1F,
                    System.Drawing.FontStyle.Bold);

            this.lblMembers.ForeColor =
                System.Drawing.Color.Transparent;

            this.lblMembers.Location =
                new System.Drawing.Point(0, 0);

            this.lblMembers.Text = "";
            // 
            // HEADER PANEL
            // 
            Panel headerPanel = new Panel();

            headerPanel.BackColor =
                System.Drawing.Color.FromArgb(18, 35, 70);

            headerPanel.Location =
                new System.Drawing.Point(30, 300);

            headerPanel.Size =
                new System.Drawing.Size(690, 40);

            // 
            // MEMBER HEADER
            // 
            Label lblMemberHeader = new Label();

            lblMemberHeader.Text = "Members";

            lblMemberHeader.ForeColor =
                System.Drawing.Color.White;

            lblMemberHeader.Font =
                new System.Drawing.Font(
                    "Arial",
                    10F,
                    System.Drawing.FontStyle.Bold);

            lblMemberHeader.Location =
                new System.Drawing.Point(25, 11);

            lblMemberHeader.AutoSize = true;

            // 
            // TASK HEADER
            // 
            Label lblTaskHeader = new Label();

            lblTaskHeader.Text = "Tasks";

            lblTaskHeader.ForeColor =
                System.Drawing.Color.White;

            lblTaskHeader.Font =
                new System.Drawing.Font(
                    "Arial",
                    10F,
                    System.Drawing.FontStyle.Bold);

            lblTaskHeader.Location =
                new System.Drawing.Point(565, 11);

            lblTaskHeader.AutoSize = true;

            headerPanel.Controls.Add(lblMemberHeader);
            headerPanel.Controls.Add(lblTaskHeader);

            // 
            // flowMembers
            // 
            this.flowMembers.AutoScroll = true;

            this.flowMembers.BackColor =
                System.Drawing.Color.FromArgb(248, 246, 242);

            this.flowMembers.FlowDirection =
                System.Windows.Forms.FlowDirection.TopDown;

            this.flowMembers.Location =
                new System.Drawing.Point(30, 345);

            this.flowMembers.Name =
                "flowMembers";

            this.flowMembers.Padding =
                new System.Windows.Forms.Padding(10);

            this.flowMembers.Size =
                new System.Drawing.Size(690, 300);

            this.flowMembers.WrapContents = false;

            // 
            // TaskProgress
            // 
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.BackColor =
                System.Drawing.Color.FromArgb(248, 246, 242);

            this.ClientSize =
                new System.Drawing.Size(750, 680);

            this.Controls.Add(this.flowMembers);
            this.Controls.Add(headerPanel);
            this.Controls.Add(this.lblMembers);
            this.Controls.Add(this.panelTop);

            this.Font =
                new System.Drawing.Font("Arial", 9F);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.Name =
                "TaskProgress";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text =
                "SyncPoint - Team Dashboard";

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();

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
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Helpers;

namespace SyncPoint.Forms
{
    public partial class xzLeaderDashboardForm : Form
    {
        private readonly Color primary = ColorTranslator.FromHtml("#1a2744");
        private readonly Color accent = ColorTranslator.FromHtml("#c9a84c");
        private readonly Color bg = ColorTranslator.FromHtml("#faf7f2");

        public xzLeaderDashboardForm()
        {
            InitializeComponent();
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.BackColor = primary; 
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Cursor = Cursors.Hand;
        }

        private void LoadStats()
        {
            var tasks = DatabaseHelper.GetTasksByGroup(Session.GroupID);

            int total = tasks.Rows.Count;
            int completed = 0;
            int pending = 0;

            foreach (DataRow row in tasks.Rows)
            {
                string status = row["Status"].ToString();
                if (status == "Completed") completed++;
                if (status == "Pending") pending++;
            }

            lblTotalNum.Text = total.ToString();
            lblCompletedNum.Text = completed.ToString();
            lblPendingNum.Text = pending.ToString();
        }

        private void LoadProgress()
        {
            var tasks = DatabaseHelper.GetTasksByGroup(Session.GroupID);

            int total = tasks.Rows.Count;
            int completed = 0;

            foreach (DataRow row in tasks.Rows)
                if (row["Status"].ToString() == "Completed")
                    completed++;

            int percent = total > 0 ? (completed * 100) / total : 0;

            pbGroupProgress.Value = percent;
            lblProgressPct.Text = percent + "%";
        }

        //private void LoadMembers()
        //{
        //    var progress = DatabaseHelper.GetMemberProgress(Session.GroupID);

        //    dgvMembers.Columns.Clear();
        //    dgvMembers.DataSource = progress;

        //    if (dgvMembers.Columns.Contains("FullName"))
        //        dgvMembers.Columns["FullName"].HeaderText = "Member";
        //    if (dgvMembers.Columns.Contains("Total"))
        //        dgvMembers.Columns["Total"].HeaderText = "Tasks";
        //    if (dgvMembers.Columns.Contains("Done"))
        //        dgvMembers.Columns["Done"].HeaderText = "Done";
        //    if (dgvMembers.Columns.Contains("CompletionRate"))
        //        dgvMembers.Columns["CompletionRate"].HeaderText = "Completion %";

        //    dgvMembers.AlternatingRowsDefaultCellStyle.BackColor =
        //        ColorTranslator.FromHtml("#faf7f2");
        //}

        private void dgvDeadlines_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDeadlines.Columns[e.ColumnIndex].Name == "Status")
            {
                string status = e.Value.ToString();

                if (status == "Completed")
                    e.CellStyle.BackColor = Color.LightGreen;
                else if (status == "In Progress")
                    e.CellStyle.BackColor = Color.Khaki;
                else if (status == "Pending")
                    e.CellStyle.BackColor = Color.LightCoral;
            }
        }

        private void LoadUpcomingDeadlines()
        {
            var tasks = DatabaseHelper.GetTasksByGroup(Session.GroupID);

            DataTable dt = new DataTable();
            dt.Columns.Add("Task");
            dt.Columns.Add("Assigned To");
            dt.Columns.Add("Deadline");
            dt.Columns.Add("Status");

            foreach (DataRow row in tasks.Rows)
            {
                DateTime deadline = DateTime.Parse(row["Deadline"].ToString());
                string status = row["Status"].ToString();

                dt.Rows.Add(
                    row["Title"].ToString(),
                    row["AssignedTo"].ToString(),
                    deadline.ToString("MMM dd"),
                    status
                );
            }

            dgvDeadlines.DataSource = dt;

            dgvDeadlines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeadlines.RowHeadersVisible = false;
            dgvDeadlines.AllowUserToAddRows = false;
        }
        private void sidebarControl1_Load(object sender, EventArgs e)
        {

        }

        private void xzLeaderDashboardForm_Load(object sender, EventArgs e)
        {
            if (Session.GroupID == -1)
            {
                MessageBox.Show(
                    "You are not part of a group yet.\n" +
                    "Please create or join a group first.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            sidebarControl1.SetActive("Dashboard");

            lblUserName.Text = "Leader: " + Session.FullName;

            LoadStats();
            LoadProgress();
            LoadUpcomingDeadlines();
            LoadStatusBreakdown();
            LoadMemberProgressBars();

            lblTotalNum.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblCompletedNum.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblPendingNum.Font = new Font("Segoe UI", 24, FontStyle.Bold);

            lblTotalNum.ForeColor = primary;
            lblCompletedNum.ForeColor = primary;
            lblPendingNum.ForeColor = primary;
        }

        private void pnlTopbar_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(ColorTranslator.FromHtml("#c9a84c"), 3);
            e.Graphics.DrawLine(pen, 0, pnlTopbar.Height - 2, pnlTopbar.Width, pnlTopbar.Height - 2);
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(ColorTranslator.FromHtml("#d4c9b0"), 1);
            e.Graphics.DrawRectangle(pen, 0, 0,
            pnlStatTotal1.Width - 1, pnlStatTotal1.Height - 1);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(ColorTranslator.FromHtml("#d4c9b0"), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, pnlStatTotal2.Width - 1, pnlStatTotal2.Height - 1);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(ColorTranslator.FromHtml("#d4c9b0"), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, pnlStatTotal3.Width - 1, pnlStatTotal3.Height - 1);
        }

        private void lblTotalNum_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalLabel_Click(object sender, EventArgs e)
        {

        }

        private void lblCompletedNum_Click(object sender, EventArgs e)
        {

        }

        private void dgvMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvMembers.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1a2744");
            //dgvMembers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dgvMembers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9f, FontStyle.Bold);
            //dgvMembers.EnableHeadersVisualStyles = false;
        }

        private void dvgDeadlines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblUpcomingDeadlines_Click(object sender, EventArgs e)
        {

        }

        private void dgvDeadlines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pnlMembersCard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblUpcomingDeadlines_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void LoadStatusBreakdown()
        {
            var tasks = DatabaseHelper.GetTasksByGroup(Session.GroupID);

            int completed = 0;
            int inProgress = 0;
            int notStarted = 0;

            foreach (DataRow row in tasks.Rows)
            {
                string status = row["Status"].ToString();

                if (status == "Completed")
                    completed++;
                else if (status == "In Progress")
                    inProgress++;
                else if (status == "Pending")
                    notStarted++;
            }

            lblCompleteNum.Text = completed + " completed";
            lblInProgress.Text = inProgress + " in progress";
            lblNotStarted.Text = notStarted + " not started";

            lblCompleteNum.ForeColor = Color.Green;
            lblInProgress.ForeColor = Color.Goldenrod;
            lblNotStarted.ForeColor = Color.Red;

            lblCompleteNum.BackColor = Color.LightGreen;
            lblInProgress.BackColor = Color.LightYellow;
            lblNotStarted.BackColor = Color.MistyRose;
        }

        private void lblInProgress_Click(object sender, EventArgs e)
        {

        }

        private void LoadMemberProgressBars()
        {
            pnlMemberProgress.Controls.Clear();

            var members = DatabaseHelper.GetMemberProgress(Session.GroupID);

            if (members.Rows.Count == 0)
            {
                Label empty = new Label();
                empty.Text = "No members yet";
                empty.ForeColor = Color.Gray;
                empty.Location = new Point(20, 20);
                empty.AutoSize = true;
                pnlMemberProgress.Controls.Add(empty);
                return;
            }
            int y = 10;

            foreach (DataRow row in members.Rows)
            {
                string name = row["FullName"].ToString();
                string initials = row["Initials"].ToString(); 
                string taskDetail = row["TaskDetail"].ToString(); 
                int percent = Convert.ToInt32(row["CompletionRate"]);

                Panel avatar = new Panel();
                avatar.Size = new Size(40, 40);
                avatar.Location = new Point(10, y);
                avatar.BackColor = percent >= 70 ? Color.DarkSlateBlue :
                                   percent >= 40 ? Color.SeaGreen : Color.Brown;

                Label lblInitials = new Label();
                lblInitials.Text = initials;
                lblInitials.ForeColor = Color.White;
                lblInitials.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblInitials.AutoSize = false;
                lblInitials.Size = avatar.Size;
                lblInitials.TextAlign = ContentAlignment.MiddleCenter;
                avatar.Controls.Add(lblInitials);

                Label lblName = new Label();
                lblName.Text = name;
                lblName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblName.Location = new Point(60, y);
                lblName.AutoSize = true;

                Label lblDetail = new Label();
                lblDetail.Text = taskDetail;
                lblDetail.Font = new Font("Segoe UI", 8);
                lblDetail.ForeColor = Color.Gray;
                lblDetail.Location = new Point(60, y + 22);
                lblDetail.AutoSize = true;

                ProgressBar pb = new ProgressBar();
                pb.Value = percent;
                pb.Location = new Point(60, y + 42);
                pb.Size = new Size(550, 12);

                Label lblPct = new Label();
                lblPct.Text = percent + "%";
                lblPct.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblPct.ForeColor = Color.White;
                lblPct.TextAlign = ContentAlignment.MiddleCenter;
                lblPct.Size = new Size(45, 22);
                lblPct.Location = new Point(620, y + 5);
                lblPct.BackColor = percent >= 70 ? Color.DarkSlateBlue :
                                   percent >= 40 ? Color.SeaGreen : Color.Brown;

                pnlMemberProgress.Controls.Add(avatar);
                pnlMemberProgress.Controls.Add(lblName);
                pnlMemberProgress.Controls.Add(lblDetail);
                pnlMemberProgress.Controls.Add(pb);
                pnlMemberProgress.Controls.Add(lblPct);

                y += 80;
            }
        }

        private void lblUserName_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStats();
            LoadProgress();
            LoadStatusBreakdown();
            LoadUpcomingDeadlines();
            LoadMemberProgressBars();
        }

            private void btnRefresh_MouseEnter(object sender, EventArgs e)
        {
            btnRefresh.BackColor = Color.FromArgb(0, 26, 39, 68);
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            btnRefresh.BackColor = SystemColors.Control;
        }

    }
}   
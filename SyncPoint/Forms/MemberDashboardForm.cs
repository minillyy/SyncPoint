using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Helpers;

namespace SyncPoint.Forms
{
    public partial class MemberDashboardForm : Form
    {
        public MemberDashboardForm()
        {
            InitializeComponent();
        }

        private void MemberDashboardForm_Load(object sender, EventArgs e)
        {
            sidebarControl1.SetActive("Dashboard");

            lblUserName.Text = "Member: " + Session.FullName;

            LoadStats();
            LoadTasks();
            LoadProgress();
        }

        private void LoadStats()
        {
            DataTable tasks = DatabaseHelper.GetTasksByMember(Session.UserID);

            int total = tasks.Rows.Count;
            int completed = 0;
            int pending = 0;

            foreach (DataRow row in tasks.Rows)
            {
                string status = row["Status"].ToString();

                if (status == "Completed")
                    completed++;
                else
                    pending++;
            }

            lblTotalNum.Text = total.ToString();
            lblCompletedNum.Text = completed.ToString();
            lblPendingNum.Text = pending.ToString();
        }

        private void LoadTasks()
        {
            DataTable dt = DatabaseHelper.GetTasksByMember(Session.UserID);

            dgvTasks.Columns.Clear();
            dgvTasks.DataSource = dt;

            if (dgvTasks.Columns.Contains("TaskID"))
                dgvTasks.Columns["TaskID"].Visible = false;

            if (dgvTasks.Columns.Contains("Title"))
                dgvTasks.Columns["Title"].HeaderText = "Task";

            if (dgvTasks.Columns.Contains("Description"))
                dgvTasks.Columns["Description"].HeaderText = "Description";

            if (dgvTasks.Columns.Contains("Deadline"))
                dgvTasks.Columns["Deadline"].HeaderText = "Deadline";

            if (dgvTasks.Columns.Contains("Difficulty"))
                dgvTasks.Columns["Difficulty"].HeaderText = "Difficulty";

            if (dgvTasks.Columns.Contains("TaskType"))
                dgvTasks.Columns["TaskType"].HeaderText = "Type";

            if (dgvTasks.Columns.Contains("Status"))
                dgvTasks.Columns["Status"].HeaderText = "Status";

            dgvTasks.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#faf7f2");

            dgvTasks.ColumnHeadersDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#1a2744");

            dgvTasks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTasks.EnableHeadersVisualStyles = false;
        }

        private void LoadProgress()
        {
            DataTable tasks = DatabaseHelper.GetTasksByMember(Session.UserID);

            int total = tasks.Rows.Count;
            int completed = 0;

            foreach (DataRow row in tasks.Rows)
            {
                if (row["Status"].ToString() == "Completed")
                    completed++;
            }

            int percent = total > 0 ? (completed * 100) / total : 0;

            pbMyProgress.Value = percent;
            lblProgressPct.Text = percent + "%";
        }

        private void btnStartTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.CurrentRow == null) return;

            int taskID = Convert.ToInt32(
                dgvTasks.CurrentRow.Cells["TaskID"].Value);

            DatabaseHelper.UpdateTaskStatus(taskID, "In Progress");

            LoadStats();
            LoadTasks();
            LoadProgress();

            MessageBox.Show("Task started.");
        }

        private void btnCompleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.CurrentRow == null) return;

            int taskID = Convert.ToInt32(
                dgvTasks.CurrentRow.Cells["TaskID"].Value);

            DatabaseHelper.UpdateTaskStatus(taskID, "Completed");

            LoadStats();
            LoadTasks();
            LoadProgress();

            MessageBox.Show("Task completed.");
        }

        private void pnlTopbar_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(
                ColorTranslator.FromHtml("#c9a84c"), 3);

            e.Graphics.DrawLine(
                pen,
                0,
                pnlTopbar.Height - 2,
                pnlTopbar.Width,
                pnlTopbar.Height - 2);
        }
    }
}
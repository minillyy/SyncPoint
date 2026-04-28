using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Helpers;

namespace SyncPoint.Forms
{
    public partial class MyTasksForm : Form
    {
        public MyTasksForm()
        {
            InitializeComponent();
        }

        private void MyTasksForm_Load(object sender, EventArgs e)
        {
            sidebarControl1.SetActive("My Tasks");

            lblUserName.Text = "Member: " + Session.FullName;

            LoadStats();
            LoadTasks();
        }

        private void LoadStats()
        {
            DataTable dt =
                DatabaseHelper.GetTasksByMember(Session.UserID);

            int total = dt.Rows.Count;
            int pending = 0;
            int completed = 0;

            foreach (DataRow row in dt.Rows)
            {
                string status =
                    row["Status"].ToString();

                if (status == "Completed")
                    completed++;
                else
                    pending++;
            }

            lblTotalTasks.Text =
                total.ToString();

            lblPending.Text =
                pending.ToString();

            lblCompleted.Text =
                completed.ToString();
        }

        private void LoadTasks()
        {
            DataTable dt =
                DatabaseHelper.GetTasksByMember(Session.UserID);

            dgvTasks.Columns.Clear();
            dgvTasks.DataSource = dt;

            if (dgvTasks.Columns.Contains("TaskID"))
                dgvTasks.Columns["TaskID"].Visible = false;

            if (dgvTasks.Columns.Contains("Title"))
                dgvTasks.Columns["Title"].HeaderText = "Task";

            if (dgvTasks.Columns.Contains("Deadline"))
                dgvTasks.Columns["Deadline"].HeaderText = "Deadline";

            if (dgvTasks.Columns.Contains("Difficulty"))
                dgvTasks.Columns["Difficulty"].HeaderText = "Difficulty";

            if (dgvTasks.Columns.Contains("Status"))
                dgvTasks.Columns["Status"].HeaderText = "Status";

            dgvTasks.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#faf7f2");

            dgvTasks.ColumnHeadersDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#1a2744");

            dgvTasks.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvTasks.EnableHeadersVisualStyles = false;
        }

        private void btnStartTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.CurrentRow == null)
                return;

            int taskID =
                Convert.ToInt32(
                    dgvTasks.CurrentRow.Cells["TaskID"].Value);

            DatabaseHelper.UpdateTaskStatus(
                taskID,
                "In Progress");

            LoadStats();
            LoadTasks();

            MessageBox.Show("Task started.");
        }

        private void btnCompleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.CurrentRow == null)
                return;

            int taskID =
                Convert.ToInt32(
                    dgvTasks.CurrentRow.Cells["TaskID"].Value);

            DatabaseHelper.UpdateTaskStatus(
                taskID,
                "Completed");

            LoadStats();
            LoadTasks();

            MessageBox.Show("Task completed.");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStats();
            LoadTasks();
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

        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
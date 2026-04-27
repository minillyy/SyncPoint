using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Helpers;

namespace SyncPoint.Forms
{
    public partial class TaskManagementForm : Form
    {
        public TaskManagementForm()
        {
            InitializeComponent();
        }

        private void TaskManagementForm_Load(object sender, EventArgs e)
        {
            sidebarControl1.SetActive("Tasks");

            lblUserName.Text = "Leader: " + Session.FullName;

            LoadStats();
            LoadTasks();
        }

        private void LoadStats()
        {
            DataTable dt = DatabaseHelper.GetTasksByGroup(Session.GroupID);

            int total = dt.Rows.Count;
            int pending = 0;
            int progress = 0;
            int completed = 0;

            foreach (DataRow row in dt.Rows)
            {
                string status = row["Status"].ToString();

                if (status == "Pending")
                    pending++;
                else if (status == "In Progress")
                    progress++;
                else if (status == "Completed")
                    completed++;
            }

            lblTotalNum.Text = total.ToString();
            lblPendingNum.Text = pending.ToString();
            lblProgressNum.Text = progress.ToString();
            lblCompletedNum.Text = completed.ToString();
        }

        private void LoadTasks()
        {
            DataTable dt = DatabaseHelper.GetTasksByGroup(Session.GroupID);

            dgvTasks.Columns.Clear();
            dgvTasks.DataSource = dt;

            if (dgvTasks.Columns.Contains("TaskID"))
                dgvTasks.Columns["TaskID"].Visible = false;

            dgvTasks.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#faf7f2");

            dgvTasks.ColumnHeadersDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#1a2744");

            dgvTasks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTasks.EnableHeadersVisualStyles = false;
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            CreateTaskForm frm = new CreateTaskForm();
            frm.ShowDialog();

            LoadStats();
            LoadTasks();
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
using SyncPoint.Data;
using SyncPoint.Forms.Auth;
using SyncPoint.Forms.Other_Forms;
using SyncPoint.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncPoint.Forms.Dashboards
{
    public partial class LeaderDashboardForm : Form
    {
        public LeaderDashboardForm()
        {
            InitializeComponent();
            sidebarControl1.AddTaskClicked += SidebarControl1_AddTaskClicked;
            sidebarControl1.MembersClicked += SidebarControl1_MembersClicked;
            sidebarControl1.ReportsClicked += SidebarControl1_ReportsClicked;
            dgvMembers.CellFormatting += dgvMembers_CellFormatting;
        }

        private void dgvMembers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Make sure we are looking at the Status column
            if (dgvMembers.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();

                // Apply colors based on the text
                if (status == "Completed")
                    e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96); // Green
                else if (status == "In Progress")
                    e.CellStyle.ForeColor = Color.FromArgb(41, 128, 185); // Blue
                else if (status == "Pending Review")
                    e.CellStyle.ForeColor = Color.FromArgb(155, 89, 182); // Purple
                else if (status == "Pending")
                    e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34); // Orange

                e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            }
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

        private void LoadMyWorkspace()
        {
            if (this.DesignMode) return;

            dgvMembers.Columns.Clear();
            dgvMembers.Rows.Clear();

            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "TaskID", Visible = false });
            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "Task Title", FillWeight = 25 });
            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Description", FillWeight = 35 });
            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Deadline", HeaderText = "Due Date", FillWeight = 20 });
            dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", FillWeight = 20 });

            var tasks = DatabaseHelper.GetTasksByMember(Session.UserID);
            foreach (DataRow row in tasks.Rows)
            {
                dgvMembers.Rows.Add(
                    row["TaskID"],
                    row["Title"],
                    row["Description"],
                    Convert.ToDateTime(row["Deadline"]).ToString("MMM dd, yyyy"),
                    row["Status"]
                );
            }

            dgvMembers.AllowUserToResizeColumns = false;
            dgvMembers.AllowUserToResizeRows = false;
            dgvMembers.RowHeadersVisible = false;
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.MultiSelect = false;
            dgvMembers.ReadOnly = true;
            dgvMembers.EnableHeadersVisualStyles = false;
            dgvMembers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMembers.ColumnHeadersHeight = 45;

            dgvMembers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvMembers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMembers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            dgvMembers.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(44, 62, 80);

            dgvMembers.DefaultCellStyle.BackColor = Color.White;
            dgvMembers.DefaultCellStyle.ForeColor = Color.Black;
            dgvMembers.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvMembers.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvMembers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 240);
            dgvMembers.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 245, 240);
            dgvMembers.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;

            dgvMembers.GridColor = Color.FromArgb(235, 235, 235);
            dgvMembers.RowTemplate.Height = 45;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvMembers.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void sidebarControl1_Load(object sender, EventArgs e)
        {
            // Intentionally left blank. AddTask is handled via AddTaskClicked event.
        }

        private void SidebarControl1_AddTaskClicked(object sender, EventArgs e)
        {
            int activeGroupId = Session.GroupID;
            string currentLeaderName = Session.FullName;

            using (AddTaskForm popUp = new AddTaskForm(activeGroupId, currentLeaderName))
            {
                if (popUp.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Task assigned and saved to database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadStats();
                    LoadMyWorkspace();
                }
            }
        }

        private void SidebarControl1_MembersClicked(object sender, EventArgs e)
        {
            using (TaskProgress progressForm = new TaskProgress())
            {
                progressForm.ShowDialog(this);

                LoadStats();
                LoadMyWorkspace();
            }
        }

        private void SidebarControl1_ReportsClicked(object sender, EventArgs e)
        {
            using (ReportsForm reports = new ReportsForm())
            {
                reports.ShowDialog(this);
            }
        }

        private void LeaderDashboardForm_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

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
            LoadMyWorkspace();
        }

        private void pnlTopbar_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(ColorTranslator.FromHtml("#c9a84c"), 3);
            e.Graphics.DrawLine(pen, 0, pnlTopbar.Height - 2, pnlTopbar.Width, pnlTopbar.Height - 2);
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

        private void dgvMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvMembers.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1a2744");
            dgvMembers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMembers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9f, FontStyle.Bold);
            dgvMembers.EnableHeadersVisualStyles = false;
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            using (AddMemberForm popUp = new AddMemberForm(Session.GroupID, Session.GroupName))
            {
                popUp.ShowDialog();
                LoadMyWorkspace();
            }
        }

        private void btnReviewSubmissions_Click(object sender, EventArgs e)
        {
            using (LeaderReviewForm reviewForm = new LeaderReviewForm())
            {
                reviewForm.ShowDialog();
            }
            LoadStats();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow == null) return;

            int taskId = Convert.ToInt32(dgvMembers.CurrentRow.Cells["TaskID"].Value);
            string title = dgvMembers.CurrentRow.Cells["Title"].Value.ToString();
            string status = dgvMembers.CurrentRow.Cells["Status"].Value.ToString();

            if (status != "In Progress")
            {
                MessageBox.Show("You can only submit tasks that are 'In Progress'.", "SyncPoint");
                return;
            }

            using (var submitForm = new SubmitTaskForm(title))
            {
                if (submitForm.ShowDialog() == DialogResult.OK)
                {
                    if (DatabaseHelper.SubmitTask(taskId, submitForm.SubmissionLink))
                    {
                        MessageBox.Show("Work submitted! Since you are the leader, don't forget to approve it in the Review section.");
                        LoadMyWorkspace();
                        LoadStats();
                    }
                }
            }
        }

        private void lblTasks_Click(object sender, EventArgs e)
        {
            using (TasksForm tasksWindow = new TasksForm())
            {
                tasksWindow.ShowDialog();
            }
            
            LoadStats();
            LoadMyWorkspace();
        }
    }
}

using SyncPoint.Data;
using SyncPoint.Forms.Other_Forms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SyncPoint.Forms.Dashboards
{
    public partial class MemberDashboardForm : Form
    {
        public MemberDashboardForm()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                SetupDataGridViewColumns();
                AttachEvents();
            }
        }

        private void AttachEvents()
        {
            this.Load += MemberDashboardForm_Load;
            lblLogout.Click += LblLogout_Click;
            lblLogout.Cursor = Cursors.Hand;

            pnlTopbar.Paint += (s, pe) =>
            {
                using (var pen = new Pen(Color.FromArgb(210, 210, 210), 1))
                {
                    pe.Graphics.DrawLine(pen, 0, pnlTopbar.Height - 1, pnlTopbar.Width, pnlTopbar.Height - 1);
                }
            };
        }

        private void SetupDataGridViewColumns()
        {
            dgvMyTasks.Columns.Clear();

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn { Name = "TaskID", Visible = false });

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Task Title",
                ReadOnly = true,
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            });

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Description",
                ReadOnly = true,
                FillWeight = 40
            });

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Deadline",
                HeaderText = "Due Date",
                ReadOnly = true,
                FillWeight = 22,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                ReadOnly = true,
                FillWeight = 18,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            foreach (DataGridViewColumn column in dgvMyTasks.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgvMyTasks.AllowUserToResizeColumns = false;
            dgvMyTasks.AllowUserToResizeRows = false;
            dgvMyTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMyTasks.RowHeadersVisible = false;
            dgvMyTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvMyTasks.EnableHeadersVisualStyles = false;
            dgvMyTasks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvMyTasks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMyTasks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            dgvMyTasks.ColumnHeadersHeight = 45;

            dgvMyTasks.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 73, 94);
            dgvMyTasks.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgvMyTasks.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvMyTasks.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvMyTasks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 240);
            dgvMyTasks.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 245, 240);
            dgvMyTasks.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;

            dgvMyTasks.RowTemplate.Height = 45;
            dgvMyTasks.CellFormatting += DgvMyTasks_CellFormatting;
        }

        private void DgvMyTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.ForeColor = Color.Black;

            if (dgvMyTasks.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Completed") e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
                else if (status == "In Progress") e.CellStyle.ForeColor = Color.FromArgb(41, 128, 185);
                else if (status == "Pending") e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34);

                e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            }
        }

        private void MemberDashboardForm_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            lblUserName.Text = Session.FullName;
            lblUserRole.Text = "Member";

            if (Session.GroupID == -1) return;
            RefreshAllData();
        }

        private void RefreshAllData()
        {
            LoadDashboardStats();
            LoadMyTasks();
        }

        private void LoadDashboardStats()
        {
            var allGroupTasks = DatabaseHelper.GetTasksByGroup(Session.GroupID);

            int total = allGroupTasks.Rows.Count;
            int completedCount = 0;
            int pendingAvailableCount = 0;

            foreach (DataRow row in allGroupTasks.Rows)
            {
                string status = row["Status"].ToString();

                if (status == "Completed")
                {
                    completedCount++;
                }
                else if (status == "Pending")
                {
                    pendingAvailableCount++;
                }
            }

            lblTotalNum.Text = total.ToString();          
            lblCompletedNum.Text = completedCount.ToString();

            lblInProgressNum.Text = pendingAvailableCount.ToString();
        }

        private void LoadMyTasks()
        {
            dgvMyTasks.Rows.Clear();
            var tasks = DatabaseHelper.GetTasksByMember(Session.UserID);

            foreach (DataRow row in tasks.Rows)
            {
                DateTime deadline = Convert.ToDateTime(row["Deadline"]);
                string deadlineText = deadline.ToString("MMM dd, yyyy");

                if ((deadline - DateTime.Now).Days <= 3 && (deadline - DateTime.Now).Days >= 0)
                {
                    deadlineText = "⚠️ " + deadlineText;
                }

                dgvMyTasks.Rows.Add(
                    row["TaskID"],
                    row["Title"],
                    row["Description"],
                    deadlineText,
                    row["Status"]
                );
            }
        }

        private void LblLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Session.Clear();
                this.Close();
            }
        }

        private void lblTasks_Click(object sender, EventArgs e)
        {
            var myTasks = DatabaseHelper.GetTasksByMember(Session.UserID);
            foreach (DataRow row in myTasks.Rows)
            {
                string status = row["Status"].ToString();
                if (status == "In Progress" || status == "Pending Review")
                {
                    MessageBox.Show("You cannot accept new tasks until your current work is Approved or Returned.", "SyncPoint - Task Limit");
                    return; 
                }
            }

            using (TasksForm tasksWindow = new TasksForm())
            {
                tasksWindow.ShowDialog();
            }

            RefreshAllData();
        }

        private void btnSubmitTask_Click(object sender, EventArgs e)
        {
            if (dgvMyTasks.CurrentRow == null) return;

            int taskId = Convert.ToInt32(dgvMyTasks.CurrentRow.Cells["TaskID"].Value);
            string taskTitle = dgvMyTasks.CurrentRow.Cells["Title"].Value.ToString();
            string status = dgvMyTasks.CurrentRow.Cells["Status"].Value.ToString();

            if (status != "In Progress")
            {
                MessageBox.Show("Only 'In Progress' tasks can be submitted.", "SyncPoint");
                return;
            }

            using (var submitForm = new SubmitTaskForm(taskTitle))
            {
                if (submitForm.ShowDialog() == DialogResult.OK)
                {
                    string link = submitForm.SubmissionLink;
                    bool success = DatabaseHelper.SubmitTask(taskId, link);

                    if (success)
                    {
                        MessageBox.Show("Work submitted for review!", "Success");
                        RefreshAllData();
                    }
                }
            }
        }
    }
}
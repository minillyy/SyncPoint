using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint.Forms
{
    public partial class MemberDashboardForm : Form
    {
        public MemberDashboardForm()
        {
            InitializeComponent();

            // Avoid running runtime-only initialization while the form is opened in the WinForms designer.
            // LicenseManager.UsageMode is a reliable way to detect design-time vs run-time here.
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                PopulateStatusDropdown();
                SetupDataGridViewColumns();
                AttachEvents();
            }
        }

        private void AttachEvents()
        {
            this.Load += MemberDashboardForm_Load;
            btnUpdateStatus.Click += BtnUpdateStatus_Click;
            btnRefresh.Click += BtnRefresh_Click;
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

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaskID",
                Visible = false
            });

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Task Title",
                ReadOnly = true,
                FillWeight = 30,
                DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10f, FontStyle.Bold) }
            });

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Description",
                ReadOnly = true,
                FillWeight = 35
            });

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Deadline",
                HeaderText = "Due Date",
                ReadOnly = true,
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Difficulty",
                HeaderText = "Level",
                ReadOnly = true,
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvMyTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                ReadOnly = true,
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvMyTasks.EnableHeadersVisualStyles = false;
            dgvMyTasks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvMyTasks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMyTasks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            dgvMyTasks.ColumnHeadersHeight = 45;
            dgvMyTasks.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvMyTasks.DefaultCellStyle.Padding = new Padding(8);
            dgvMyTasks.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 242, 255);
            dgvMyTasks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 240);
            dgvMyTasks.RowTemplate.Height = 45;
            dgvMyTasks.CellFormatting += DgvMyTasks_CellFormatting;
        }

        private void DgvMyTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMyTasks.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Completed")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
                    e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }
                else if (status == "In Progress")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(41, 128, 185);
                    e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }
                else if (status == "Pending")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                    e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }
            }

            if (dgvMyTasks.Columns[e.ColumnIndex].Name == "Difficulty" && e.Value != null)
            {
                string difficulty = e.Value.ToString();
                switch (difficulty)
                {
                    case "Easy":
                        e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
                        break;
                    case "Medium":
                        e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                        break;
                    case "Hard":
                        e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
                        break;
                }
            }
        }

        private void MemberDashboardForm_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Session.FullName;
            lblUserRole.Text = "Member";

            if (Session.GroupID == -1)
            {
                MessageBox.Show("You are not yet assigned to any group.\n\nPlease wait for your Instructor to add you to a group.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RefreshAllData();
        }

        private void RefreshAllData()
        {
            LoadDashboardStats();
            LoadMyTasks();
        }

        private void LoadDashboardStats()
        {
            if (Session.GroupID == -1) return;

            var tasks = DatabaseHelper.GetTasksByMember(Session.UserID);
            int total = tasks.Rows.Count;
            int completed = 0;
            int inProgress = 0;

            foreach (DataRow row in tasks.Rows)
            {
                string status = row["Status"].ToString();
                if (status == "Completed") completed++;
                else if (status == "In Progress") inProgress++;
            }

            lblTotalNum.Text = total.ToString();
            lblCompletedNum.Text = completed.ToString();
            lblInProgressNum.Text = inProgress.ToString();

            int percent = total > 0 ? (completed * 100) / total : 0;
            pbMyProgress.Value = percent;
            lblProgressPercent.Text = $"{percent}% Complete";

            AnimateProgressBar(percent);
        }

        private async void AnimateProgressBar(int targetValue)
        {
            int current = pbMyProgress.Value;
            int step = targetValue > current ? 5 : -5;
            for (int i = current; step > 0 ? i <= targetValue : i >= targetValue; i += step)
            {
                pbMyProgress.Value = Math.Max(0, Math.Min(100, i));
                await System.Threading.Tasks.Task.Delay(10);
            }
            pbMyProgress.Value = targetValue;
        }

        private void LoadMyTasks()
        {
            dgvMyTasks.Rows.Clear();

            if (Session.GroupID == -1)
            {
                lblTaskCount.Text = "0 tasks assigned";
                return;
            }

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
                    row["Difficulty"],
                    row["Status"]
                );
            }
            lblTaskCount.Text = $"{tasks.Rows.Count} task{(tasks.Rows.Count != 1 ? "s" : "")} assigned";
        }

        private void PopulateStatusDropdown()
        {
            cmbNewStatus.Items.Clear();
            cmbNewStatus.Items.Add("Pending");
            cmbNewStatus.Items.Add("In Progress");
            cmbNewStatus.Items.Add("Completed");
            cmbNewStatus.SelectedIndex = 0;
        }

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvMyTasks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task first.", "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbNewStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a new status.", "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int taskID = Convert.ToInt32(dgvMyTasks.SelectedRows[0].Cells["TaskID"].Value);
            string currentStatus = dgvMyTasks.SelectedRows[0].Cells["Status"].Value.ToString();
            string newStatus = cmbNewStatus.SelectedItem.ToString();

            if (currentStatus == newStatus)
            {
                MessageBox.Show($"Task is already marked as '{newStatus}'.", "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Update task status from '{currentStatus}' to '{newStatus}'?",
                "SyncPoint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                DatabaseHelper.UpdateTaskStatus(taskID, newStatus);

                if (newStatus == "Completed")
                {
                    var tasks = DatabaseHelper.GetTasksByMember(Session.UserID);
                    foreach (DataRow row in tasks.Rows)
                    {
                        if (Convert.ToInt32(row["TaskID"]) == taskID)
                        {
                            DateTime deadline = Convert.ToDateTime(row["Deadline"]);
                            int difficulty = Convert.ToInt32(row["Difficulty"]);
                            DatabaseHelper.RecordScore(taskID, Session.UserID, deadline, difficulty);
                            break;
                        }
                    }
                }

                MessageBox.Show($"Task status updated to '{newStatus}'!", "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllData();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshAllData();
            btnRefresh.Text = "✓ Refreshed!";
            btnRefresh.Enabled = false;
            System.Threading.Tasks.Task.Delay(1500).ContinueWith(_ =>
            {
                this.Invoke(new Action(() =>
                {
                    btnRefresh.Text = "⟳ Refresh";
                    btnRefresh.Enabled = true;
                }));
            });
        }

        private void LblLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                "SyncPoint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Session.Clear();
                this.Close();
            }
        }
    }
}
using SyncPoint.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncPoint.Forms.Other_Forms
{
    public partial class LeaderReviewForm : Form
    {
        public LeaderReviewForm()
        {
            InitializeComponent();
            SetupHeader();
            SetupGrid();
            LoadSubmissions();
        }

        private void SetupHeader()
        {
            pnlHeader.BackColor = Color.FromArgb(44, 62, 80);
            lblTitle.ForeColor = Color.White;
            lblTitle.Text = "Submission Review Queue";
            lblInfo.Text = "Review member work below. Click the link to verify, then Approve to award points.";
            lblInfo.ForeColor = Color.FromArgb(200, 200, 200);
        }

        private void SetupGrid()
        {
            dgvReview.Columns.Clear();
            dgvReview.DataSource = null;

            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "TaskID", Visible = false });
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "UserID", Visible = false });
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "Member", HeaderText = "Member", FillWeight = 20 });
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "Task Title", FillWeight = 25 });
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "Points", HeaderText = "Pts", FillWeight = 10 });

            DataGridViewLinkColumn linkCol = new DataGridViewLinkColumn
            {
                Name = "WorkLink",
                HeaderText = "Submission Link",
                FillWeight = 30,
                ActiveLinkColor = Color.FromArgb(41, 128, 185),
                LinkBehavior = LinkBehavior.HoverUnderline,
                LinkColor = Color.FromArgb(41, 128, 185),
                TrackVisitedState = false
            };
            dgvReview.Columns.Add(linkCol);
            dgvReview.Columns.Add(new DataGridViewTextBoxColumn { Name = "Date", HeaderText = "Submitted On", FillWeight = 15 });

            dgvReview.AllowUserToResizeColumns = false;
            dgvReview.AllowUserToResizeRows = false;
            dgvReview.RowHeadersVisible = false;
            dgvReview.AllowUserToAddRows = false;
            dgvReview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvReview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvReview.EnableHeadersVisualStyles = false;
            dgvReview.ColumnHeadersHeight = 45;
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(44, 62, 80);
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            headerStyle.SelectionBackColor = Color.FromArgb(44, 62, 80);
            headerStyle.SelectionForeColor = Color.White;
            dgvReview.ColumnHeadersDefaultCellStyle = headerStyle;

            dgvReview.BackgroundColor = Color.White;
            dgvReview.GridColor = Color.FromArgb(235, 235, 235);
            dgvReview.BorderStyle = BorderStyle.None;

            DataGridViewCellStyle bodyStyle = new DataGridViewCellStyle();
            bodyStyle.BackColor = Color.White;
            bodyStyle.ForeColor = Color.Black;

            bodyStyle.SelectionBackColor = Color.FromArgb(235, 245, 255);
            bodyStyle.SelectionForeColor = Color.Black;

            bodyStyle.Font = new Font("Segoe UI", 9);
            dgvReview.DefaultCellStyle = bodyStyle;

            dgvReview.MultiSelect = false;
            dgvReview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvReview.RowTemplate.Height = 50;

            foreach (DataGridViewColumn col in dgvReview.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void LoadSubmissions()
        {
            dgvReview.Rows.Clear();
            DataTable dt = DatabaseHelper.GetPendingSubmissions(Session.GroupID);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dgvReview.Rows.Add(
                        row["TaskID"],
                        row["UserID"],
                        row["Member"],
                        row["Title"],
                        row["Points"],
                        row["Work Link"],
                        Convert.ToDateTime(row["Date Submitted"]).ToString("MMM dd, HH:mm")
                    );
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvReview.CurrentRow == null)
            {
                MessageBox.Show("Please select a submission to return.");
                return;
            }

            int taskId = Convert.ToInt32(dgvReview.CurrentRow.Cells["TaskID"].Value);
            string member = dgvReview.CurrentRow.Cells["Member"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to return this task to {member}?\n\nThe task will go back to their active workspace for revision.",
                "Return to Member",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (DatabaseHelper.ReturnTaskToMember(taskId))
                {
                    MessageBox.Show("Task returned successfully.");
                    LoadSubmissions();
                }
                else
                {
                    MessageBox.Show("Error returning task. Please try again.");
                }
            }
        }

        private void dgvReview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvReview.Columns[e.ColumnIndex].Name == "WorkLink")
            {
                string url = dgvReview.Rows[e.RowIndex].Cells["WorkLink"].Value.ToString();

                if (!string.IsNullOrWhiteSpace(url) && url.StartsWith("http"))
                {
                    try
                    {
                        // This command tells Windows: "Open this URL with the default browser"
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not open the browser. Error: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("The member provided an invalid link format.");
                }
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvReview.CurrentRow == null) return;

            int taskId = Convert.ToInt32(dgvReview.CurrentRow.Cells["TaskID"].Value);
            int userId = Convert.ToInt32(dgvReview.CurrentRow.Cells["UserID"].Value);
            int pts = Convert.ToInt32(dgvReview.CurrentRow.Cells["Points"].Value);
            string member = dgvReview.CurrentRow.Cells["Member"].Value.ToString();

            DialogResult confirm = MessageBox.Show($"Approve submission and award {pts} points to {member}?", "Confirm Review", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                if (DatabaseHelper.ApproveTask(taskId, userId, pts))
                {
                    MessageBox.Show("Task Completed! Member has been scored.", "SyncPoint");
                    LoadSubmissions();
                }
            }
        }
    }
}

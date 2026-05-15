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
using SyncPoint.Forms.Auth;

namespace SyncPoint.Forms.Dashboards
{
    public partial class LeaderDashboardForm : Form
    {
        public LeaderDashboardForm()
        {
            InitializeComponent();
            sidebarControl1.AddTaskClicked += SidebarControl1_AddTaskClicked;

            // ADD THIS LINE TO LINK THE MEMBERS BUTTON:
            sidebarControl1.MembersClicked += SidebarControl1_MembersClicked;
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
        }

        private void LoadMembers()
        {
            // Safety check for the Designer
            if (this.DesignMode) return;

            // 1. Fetch Fresh Data
            var progress = DatabaseHelper.GetMemberProgress(Session.GroupID);

            // 2. Clear and Bind Data
            dgvMembers.DataSource = null;
            dgvMembers.RowTemplate.Height = 40;
            dgvMembers.DataSource = progress;

            // 3. Header Sizing (Mode must be set before Height)
            dgvMembers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMembers.ColumnHeadersHeight = 50;

            // 4. Remove unwanted "Completion %" column
            if (dgvMembers.Columns.Contains("CompletionRate"))
            {
                dgvMembers.Columns.Remove("CompletionRate");
            }

            // 5. Header Texts, Alignment, and DISABLE SORTING
            foreach (DataGridViewColumn column in dgvMembers.Columns)
            {
                // This removes the arrow and prevents clicking from shuffling rows
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (dgvMembers.Columns.Contains("FullName"))
            {
                dgvMembers.Columns["FullName"].HeaderText = "Member";
                dgvMembers.Columns["FullName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dgvMembers.Columns.Contains("Total"))
            {
                dgvMembers.Columns["Total"].HeaderText = "Tasks";
                dgvMembers.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvMembers.Columns.Contains("Done"))
            {
                dgvMembers.Columns["Done"].HeaderText = "Done";
                dgvMembers.Columns["Done"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // 6. Header Styling & Invisible Header Highlight
            dgvMembers.EnableHeadersVisualStyles = false;
            dgvMembers.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1a2744");
            dgvMembers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMembers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10f, FontStyle.Bold);
            dgvMembers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMembers.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#1a2744");
            dgvMembers.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // 7. Body Styling & Invisible Row Highlights
            dgvMembers.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvMembers.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvMembers.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#faf7f2");
            dgvMembers.AlternatingRowsDefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#faf7f2");
            dgvMembers.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;

            // 8. General Grid Cleanup
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.AllowUserToResizeColumns = false;
            dgvMembers.AllowUserToResizeRows = false;
            dgvMembers.RowHeadersVisible = false;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 9. Force row height update
            foreach (DataGridViewRow row in dgvMembers.Rows)
            {
                row.Height = 40;
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

                    // Refresh dashboard stats
                    LoadStats();
                    LoadProgress();
                    LoadMembers();
                }
            }
        }

        private void SidebarControl1_MembersClicked(object sender, EventArgs e)
        {
            // We use 'using' to ensure the form is properly disposed of after closing
            using (TaskProgress progressForm = new TaskProgress())
            {
                // ShowDialog opens it as a popup window
                progressForm.ShowDialog(this);

                // Optional: Refresh the dashboard stats when the user closes the progress form
                // in case any status updates happened.
                LoadStats();
                LoadMembers();
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
            LoadProgress();
            LoadMembers();
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
                LoadMembers();
            }
        }
    }
}

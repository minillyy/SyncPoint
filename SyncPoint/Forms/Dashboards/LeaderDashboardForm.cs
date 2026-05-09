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
            // Subscribe to sidebar control Add Task clicks so the form can open the AddTaskForm
            sidebarControl1.AddTaskClicked += SidebarControl1_AddTaskClicked;
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
            var progress = DatabaseHelper.GetMemberProgress(Session.GroupID);

            dgvMembers.Columns.Clear();
            dgvMembers.DataSource = progress;

            if (dgvMembers.Columns.Contains("FullName"))
                dgvMembers.Columns["FullName"].HeaderText = "Member";
            if (dgvMembers.Columns.Contains("Total"))
                dgvMembers.Columns["Total"].HeaderText = "Tasks";
            if (dgvMembers.Columns.Contains("Done"))
                dgvMembers.Columns["Done"].HeaderText = "Done";
            if (dgvMembers.Columns.Contains("CompletionRate"))
                dgvMembers.Columns["CompletionRate"].HeaderText = "Completion %";

            dgvMembers.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#faf7f2");

            dgvMembers.AllowUserToResizeColumns = false;
            dgvMembers.AllowUserToResizeRows = false;
            dgvMembers.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode
                    .DisableResizing;
            dgvMembers.RowHeadersWidthSizeMode =
                DataGridViewRowHeadersWidthSizeMode
                    .DisableResizing;
            dgvMembers.AllowUserToOrderColumns = false;
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

        private void LeaderDashboardForm_Load(object sender, EventArgs e)
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
            new AddMemberForm(Session.GroupID, Session.GroupName).ShowDialog();
        }
    }
}

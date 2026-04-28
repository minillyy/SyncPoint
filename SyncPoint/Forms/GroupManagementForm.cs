using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Helpers;

namespace SyncPoint.Forms
{
    public partial class GroupManagementForm : Form
    {
        public GroupManagementForm()
        {
            InitializeComponent();
        }

        private void GroupManagementForm_Load(object sender, EventArgs e)
        {
            sidebarControl1.SetActive("Groups");

            lblUserName.Text = "Instructor: " + Session.FullName;

            LoadStats();
            LoadGroups();
        }

        private void LoadStats()
        {
            DataTable groups =
                DatabaseHelper.GetGroupsByInstructor(Session.UserID);

            int totalGroups = groups.Rows.Count;
            int totalMembers = 0;
            int leadersAssigned = 0;

            foreach (DataRow row in groups.Rows)
            {
                totalMembers += Convert.ToInt32(row["MemberCount"]);

                if (row["LeaderName"] != DBNull.Value &&
                    row["LeaderName"].ToString() != "")
                {
                    leadersAssigned++;
                }
            }

            lblTotalGroups.Text = totalGroups.ToString();
            lblTotalMembers.Text = totalMembers.ToString();
            lblLeadersAssigned.Text = leadersAssigned.ToString();
        }

        private void LoadGroups()
        {
            DataTable dt =
                DatabaseHelper.GetGroupsByInstructor(Session.UserID);

            dgvGroups.Columns.Clear();
            dgvGroups.DataSource = dt;

            if (dgvGroups.Columns.Contains("GroupID"))
                dgvGroups.Columns["GroupID"].Visible = false;

            if (dgvGroups.Columns.Contains("GroupName"))
                dgvGroups.Columns["GroupName"].HeaderText = "Group Name";

            if (dgvGroups.Columns.Contains("LeaderName"))
                dgvGroups.Columns["LeaderName"].HeaderText = "Leader";

            if (dgvGroups.Columns.Contains("MemberCount"))
                dgvGroups.Columns["MemberCount"].HeaderText = "Members";

            dgvGroups.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#faf7f2");

            dgvGroups.ColumnHeadersDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#1a2744");

            dgvGroups.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvGroups.EnableHeadersVisualStyles = false;
        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            string groupName =
                Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter Group Name:",
                    "Create Group",
                    "");

            if (groupName.Trim() == "")
                return;

            DatabaseHelper.CreateGroup(
                groupName,
                Session.UserID);

            LoadStats();
            LoadGroups();

            MessageBox.Show("Group created successfully.");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStats();
            LoadGroups();
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

        private void dgvGroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
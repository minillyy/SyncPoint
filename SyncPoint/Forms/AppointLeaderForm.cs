using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint.Forms
{
    public partial class AppointLeaderForm : Form
    {
        private int _groupID;
        private string _groupName;

        public AppointLeaderForm(int groupID, string groupName)
        {
            InitializeComponent();
            _groupID = groupID;
            _groupName = groupName;
        }

        private void AppointLeaderForm_Load(
            object sender, EventArgs e)
        {
            lblGroupName.Text = "Group: " + _groupName;

            // Check if group already has a leader
            if (DatabaseHelper.GroupHasLeader(_groupID))
            {
                lblInstruction.Text =
                    "This group already has a Leader. " +
                    "Appointing a new one will replace them.";
                lblInstruction.ForeColor =
                    ColorTranslator.FromHtml("#8b2020");
            }

            SetupGrid();
            LoadMembers();
        }

        private void SetupGrid()
        {
            dgvMembers.Columns.Clear();

            dgvMembers.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "UserID",
                    Visible = false
                });

            dgvMembers.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "FullName",
                    HeaderText = "Full Name",
                    FillWeight = 50
                });

            dgvMembers.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Username",
                    HeaderText = "Username",
                    FillWeight = 50
                });

            // Style header
            dgvMembers.ColumnHeadersDefaultCellStyle
                .BackColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvMembers.ColumnHeadersDefaultCellStyle
                .ForeColor = Color.White;
            dgvMembers.ColumnHeadersDefaultCellStyle.Font =
                new Font("Arial", 9f, FontStyle.Bold);
            dgvMembers.EnableHeadersVisualStyles = false;
            dgvMembers.ColumnHeadersHeight = 34;
            dgvMembers.RowTemplate.Height = 36;
            dgvMembers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvMembers.DefaultCellStyle.Font =
                new Font("Arial", 9.5f);
            dgvMembers.AlternatingRowsDefaultCellStyle
                .BackColor =
                ColorTranslator.FromHtml("#faf7f2");
        }

        private void LoadMembers()
        {
            // Load all registered Members
            var members = DatabaseHelper.GetAllMembers();

            dgvMembers.Rows.Clear();

            if (members.Rows.Count == 0)
            {
                lblNone.Visible = true;
                btnAppoint.Enabled = false;
                return;
            }

            foreach (DataRow row in members.Rows)
            {
                dgvMembers.Rows.Add(
                    row["UserID"],
                    row["FullName"],
                    row["Username"]
                );
            }
        }

        private void btnAppoint_Click(
            object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a student to appoint.",
                    "SyncPoint",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int userID = Convert.ToInt32(
                dgvMembers.SelectedRows[0]
                          .Cells["UserID"].Value);

            string name =
                dgvMembers.SelectedRows[0]
                          .Cells["FullName"].Value.ToString();

            // Confirm before appointing
            var confirm = MessageBox.Show(
                $"Appoint \"{name}\" as the Leader of " +
                $"\"{_groupName}\"?\n\n" +
                "They will be able to log in as Leader " +
                "and invite members to the group.",
                "SyncPoint — Confirm Appointment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            DatabaseHelper.AppointLeader(_groupID, userID);

            MessageBox.Show(
                $"✓ {name} has been appointed as Leader!\n\n" +
                $"They can now log in using the " +
                $"\"Login as Leader\" button.",
                "SyncPoint — Leader Appointed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.Close();
        }

        private void btnCancel_Click(
            object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
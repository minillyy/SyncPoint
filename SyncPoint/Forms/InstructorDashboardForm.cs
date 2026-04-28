using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint.Forms
{
    public partial class InstructorDashboardForm : Form
    {
        public InstructorDashboardForm()
        {
            InitializeComponent();
        }

        // ── Form Load ────────────────────────────────────────
        private void InstructorDashboardForm_Load(
            object sender, EventArgs e)
        {
            lblUser.Text = "Instructor: " + Session.FullName;
            SetActiveNav(lblNavGroups);
            SetupGroupsGrid();
            LoadGroups();
            PaintTopbarBorder();
        }

        // ── Paint gold border on topbar ──────────────────────
        private void PaintTopbarBorder()
        {
            pnlTopbar.Paint += (s, pe) =>
            {
                var pen = new Pen(
                    ColorTranslator.FromHtml("#c9a84c"), 3);
                pe.Graphics.DrawLine(pen,
                    0, pnlTopbar.Height - 2,
                    pnlTopbar.Width, pnlTopbar.Height - 2);
            };
        }

        // ── Highlight active nav item ────────────────────────
        private void SetActiveNav(Label active)
        {
            Label[] navItems = {
                lblNavGroups, lblNavCreate, lblNavReports };

            foreach (var lbl in navItems)
            {
                lbl.ForeColor = ColorTranslator.FromHtml("#8fa3c4");
                lbl.BackColor = Color.Transparent;
            }

            active.ForeColor =
                ColorTranslator.FromHtml("#f5f0e8");
            active.BackColor =
                Color.FromArgb(30, 201, 168, 76);
        }

        // ── Setup DataGridView columns ───────────────────────
        private void SetupGroupsGrid()
        {
            dgvGroups.Columns.Clear();

            // Hidden GroupID column
            dgvGroups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GroupID",
                Visible = false
            });

            // Group name
            dgvGroups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GroupName",
                HeaderText = "Group Name",
                ReadOnly = true,
                FillWeight = 30
            });

            // Leader name
            dgvGroups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LeaderName",
                HeaderText = "Appointed Leader",
                ReadOnly = true,
                FillWeight = 25
            });

            // Member count
            dgvGroups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MemberCount",
                HeaderText = "Members",
                ReadOnly = true,
                FillWeight = 15
            });

            // Appoint Leader button
            dgvGroups.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "BtnAppoint",
                HeaderText = "",
                Text = "Appoint Leader",
                UseColumnTextForButtonValue = true,
                FillWeight = 15,
                FlatStyle = FlatStyle.Flat
            });

            // View Group button
            dgvGroups.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "BtnView",
                HeaderText = "",
                Text = "View Group",
                UseColumnTextForButtonValue = true,
                FillWeight = 15,
                FlatStyle = FlatStyle.Flat
            });

            // Style the header
            dgvGroups.ColumnHeadersDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvGroups.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            dgvGroups.ColumnHeadersDefaultCellStyle.Font =
                new Font("Arial", 9f, FontStyle.Bold);
            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.ColumnHeadersHeight = 36;

            // Style rows
            dgvGroups.DefaultCellStyle.Font =
                new Font("Arial", 9.5f);
            dgvGroups.DefaultCellStyle.ForeColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvGroups.RowTemplate.Height = 40;
            dgvGroups.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#faf7f2");

            // Wire up button click
            dgvGroups.CellClick +=
                new DataGridViewCellEventHandler(
                    dgvGroups_CellClick);
        }

        // ── Load groups into grid ────────────────────────────
        private void LoadGroups()
        {
            var groups = DatabaseHelper.GetAllGroups();

            dgvGroups.Rows.Clear();

            foreach (DataRow row in groups.Rows)
            {
                dgvGroups.Rows.Add(
                    row["GroupID"],
                    row["GroupName"],
                    row["LeaderName"],
                    row["MemberCount"]
                );
            }

            lblGroupCount.Text = groups.Rows.Count + " group(s)";
        }

        // ── Handle grid button clicks ────────────────────────
        private void dgvGroups_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int groupID = Convert.ToInt32(
                dgvGroups.Rows[e.RowIndex]
                         .Cells["GroupID"].Value);

            string groupName =
                dgvGroups.Rows[e.RowIndex]
                         .Cells["GroupName"].Value.ToString();

            // Appoint Leader button clicked
            if (e.ColumnIndex ==
                dgvGroups.Columns["BtnAppoint"].Index)
            {
                var form = new AppointLeaderForm(
                    groupID, groupName);
                form.ShowDialog();
                LoadGroups(); // refresh after appointing
            }

            // View Group button clicked

            else if (e.ColumnIndex ==
                     dgvGroups.Columns["BtnView"].Index)
            {
                // TODO: Build ViewGroupForm later
                MessageBox.Show(
                    $"Group: {groupName}\n\nViewGroupForm coming soon.",
                    "SyncPoint",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        // ── Create Group button ──────────────────────────────
        private void btnCreateGroup_Click(
            object sender, EventArgs e)
        {
            var form = new CreateGroupForm();
            form.ShowDialog();
            LoadGroups(); // refresh after creating
        }

        // ── Sidebar navigation ───────────────────────────────
        private void lblNavGroups_Click(
            object sender, EventArgs e)
        {
            SetActiveNav(lblNavGroups);
            lblPageTitle.Text = "My Groups";
            LoadGroups();
        }

        private void lblNavCreate_Click(
            object sender, EventArgs e)
        {
            var form = new CreateGroupForm();
            form.ShowDialog();
            LoadGroups();
        }

        private void lblNavReports_Click(
            object sender, EventArgs e)
        {
            SetActiveNav(lblNavReports);
            lblPageTitle.Text = "Reports";
            // Open reports logic here later
        }

        // Logout
        private void lblLogout_Click(
            object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to logout?",
                "SyncPoint",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Session.Clear();
                this.Close();
            }
        }
    }
}
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint.Forms
{
    public partial class InstructorDashboardForm : Form
    {
        // Tracks currently selected group
        private int _selectedGroupID = -1;
        private string _selectedGroupName = "";

        public InstructorDashboardForm()
        {
            InitializeComponent();
            WireUpEvents();
        }

        // ════════════════════════════════════════════════════
        //  WIRE UP ALL EVENTS IN CODE
        //  This replaces all designer event connections
        //  so nothing is missed or duplicated
        // ════════════════════════════════════════════════════
        private void WireUpEvents()
        {
            // Form load
            this.Load +=
                new EventHandler(
                    InstructorDashboardForm_Load);

            // Nav label clicks
            lblNavGroups.Click +=
                new EventHandler(lblNavGroups_Click);
            lblNavReports.Click +=
                new EventHandler(lblNavReports_Click);

            // Make nav labels look clickable
            lblNavGroups.Cursor = Cursors.Hand;
            lblNavReports.Cursor = Cursors.Hand;

            // Logout label
            lblLogout.Click +=
                new EventHandler(lblLogout_Click);
            lblLogout.Cursor = Cursors.Hand;

            // Create group button
            // Wire ONLY here — remove from designer
            btnCreateGroup.Click +=
                new EventHandler(btnCreateGroup_Click);

            // Topbar gold border paint
            pnlTopbar.Paint += (s, pe) =>
            {
                var pen = new Pen(
                    ColorTranslator.FromHtml("#c9a84c"), 3);
                pe.Graphics.DrawLine(pen,
                    0, pnlTopbar.Height - 2,
                    pnlTopbar.Width, pnlTopbar.Height - 2);
            };

            // Sidebar footer divider paint
            pnlSidebarFooter.Paint += (s, pe) =>
            {
                var pen = new Pen(
                    ColorTranslator.FromHtml("#2e3f5c"), 1);
                pe.Graphics.DrawLine(pen,
                    0, 0,
                    pnlSidebarFooter.Width, 0);
            };
        }

        // ════════════════════════════════════════════════════
        //  FORM LOAD
        // ════════════════════════════════════════════════════
        private void InstructorDashboardForm_Load(
            object sender, EventArgs e)
        {
            lblUser.Text =
                "Instructor: " + Session.FullName;

            // Setup grid columns FIRST
            // then load data
            SetupGroupsGrid();

            // Default to Groups tab on load
            ShowGroupsTab();
        }

        // ════════════════════════════════════════════════════
        //  SET ACTIVE NAV LABEL
        // ════════════════════════════════════════════════════
        private void SetActiveNav(Label activeLabel)
        {
            Label[] navLabels = {
                lblNavGroups,
                lblNavReports
            };

            // Reset all labels to inactive style
            foreach (Label lbl in navLabels)
            {
                lbl.ForeColor =
                    ColorTranslator.FromHtml("#8fa3c4");
                lbl.BackColor = Color.Transparent;
                lbl.Font =
                    new Font("Arial", 10f,
                        FontStyle.Regular);
                lbl.Tag = "";
                lbl.Invalidate(); // force visual refresh
            }

            // Apply active style to clicked label
            activeLabel.ForeColor =
                ColorTranslator.FromHtml("#f5f0e8");
            activeLabel.BackColor =
                Color.FromArgb(30, 201, 168, 76);
            activeLabel.Font =
                new Font("Arial", 10f, FontStyle.Bold);
            activeLabel.Tag = "active";
            activeLabel.Invalidate(); // force visual refresh
        }

        // ════════════════════════════════════════════════════
        //  NAV LABEL CLICKS
        // ════════════════════════════════════════════════════
        private void lblNavGroups_Click(
            object sender, EventArgs e)
        {
            ShowGroupsTab();
        }

        private void lblNavReports_Click(
            object sender, EventArgs e)
        {
            ShowReportsTab();
        }

        // ════════════════════════════════════════════════════
        //  SHOW GROUPS TAB
        // ════════════════════════════════════════════════════
        private void ShowGroupsTab()
        {
            SetActiveNav(lblNavGroups);
            lblPageTitle.Text = "My Groups";
            LoadGroups();
        }

        // ════════════════════════════════════════════════════
        //  SHOW REPORTS TAB
        // ════════════════════════════════════════════════════
        private void ShowReportsTab()
        {
            SetActiveNav(lblNavReports);
            lblPageTitle.Text = "Reports";
            // Reports logic will go here later
        }

        // ════════════════════════════════════════════════════
        //  SETUP GROUPS GRID COLUMNS
        //  Called once before any data is loaded
        // ════════════════════════════════════════════════════
        private void SetupGroupsGrid()
        {
            // Prevent adding columns twice
            if (dgvGroups.Columns.Count > 0) return;

            // Hidden GroupID — used to identify rows
            dgvGroups.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "GroupID",
                    Visible = false
                });

            // Group Name
            dgvGroups.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "GroupName",
                    HeaderText = "Group Name",
                    ReadOnly = true,
                    FillWeight = 35
                });

            // Appointed Leader
            dgvGroups.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "LeaderName",
                    HeaderText = "Appointed Leader",
                    ReadOnly = true,
                    FillWeight = 30
                });

            // Member Count
            dgvGroups.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "MemberCount",
                    HeaderText = "Members",
                    ReadOnly = true,
                    FillWeight = 15
                });

            // Appoint Leader Button
            dgvGroups.Columns.Add(
                new DataGridViewButtonColumn
                {
                    Name = "BtnAppoint",
                    HeaderText = "",
                    Text = "Appoint Leader",
                    UseColumnTextForButtonValue = true,
                    FillWeight = 20
                });

            // Style header
            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.ColumnHeadersDefaultCellStyle
                .BackColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvGroups.ColumnHeadersDefaultCellStyle
                .ForeColor = Color.White;
            dgvGroups.ColumnHeadersDefaultCellStyle
                .Font =
                new Font("Arial", 9f, FontStyle.Bold);
            dgvGroups.ColumnHeadersHeight = 36;

            // Style rows
            dgvGroups.BackgroundColor = Color.White;
            dgvGroups.BorderStyle = BorderStyle.None;
            dgvGroups.RowHeadersVisible = false;
            dgvGroups.AllowUserToAddRows = false;
            dgvGroups.AllowUserToDeleteRows = false;
            dgvGroups.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvGroups.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvGroups.DefaultCellStyle.Font =
                new Font("Arial", 9.5f);
            dgvGroups.DefaultCellStyle.ForeColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvGroups.RowTemplate.Height = 40;
            dgvGroups.AlternatingRowsDefaultCellStyle
                .BackColor =
                ColorTranslator.FromHtml("#faf7f2");
            dgvGroups.MultiSelect = false;

            // Wire CellClick ONCE here
            dgvGroups.CellClick +=
                new DataGridViewCellEventHandler(
                    dgvGroups_CellClick);
        }

        // ════════════════════════════════════════════════════
        //  LOAD GROUPS INTO GRID
        // ════════════════════════════════════════════════════
        private void LoadGroups()
        {
            // Safety check — setup columns if missing
            if (dgvGroups.Columns.Count == 0)
                SetupGroupsGrid();

            var groups = DatabaseHelper.GetAllGroups();

            dgvGroups.Rows.Clear();

            foreach (DataRow row in groups.Rows)
            {
                dgvGroups.Rows.Add(
                    row["GroupID"],    // col 0 - hidden
                    row["GroupName"],  // col 1
                    row["LeaderName"], // col 2
                    row["MemberCount"] // col 3
                                       // col 4 BtnAppoint is a button
                                       // no value needed for button columns
                );
            }

            lblGroupCount.Text =
                groups.Rows.Count + " group(s)";
        }

        // ════════════════════════════════════════════════════
        //  GRID CELL CLICK
        //  Handles both row selection and button clicks
        // ════════════════════════════════════════════════════
        private void dgvGroups_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            // Ignore header row clicks
            if (e.RowIndex < 0) return;

            int groupID = Convert.ToInt32(
                dgvGroups.Rows[e.RowIndex]
                         .Cells["GroupID"].Value);

            string groupName =
                dgvGroups.Rows[e.RowIndex]
                         .Cells["GroupName"]
                         .Value.ToString();

            // Store selected group
            _selectedGroupID = groupID;
            _selectedGroupName = groupName;

            // Appoint Leader button clicked
            if (e.ColumnIndex ==
                dgvGroups.Columns["BtnAppoint"].Index)
            {
                var form = new AppointLeaderForm(groupID, groupName);
                form.ShowDialog();
                LoadGroups(); // refresh list
            }
        }

        // ════════════════════════════════════════════════════
        //  CREATE GROUP BUTTON
        //  Only one method — duplicate removed
        // ════════════════════════════════════════════════════
        private void btnCreateGroup_Click(
            object sender, EventArgs e)
        {
            var form = new CreateGroupForm();
            form.ShowDialog();
            LoadGroups(); // refresh after creating
        }

        // ════════════════════════════════════════════════════
        //  LOGOUT
        // ════════════════════════════════════════════════════
        private void lblLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to logout?",
                "SyncPoint — Logout",
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
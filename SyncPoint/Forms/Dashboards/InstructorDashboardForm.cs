using SyncPoint.Data;
using SyncPoint.Forms.Auth;
using SyncPoint.Forms.Other_Forms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SyncPoint.Forms.Dashboards
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
        // ════════════════════════════════════════════════════
        private void WireUpEvents()
        {
            // Form load
            this.Load += new EventHandler(InstructorDashboardForm_Load);

            // Nav label clicks
            lblNavGroups.Click += new EventHandler(lblNavGroups_Click);
            lblNavReports.Click += new EventHandler(lblNavReports_Click);

            // Make nav labels look clickable
            lblNavGroups.Cursor = Cursors.Hand;
            lblNavReports.Cursor = Cursors.Hand;

            // Logout label
            lblLogout.Click += new EventHandler(lblLogout_Click);
            lblLogout.Cursor = Cursors.Hand;

            // Create group button
            btnCreateGroup.Click += new EventHandler(btnCreateGroup_Click);

            // Topbar gold border paint
            pnlTopbar.Paint += (s, pe) =>
            {
                var pen = new Pen(ColorTranslator.FromHtml("#c9a84c"), 3);
                pe.Graphics.DrawLine(pen, 0, pnlTopbar.Height - 2,
                    pnlTopbar.Width, pnlTopbar.Height - 2);
            };

            // Sidebar footer divider paint
            pnlSidebarFooter.Paint += (s, pe) =>
            {
                var pen = new Pen(ColorTranslator.FromHtml("#2e3f5c"), 1);
                pe.Graphics.DrawLine(pen, 0, 0, pnlSidebarFooter.Width, 0);
            };
        }

        // ════════════════════════════════════════════════════
        //  FORM LOAD
        // ════════════════════════════════════════════════════
        private void InstructorDashboardForm_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Instructor";

            SetupGroupsGrid();
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
                lbl.ForeColor = ColorTranslator.FromHtml("#8fa3c4");
                lbl.BackColor = Color.Transparent;
                lbl.Font = new Font("Arial", 10f, FontStyle.Regular);
                lbl.Tag = "";
                lbl.Invalidate(); // force visual refresh
            }

            // Apply active style to clicked label
            activeLabel.ForeColor = ColorTranslator.FromHtml("#f5f0e8");
            activeLabel.BackColor = Color.FromArgb(30, 201, 168, 76);
            activeLabel.Font = new Font("Arial", 10f, FontStyle.Bold);
            activeLabel.Tag = "active";
            activeLabel.Invalidate(); // force visual refresh
        }

        // ════════════════════════════════════════════════════
        //  NAV LABEL CLICKS
        // ════════════════════════════════════════════════════
        private void lblNavGroups_Click(object sender, EventArgs e)
        {
            ShowGroupsTab();
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

            // Hidden GroupID
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
                    FillWeight = 15,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment =
                            DataGridViewContentAlignment.MiddleCenter
                    },
                    HeaderCell = {
            Style = new DataGridViewCellStyle {
                Alignment =
                    DataGridViewContentAlignment
                        .MiddleCenter
            }
                    }
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

            // Remove sort arrows from all columns
            foreach (DataGridViewColumn col in dgvGroups.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // ── TABLE BACKGROUND ─────────────────────────────────
            // White background
            dgvGroups.BackgroundColor = Color.White;

            // Prevent table resizing

            dgvGroups.AllowUserToResizeColumns = false;
            dgvGroups.AllowUserToResizeRows = false;
            dgvGroups.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode
                    .DisableResizing;
            dgvGroups.RowHeadersWidthSizeMode =
                DataGridViewRowHeadersWidthSizeMode
                    .DisableResizing;
            dgvGroups.AllowUserToOrderColumns = false;

            // ── HEADER ROW ───────────────────────────────────────
            // Dark navy
            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.ColumnHeadersDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvGroups.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            dgvGroups.ColumnHeadersDefaultCellStyle.Font =
                new Font("Georgia", 9f, FontStyle.Bold);
            dgvGroups.ColumnHeadersDefaultCellStyle.Padding =
                new Padding(8, 0, 0, 0);
            dgvGroups.ColumnHeadersHeight = 38;

            // ── DATA ROWS ────────────────────────────────────────
            // White rows with navy text
            dgvGroups.DefaultCellStyle.BackColor =
                Color.White;
            dgvGroups.DefaultCellStyle.ForeColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvGroups.DefaultCellStyle.Font =
                new Font("Arial", 9.5f);
            dgvGroups.DefaultCellStyle.Padding =
                new Padding(8, 0, 0, 0);
            dgvGroups.DefaultCellStyle.SelectionBackColor =
                ColorTranslator.FromHtml("#e8e4f0");
            dgvGroups.DefaultCellStyle.SelectionForeColor =
                ColorTranslator.FromHtml("#1a2744");

            // Alternating row — very light cream
            dgvGroups.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#faf7f2");
            dgvGroups.AlternatingRowsDefaultCellStyle.ForeColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvGroups.AlternatingRowsDefaultCellStyle
                .SelectionBackColor =
                ColorTranslator.FromHtml("#e8e4f0");
            dgvGroups.AlternatingRowsDefaultCellStyle
                .SelectionForeColor =
                ColorTranslator.FromHtml("#1a2744");

            // ── GRID LINES ───────────────────────────────────────
            // Subtle horizontal lines only — cleaner look
            dgvGroups.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;
            dgvGroups.GridColor =
                ColorTranslator.FromHtml("#e8e0d0");

            // ── BUTTON COLUMN STYLE ──────────────────────────────
            // Dark navy button matching your UI buttons
            dgvGroups.Columns["BtnAppoint"]
                .DefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvGroups.Columns["BtnAppoint"]
                .DefaultCellStyle.ForeColor =
                Color.White;
            dgvGroups.Columns["BtnAppoint"]
                .DefaultCellStyle.Font =
                new Font("Arial", 8.5f, FontStyle.Bold);
            dgvGroups.Columns["BtnAppoint"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvGroups.Columns["BtnAppoint"]
                .DefaultCellStyle.SelectionBackColor =
                ColorTranslator.FromHtml("#2e3f5c");
            dgvGroups.Columns["BtnAppoint"]
                .DefaultCellStyle.SelectionForeColor =
                Color.White;

            // ── OTHER GRID PROPERTIES ────────────────────────────
            dgvGroups.BorderStyle = BorderStyle.None;
            dgvGroups.RowHeadersVisible = false;
            dgvGroups.AllowUserToAddRows = false;
            dgvGroups.AllowUserToDeleteRows = false;
            dgvGroups.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvGroups.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvGroups.RowTemplate.Height = 42;
            dgvGroups.MultiSelect = false;

            // Remove blue selection highlight
            dgvGroups.DefaultCellStyle.SelectionBackColor =
                Color.White;
            dgvGroups.DefaultCellStyle.SelectionForeColor =
                ColorTranslator.FromHtml("#1a2744");

            dgvGroups.AlternatingRowsDefaultCellStyle
                .SelectionBackColor =
                ColorTranslator.FromHtml("#faf7f2");
            dgvGroups.AlternatingRowsDefaultCellStyle
                .SelectionForeColor =
                ColorTranslator.FromHtml("#1a2744");

            // Wire CellClick once
            dgvGroups.CellClick += new DataGridViewCellEventHandler(dgvGroups_CellClick);

            // Wire custom button painter
            dgvGroups.CellPainting += new DataGridViewCellPaintingEventHandler(dgvGroups_CellPainting);
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

            lblGroupCount.Text = groups.Rows.Count + " group(s)";
            StyleButtonCells();
        }

        private void dgvGroups_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Only paint the BtnAppoint column
            if (e.ColumnIndex !=
                dgvGroups.Columns["BtnAppoint"].Index ||
                e.RowIndex < 0)
                return;

            e.Paint(e.ClipBounds,
                DataGridViewPaintParts.Background);

            // Draw the navy button background
            Color btnColor =
                ColorTranslator.FromHtml("#1a2744");
            Color btnHover =
                ColorTranslator.FromHtml("#2e3f5c");

            using (var brush = new SolidBrush(btnColor))
            {
                // Button rectangle with padding inside the cell
                Rectangle btnRect = new Rectangle(
                    e.CellBounds.X + 6,
                    e.CellBounds.Y + 5,
                    e.CellBounds.Width - 12,
                    e.CellBounds.Height - 10);

                // Draw filled rounded-looking button
                e.Graphics.FillRectangle(brush, btnRect);

                // Draw button text in white
                using (var textBrush = new SolidBrush(Color.White))
                using (var font = new Font(
                    "Arial", 8.5f, FontStyle.Bold))
                {
                    var format = new StringFormat
                    {
                        Alignment =
                            StringAlignment.Center,
                        LineAlignment =
                            StringAlignment.Center
                    };

                    e.Graphics.DrawString(
                        "Appoint Leader",
                        font,
                        textBrush,
                        btnRect,
                        format);
                }
            }

            e.Handled = true; // prevent default drawing
        }

        private void StyleButtonCells()
        {
            foreach (DataGridViewRow row in dgvGroups.Rows)
            {
                if (row.IsNewRow) continue;

                // Style the Appoint Leader button cell
                var cell = row.Cells["BtnAppoint"]
                    as DataGridViewButtonCell;
                if (cell != null)
                {
                    cell.Style.BackColor =
                        ColorTranslator.FromHtml("#1a2744");
                    cell.Style.ForeColor = Color.White;
                    cell.Style.Font =
                        new Font("Arial", 8.5f, FontStyle.Bold);
                }
            }
        }

        // ════════════════════════════════════════════════════
        //  GRID CELL CLICK
        //  Handles both row selection and button clicks
        // ════════════════════════════════════════════════════
        private void dgvGroups_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header row clicks
            if (e.RowIndex < 0) return;

            int groupID = Convert.ToInt32(dgvGroups.Rows[e.RowIndex].Cells["GroupID"].Value);

            string groupName = dgvGroups.Rows[e.RowIndex].Cells["GroupName"].Value.ToString();

            // Store selected group
            _selectedGroupID = groupID;
            _selectedGroupName = groupName;

            // Appoint Leader button clicked
            if (e.ColumnIndex == dgvGroups.Columns["BtnAppoint"].Index)
            {
                var form = new AppointLeaderForm(groupID, groupName);
                form.ShowDialog();
                LoadGroups(); // refresh list
            }
        }

        // ════════════════════════════════════════════════════
        //  CREATE GROUP BUTTON
        // ════════════════════════════════════════════════════
        private void btnCreateGroup_Click(object sender, EventArgs e)
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

        private void lblNavReports_Click(object sender, EventArgs e)
        {
            using (ReportsForm reportsWindow = new ReportsForm())
            {
                reportsWindow.ShowDialog();
            }
        }
    }
}
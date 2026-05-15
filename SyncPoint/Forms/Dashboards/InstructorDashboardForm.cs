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
            this.Load += new EventHandler(InstructorDashboardForm_Load);

            lblNavGroups.Click += new EventHandler(lblNavGroups_Click);
            lblNavReports.Click += new EventHandler(lblNavReports_Click);

            lblNavGroups.Cursor = Cursors.Hand;
            lblNavReports.Cursor = Cursors.Hand;

            lblLogout.Click += new EventHandler(lblLogout_Click);
            lblLogout.Cursor = Cursors.Hand;

            btnCreateGroup.Click += new EventHandler(btnCreateGroup_Click);

            pnlTopbar.Paint += (s, pe) =>
            {
                var pen = new Pen(ColorTranslator.FromHtml("#c9a84c"), 3);
                pe.Graphics.DrawLine(pen, 0, pnlTopbar.Height - 2,
                    pnlTopbar.Width, pnlTopbar.Height - 2);
            };

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

            foreach (Label lbl in navLabels)
            {
                lbl.ForeColor = ColorTranslator.FromHtml("#8fa3c4");
                lbl.BackColor = Color.Transparent;
                lbl.Font = new Font("Arial", 10f, FontStyle.Regular);
                lbl.Tag = "";
                lbl.Invalidate();
            }

            activeLabel.ForeColor = ColorTranslator.FromHtml("#f5f0e8");
            activeLabel.BackColor = Color.FromArgb(30, 201, 168, 76);
            activeLabel.Font = new Font("Arial", 10f, FontStyle.Bold);
            activeLabel.Tag = "active";
            activeLabel.Invalidate();
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
        //  SETUP GROUPS GRID COLUMNS
        //  Called once before any data is loaded
        // ════════════════════════════════════════════════════
        private void SetupGroupsGrid()
        {
            if (dgvGroups.Columns.Count > 0) return;

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

            foreach (DataGridViewColumn col in dgvGroups.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgvGroups.BackgroundColor = Color.White;

            dgvGroups.AllowUserToResizeColumns = false;
            dgvGroups.AllowUserToResizeRows = false;
            dgvGroups.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode
                    .DisableResizing;
            dgvGroups.RowHeadersWidthSizeMode =
                DataGridViewRowHeadersWidthSizeMode
                    .DisableResizing;
            dgvGroups.AllowUserToOrderColumns = false;

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

            dgvGroups.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;
            dgvGroups.GridColor =
                ColorTranslator.FromHtml("#e8e0d0");

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

            dgvGroups.CellClick += new DataGridViewCellEventHandler(dgvGroups_CellClick);

            dgvGroups.CellPainting += new DataGridViewCellPaintingEventHandler(dgvGroups_CellPainting);
        }

        // ════════════════════════════════════════════════════
        //  LOAD GROUPS INTO GRID
        // ════════════════════════════════════════════════════
        private void LoadGroups()
        {
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
                );
            }

            lblGroupCount.Text = groups.Rows.Count + " group(s)";
            StyleButtonCells();
        }

        private void dgvGroups_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex !=
                dgvGroups.Columns["BtnAppoint"].Index ||
                e.RowIndex < 0)
                return;

            e.Paint(e.ClipBounds,
                DataGridViewPaintParts.Background);

            Color btnColor =
                ColorTranslator.FromHtml("#1a2744");
            Color btnHover =
                ColorTranslator.FromHtml("#2e3f5c");

            using (var brush = new SolidBrush(btnColor))
            {
                Rectangle btnRect = new Rectangle(
                    e.CellBounds.X + 6,
                    e.CellBounds.Y + 5,
                    e.CellBounds.Width - 12,
                    e.CellBounds.Height - 10);

                e.Graphics.FillRectangle(brush, btnRect);

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

            e.Handled = true;
        }

        private void StyleButtonCells()
        {
            foreach (DataGridViewRow row in dgvGroups.Rows)
            {
                if (row.IsNewRow) continue;

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
            if (e.RowIndex < 0) return;

            int groupID = Convert.ToInt32(dgvGroups.Rows[e.RowIndex].Cells["GroupID"].Value);

            string groupName = dgvGroups.Rows[e.RowIndex].Cells["GroupName"].Value.ToString();

            _selectedGroupID = groupID;
            _selectedGroupName = groupName;

            if (e.ColumnIndex == dgvGroups.Columns["BtnAppoint"].Index)
            {
                var form = new AppointLeaderForm(groupID, groupName);
                form.ShowDialog();
                LoadGroups();
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
            if (dgvGroups.CurrentRow == null)
            {
                MessageBox.Show("Please select a group from the list first to view its reports.", "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int selectedGroupId = Convert.ToInt32(dgvGroups.CurrentRow.Cells["GroupID"].Value);
                string groupName = dgvGroups.CurrentRow.Cells["GroupName"].Value?.ToString() ?? "Selected Group";

                using (ReportsForm reportWindow = new ReportsForm(selectedGroupId))
                {
                    reportWindow.Text = $"Reports - {groupName}";
                    reportWindow.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Make sure the GroupID column is correctly set in your grid. Error: " + ex.Message);
            }
        }
    }
}
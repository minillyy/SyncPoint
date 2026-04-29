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

        // Stores the full student list for filtering
        private DataTable _allStudents;

        public AppointLeaderForm(
            int groupID, string groupName)
        {
            InitializeComponent();
            _groupID = groupID;
            _groupName = groupName;
            WireUpEvents();
        }

        // ════════════════════════════════════════════════════
        //  WIRE UP EVENTS
        // ════════════════════════════════════════════════════
        private void WireUpEvents()
        {
            this.Load += AppointLeaderForm_Load;

            // Search bar — filter as user types
            txtSearch.TextChanged += (s, e) =>
                FilterStudents(txtSearch.Text);

            // Clear search placeholder on focus
            txtSearch.Enter += (s, e) =>
            {
                if (txtSearch.Text == "Search by name or username...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor =
                        ColorTranslator.FromHtml("#2c2416");
                }
            };

            // Restore placeholder if empty
            txtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text =
                        "Search by name or username...";
                    txtSearch.ForeColor =
                        ColorTranslator.FromHtml("#b0a898");
                }
            };

            // Row selection — update selected label
            dgvStudents.SelectionChanged += (s, e) =>
                UpdateSelectedLabel();

            // Double click a row = quick appoint
            dgvStudents.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    btnAppoint_Click(s, e);
            };

            // Button clicks
            btnAppoint.Click += btnAppoint_Click;
            btnCancel.Click += (s, e) => this.Close();

            // Header paint
            pnlHeader.Paint += (s, pe) =>
            {
                var pen = new Pen(
                    ColorTranslator.FromHtml("#c9a84c"), 3);
                pe.Graphics.DrawLine(pen,
                    0, pnlHeader.Height - 2,
                    pnlHeader.Width,
                    pnlHeader.Height - 2);
            };
        }

        // ════════════════════════════════════════════════════
        //  FORM LOAD
        // ════════════════════════════════════════════════════
        private void AppointLeaderForm_Load(
            object sender, EventArgs e)
        {
            // Show group name in header
            lblGroup.Text = "Group:  " + _groupName;

            // Check if group already has a leader
            if (DatabaseHelper.GroupHasLeader(_groupID))
            {
                lblInstruction.Text =
                    "⚠  This group already has a Leader.";
                lblInstruction.ForeColor =
                    ColorTranslator.FromHtml("#8b2020");
            }
            else
            {
                lblInstruction.Text =
                    "Select a registered student ";
                lblInstruction.ForeColor =
                    ColorTranslator.FromHtml("#7a6f5a");
            }

            // Setup grid then load students
            SetupStudentsGrid();
            LoadStudents();

            // Set search placeholder
            txtSearch.Text =
                "Search by name or username...";
            txtSearch.ForeColor =
                ColorTranslator.FromHtml("#b0a898");

            // Disable appoint button until
            // a student is selected
            btnAppoint.Enabled = false;
        }

        // ════════════════════════════════════════════════════
        //  SETUP STUDENTS GRID
        // ════════════════════════════════════════════════════
        private void SetupStudentsGrid()
        {
            dgvStudents.Columns.Clear();

            // Hidden UserID
            dgvStudents.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "UserID",
                    Visible = false
                });

            // Full Name
            dgvStudents.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "FullName",
                    HeaderText = "Full Name",
                    ReadOnly = true,
                    FillWeight = 50
                });

            // Username
            dgvStudents.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Username",
                    HeaderText = "Username",
                    ReadOnly = true,
                    FillWeight = 50
                });

            // ── Header style ──────────────────────────────
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.ColumnHeadersDefaultCellStyle
                .BackColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvStudents.ColumnHeadersDefaultCellStyle
                .ForeColor = Color.White;
            dgvStudents.ColumnHeadersDefaultCellStyle
                .Font =
                new Font("Arial", 9f, FontStyle.Bold);
            dgvStudents.ColumnHeadersDefaultCellStyle
                .Padding = new Padding(8, 0, 0, 0);
            dgvStudents.ColumnHeadersHeight = 36;

            // ── Row style ─────────────────────────────────
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.DefaultCellStyle.BackColor =
                Color.White;
            dgvStudents.DefaultCellStyle.ForeColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvStudents.DefaultCellStyle.Font =
                new Font("Arial", 9.5f);
            dgvStudents.DefaultCellStyle.Padding =
                new Padding(8, 0, 0, 0);

            // ── Selection — gold highlight ─────────────────
            dgvStudents.DefaultCellStyle
                .SelectionBackColor =
                ColorTranslator.FromHtml("#c9a84c");
            dgvStudents.DefaultCellStyle
                .SelectionForeColor = Color.White;

            dgvStudents.AlternatingRowsDefaultCellStyle
                .BackColor =
                ColorTranslator.FromHtml("#faf7f2");
            dgvStudents.AlternatingRowsDefaultCellStyle
                .ForeColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvStudents.AlternatingRowsDefaultCellStyle
                .SelectionBackColor =
                ColorTranslator.FromHtml("#c9a84c");
            dgvStudents.AlternatingRowsDefaultCellStyle
                .SelectionForeColor = Color.White;

            // ── Grid properties ───────────────────────────
            dgvStudents.BorderStyle =
                BorderStyle.None;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.CellBorderStyle =
                DataGridViewCellBorderStyle
                    .SingleHorizontal;
            dgvStudents.GridColor =
                ColorTranslator.FromHtml("#e8e0d0");
            dgvStudents.RowTemplate.Height = 38;
            dgvStudents.MultiSelect = false;

            // Remove sort arrows
            foreach (DataGridViewColumn col
                in dgvStudents.Columns)
            {
                col.SortMode =
                    DataGridViewColumnSortMode
                        .NotSortable;
            }
        }

        // ════════════════════════════════════════════════════
        //  LOAD ALL STUDENTS FROM DATABASE
        // ════════════════════════════════════════════════════
        private void LoadStudents()
        {
            _allStudents = DatabaseHelper.GetAllMembers();
            DisplayStudents(_allStudents);
        }

        // ════════════════════════════════════════════════════
        //  DISPLAY STUDENTS IN GRID
        // ════════════════════════════════════════════════════
        private void DisplayStudents(DataTable students)
        {
            dgvStudents.Rows.Clear();

            if (students.Rows.Count == 0)
            {
                lblCount.Text =
                    "No registered students found.";
                lblCount.ForeColor =
                    ColorTranslator.FromHtml("#8b2020");
                btnAppoint.Enabled = false;
                return;
            }

            foreach (DataRow row in students.Rows)
            {
                dgvStudents.Rows.Add(
                    row["UserID"],
                    row["FullName"],
                    row["Username"]
                );
            }

            lblCount.Text =
                students.Rows.Count +
                " student(s) available";
            lblCount.ForeColor =
                ColorTranslator.FromHtml("#7a6f5a");
        }

        // ════════════════════════════════════════════════════
        //  FILTER STUDENTS — runs as user types in search
        // ════════════════════════════════════════════════════
        private void FilterStudents(string searchText)
        {
            if (_allStudents == null) return;

            // If search is empty or placeholder,
            // show all students
            if (string.IsNullOrWhiteSpace(searchText) ||
                searchText ==
                    "Search by name or username...")
            {
                DisplayStudents(_allStudents);
                return;
            }

            // Filter rows where name OR username
            // contains the search text
            string filter = searchText
                .ToLower().Trim();

            DataTable filtered =
                _allStudents.Clone(); // same structure

            foreach (DataRow row in _allStudents.Rows)
            {
                string fullName =
                    row["FullName"].ToString().ToLower();
                string username =
                    row["Username"].ToString().ToLower();

                if (fullName.Contains(filter) ||
                    username.Contains(filter))
                {
                    filtered.ImportRow(row);
                }
            }

            DisplayStudents(filtered);

            // Update count to show filter result
            if (filtered.Rows.Count == 0)
            {
                lblCount.Text =
                    $"No results for \"{searchText}\"";
                lblCount.ForeColor =
                    ColorTranslator.FromHtml("#8b2020");
            }
            else
            {
                lblCount.Text =
                    filtered.Rows.Count +
                    " result(s) found";
                lblCount.ForeColor =
                    ColorTranslator.FromHtml("#7a6f5a");
            }
        }

        // ════════════════════════════════════════════════════
        //  UPDATE SELECTED LABEL
        //  Shows who is currently selected in the grid
        // ════════════════════════════════════════════════════
        private void UpdateSelectedLabel()
        {
            if (dgvStudents.SelectedRows.Count == 0 ||
                dgvStudents.SelectedRows[0].IsNewRow)
            {
                lblSelected.Text =
                    "No student selected";
                lblSelected.ForeColor =
                    ColorTranslator.FromHtml("#7a6f5a");
                btnAppoint.Enabled = false;
                return;
            }

            string name =
                dgvStudents.SelectedRows[0]
                           .Cells["FullName"]
                           .Value.ToString();

            string username =
                dgvStudents.SelectedRows[0]
                           .Cells["Username"]
                           .Value.ToString();

            lblSelected.Text =
                $"Selected:  {name}  (@{username})";
            lblSelected.ForeColor =
                ColorTranslator.FromHtml("#085041");

            // Enable appoint button
            // now that a student is selected
            btnAppoint.Enabled = true;
        }

        // ════════════════════════════════════════════════════
        //  APPOINT BUTTON CLICK
        // ════════════════════════════════════════════════════
        private void btnAppoint_Click(
            object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a student first.",
                    "SyncPoint",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int userID = Convert.ToInt32(
                dgvStudents.SelectedRows[0]
                           .Cells["UserID"].Value);

            string name =
                dgvStudents.SelectedRows[0]
                           .Cells["FullName"]
                           .Value.ToString();

            // If group already has a leader,
            // warn before replacing
            if (DatabaseHelper.GroupHasLeader(_groupID))
            {
                var warn = MessageBox.Show(
                    $"This group already has a Leader.\n\n" +
                    $"Replace with \"{name}\"?\n\n" +
                    $"The previous leader will be " +
                    $"reverted to Member.",
                    "SyncPoint — Replace Leader?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (warn != DialogResult.Yes) return;
            }
            else
            {
                // Normal confirmation
                var confirm = MessageBox.Show(
                    $"Appoint \"{name}\" as the Leader " +
                    $"of \"{_groupName}\"?\n\n" +
                    $"They will be able to log in " +
                    $"using the Leader button.",
                    "SyncPoint — Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;
            }

            // Save to database
            DatabaseHelper.AppointLeader(
                _groupID, userID);

            MessageBox.Show(
                $"✓  {name} has been appointed " +
                $"as Leader of \"{_groupName}\"!\n\n" +
                $"They can now log in using " +
                $"the Leader button.",
                "SyncPoint — Leader Appointed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.Close();
        }
    }
}
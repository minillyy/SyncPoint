using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint.Forms.Other_Forms
{
    public partial class AppointLeaderForm : Form
    {
        private int _groupID;
        private string _groupName;
        private DataTable _allStudents;

        public AppointLeaderForm(int groupID, string groupName)
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

            txtSearch.TextChanged += (s, e) =>
                FilterStudents(txtSearch.Text);

            txtSearch.Enter += (s, e) =>
            {
                if (txtSearch.Text ==
                    "Search by name or username...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor =
                        ColorTranslator.FromHtml(
                            "#2c2416");
                }
            };

            txtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(
                    txtSearch.Text))
                {
                    txtSearch.Text =
                        "Search by name or username...";
                    txtSearch.ForeColor =
                        ColorTranslator.FromHtml(
                            "#b0a898");
                }
            };

            dgvStudents.SelectionChanged += (s, e) =>
                UpdateSelectedLabel();

            dgvStudents.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgvStudents.Rows[e.RowIndex]
                        .Selected = true;
            };

            btnAppoint.Click += btnAppoint_Click;
            btnCancel.Click += (s, e) => this.Close();

            pnlHeader.Paint += (s, pe) =>
            {
                var pen = new Pen(
                    ColorTranslator.FromHtml("#c9a84c"),
                    3);
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
            lblGroup.Text = "Group:  " + _groupName;

            if (DatabaseHelper.GroupHasLeader(_groupID))
            {
                lblInstruction.Text =
                    "⚠  This group already has a " +
                    "Leader.";
                lblInstruction.ForeColor =
                    ColorTranslator.FromHtml("#8b2020");
            }
            else
            {
                lblInstruction.Text =
                    "Select a registered student " +
                    "to appoint as Leader:";
                lblInstruction.ForeColor =
                    ColorTranslator.FromHtml("#7a6f5a");
            }

            SetupStudentsGrid();
            LoadStudents();

            txtSearch.Text =
                "Search by name or username...";
            txtSearch.ForeColor =
                ColorTranslator.FromHtml("#b0a898");

            btnAppoint.Enabled = false;
        }

        // ════════════════════════════════════════════════════
        //  SETUP STUDENTS GRID
        // ════════════════════════════════════════════════════
        private void SetupStudentsGrid()
        {
            dgvStudents.Columns.Clear();

            dgvStudents.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "UserID",
                    Visible = false
                });

            dgvStudents.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "FullName",
                    HeaderText = "Full Name",
                    ReadOnly = true,
                    FillWeight = 50
                });

            dgvStudents.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "Username",
                    HeaderText = "Username",
                    ReadOnly = true,
                    FillWeight = 50
                });

            dgvStudents.AllowUserToResizeColumns = false;
            dgvStudents.AllowUserToResizeRows = false;
            dgvStudents.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode
                    .DisableResizing;
            dgvStudents.RowHeadersWidthSizeMode =
                DataGridViewRowHeadersWidthSizeMode
                    .DisableResizing;
            dgvStudents.AllowUserToOrderColumns = false;

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

            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.DefaultCellStyle.BackColor =
                Color.White;
            dgvStudents.DefaultCellStyle.ForeColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvStudents.DefaultCellStyle.Font =
                new Font("Arial", 9.5f);
            dgvStudents.DefaultCellStyle.Padding =
                new Padding(8, 0, 0, 0);

            dgvStudents.DefaultCellStyle
                .SelectionBackColor =
                ColorTranslator.FromHtml("#dbeafe");
            dgvStudents.DefaultCellStyle
                .SelectionForeColor =
                ColorTranslator.FromHtml("#1a2744");

            dgvStudents.AlternatingRowsDefaultCellStyle
                .BackColor =
                ColorTranslator.FromHtml("#f8f8f8");
            dgvStudents.AlternatingRowsDefaultCellStyle
                .ForeColor =
                ColorTranslator.FromHtml("#1a2744");
            dgvStudents.AlternatingRowsDefaultCellStyle
                .SelectionBackColor =
                ColorTranslator.FromHtml("#dbeafe");
            dgvStudents.AlternatingRowsDefaultCellStyle
                .SelectionForeColor =
                ColorTranslator.FromHtml("#1a2744");

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

            foreach (DataGridViewColumn col
                in dgvStudents.Columns)
            {
                col.SortMode =
                    DataGridViewColumnSortMode
                        .NotSortable;
            }
        }

        // ════════════════════════════════════════════════════
        //  LOAD STUDENTS
        // ════════════════════════════════════════════════════
        private void LoadStudents()
        {
            _allStudents = DatabaseHelper.GetAllMembers();
            DisplayStudents(_allStudents);
        }

        // ════════════════════════════════════════════════════
        //  DISPLAY STUDENTS
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
        //  FILTER STUDENTS
        // ════════════════════════════════════════════════════
        private void FilterStudents(string searchText)
        {
            if (_allStudents == null) return;

            if (string.IsNullOrWhiteSpace(searchText) ||
                searchText ==
                    "Search by name or username...")
            {
                DisplayStudents(_allStudents);
                return;
            }

            string filter = searchText.ToLower().Trim();
            DataTable filtered = _allStudents.Clone();

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

            lblCount.Text =
                filtered.Rows.Count == 0
                    ? $"No results for \"{searchText}\""
                    : filtered.Rows.Count +
                      " result(s) found";

            lblCount.ForeColor =
                filtered.Rows.Count == 0
                    ? ColorTranslator.FromHtml("#8b2020")
                    : ColorTranslator.FromHtml("#7a6f5a");
        }

        // ════════════════════════════════════════════════════
        //  UPDATE SELECTED LABEL
        // ════════════════════════════════════════════════════
        private void UpdateSelectedLabel()
        {
            if (dgvStudents.SelectedRows.Count == 0 ||
                dgvStudents.SelectedRows[0].IsNewRow)
            {
                lblSelected.Text = "No student selected";
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

            btnAppoint.Enabled = true;
        }

        // ════════════════════════════════════════════════════
        //  APPOINT BUTTON CLICK
        // ════════════════════════════════════════════════════
        private void btnAppoint_Click(object sender, EventArgs e)
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

            // Warn if replacing existing leader
            if (DatabaseHelper.GroupHasLeader(_groupID))
            {
                var warn = MessageBox.Show(
                    $"This group already has a Leader.\n\n" +
                    $"Replace with \"{name}\"?\n\n" +
                    $"The previous Leader will be " +
                    $"reverted to Member.",
                    "SyncPoint — Replace Leader?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (warn != DialogResult.Yes) return;
            }
            else
            {
                var confirm = MessageBox.Show(
                    $"Appoint \"{name}\" as the Leader " +
                    $"of \"{_groupName}\"?\n\n" +
                    $"They can log in as " +
                    $"a Leader.",
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
                $"They can now access " +
                $"the Leader Dashboard.",
                "SyncPoint — Leader Appointed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.Close();
        }
    }
}
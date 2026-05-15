using System;
using System.Data;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint.Forms.Other_Forms
{
    public partial class AddMemberForm : Form
    {
        private readonly int _groupId;
        private readonly string _groupName;
        private DataTable _dtAvailableUsers;

        public AddMemberForm(int groupId, string groupName)
        {
            InitializeComponent();
            _groupId = groupId;
            _groupName = groupName;

            try
            {
                lblGroupName.Text = string.IsNullOrWhiteSpace(_groupName)
                    ? "Group: (Unknown)"
                    : $"Group: {_groupName}";
            }
            catch
            {
                // Designer-time or unexpected state: ignore and let Load handler set it.
            }
        }

        private void AddMemberForm_Load(object sender, EventArgs e)
        {
            lblGroupName.Text = $"Group: {_groupName}";
            LoadAvailableUsers();
        }

        private void LoadAvailableUsers()
        {
            try
            {
                // Fetches registered students (RoleID = 3) who are NOT in the group yet
                _dtAvailableUsers = DatabaseHelper.GetAvailableMembersForGroup(_groupId);

                if (_dtAvailableUsers != null)
                {
                    dgvUsers.DataSource = _dtAvailableUsers;

                    // Ensure only members (exclude leaders RoleID = 2) are shown by default
                    // Combine with any search filter via DefaultView.RowFilter when needed
                    _dtAvailableUsers.DefaultView.RowFilter = BuildCombinedFilter(string.Empty);

                    if (dgvUsers.Columns.Contains("UserID"))
                        dgvUsers.Columns["UserID"].Visible = false;

                    if (dgvUsers.Columns.Contains("FullName"))
                        dgvUsers.Columns["FullName"].HeaderText = "Full Name";

                    if (dgvUsers.Columns.Contains("Username"))
                        dgvUsers.Columns["Username"].HeaderText = "Username";

                    UpdateStatusAndSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Live search/filtering logic
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_dtAvailableUsers == null) return;

            string filterValue = txtSearch.Text.Trim();

            try
            {
                _dtAvailableUsers.DefaultView.RowFilter = BuildCombinedFilter(filterValue);
                UpdateStatusAndSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string BuildCombinedFilter(string searchValue)
        {
            if (_dtAvailableUsers == null)
                return string.Empty;

            string roleFilter = _dtAvailableUsers.Columns.Contains("RoleID") ? "RoleID <> 2" : string.Empty;

            string searchFilter = string.Empty;
            if (!string.IsNullOrEmpty(searchValue))
            {
                string safe = searchValue.Replace("'", "''"); // Escape single quotes for RowFilter
                searchFilter = $"(FullName LIKE '%{safe}%' OR Username LIKE '%{safe}%')";
            }

            if (!string.IsNullOrEmpty(roleFilter) && !string.IsNullOrEmpty(searchFilter))
                return roleFilter + " AND " + searchFilter;

            if (!string.IsNullOrEmpty(roleFilter))
                return roleFilter;

            if (!string.IsNullOrEmpty(searchFilter))
                return searchFilter;

            return string.Empty;
        }

        private void UpdateStatusAndSelection()
        {
            int visibleCount = dgvUsers.Rows.Count;
            lblStatusCount.Text = $"{visibleCount} student(s) available";

            if (dgvUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];
                string fullName = selectedRow.Cells["FullName"].Value?.ToString();
                string username = selectedRow.Cells["Username"].Value?.ToString();

                lblSelectedUser.Text = $"Selected: {fullName} (@{username})";
                btnAddMember.Enabled = true;
            }
            else
            {
                lblSelectedUser.Text = "Selected: None";
                btnAddMember.Enabled = false;
            }
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatusAndSelection();
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student from the list first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];
                int selectedUserId = Convert.ToInt32(selectedRow.Cells["UserID"].Value);
                string selectedFullName = selectedRow.Cells["FullName"].Value?.ToString();

                DatabaseHelper.AddMemberToGroup(_groupId, selectedUserId);

                MessageBox.Show($"{selectedFullName} has been successfully added to group!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not add member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
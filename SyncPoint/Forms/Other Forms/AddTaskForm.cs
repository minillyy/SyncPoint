using System;
using System.Data;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint
{
    public partial class AddTaskForm : Form
    {
        private readonly int _groupId;
        private readonly string _leaderName;

        // Constructor requires the Leader Name to show on the header, and GroupID to find assignees
        public AddTaskForm(int groupId, string leaderName)
        {
            InitializeComponent();
            _groupId = groupId;
            _leaderName = leaderName;
        }

        private void AddTaskForm_Load(object sender, EventArgs e)
        {
            // Set header leader name label (show leader in header title)
            lblHeaderTitle.Text = $"SyncPoint - Add Task";

            // Set default date picker constraints (e.g., minimum of today)
            dtpDeadline.MinDate = DateTime.Today;
            dtpDeadline.Value = DateTime.Today.AddDays(7); // Default to 1 week out

            // Fill ComboBox with members of this specific group from the DB
            LoadGroupMembers();
        }

        private void LoadGroupMembers()
        {
            try
            {
                DataTable membersTable = DatabaseHelper.GetMembersOfGroup(_groupId);

                if (membersTable != null && membersTable.Rows.Count > 0)
                {
                    // Create a new table with a leading "Noone" row whose UserID is DBNull
                    var dt = membersTable.Clone();
                    // Add the Noone row
                    var nooneRow = dt.NewRow();
                    // Make sure columns exist before assigning
                    if (dt.Columns.Contains("UserID")) nooneRow["UserID"] = DBNull.Value;
                    if (dt.Columns.Contains("FullName")) nooneRow["FullName"] = "Any Member";
                    dt.Rows.Add(nooneRow);
                    // Import existing members
                    foreach (DataRow r in membersTable.Rows)
                        dt.ImportRow(r);

                    cmbAssignTo.DataSource = dt;
                    cmbAssignTo.DisplayMember = "FullName"; // What is shown in the ComboBox UI
                    cmbAssignTo.ValueMember = "UserID";     // The key we save to the DB table
                }
                else
                {
                    MessageBox.Show("No members found in this group to assign tasks to.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load group members: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAssignTask_Click(object sender, EventArgs e)
        {
            // 1. Validation
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text) || cmbWeight.SelectedIndex == -1)
            {
                MessageBox.Show("Please enter a title and select a Task Weight (Points).", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Extract Points from ComboBox (Extracts '1', '3', or '5' from the string)
            int selectedWeight = int.Parse(cmbWeight.Text.Substring(0, 1));

            // 3. Get Assigned ID from your "Assign to" ComboBox (assuming it's cmbMembers)
            int? assignedMemberId = null;
            if (cmbAssignTo.SelectedValue != null && cmbAssignTo.SelectedValue != DBNull.Value)
                assignedMemberId = (int)cmbAssignTo.SelectedValue;

            // 4. Save to Database
            DatabaseHelper.CreateTask(
                Session.GroupID,
                txtTaskTitle.Text,
                txtDescription.Text,
                dtpDeadline.Value,
                assignedMemberId,
                selectedWeight
            );

            MessageBox.Show($"Task created with a weight of {selectedWeight} points!", "Success");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
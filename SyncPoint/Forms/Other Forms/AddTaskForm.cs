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
            lblHeaderTitle.Text = $"SyncPoint - Add Task (Leader: {_leaderName})";

            // Task type selection removed - default handled when creating the task

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
            // 1. Validation Checks
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                MessageBox.Show("Please enter a task title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaskTitle.Focus();
                return;
            }

            // 2. Gather Inputs
            string title = txtTaskTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            DateTime deadline = dtpDeadline.Value;
            int? assignedToUserId = null;
            if (cmbAssignTo.SelectedValue != null && cmbAssignTo.SelectedValue != DBNull.Value)
                assignedToUserId = Convert.ToInt32(cmbAssignTo.SelectedValue);

            // Set default difficulty to 1 (as seen in database table constraint)
            int difficulty = 1;

            // 3. Write data to your DB via the DatabaseHelper helper class
            try
            {
                // TaskType is no longer selected in the form; default to "Individual"
                string taskType = "Individual";

                int newTaskID = DatabaseHelper.CreateTask(
                    _groupId,
                    title,
                    description,
                    deadline,
                    difficulty,
                    taskType,
                    assignedToUserId
                );

                if (newTaskID > 0)
                {
                    this.DialogResult = DialogResult.OK; // Signals success to the main window
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save the task. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving task to the database:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
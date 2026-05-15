using System;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint.Forms.Other_Forms
{
    public partial class AddTaskForm : Form
    {
        private readonly int _groupId;

        public AddTaskForm(int groupId, string leaderName)
        {
            InitializeComponent();
            _groupId = groupId;
        }

        private void AddTaskForm_Load(object sender, EventArgs e)
        {
            lblHeaderTitle.Text = "SyncPoint - Add Group Task";

            dtpDeadline.MinDate = DateTime.Today;
            dtpDeadline.Value = DateTime.Today.AddDays(7);
        }

        private void btnAssignTask_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text) || cmbWeight.SelectedIndex == -1)
            {
                MessageBox.Show("Please enter a title and select a Task Weight (Points).", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedWeight = int.Parse(cmbWeight.Text.Substring(0, 1));

            int? assignedMemberId = null;

            DatabaseHelper.CreateTask(
                Session.GroupID,
                txtTaskTitle.Text,
                txtDescription.Text,
                dtpDeadline.Value,
                assignedMemberId,
                selectedWeight
            );

            MessageBox.Show($"Task created successfully! It is now available for any member to claim.", "Success");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
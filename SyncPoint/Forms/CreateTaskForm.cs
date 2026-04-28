using System;
using System.Data;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Helpers;

namespace SyncPoint.Forms
{
    public partial class CreateTaskForm : Form
    {
        public CreateTaskForm()
        {
            InitializeComponent();
        }

        private void CreateTaskForm_Load(object sender, EventArgs e)
        {
            LoadMembers();

            cmbDifficulty.Items.Clear();
            cmbDifficulty.Items.Add("1");
            cmbDifficulty.Items.Add("2");
            cmbDifficulty.Items.Add("3");
            cmbDifficulty.Items.Add("4");
            cmbDifficulty.Items.Add("5");

            cmbDifficulty.SelectedIndex = 0;

            cmbTaskType.Items.Clear();
            cmbTaskType.Items.Add("Individual");
            cmbTaskType.Items.Add("Group");

            cmbTaskType.SelectedIndex = 0;

            dtpDeadline.Value = DateTime.Now.AddDays(3);
        }

        private void LoadMembers()
        {
            DataTable dt =
                DatabaseHelper.GetGroupMembers(Session.GroupID);

            cmbAssignedTo.DataSource = dt;
            cmbAssignedTo.DisplayMember = "FullName";
            cmbAssignedTo.ValueMember = "UserID";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Please enter task title.");
                return;
            }

            int assignedUser =
                Convert.ToInt32(cmbAssignedTo.SelectedValue);

            int difficulty =
                Convert.ToInt32(cmbDifficulty.SelectedItem);

            DatabaseHelper.CreateTask(
                Session.GroupID,
                txtTitle.Text.Trim(),
                txtDescription.Text.Trim(),
                dtpDeadline.Value,
                difficulty,
                cmbTaskType.Text,
                assignedUser
            );

            MessageBox.Show(
                "Task created successfully!",
                "SyncPoint",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
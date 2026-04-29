using System;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint.Forms
{
    public partial class CreateGroupForm : Form
    {
        public CreateGroupForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string groupName = txtGroupName.Text.Trim();

            if (string.IsNullOrEmpty(groupName))
            {
                MessageBox.Show(
                    "Please enter a group name.",
                    "SyncPoint",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtGroupName.Focus();
                return;
            }

            if (groupName.Length < 3)
            {
                MessageBox.Show(
                    "Group name must be at least 3 characters.",
                    "SyncPoint",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int groupID = DatabaseHelper.CreateGroup(groupName, Session.UserID);

            MessageBox.Show(
                $"Group \"{groupName}\" created successfully!\n\n" +
                "You can now appoint a Leader for this group.",
                "SyncPoint — Group Created",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.Close();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using SyncPoint.Data;
using SyncPoint.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncPoint
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Password_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblTotal_NumClick(object sender, EventArgs e)
        {

        }
        private void Username_Click(object sender, EventArgs e)
        {

        }

        private void Login(string expectedRole)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your username and password.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = DatabaseHelper.ValidateLogin(username, password);

            if (user == null)
            {
                MessageBox.Show("Incorrect username or password.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string actualRole = user["RoleName"].ToString();

            if (actualRole != expectedRole)
            {
                MessageBox.Show(
                    $"This account is not a {expectedRole}.\n" +
                    $"Please use the correct login button for your role.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Session.UserID = Convert.ToInt32(user["UserID"]);
            Session.FullName = user["FullName"].ToString();
            Session.Username = user["Username"].ToString();
            Session.RoleID = Convert.ToInt32(user["RoleID"]);
            Session.RoleName = actualRole;
            Session.GroupID = DatabaseHelper.GetUserGroupID(Session.UserID);

            this.Hide();

            switch (actualRole)
            {
                case "Instructor":
                    break;
                case "Leader":
                    new LeaderDashboardForm().ShowDialog();
                    break;
                case "Member":
                    break;
            }

            this.Show();
        }

        private void btnInstructor_Click(object sender, EventArgs e) => Login("Instructor");
        private void btnLeader_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (user == "juan" && pass == "password")
            {
                this.Hide();
                var dashboard = new LeaderDashboardForm();
                dashboard.FormClosed += (s, args) => this.Close();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Invalid credentials.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnMember_Click(object sender, EventArgs e) => Login("Member");

        private void NoAccount_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            var registerForm = new RegisterForm();
            registerForm.FormClosed += (s, args) => this.Show();
            registerForm.ShowDialog();
        }
    }
}

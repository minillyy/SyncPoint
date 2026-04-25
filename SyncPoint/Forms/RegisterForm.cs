using SyncPoint.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncPoint.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            StyleTextBox(txtFullName, "Enter your full name");
            StyleTextBox(txtUsername, "Choose a username");
        }

        private void lblSub_Click(object sender, EventArgs e)
        {

        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void StyleTextBox(TextBox txt, string placeholder)
        {
            txt.Text = placeholder;
            txt.ForeColor = ColorTranslator.FromHtml("#b0a898");

            txt.Enter += (s, e) => {
                if (txt.Text == placeholder)
                {
                    txt.Text = "";
                    txt.ForeColor = ColorTranslator.FromHtml("#2c2416");
                }
            };

            txt.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = placeholder;
                    txt.ForeColor = ColorTranslator.FromHtml("#b0a898");
                }
            };
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                txtFullName.Text == "Enter your full name")
            {
                MessageBox.Show("Please enter your full name.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                txtUsername.Text == "Choose a username")
            {
                MessageBox.Show("Please enter a username.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (txtUsername.Text.Length < 4)
            {
                MessageBox.Show("Username must be at least 4 characters.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show("Passwords do not match.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirm.Focus();
                return false;
            }

            return true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            bool success = DatabaseHelper.RegisterUser(
                fullName, username, password, "Member");

            if (success)
            {
                MessageBox.Show(
                    $"Account created!\n\nUsername: {username}\n\n" +
                    "You will be added to a group by your Instructor.",
                    "SyncPoint — Welcome!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "That username is already taken. Please choose another.",
                    "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                txtUsername.SelectAll();
            }
        }
    }
}

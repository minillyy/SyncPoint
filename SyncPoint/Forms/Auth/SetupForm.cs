using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SyncPoint.Forms.Auth
{
    public partial class SetupForm : Form
    {
        private static readonly string ConfigPath =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "instructor.config");

        public SetupForm()
        {
            InitializeComponent();
        }

        // ════════════════════════════════════════════════
        //  FORM LOAD
        // ════════════════════════════════════════════════
        private void SetupForm_Load(
            object sender, EventArgs e)
        {
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

            pnlButtons.Paint += (s, pe) =>
            {
                var pen = new Pen(
                    ColorTranslator.FromHtml("#d4c9b0"),
                    1);
                pe.Graphics.DrawLine(pen,
                    0, 0,
                    pnlButtons.Width, 0);
            };

            btnCreate.FlatAppearance.BorderColor =
                ColorTranslator.FromHtml("#c9a84c");
            btnCreate.FlatAppearance.BorderSize = 1;

            txtFullName.Select();
        }

        // ════════════════════════════════════════════════
        //  CREATE BUTTON CLICK
        // ════════════════════════════════════════════════
        private void btnCreate_Click(
            object sender, EventArgs e)
        {
            // Validate Full Name
            if (string.IsNullOrWhiteSpace(
                txtFullName.Text))
            {
                MessageBox.Show(
                    "Please enter the instructor's " +
                    "full name.",
                    "SyncPoint — Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            // Validate Username 
            if (string.IsNullOrWhiteSpace(
                txtUsername.Text))
            {
                MessageBox.Show(
                    "Please enter a username.",
                    "SyncPoint — Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (txtUsername.Text.Trim().Length < 4)
            {
                MessageBox.Show(
                    "Username must be at least " +
                    "4 characters.",
                    "SyncPoint — Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            // No spaces in username
            if (txtUsername.Text.Contains(" "))
            {
                MessageBox.Show(
                    "Username cannot contain spaces.",
                    "SyncPoint — Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            // Validate Password
            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show(
                    "Password must be at least " +
                    "6 characters.",
                    "SyncPoint — Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Validate Confirm Password
            if (txtPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show(
                    "Passwords do not match.\n" +
                    "Please try again.",
                    "SyncPoint — Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtConfirm.Clear();
                txtConfirm.Focus();
                return;
            }

            // Write config file 
            try
            {
                string[] lines = {
                    "# SyncPoint Instructor Config",
                    "# Auto-generated on first run.",
                    "# Do not share or push to GitHub.",
                    $"username={txtUsername.Text.Trim()}",
                    $"password={txtPassword.Text}",
                    $"fullname={txtFullName.Text.Trim()}"
                };

                File.WriteAllLines(ConfigPath, lines);

                MessageBox.Show(
                    "Instructor account set up!\n\n" +
                    "Full Name:  " +
                    txtFullName.Text.Trim() + "\n" +
                    "Username:   " +
                    txtUsername.Text.Trim() + "\n\n" +
                    "Please remember these credentials.\n" +
                    "The application will now start.",
                    "SyncPoint — Setup Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to create config file.\n\n" +
                    "Error: " + ex.Message,
                    "SyncPoint — Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
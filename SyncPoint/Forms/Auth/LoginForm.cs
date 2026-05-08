using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Forms;

namespace SyncPoint
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // ════════════════════════════════════════════════════
        //  FORM LOAD
        // ════════════════════════════════════════════════════
        private void LoginForm_Load(
            object sender, EventArgs e)
        {
            // Style the login button border
            btnLogin.FlatAppearance.BorderColor =
                ColorTranslator.FromHtml("#c9a84c");
            btnLogin.FlatAppearance.BorderSize = 1;

            // Paint gold border on header
            pnlHeader.Paint += (s, pe) =>
            {
                var pen = new Pen(
                    ColorTranslator.FromHtml("#c9a84c"), 3);
                pe.Graphics.DrawLine(pen,
                    0, pnlHeader.Height - 2,
                    pnlHeader.Width,
                    pnlHeader.Height - 2);
            };

            // Allow pressing Enter to login
            this.AcceptButton = btnLogin;

            // Focus on username when form opens
            txtUsername.Select();
        }

        // ════════════════════════════════════════════════════
        //  LOGIN BUTTON CLICK
        // ════════════════════════════════════════════════════
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        // ════════════════════════════════════════════════════
        //  CORE LOGIN METHOD
        //  Validates credentials and detects role
        //  automatically from the database
        // ════════════════════════════════════════════════════
        private void Login()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // ── Step 1: Check inputs are not empty ────────
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show(
                    "Please enter your username.",
                    "SyncPoint",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Please enter your password.",
                    "SyncPoint",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // ── Step 2: Validate against database ─────────
            // ValidateLogin hashes the password and
            // compares it with what's stored in the DB
            DataRow user =
                DatabaseHelper.ValidateLogin(
                    username, password);

            // No matching user found
            if (user == null)
            {
                MessageBox.Show(
                    "Incorrect username or password.\n" +
                    "Please try again.",
                    "SyncPoint — Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Clear password and refocus
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            // ── Step 3: Read role from database ───────────
            // No need for the user to tell us their role
            // — we read it directly from the DB
            string role = user["RoleName"].ToString();

            // ── Step 4: Save session data ─────────────────
            Session.UserID =
                Convert.ToInt32(user["UserID"]);
            Session.FullName =
                user["FullName"].ToString();
            Session.Username =
                user["Username"].ToString();
            Session.RoleID =
                Convert.ToInt32(user["RoleID"]);
            Session.RoleName = role;
            // Prefer GroupID from the validated user row if present;
            // fall back to the helper lookup only when necessary.
            if (user.Table.Columns.Contains("GroupID") &&
                user["GroupID"] != DBNull.Value)
            {
                Session.GroupID = Convert.ToInt32(user["GroupID"]);
            }
            else
            {
                Session.GroupID =
                    DatabaseHelper.GetUserGroupID(
                        Session.UserID);
            }

            // ── Step 5: Open the correct dashboard ────────
            // Prevent members without a group from opening the Member Dashboard
            if (role == "Member" && Session.GroupID == -1)
            {
                MessageBox.Show(
                    "You must be a member of a group to access the Member Dashboard.",
                    "SyncPoint",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            OpenDashboard(role);
        }

        // ════════════════════════════════════════════════════
        //  OPEN DASHBOARD
        //  Automatically routes to the right form
        //  based on the role read from the database
        // ════════════════════════════════════════════════════
        private void OpenDashboard(string role)
        {
            this.Hide();

            switch (role)
            {
                case "Instructor":
                    new InstructorDashboardForm()
                        .ShowDialog();
                    break;

                case "Leader":
                    new LeaderDashboardForm()
                        .ShowDialog();
                    break;

                case "Member":
                    new MemberDashboardForm()
                        .ShowDialog();
                    break;

                default:
                    // Unknown role — should never happen
                    // but handle it gracefully
                    MessageBox.Show(
                        $"Unknown role: {role}\n\n" +
                        "Please contact your administrator.",
                        "SyncPoint — Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
            }

            // When dashboard closes, clear fields
            // and show login again for the next user
            // Guard against the case where the login
            // form was disposed while the dashboard
            // was open (prevents ObjectDisposedException).
            if (this.IsDisposed || this.Disposing)
                return;

            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
            try
            {
                this.Show();
            }
            catch (ObjectDisposedException)
            {
                // If disposal races with Show, ignore —
                // the application is shutting down or
                // another flow disposed the login form.
            }
        }

        // ════════════════════════════════════════════════════
        //  REGISTER LINK
        // ════════════════════════════════════════════════════

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new RegisterForm().ShowDialog();
        }
    }
}
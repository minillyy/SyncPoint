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
        // ── Pre-determined Instructor credentials ────────────
        // These are fixed and cannot be changed from the UI
        // The instructor account is seeded in the database
        private const string INSTRUCTOR_USERNAME = "instructor";
        private const string INSTRUCTOR_PASSWORD = "instructor123";

        public LoginForm()
        {
            InitializeComponent();
        }

        // ── Form Load ────────────────────────────────────────
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Paint gold border on header
            panelHeader.Paint += (s, pe) =>
            {
                var pen = new Pen(
                    ColorTranslator.FromHtml("#c9a84c"), 3);
                pe.Graphics.DrawLine(pen,
                    0, panelHeader.Height - 2,
                    panelHeader.Width, panelHeader.Height - 2);
            };

            // Style the flat buttons border
            btnInstructor.FlatAppearance.BorderColor =
                ColorTranslator.FromHtml("#c9a84c");
            btnInstructor.FlatAppearance.BorderSize = 1;

            btnLeader.FlatAppearance.BorderColor =
                ColorTranslator.FromHtml("#c4b89a");
            btnLeader.FlatAppearance.BorderSize = 1;

            btnMember.FlatAppearance.BorderColor =
                ColorTranslator.FromHtml("#c4b89a");
            btnMember.FlatAppearance.BorderSize = 1;

            // Allow pressing Enter to submit
            this.AcceptButton = btnMember;

            // Focus on username field
            txtUsername.Select();
        }

        // ════════════════════════════════════════════════════
        //  CORE LOGIN METHOD
        //  expectedRole = "Instructor" / "Leader" / "Member"
        // ════════════════════════════════════════════════════
        private void Login(string expectedRole)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // ── Validate inputs ───────────────────────────────
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

            // ── Validate against database ─────────────────────
            DataRow user =
                DatabaseHelper.ValidateLogin(username, password);

            if (user == null)
            {
                MessageBox.Show(
                    "Incorrect username or password.\n" +
                    "Please try again.",
                    "SyncPoint — Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            string actualRole = user["RoleName"].ToString();

            // ── Check role matches button clicked ─────────────
            if (actualRole != expectedRole)
            {
                MessageBox.Show(
                    GetRoleMismatchMessage(
                        actualRole, expectedRole),
                    "SyncPoint — Wrong Login Button",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // ── All good — open dashboard ─────────────────────
            LoadSession(user);
            OpenDashboard(actualRole);
        }

        // ════════════════════════════════════════════════════
        //  ROLE MISMATCH MESSAGE
        // ════════════════════════════════════════════════════
        private string GetRoleMismatchMessage(
            string actualRole, string expectedRole)
        {
            if (actualRole == "Instructor")
                return
                    "This is an Instructor account.\n" +
                    "Please click the Instructor button.";

            if (actualRole == "Leader" &&
                expectedRole == "Member")
                return
                    "You have been appointed as Leader.\n" +
                    "Please use the Leader button instead.";

            if (actualRole == "Member" &&
                expectedRole == "Leader")
                return
                    "You are not a Leader yet.\n\n" +
                    "Ask your Instructor to appoint " +
                    "you as Leader first.";

            return
                $"This account is registered as " +
                $"{actualRole}.\n" +
                $"Please use the {actualRole} button.";
        }

        // ════════════════════════════════════════════════════
        //  LOAD SESSION
        //  Saves the logged-in user's data into Session
        // ════════════════════════════════════════════════════
        private void LoadSession(DataRow user)
        {
            Session.UserID = Convert.ToInt32(user["UserID"]);
            Session.FullName = user["FullName"].ToString();
            Session.Username = user["Username"].ToString();
            Session.RoleID = Convert.ToInt32(user["RoleID"]);
            Session.RoleName = user["RoleName"].ToString();
            Session.GroupID = DatabaseHelper.GetUserGroupID(Session.UserID);
        }

        // ════════════════════════════════════════════════════
        //  OPEN DASHBOARD
        //  Opens the correct form based on role
        // ════════════════════════════════════════════════════
        private void OpenDashboard(string role)
        {
            this.Hide();

            switch (role)
            {
                case "Instructor":
                    new InstructorDashboardForm().ShowDialog();
                    break;

                case "Leader":
                    new LeaderDashboardForm().ShowDialog();
                    break;

                case "Member":
                    // TO ADD ONCE MEMBER DASHBOARD IS READY
                     MessageBox.Show(
                        "Member dashboard is under construction.\n\n" +
                        "Please check back later.",
                        "SyncPoint",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    break;
            }

            // When the dashboard closes, show login again
            // so another user can log in
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
            this.Show();
        }

        // ════════════════════════════════════════════════════
        //  BUTTON CLICK EVENTS
        // ════════════════════════════════════════════════════

        // Instructor button
        private void btnInstructor_Click(object sender, EventArgs e)
        {
            Login("Instructor");
        }

        // Leader button
        private void btnLeader_Click(object sender, EventArgs e)
        {
            Login("Leader");
        }

        // Member button
        private void btnMember_Click(object sender, EventArgs e)
        {
            Login("Member");
        }

        // Register link
        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new RegisterForm().ShowDialog();
        }

        // ════════════════════════════════════════════════════
        //  KEYBOARD SHORTCUT
        //  Allow pressing Enter on the password field
        //  to trigger the last clicked button
        // ════════════════════════════════════════════════════
        private Button _lastClickedButton = null;

        private void btnInstructor_MouseDown(object sender, MouseEventArgs e)
        {
            _lastClickedButton = btnInstructor;
        }

        private void btnLeader_MouseDown(object sender, MouseEventArgs e)
        {
            _lastClickedButton = btnLeader;
        }

        private void btnMember_MouseDown(object sender, MouseEventArgs e)
        {
            _lastClickedButton = btnMember;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_lastClickedButton != null)
                    _lastClickedButton.PerformClick();
                else
                    btnMember.PerformClick(); // default
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new RegisterForm().ShowDialog();
        }
    }
}
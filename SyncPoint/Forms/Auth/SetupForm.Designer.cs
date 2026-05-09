namespace SyncPoint.Forms.Auth
{
    partial class SetupForm
    {
        private System.ComponentModel.IContainer
            components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCreate = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ──────────────────────────────
            this.pnlHeader.BackColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#1a2744");
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location =
                new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size =
                new System.Drawing.Size(420, 70);
            this.pnlHeader.TabIndex = 0;

            // ── lblTitle ───────────────────────────────
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Georgia", 13f,
                    System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#f5f0e8");
            this.lblTitle.Location =
                new System.Drawing.Point(16, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text =
                "First Time Setup";

            // ── lblSub ─────────────────────────────────
            this.lblSub.AutoSize = true;
            this.lblSub.Font =
                new System.Drawing.Font(
                    "Arial", 9f);
            this.lblSub.ForeColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#c9a84c");
            this.lblSub.Location =
                new System.Drawing.Point(16, 44);
            this.lblSub.Name = "lblSub";
            this.lblSub.Text =
                "Set up the Instructor account";

            // ── pnlBody ────────────────────────────────
            this.pnlBody.BackColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#faf7f2");
            this.pnlBody.Controls.Add(this.txtConfirm);
            this.pnlBody.Controls.Add(this.lblConfirm);
            this.pnlBody.Controls.Add(this.txtPassword);
            this.pnlBody.Controls.Add(this.lblPassword);
            this.pnlBody.Controls.Add(this.txtUsername);
            this.pnlBody.Controls.Add(this.lblUsername);
            this.pnlBody.Controls.Add(this.txtFullName);
            this.pnlBody.Controls.Add(this.lblFullName);
            this.pnlBody.Controls.Add(this.lblInfo);
            this.pnlBody.Dock =
                System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location =
                new System.Drawing.Point(0, 70);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding =
                new System.Windows.Forms.Padding(
                    20, 12, 20, 12);
            this.pnlBody.Size =
                new System.Drawing.Size(420, 290);
            this.pnlBody.TabIndex = 1;

            // ── lblInfo ────────────────────────────────
            this.lblInfo.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Font =
                new System.Drawing.Font("Arial", 8.5f);
            this.lblInfo.ForeColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#7a6f5a");
            this.lblInfo.Height = 38;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Text =
                "These credentials will be used to " +
                "log in as Instructor. Store them " +
                "safely — they will not be shown again.";

            // ── lblFullName ────────────────────────────
            this.lblFullName.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.lblFullName.Font =
                new System.Drawing.Font(
                    "Arial", 9f,
                    System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#1a2744");
            this.lblFullName.Height = 22;
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Text =
                "Instructor Full Name *";

            // ── txtFullName ────────────────────────────
            this.txtFullName.BackColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#fdf9f3");
            this.txtFullName.BorderStyle =
                System.Windows.Forms.BorderStyle
                    .FixedSingle;
            this.txtFullName.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.txtFullName.Font =
                new System.Drawing.Font("Arial", 10f);
            this.txtFullName.Height = 34;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.TabIndex = 0;

            // ── lblUsername ────────────────────────────
            this.lblUsername.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.lblUsername.Font =
                new System.Drawing.Font(
                    "Arial", 9f,
                    System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#1a2744");
            this.lblUsername.Height = 26;
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Text = "Username *";

            // ── txtUsername ────────────────────────────
            this.txtUsername.BackColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#fdf9f3");
            this.txtUsername.BorderStyle =
                System.Windows.Forms.BorderStyle
                    .FixedSingle;
            this.txtUsername.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.txtUsername.Font =
                new System.Drawing.Font("Arial", 10f);
            this.txtUsername.Height = 34;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.TabIndex = 1;

            // ── lblPassword ────────────────────────────
            this.lblPassword.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.lblPassword.Font =
                new System.Drawing.Font(
                    "Arial", 9f,
                    System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#1a2744");
            this.lblPassword.Height = 26;
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Text = "Password *";

            // ── txtPassword ────────────────────────────
            this.txtPassword.BackColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#fdf9f3");
            this.txtPassword.BorderStyle =
                System.Windows.Forms.BorderStyle
                    .FixedSingle;
            this.txtPassword.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.txtPassword.Font =
                new System.Drawing.Font("Arial", 10f);
            this.txtPassword.Height = 34;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.TabIndex = 2;

            // ── lblConfirm ─────────────────────────────
            this.lblConfirm.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.lblConfirm.Font =
                new System.Drawing.Font(
                    "Arial", 9f,
                    System.Drawing.FontStyle.Bold);
            this.lblConfirm.ForeColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#1a2744");
            this.lblConfirm.Height = 26;
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Text =
                "Confirm Password *";

            // ── txtConfirm ─────────────────────────────
            this.txtConfirm.BackColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#fdf9f3");
            this.txtConfirm.BorderStyle =
                System.Windows.Forms.BorderStyle
                    .FixedSingle;
            this.txtConfirm.Dock =
                System.Windows.Forms.DockStyle.Top;
            this.txtConfirm.Font =
                new System.Drawing.Font("Arial", 10f);
            this.txtConfirm.Height = 34;
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '●';
            this.txtConfirm.TabIndex = 3;

            // ── pnlButtons ─────────────────────────────
            this.pnlButtons.BackColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#f0ebe0");
            this.pnlButtons.Controls.Add(this.btnCreate);
            this.pnlButtons.Dock =
                System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Height = 56;
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding =
                new System.Windows.Forms.Padding(
                    16, 10, 16, 10);
            this.pnlButtons.TabIndex = 2;

            // ── btnCreate ──────────────────────────────
            this.btnCreate.BackColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#1a2744");
            this.btnCreate.Dock =
                System.Windows.Forms.DockStyle.Fill;
            this.btnCreate.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font =
                new System.Drawing.Font(
                    "Arial", 10f,
                    System.Drawing.FontStyle.Bold);
            this.btnCreate.ForeColor =
                System.Drawing.Color.White;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Text =
                "Create Instructor Account";
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Click +=
                new System.EventHandler(
                    this.btnCreate_Click);

            // ── SetupForm ──────────────────────────────
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(6f, 13f);
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor =
                System.Drawing.ColorTranslator
                    .FromHtml("#faf7f2");
            this.ClientSize =
                new System.Drawing.Size(420, 420);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle
                    .FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition
                    .CenterScreen;
            this.Text =
                "SyncPoint — First Time Setup";
            this.Load +=
                new System.EventHandler(
                    this.SetupForm_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCreate;
    }
}
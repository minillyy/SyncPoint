using System;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Forms.Auth;       // ← LoginForm, RegisterForm, SetupForm
using SyncPoint.Forms.Dashboards;  // ← InstructorDashboardForm, etc.

namespace SyncPoint
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(
                false);

            // Check if first run
            if (FirstRunSetup.IsFirstRun())
            {
                bool setupCompleted =
                    FirstRunSetup.RunSetup();

                if (!setupCompleted)
                {
                    MessageBox.Show(
                        "Setup was not completed.\n\n" +
                        "The application will now close.",
                        "SyncPoint",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            // Initialize database
            DatabaseHelper.InitializeDatabase();

            // Open Login form
            Application.Run(new LoginForm());
        }
    }
}
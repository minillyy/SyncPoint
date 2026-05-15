using System;
using System.IO;
using System.Windows.Forms;
using SyncPoint.Forms.Auth;

namespace SyncPoint.Data
{
    /// <summary>
    /// Runs on first launch if instructor.config
    /// is missing. Asks the instructor to set their
    /// credentials and creates the config file.
    /// Only runs once — never again after that.
    /// </summary>
    public static class FirstRunSetup
    {
        private static readonly string ConfigPath =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "instructor.config");

        // Check if setup is needed 
        public static bool IsFirstRun()
        {
            return !File.Exists(ConfigPath);
        }

        // Run the setup wizard
        public static bool RunSetup()
        {
            MessageBox.Show(
                "Welcome to SyncPoint!\n\n" +
                "This appears to be the first time " +
                "the application is running.\n\n" +
                "Please set up the Instructor account " +
                "credentials on the next screen.",
                "SyncPoint — First Time Setup",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            using (var form = new Forms.Auth.SetupForm())
            {
                var result = form.ShowDialog();
                return result == DialogResult.OK;
            }
        }
    }
}
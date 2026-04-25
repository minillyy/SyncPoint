using System;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DatabaseHelper.InitializeDatabase();
            Application.Run(new LoginForm());
        }
    }
}

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
    public partial class SidebarControl : UserControl
    {
        public SidebarControl()
        {
            InitializeComponent();
        }

        // Raised when the user clicks the Add Task item in the sidebar
        public event EventHandler AddTaskClicked;

        private void pnlNav_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SidebarControl_Load(object sender, EventArgs e)
        {

        }

        private void pnlFooter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlNav_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblDashboard_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            parent.Hide();
            new LeaderDashboardForm().ShowDialog();
            parent.Close();
        }

        private void lblAddTask_Click(object sender, EventArgs e)
        {
            AddTaskClicked?.Invoke(this, EventArgs.Empty);
        }

        private void lblMembers_Click(object sender, EventArgs e)
        {
            
        }

        private void lblProgress_Click(object sender, EventArgs e)
        {
            
        }

        private void lblReports_Click(object sender, EventArgs e)
        {
            
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to logout?",
                "SyncPoint", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Session.Clear();
                Application.Restart();
            }
        }

        private void pnlFooter_Paint_1(object sender, PaintEventArgs e)
        {
            var pen = new Pen(ColorTranslator.FromHtml("#2e3f5c"), 1);
            e.Graphics.DrawLine(pen, 0, 0, pnlFooter.Width, 0);
        }

        public void SetActive(string pageName)
        {
            foreach (Control c in pnlNav.Controls)
            {
                if (c is Label lbl)
                {
                    bool isActive = lbl.Text == pageName;

                    lbl.ForeColor = isActive
                        ? ColorTranslator.FromHtml("#f5f0e8")
                        : ColorTranslator.FromHtml("#8fa3c4");  

                    lbl.BackColor = isActive
                        ? Color.FromArgb(30, 201, 168, 76)
                        : Color.Transparent;
                }
            }
        }
    }
}

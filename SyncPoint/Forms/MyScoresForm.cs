using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Helpers;

namespace SyncPoint.Forms
{
    public partial class MyScoresForm : Form
    {
        public MyScoresForm()
        {
            InitializeComponent();
        }

        private void MyScoresForm_Load(object sender, EventArgs e)
        {
            sidebarControl1.SetActive("Scores");

            lblUserName.Text = "Member: " + Session.FullName;

            LoadStats();
            LoadScores();
        }

        private void LoadStats()
        {
            DataTable dt =
                DatabaseHelper.GetTasksByMember(Session.UserID);

            int totalTasks = dt.Rows.Count;
            int completed = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["Status"].ToString() == "Completed")
                    completed++;
            }

            lblTotalTasks.Text = totalTasks.ToString();
            lblCompleted.Text = completed.ToString();
        }

        private void LoadScores()
        {
            DataTable tasks =
                DatabaseHelper.GetTasksByMember(Session.UserID);

            DataTable finalTable = new DataTable();

            finalTable.Columns.Add("Task");
            finalTable.Columns.Add("Status");
            finalTable.Columns.Add("Difficulty");
            finalTable.Columns.Add("Estimated Score");

            foreach (DataRow row in tasks.Rows)
            {
                int diff =
                    Convert.ToInt32(row["Difficulty"]);

                int score = diff * 10;

                finalTable.Rows.Add(
                    row["Title"],
                    row["Status"],
                    row["Difficulty"],
                    score);
            }

            dgvScores.Columns.Clear();
            dgvScores.DataSource = finalTable;

            dgvScores.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#faf7f2");

            dgvScores.ColumnHeadersDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#1a2744");

            dgvScores.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvScores.EnableHeadersVisualStyles = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStats();
            LoadScores();
        }

        private void pnlTopbar_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(
                ColorTranslator.FromHtml("#c9a84c"), 3);

            e.Graphics.DrawLine(
                pen,
                0,
                pnlTopbar.Height - 2,
                pnlTopbar.Width,
                pnlTopbar.Height - 2);
        }

        private void dgvScores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
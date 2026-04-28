using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SyncPoint.Data;
using SyncPoint.Helpers;

namespace SyncPoint.Forms
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            sidebarControl1.SetActive("Reports");

            lblUserName.Text = "Instructor: " + Session.FullName;

            LoadStats();
            LoadScores();
        }

        private void LoadStats()
        {
            DataTable groups =
                DatabaseHelper.GetGroupsByInstructor(Session.UserID);

            int totalGroups = groups.Rows.Count;
            int totalReports = 0;

            foreach (DataRow row in groups.Rows)
            {
                int groupID =
                    Convert.ToInt32(row["GroupID"]);

                DataTable reports =
                    DatabaseHelper.GetReportsByGroup(groupID);

                totalReports += reports.Rows.Count;
            }

            lblTotalGroups.Text =
                totalGroups.ToString();

            lblTotalReports.Text =
                totalReports.ToString();
        }

        private void LoadScores()
        {
            DataTable groups =
                DatabaseHelper.GetGroupsByInstructor(Session.UserID);

            DataTable finalTable = new DataTable();

            finalTable.Columns.Add("Member");
            finalTable.Columns.Add("TasksDone");
            finalTable.Columns.Add("TotalScore");
            finalTable.Columns.Add("AvgScore");

            foreach (DataRow row in groups.Rows)
            {
                int groupID =
                    Convert.ToInt32(row["GroupID"]);

                DataTable scores =
                    DatabaseHelper.GetScoresByGroup(groupID);

                foreach (DataRow scoreRow in scores.Rows)
                {
                    finalTable.Rows.Add(
                        scoreRow["FullName"],
                        scoreRow["TasksDone"],
                        scoreRow["TotalScore"],
                        scoreRow["AvgScore"]);
                }
            }

            dgvReports.Columns.Clear();
            dgvReports.DataSource = finalTable;

            dgvReports.AlternatingRowsDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#faf7f2");

            dgvReports.ColumnHeadersDefaultCellStyle.BackColor =
                ColorTranslator.FromHtml("#1a2744");

            dgvReports.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvReports.EnableHeadersVisualStyles = false;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DataTable groups =
                DatabaseHelper.GetGroupsByInstructor(Session.UserID);

            foreach (DataRow row in groups.Rows)
            {
                int groupID =
                    Convert.ToInt32(row["GroupID"]);

                DatabaseHelper.GenerateReport(
                    groupID,
                    Session.UserID);
            }

            LoadStats();

            MessageBox.Show(
                "Reports generated successfully.");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStats();
            LoadScores();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Export feature can be added next.");
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

        private void dgvReports_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
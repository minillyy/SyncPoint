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

namespace SyncPoint.Forms.Other_Forms
{
    public partial class ReportsForm : Form
    {
        private int _currentGroupId;
        public ReportsForm(int? targetGroupId = null)
        {
            _currentGroupId = targetGroupId ?? Session.GroupID;
            InitializeComponent();
            this.Load += (s, e) => LoadAllReports();
        }

        private void LoadAllReports()
        {
            dgvLeaderboard.DataSource = DatabaseHelper.GetLeaderboard(_currentGroupId);
            dgvDistribution.DataSource = DatabaseHelper.GetTaskDistribution(_currentGroupId);

            FormatGrid(dgvLeaderboard);
            FormatGrid(dgvDistribution);
        }

        private void FormatGrid(DataGridView dgv)
        {
            dgv.RowTemplate.Height = 50;
            dgv.ColumnHeadersHeight = 50;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Height = 50;
            }

            dgv.ReadOnly = true;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11f, FontStyle.Bold); // Slightly larger font
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(44, 62, 80);

            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.FromArgb(235, 235, 235);

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 240);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 245, 240);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}

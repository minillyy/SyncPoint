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
        public ReportsForm()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadAllReports();
        }

        private void LoadAllReports()
        {
            // 1. Fetch Data
            dgvLeaderboard.DataSource = DatabaseHelper.GetLeaderboard(Session.GroupID);
            dgvDistribution.DataSource = DatabaseHelper.GetTaskDistribution(Session.GroupID);

            // 2. Apply "Locked" Formatting
            FormatGrid(dgvLeaderboard);
            FormatGrid(dgvDistribution);
        }

        private void FormatGrid(DataGridView dgv)
        {
            // --- 1. SIZE & DIMENSIONS ---
            dgv.RowTemplate.Height = 50;      // Big rows
            dgv.ColumnHeadersHeight = 50;    // Big headers
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Force existing rows to resize immediately
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Height = 50;
            }

            // --- 2. LOCKDOWN (No Resize, No Sort, No Edit) ---
            dgv.ReadOnly = true;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // --- 3. HEADER STYLING (44, 62, 80) ---
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11f, FontStyle.Bold); // Slightly larger font
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(44, 62, 80);

            // --- 4. BODY STYLING (No Blue Highlights) ---
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.FromArgb(235, 235, 235);

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10f);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Selection "Ghosting" (Highlights stay white/beige)
            dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 240);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 245, 240);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;

            // --- 5. DISABLE SORTING ---
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}

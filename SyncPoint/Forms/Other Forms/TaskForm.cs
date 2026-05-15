using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SyncPoint.Data;

namespace SyncPoint.Forms.Other_Forms
{
    public partial class TasksForm : Form
    {
        public TasksForm()
        {
            InitializeComponent();

            SetupHeaderPanel();
            SetupDataGridViewStyle();
            LoadAvailableTasks();
        }

        private void SetupHeaderPanel()
        {
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(44, 62, 80),
                Name = "pnlHeader"
            };

            Label lblTitle = new Label
            {
                Name = "lblTitle",
                Text = "Available Tasks",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Label lblInfo = new Label
            {
                Name = "lblInfo",
                Text = "Browse the list below. Press 'Accept' to assign a task to yourself.",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(200, 200, 200),
                Location = new Point(23, 55),
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblInfo);
            this.Controls.Add(pnlHeader);
        }

        private void SetupDataGridViewStyle()
        {
            dgvTasks.Columns.Clear();
            dgvTasks.DataSource = null;

            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { Name = "TaskID", Visible = false });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "Title", FillWeight = 25, MinimumWidth = 100 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Description", FillWeight = 40, MinimumWidth = 150 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Deadline", HeaderText = "Deadline", FillWeight = 15, MinimumWidth = 80 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaskWeight",
                HeaderText = "Points",
                FillWeight = 10,
                MinimumWidth = 50,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgvTasks.Columns.Add(new DataGridViewButtonColumn { Name = "Accept", HeaderText = "Action", FillWeight = 10, MinimumWidth = 70 });

            dgvTasks.BackgroundColor = Color.White;
            dgvTasks.GridColor = Color.FromArgb(235, 235, 235);
            dgvTasks.BorderStyle = BorderStyle.None;
            dgvTasks.RowHeadersVisible = false;
            dgvTasks.AllowUserToAddRows = false;
            dgvTasks.AllowUserToResizeRows = false;
            dgvTasks.AllowUserToResizeColumns = false;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTasks.EnableHeadersVisualStyles = false;
            dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTasks.ColumnHeadersHeight = 45;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = Color.FromArgb(18, 35, 70);
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            headerStyle.SelectionBackColor = Color.FromArgb(18, 35, 70);
            dgvTasks.ColumnHeadersDefaultCellStyle = headerStyle;

            dgvTasks.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvTasks.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvTasks.RowTemplate.Height = 50;

            dgvTasks.CellPainting -= dgvTasks_CellPainting;
            dgvTasks.CellPainting += dgvTasks_CellPainting;
            dgvTasks.CellContentClick -= dgvTasks_CellContentClick;
            dgvTasks.CellContentClick += dgvTasks_CellContentClick;
        }

        private void LoadAvailableTasks()
        {
            DataTable myTasks = DatabaseHelper.GetTasksByMember(Session.UserID);
            bool isBusy = false;
            if (myTasks != null)
            {
                foreach (DataRow row in myTasks.Rows)
                {
                    if (row["Status"].ToString() == "In Progress") { isBusy = true; break; }
                }
            }

            if (isBusy)
            {
                dgvTasks.Rows.Clear();
                Control[] found = this.Controls.Find("lblInfo", true);
                if (found.Length > 0) { found[0].Text = "Submit your active task first!"; found[0].ForeColor = Color.Red; }
                return;
            }

            DataTable allTasks = DatabaseHelper.GetTasksByGroup(Session.GroupID);
            dgvTasks.Rows.Clear();

            if (allTasks != null)
            {
                foreach (DataRow row in allTasks.Rows)
                {
                    if (row["Status"].ToString() == "Pending")
                    {
                        dgvTasks.Rows.Add(
                            row["TaskID"],
                            row["Title"],
                            row["Description"],
                            Convert.ToDateTime(row["Deadline"]).ToString("MMM dd, yyyy"),
                            row["TaskWeight"],
                            "Accept"
                        );
                    }
                }
            }

            foreach (DataGridViewColumn col in dgvTasks.Columns) col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTasks.Columns[e.ColumnIndex].Name == "Accept")
            {
                string taskTitle = dgvTasks.Rows[e.RowIndex].Cells["Title"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to accept \"{taskTitle}\"?\n\nOnce accepted, this task will be moved to your workspace and hidden from other members.",
                    "Confirm Task Acceptance",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    int taskID = Convert.ToInt32(dgvTasks.Rows[e.RowIndex].Cells["TaskID"].Value);

                    bool success = DatabaseHelper.AssignAndAcceptTask(taskID, Session.UserID);

                    if (success)
                    {
                        MessageBox.Show("Task accepted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Oops! This task might have been taken by another member just now.", "SyncPoint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadAvailableTasks();
                    }
                }
            }
        }

        private void dgvTasks_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvTasks.Columns[e.ColumnIndex].Name == "Accept")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                int btnW = 80, btnH = 30;
                int x = e.CellBounds.X + (e.CellBounds.Width - btnW) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - btnH) / 2;
                Rectangle btnRect = new Rectangle(x, y, btnW, btnH);

                using (GraphicsPath path = new GraphicsPath())
                using (SolidBrush fill = new SolidBrush(Color.FromArgb(39, 174, 96)))
                {
                    int r = 6;
                    path.AddArc(btnRect.X, btnRect.Y, r * 2, r * 2, 180, 90);
                    path.AddArc(btnRect.Right - r * 2, btnRect.Y, r * 2, r * 2, 270, 90);
                    path.AddArc(btnRect.Right - r * 2, btnRect.Bottom - r * 2, r * 2, r * 2, 0, 90);
                    path.AddArc(btnRect.X, btnRect.Bottom - r * 2, r * 2, r * 2, 90, 90);
                    path.CloseFigure();

                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(fill, path);
                }

                TextRenderer.DrawText(e.Graphics, "Accept",
                    new Font("Segoe UI", 9, FontStyle.Bold),
                    btnRect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }
    }
}
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

            // Apply Design and Logic
            SetupHeaderPanel();
            SetupDataGridViewStyle();
            LoadAvailableTasks();
        }

        private void SetupHeaderPanel()
        {
            // 1. Create the Navy Blue Panel
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(18, 35, 70), // Navy Blue
                Name = "pnlHeader"
            };

            // 2. Add Title Label
            Label lblTitle = new Label
            {
                Name = "lblTitle", // Added Name for internal lookup
                Text = "Available Tasks",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            // 3. Add Info Label
            Label lblInfo = new Label
            {
                Name = "lblInfo", // Added Name so LoadAvailableTasks can find it
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
            dgvTasks.AllowUserToResizeColumns = false;
            dgvTasks.AllowUserToResizeRows = false;
            dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTasks.RowHeadersVisible = false;
            dgvTasks.AllowUserToAddRows = false;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTasks.BackgroundColor = Color.White;
            dgvTasks.BorderStyle = BorderStyle.None;

            dgvTasks.EnableHeadersVisualStyles = false;
            dgvTasks.ColumnHeadersHeight = 45;
            dgvTasks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(18, 35, 70);
            dgvTasks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTasks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTasks.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(18, 35, 70);

            dgvTasks.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvTasks.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvTasks.RowTemplate.Height = 50;

            dgvTasks.CellPainting += dgvTasks_CellPainting;
            dgvTasks.CellContentClick += dgvTasks_CellContentClick;
        }

        private void LoadAvailableTasks()
        {
            // 1. Fetch tasks assigned to the user to check the 1:1 Policy
            DataTable myTasks = DatabaseHelper.GetTasksByMember(Session.UserID);
            bool isBusy = false;

            if (myTasks != null)
            {
                foreach (DataRow row in myTasks.Rows)
                {
                    // If they have any task that is 'In Progress', they are busy
                    if (row["Status"].ToString() == "In Progress")
                    {
                        isBusy = true;
                        break;
                    }
                }
            }

            // 2. Handle the "Busy" state safely
            if (isBusy)
            {
                dgvTasks.DataSource = null;
                dgvTasks.Columns.Clear();

                // Safely find the label to update the text
                Control[] foundControls = this.Controls.Find("lblInfo", true);
                if (foundControls.Length > 0 && foundControls[0] is Label lblInfo)
                {
                    lblInfo.Text = "Policy: You can only have one active task at a time.";
                    lblInfo.ForeColor = Color.FromArgb(231, 76, 60); // Red warning
                }
                return;
            }

            // 3. Requirement: Only show tasks that are NOT In-Progress by anyone else
            DataTable allTasks = DatabaseHelper.GetTasksByGroup(Session.GroupID);

            // We filter the table to only show 'Pending' tasks
            DataTable availableOnly = allTasks.Clone();
            if (allTasks != null)
            {
                foreach (DataRow row in allTasks.Rows)
                {
                    if (row["Status"].ToString() == "Pending")
                    {
                        availableOnly.ImportRow(row);
                    }
                }
            }

            // 4. Bind the filtered data
            dgvTasks.DataSource = null;
            dgvTasks.Columns.Clear();
            dgvTasks.DataSource = availableOnly;

            // Grid Formatting
            if (dgvTasks.Columns.Contains("TaskID")) dgvTasks.Columns["TaskID"].Visible = false;

            if (availableOnly.Rows.Count > 0 && !dgvTasks.Columns.Contains("Accept"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                {
                    Name = "Accept",
                    HeaderText = "Action",
                    Text = "Accept",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat
                };
                dgvTasks.Columns.Add(btn);
            }

            // Remove sorting arrows
            foreach (DataGridViewColumn col in dgvTasks.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTasks.Columns[e.ColumnIndex].Name == "Accept")
            {
                int taskID = Convert.ToInt32(dgvTasks.Rows[e.RowIndex].Cells["TaskID"].Value);

                // Final safety check: Assign the task to the current UserID and set status
                // You should update your DatabaseHelper to include Session.UserID in the assignment
                bool success = DatabaseHelper.AssignAndAcceptTask(taskID, Session.UserID);

                if (success)
                {
                    MessageBox.Show("Task Accepted! It is now assigned to you.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Close form so they go to their workspace
                }
                else
                {
                    MessageBox.Show("This task was just taken by someone else or you already have a task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadAvailableTasks();
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
// ===============================
// TaskProgress.cs
// ===============================

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SyncPoint.Forms
{
    public partial class TaskProgress : Form
    {
        public TaskProgress()
        {
            InitializeComponent();
            LoadMembers();
        }

        private void LoadMembers()
        {
            AddMember("John Carter", 12);
            AddMember("Maria Santos", 14);
            AddMember("Paolo Reyes", 8);
            AddMember("Elena Garcia", 15);
        }

        private void AddMember(string name, int completedTasks)
        {
            // MAIN MEMBER CARD
            Panel memberPanel = new Panel();
            memberPanel.Size = new Size(640, 70);
            memberPanel.BackColor = Color.White;
            memberPanel.Margin = new Padding(0, 0, 0, 12);
            memberPanel.Padding = new Padding(10);

            // BORDER EFFECT
            memberPanel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(
                    e.Graphics,
                    memberPanel.ClientRectangle,
                    Color.FromArgb(225, 225, 225),
                    ButtonBorderStyle.Solid);
            };

            // LEFT BUBBLE ICON
            Panel bubble = new Panel();
            bubble.Size = new Size(42, 42);
            bubble.BackColor = Color.FromArgb(18, 35, 70);
            bubble.Location = new Point(15, 13);

            Label lblInitial = new Label();
            lblInitial.Text = name.Substring(0, 1).ToUpper();
            lblInitial.ForeColor = Color.White;
            lblInitial.Font = new Font("Arial", 14, FontStyle.Bold);
            lblInitial.Dock = DockStyle.Fill;
            lblInitial.TextAlign = ContentAlignment.MiddleCenter;

            bubble.Controls.Add(lblInitial);

            // MEMBER NAME
            Label lblName = new Label();
            lblName.Text = name;
            lblName.Font = new Font("Arial", 11, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(18, 35, 70);
            lblName.Location = new Point(75, 24);
            lblName.AutoSize = true;

            // TASK BOX RIGHT SIDE
            Panel taskBox = new Panel();
            taskBox.Size = new Size(170, 38);
            taskBox.BackColor = Color.FromArgb(248, 246, 242);
            taskBox.Location = new Point(445, 15);

            Label lblTasks = new Label();
            lblTasks.Text = completedTasks + " Tasks Done";
            lblTasks.Font = new Font("Arial", 10, FontStyle.Bold);
            lblTasks.ForeColor = Color.FromArgb(196, 155, 60);
            lblTasks.Dock = DockStyle.Fill;
            lblTasks.TextAlign = ContentAlignment.MiddleCenter;

            taskBox.Controls.Add(lblTasks);

            // ADD CONTROLS
            memberPanel.Controls.Add(bubble);
            memberPanel.Controls.Add(lblName);
            memberPanel.Controls.Add(taskBox);

            flowMembers.Controls.Add(memberPanel);
        }

        private void btnViewProgress_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Team progress updated successfully!",
                "SyncPoint",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
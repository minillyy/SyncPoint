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
    public partial class SubmitTaskForm : Form
    {
        public string SubmissionLink { get; private set; }

        public SubmitTaskForm(string taskTitle)
        {
            InitializeComponent();
            lblPaste.Text = "Submitting: " + taskTitle;
            this.BackColor = Color.White;
            pnlHeader.BackColor = Color.FromArgb(44, 62, 80);
        }

        private void btnFinalSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLink.Text) || txtLink.Text == "https://")
            {
                MessageBox.Show("Please provide a valid link.");
                return;
            }

            SubmissionLink = txtLink.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

namespace SyncPoint.Forms.Other_Forms
{
    partial class SubmitTaskForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPaste = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.btnFinalSubmit = new System.Windows.Forms.Button();
            this.lblText = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblPaste);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(374, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblPaste
            // 
            this.lblPaste.AutoSize = true;
            this.lblPaste.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaste.ForeColor = System.Drawing.Color.White;
            this.lblPaste.Location = new System.Drawing.Point(12, 26);
            this.lblPaste.Name = "lblPaste";
            this.lblPaste.Size = new System.Drawing.Size(263, 23);
            this.lblPaste.TabIndex = 0;
            this.lblPaste.Text = "Paste your submission link below:";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(30, 113);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(305, 22);
            this.txtLink.TabIndex = 1;
            // 
            // btnFinalSubmit
            // 
            this.btnFinalSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnFinalSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalSubmit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalSubmit.ForeColor = System.Drawing.Color.White;
            this.btnFinalSubmit.Location = new System.Drawing.Point(63, 154);
            this.btnFinalSubmit.Name = "btnFinalSubmit";
            this.btnFinalSubmit.Size = new System.Drawing.Size(231, 36);
            this.btnFinalSubmit.TabIndex = 1;
            this.btnFinalSubmit.Text = "Submit";
            this.btnFinalSubmit.UseVisualStyleBackColor = false;
            this.btnFinalSubmit.Click += new System.EventHandler(this.btnFinalSubmit_Click);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblText.Location = new System.Drawing.Point(26, 85);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(228, 20);
            this.lblText.TabIndex = 2;
            this.lblText.Text = "Paste your submission link below:";
            // 
            // SubmitTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 204);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnFinalSubmit);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.pnlHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubmitTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Submit Task";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPaste;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Button btnFinalSubmit;
        private System.Windows.Forms.Label lblText;
    }
}
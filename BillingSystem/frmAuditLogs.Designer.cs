namespace BillingSystem
{
    partial class frmAuditLogs
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
            lblTitle = new Label();
            lblFrom = new Label();
            lblTo = new Label();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            dgvAuditLogs = new DataGridView();
            btnSearch = new Button();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAuditLogs).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(330, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(211, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Audit Log Report";
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(24, 76);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(58, 25);
            lblFrom.TabIndex = 1;
            lblFrom.Text = "From:";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(396, 76);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(34, 25);
            lblTo.TabIndex = 2;
            lblTo.Text = "To:";
            // 
            // dtpFrom
            // 
            dtpFrom.Location = new Point(88, 73);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(293, 31);
            dtpFrom.TabIndex = 3;
            // 
            // dtpTo
            // 
            dtpTo.Location = new Point(436, 73);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(293, 31);
            dtpTo.TabIndex = 4;
            // 
            // dgvAuditLogs
            // 
            dgvAuditLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuditLogs.Location = new Point(24, 120);
            dgvAuditLogs.Name = "dgvAuditLogs";
            dgvAuditLogs.RowHeadersWidth = 51;
            dgvAuditLogs.Size = new Size(831, 324);
            dgvAuditLogs.TabIndex = 5;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(753, 71);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(102, 33);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(635, 456);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(220, 35);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // frmAuditLogs
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 503);
            Controls.Add(btnClose);
            Controls.Add(btnSearch);
            Controls.Add(dgvAuditLogs);
            Controls.Add(dtpTo);
            Controls.Add(dtpFrom);
            Controls.Add(lblTo);
            Controls.Add(lblFrom);
            Controls.Add(lblTitle);
            Name = "frmAuditLogs";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Audit Log Report";
            Load += frmAuditLogs_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAuditLogs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblFrom;
        private Label lblTo;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private DataGridView dgvAuditLogs;
        private Button btnSearch;
        private Button btnClose;
    }
}
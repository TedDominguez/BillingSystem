using BillingSystem.Database;
using BillingSystem.Utils;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; // Meron na pala nito
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace BillingSystem
{
    public partial class CustomerListForm : Form
    {
        // Stores the CustomerID of the currently selected row.
        // 0 means no customer is currently selected.
        private int _selectedCustomerId = 0;
        private bool _canEdit = true;

        public CustomerListForm()
        {
            InitializeComponent();
            ConfigureMainLayout();
            ConfigureDataGridView();
        }


        private void ConfigureMainLayout()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Billing System | Customer Management";
            BackColor = AppTheme.BackgroundColor;
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1100, 700);
            WindowState = FormWindowState.Maximized;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.BackgroundColor,
                Padding = new Padding(22),
                ColumnCount = 1,
                RowCount = 5
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 112));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

            var header = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.PrimaryColor, Padding = new Padding(18, 8, 18, 8) };
            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.Text = "Customer Management";
            header.Controls.Add(lblTitle);
            root.Controls.Add(header, 0, 0);

            var searchPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 10, 0, 8),
                BackColor = AppTheme.BackgroundColor
            };
            var searchLabel = new Label
            {
                Text = "Search customers",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = AppTheme.TextColor,
                Margin = new Padding(0, 9, 12, 0)
            };
            txtSearch.Width = 390;
            txtSearch.Height = 34;
            txtSearch.Margin = new Padding(0, 2, 10, 0);
            btnSearch.Size = new Size(112, 36);
            btnSearch.Margin = new Padding(0, 1, 0, 0);
            searchPanel.Controls.Add(searchLabel);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(btnSearch);
            root.Controls.Add(searchPanel, 0, 1);

            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Margin = new Padding(0, 4, 0, 12);
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            root.Controls.Add(dgvCustomers, 0, 2);

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(0, 6, 0, 4),
                BackColor = AppTheme.BackgroundColor
            };
            Button[] actionButtons =
            {
                btnAdd, btnDelete, btnAnalytics, btnExportExcel, btnExportPdf,
                btnAuditLog, btnManagePermissions, btnChangePassword, btnViewBilling, btnLogout
            };
            foreach (var button in actionButtons)
            {
                button.Size = new Size(178, 38);
                button.Margin = new Padding(0, 0, 10, 8);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                actions.Controls.Add(button);
            }
            root.Controls.Add(actions, 0, 3);

            statusStrip1.Dock = DockStyle.Fill;
            statusStrip1.SizingGrip = false;
            lblStatusSep.Spring = true;
            root.Controls.Add(statusStrip1, 0, 4);

            Controls.Add(root);
            ResumeLayout(true);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            using var changePasswordForm = new frmChangePassword();
            changePasswordForm.ShowDialog(this);
        }

        private void btnViewBilling_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a customer first.",
                    "No Customer Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            object? idValue = dgvCustomers.SelectedRows[0].Cells["CustomerID"].Value;

            if (idValue == null ||
                !int.TryParse(Convert.ToString(idValue), out int customerId) ||
                customerId <= 0)
            {
                MessageBox.Show(
                    "The selected customer ID is invalid.",
                    "Invalid Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            using var historyForm = new frmBillingHistory(customerId);
            historyForm.ShowDialog(this);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (AddCustomerForm addCustomerForm = new AddCustomerForm())
            {
                addCustomerForm.ShowDialog(this);
            }

            // Refresh the list after the add form closes so newly saved records appear.
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // SELECT all customers, most recently added first
                    string sql = @"SELECT CustomerID,
                                  FullName,
                                  Address,
                                  ContactNumber,
                                  Email,
                                  Balance,
                                  Status
                           FROM   Customers
                           ORDER  BY FullName ASC;";

                    using (var adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the DataTable to the grid
                        dgvCustomers.DataSource = dt;

                        // Improve column headers for readability
                        if (dgvCustomers.Columns.Count > 0)
                        {
                            if (dgvCustomers.Columns["CustomerID"] is DataGridViewColumn customerIdColumn) customerIdColumn.HeaderText = "ID";
                            if (dgvCustomers.Columns["FullName"] is DataGridViewColumn customerNameColumn) customerNameColumn.HeaderText = "Full Name";
                            if (dgvCustomers.Columns["ContactNumber"] is DataGridViewColumn customerContactColumn) customerContactColumn.HeaderText = "Contact No.";
                            if (dgvCustomers.Columns["Balance"] is DataGridViewColumn customerBalanceColumn) customerBalanceColumn.HeaderText = "Balance (PHP)";
                            if (dgvCustomers.Columns["Status"] is DataGridViewColumn customerStatusColumn) customerStatusColumn.HeaderText = "Account Status";
                        }

                        lblTitle.Text = $"Customer List  ({dt.Rows.Count} record(s))";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CustomerListForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadCustomers();
            ApplyPermissions();
            InitStatusStrip();
        }

        private void SearchCustomers(string keyword)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Parameterized SELECT with WHERE ... LIKE
                    string sql = @"SELECT CustomerID,
                                  FullName,
                                  Address,
                                  ContactNumber,
                                  Email,
                                  Balance,
                                  Status
                           FROM   Customers
                           WHERE  FullName      LIKE @keyword
                              OR  Address       LIKE @keyword
                              OR  ContactNumber LIKE @keyword
                           ORDER  BY FullName ASC;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        // %keyword% matches the search text anywhere in the column
                        cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dgvCustomers.DataSource = dt;
                            lblTitle.Text = $"Customer List  ({dt.Rows.Count} result(s))";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching customers:\n{ex.Message}",
                    "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                // Empty search box → show all customers again
                LoadCustomers();
            }
            else
            {
                SearchCustomers(keyword);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void ConfigureDataGridView()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.ColumnHeadersHeight = 42;
            dgvCustomers.RowTemplate.Height = 34;
            if (dgvCustomers.Columns["Balance"] is DataGridViewColumn balanceFormatColumn)
                balanceFormatColumn.DefaultCellStyle.Format = "\"PHP \"#,##0.00";
            if (dgvCustomers.Columns["CustomerID"] is DataGridViewColumn customerIdBinding) customerIdBinding.DataPropertyName = "CustomerID";
            if (dgvCustomers.Columns["FullName"] is DataGridViewColumn customerNameBinding) customerNameBinding.DataPropertyName = "FullName";
            if (dgvCustomers.Columns["Address"] is DataGridViewColumn customerAddressBinding) customerAddressBinding.DataPropertyName = "Address";
            if (dgvCustomers.Columns["ContactNumber"] is DataGridViewColumn customerContactBinding) customerContactBinding.DataPropertyName = "ContactNumber";
            if (dgvCustomers.Columns["Email"] is DataGridViewColumn customerEmailBinding) customerEmailBinding.DataPropertyName = "Email";
            if (dgvCustomers.Columns["Balance"] is DataGridViewColumn customerBalanceBinding) customerBalanceBinding.DataPropertyName = "Balance";
            if (dgvCustomers.Columns["Status"] is DataGridViewColumn customerStatusBinding) customerStatusBinding.DataPropertyName = "Status";
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            _selectedCustomerId = 0;

            if (dgvCustomers.SelectedRows.Count == 0)
                return;

            object? idCell = dgvCustomers.SelectedRows[0].Cells["CustomerID"].Value;

            if (idCell != null &&
                int.TryParse(Convert.ToString(idCell), out int id))
            {
                _selectedCustomerId = id;
            }
        }
        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_canEdit) return;
            // e.RowIndex is -1 when the header row is double-clicked — ignore it
            if (e.RowIndex < 0) return;

            OpenEditForm();
        }

        private void OpenEditForm()
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to edit.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open AddCustomerForm in EDIT mode, passing the selected CustomerID
            AddCustomerForm editForm = new AddCustomerForm(_selectedCustomerId);

            // Refresh the grid automatically once the edit form closes
            editForm.FormClosed += (s, args) => LoadCustomers();

            editForm.ShowDialog(this);
        }

        private void DeleteCustomer(int customerId)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Parameterized DELETE — removes exactly one row
                    string sql = "DELETE FROM Customers WHERE CustomerID = @CustomerID;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            AuditLogger.Log("DELETE_CUSTOMER", $"Customer ID {customerId} deleted by {AppSession.CurrentUsername}.");

                            MessageBox.Show("Customer deleted successfully.",
                                "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadCustomers();   // Refresh the grid
                            _selectedCustomerId = 0;   // Clear selection tracker
                        }
                        else
                        {
                            MessageBox.Show("Customer could not be deleted. It may no longer exist.",
                                "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Step 1: Make sure a customer is selected
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer to delete.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Step 2: Confirm before deleting — this cannot be undone
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this customer?\n" +
                "All billing records for this customer will also be deleted.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            // Step 3: Only delete if the user clicked Yes
            if (confirm == DialogResult.Yes)
            {
                DeleteCustomer(_selectedCustomerId);
            }
            // If the user clicked No, do nothing — the record is preserved
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            AuditLogger.Log("VIEW_ANALYTICS", $"{AppSession.CurrentUsername} opened the Analytics Dashboard.");


            // Open the Analytics Dashboard as a dialog so the
            // Customer List stays open in the background
            frmAnalytics analyticsForm = new frmAnalytics();
            analyticsForm.ShowDialog(this);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Make sure there is something to export
            if (dgvCustomers.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.",
                    "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Let the user choose where to save the file
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveDialog.FileName = "CustomerList.xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Customers");

                        // Write column headers in row 1
                        for (int col = 0; col < dgvCustomers.Columns.Count; col++)
                        {
                            worksheet.Cell(1, col + 1).Value = dgvCustomers.Columns[col].HeaderText;
                            worksheet.Cell(1, col + 1).Style.Font.Bold = true;
                        }

                        // Write each data row starting from row 2
                        for (int row = 0; row < dgvCustomers.Rows.Count; row++)
                        {
                            for (int col = 0; col < dgvCustomers.Columns.Count; col++)
                            {
                                var cellValue = dgvCustomers.Rows[row].Cells[col].Value;
                                worksheet.Cell(row + 2, col + 1).Value = cellValue?.ToString() ?? "";
                            }
                        }

                        // Auto-adjust column widths to fit the content
                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(saveDialog.FileName);
                    }

                    AuditLogger.Log("EXPORT_EXCEL", $"{AppSession.CurrentUsername} exported customer list to Excel.");

                    MessageBox.Show("Customer list exported successfully to Excel.",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting to Excel:\n{ex.Message}",
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            // Make sure there is something to export
            if (dgvCustomers.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.",
                    "Export to PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF Document (*.pdf)|*.pdf";
                saveDialog.FileName = "CustomerList.pdf";

                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (PdfDocument document = new PdfDocument())
                    {
                        // Create a new page set to Landscape orientation
                        PdfPage page = document.AddPage();
                        page.Orientation = PdfSharp.PageOrientation.Landscape;

                        XGraphics gfx = XGraphics.FromPdfPage(page);
                        {
                            XFont titleFont = new XFont("Arial", 16, XFontStyleEx.Bold);
                            XFont headerFont = new XFont("Arial", 10, XFontStyleEx.Bold);
                            XFont cellFont = new XFont("Arial", 9, XFontStyleEx.Regular);

                            // Title
                            gfx.DrawString("Customer List Report", titleFont, XBrushes.Black,
                                new XRect(0, 20, page.Width.Point, 30), XStringFormats.TopCenter);

                            int columnCount = dgvCustomers.Columns.Count;
                            double margin = 30;
                            double tableWidth = page.Width.Point - (margin * 2);
                            double colWidth = tableWidth / columnCount;
                            double rowHeight = 22;
                            double y = 60;

                            // Draw column headers
                            double x = margin;
                            for (int col = 0; col < columnCount; col++)
                            {
                                gfx.DrawString(dgvCustomers.Columns[col].HeaderText, headerFont,
                                    XBrushes.Black, new XRect(x, y, colWidth, rowHeight),
                                    XStringFormats.CenterLeft);
                                x += colWidth;
                            }

                            y += rowHeight;
                            gfx.DrawLine(XPens.Black, margin, y, page.Width.Point - margin, y);

                            // Draw each data row
                            foreach (DataGridViewRow row in dgvCustomers.Rows)
                            {
                                x = margin;
                                y += rowHeight;

                                // Start a new page if we run out of vertical space
                                if (y > page.Height.Point - margin)
                                {
                                    page = document.AddPage();
                                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                                    gfx.Dispose();
                                    gfx = XGraphics.FromPdfPage(page);
                                    y = 40;
                                }

                                for (int col = 0; col < columnCount; col++)
                                {
                                    string text = row.Cells[col].Value?.ToString() ?? "";
                                    gfx.DrawString(text, cellFont, XBrushes.Black,
                                        new XRect(x, y, colWidth, rowHeight),
                                        XStringFormats.CenterLeft);
                                    x += colWidth;
                                }
                            }
                        }

                        gfx.Dispose();   // finish drawing before saving
                        document.Save(saveDialog.FileName);
                    }

                    AuditLogger.Log("EXPORT_PDF", $"{AppSession.CurrentUsername} exported customer list to PDF.");

                    MessageBox.Show("Customer list exported successfully to PDF.",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting to PDF:\n{ex.Message}",
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AuditLogger.Log("LOGOUT", $"{AppSession.CurrentFullName} ({AppSession.CurrentRole}) logged out.");
            AppSession.Clear();

            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void btnAuditLog_Click(object sender, EventArgs e)
        {
            frmAuditLogs auditForm = new frmAuditLogs();
            auditForm.ShowDialog(this);
        }

        private void btnManagePermissions_Click(object sender, EventArgs e)
        {
            frmManagePermissions permForm = new frmManagePermissions();
            permForm.ShowDialog(this);
        }

        private void ApplyPermissions()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT PermissionName, IsAllowed
                           FROM   UserPermissions
                           WHERE  Role = @Role;";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Role", AppSession.CurrentRole);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string permName = reader.GetString("PermissionName");
                                bool isAllowed = reader.GetBoolean("IsAllowed");

                                switch (permName)
                                {
                                    case "AddCustomer":
                                        btnAdd.Enabled = isAllowed; break;
                                    case "EditCustomer":
                                        // Edit is triggered by double-click — remember the
                                        // permission so CellDoubleClick can check it
                                        _canEdit = isAllowed;
                                        break;
                                    case "DeleteCustomer":
                                        btnDelete.Enabled = isAllowed; break;
                                    case "Analytics":
                                        btnAnalytics.Enabled = isAllowed; break;
                                    case "ExportExcel":
                                        btnExportExcel.Enabled = isAllowed; break;
                                    case "ExportPdf":
                                        btnExportPdf.Enabled = isAllowed; break;
                                    case "AuditLogs":
                                        btnAuditLog.Enabled = isAllowed; break;
                                    case "ManagePermissions":
                                        btnManagePermissions.Enabled = isAllowed; break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading permissions:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Call this inside CustomerListForm_Load after ApplyPermissions()
        private void InitStatusStrip()
        {
            lblStatusUser.Text =
                $"User: {AppSession.CurrentFullName}  |  Role: {AppSession.CurrentRole}";
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblStatusTime.Text = DateTime.Now.ToString("dddd, MMMM dd yyyy   hh:mm:ss tt");
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void ApplyTheme()
        {
            // Form background
            this.BackColor = AppTheme.BackgroundColor;
            this.Font = new Font("Segoe UI", 9F);
            lblTitle.ForeColor = AppTheme.PrimaryColor;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            dgvCustomers.BackgroundColor = AppTheme.SurfaceColor;
            dgvCustomers.BorderStyle = BorderStyle.FixedSingle;
            dgvCustomers.GridColor = AppTheme.BorderColor;
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.ColumnHeadersHeight = 42;
            dgvCustomers.RowTemplate.Height = 34;
            dgvCustomers.ColumnHeadersDefaultCellStyle.SelectionBackColor = AppTheme.PrimaryColor;
            dgvCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(246, 232, 205);
            dgvCustomers.DefaultCellStyle.SelectionForeColor = AppTheme.TextColor;
            dgvCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            // Consistent action-button treatment
            foreach (var button in new[] { btnAdd, btnDelete, btnLogout, btnSearch,
                         btnChangePassword, btnViewBilling, btnAnalytics, btnExportExcel,
                         btnExportPdf, btnAuditLog, btnManagePermissions })
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.ForeColor = Color.White;
                button.BackColor = AppTheme.PrimaryColor;
            }
            btnAdd.BackColor = AppTheme.SuccessColor;
            btnDelete.BackColor = AppTheme.DangerColor;
            btnManagePermissions.BackColor = AppTheme.SecondaryColor;
            btnLogout.BackColor = AppTheme.PrimaryDark;
            btnSearch.BackColor = AppTheme.AccentColor;
            btnSearch.ForeColor = AppTheme.PrimaryDark;

            // DataGridView header colors
            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryColor;
            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);

            // Alternating row colors
            dgvCustomers.AlternatingRowsDefaultCellStyle.BackColor = AppTheme.GridRowAlt;
        }

        private void dgvCustomers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Skip the header row
            if (e.RowIndex < 0) return;


            // Read Status from the data behind this row (the DataTable from LoadCustomers)
            var rowData = dgvCustomers.Rows[e.RowIndex].DataBoundItem as DataRowView;
            string status = rowData?["Status"]?.ToString() ?? "";


            if (status == "Inactive")
            {
                // The whole row turns light red
                e.CellStyle.BackColor = AppTheme.InactiveRowColor;
            }
            else if (status == "Active" &&
                     dgvCustomers.Columns[e.ColumnIndex].DataPropertyName == "Status")
            {
                // Only the Status cell turns light green, so the
                // alternating row colors stay visible
                e.CellStyle.BackColor = AppTheme.ActiveRowColor;
            }
        }


    }
}






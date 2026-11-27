using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BillingSystem.Database;
using BillingSystem.Utils;

namespace BillingSystem
{
    public class frmBillingHistory : Form
    {
        private readonly int _customerId;
        private readonly Label lblCustomerName = new();
        private readonly DataGridView dgvBilling = new();
        private readonly Label lblEmptyState = new();

        public frmBillingHistory(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(customerId));

            _customerId = customerId;

            Text = "Billing History";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1100, 590);
            MinimumSize = new Size(900, 480);
            Font = new Font("Segoe UI", 9F);

            BuildLayout();
            AppTheme.Apply(this);
            LoadBillingHistory();
        }

        private void BuildLayout()
        {
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCustomerName.Location = new Point(22, 18);
            lblCustomerName.Text = "Billing History";
            Controls.Add(lblCustomerName);

            dgvBilling.Location = new Point(22, 65);
            dgvBilling.Size = new Size(1040, 420);
            dgvBilling.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                               AnchorStyles.Left | AnchorStyles.Right;
            dgvBilling.ReadOnly = true;
            dgvBilling.AllowUserToAddRows = false;
            dgvBilling.AllowUserToDeleteRows = false;
            dgvBilling.MultiSelect = false;
            dgvBilling.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBilling.AutoGenerateColumns = true;
            dgvBilling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBilling.RowHeadersVisible = false;
            Controls.Add(dgvBilling);

            lblEmptyState.AutoSize = true;
            lblEmptyState.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblEmptyState.ForeColor = Color.DimGray;
            lblEmptyState.Location = new Point(25, 500);
            lblEmptyState.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblEmptyState.Visible = false;
            Controls.Add(lblEmptyState);

            var btnClose = new Button
            {
                Text = "Close",
                Size = new Size(110, 34),
                Location = new Point(952, 500),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnClose.Click += (_, _) => Close();
            Controls.Add(btnClose);
        }

        private void LoadBillingHistory()
        {
            try
            {
                using var connection = DatabaseConnection.GetConnection();
                connection.Open();

                const string customerSql =
                    "SELECT FullName FROM Customers WHERE CustomerID = @CustomerID;";

                string customerName;
                using (var customerCommand = new MySqlCommand(customerSql, connection))
                {
                    customerCommand.Parameters.AddWithValue("@CustomerID", _customerId);
                    object? result = customerCommand.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show(
                            "The selected customer could not be found.",
                            "Customer Not Found",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        Close();
                        return;
                    }

                    customerName = Convert.ToString(result) ?? "Customer";
                }

                lblCustomerName.Text = "Billing History — " + customerName;

                const string billingSql = @"
                    SELECT
                        BillingMonth,
                        PreviousReading,
                        PresentReading,
                        Consumption,
                        RatePerCubic,
                        TotalAmount,
                        Status
                    FROM Billing
                    WHERE CustomerID = @CustomerID
                    ORDER BY BillingDate DESC, BillingID DESC;";

                using var billingCommand = new MySqlCommand(billingSql, connection);
                billingCommand.Parameters.AddWithValue("@CustomerID", _customerId);

                using var adapter = new MySqlDataAdapter(billingCommand);
                var table = new DataTable();
                adapter.Fill(table);

                dgvBilling.DataSource = table;

                SetHeader("BillingMonth", "Billing Month");
                SetHeader("PreviousReading", "Previous Reading");
                SetHeader("PresentReading", "Present Reading");
                SetHeader("Consumption", "Consumption");
                SetHeader("RatePerCubic", "Rate Per Cubic");
                SetHeader("TotalAmount", "Total Amount");

                if (table.Rows.Count == 0)
                {
                    lblEmptyState.Text = "No billing records are available for this customer.";
                    lblEmptyState.Visible = true;
                }
                else
                {
                    AuditLogger.Log(
                        "VIEW_BILLING_HISTORY",
                        $"{AppSession.CurrentUsername} viewed billing history for customer ID {_customerId}.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load billing history:\n" + ex.Message,
                    "Billing History Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetHeader(string columnName, string headerText)
        {
            if (dgvBilling.Columns[columnName] is DataGridViewColumn column)
                column.HeaderText = headerText;
        }
    }
}

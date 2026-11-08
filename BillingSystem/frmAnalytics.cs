using Color = System.Drawing.Color;
using FontStyle = System.Drawing.FontStyle;
using Label = System.Windows.Forms.Label;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using MySql.Data.MySqlClient;
using BillingSystem.Database;
using BillingSystem.Utils;
using ScottPlot;



namespace BillingSystem
{
    public partial class frmAnalytics : Form
    {
        public frmAnalytics()
        {
            InitializeComponent();
            ConfigureDashboardLayout();
            dgvTop5.AutoGenerateColumns = false;
            if (dgvTop5.Columns["FullName"] is DataGridViewColumn analyticsNameColumn) analyticsNameColumn.DataPropertyName = "FullName";
            if (dgvTop5.Columns["TotalConsumption"] is DataGridViewColumn analyticsConsumptionColumn) analyticsConsumptionColumn.DataPropertyName = "TotalConsumption";
            if (dgvTop5.Columns["TotalBilled"] is DataGridViewColumn analyticsBilledColumn) analyticsBilledColumn.DataPropertyName = "TotalBilled";
        }


        private void ConfigureDashboardLayout()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Billing System | Analytics";
            BackColor = AppTheme.BackgroundColor;
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1050, 720);
            WindowState = FormWindowState.Maximized;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.BackgroundColor,
                Padding = new Padding(24),
                ColumnCount = 1,
                RowCount = 6
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 62));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 112));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 43));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 57));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

            var header = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.PrimaryColor, Padding = new Padding(18, 8, 18, 8) };
            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.Text = "Analytics Overview";
            header.Controls.Add(lblTitle);
            root.Controls.Add(header, 0, 0);

            var kpis = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, Padding = new Padding(0, 12, 0, 12), BackColor = AppTheme.BackgroundColor };
            for (int i = 0; i < 3; i++) kpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            kpis.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            AddKpiCard(kpis, lblTotalCustomers, "CUSTOMERS", 0);
            AddKpiCard(kpis, lblTotalRevenue, "PAID REVENUE", 1);
            AddKpiCard(kpis, lblTotalUnpaid, "UNPAID BALANCE", 2);
            root.Controls.Add(kpis, 0, 1);

            var charts = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, BackColor = AppTheme.BackgroundColor, Padding = new Padding(0, 8, 0, 12) };
            charts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
            charts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
            charts.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            StylePlot(plotMonthlyRevenue);
            StylePlot(plotPaidUnpaid);
            charts.Controls.Add(plotMonthlyRevenue, 0, 0);
            charts.Controls.Add(plotPaidUnpaid, 1, 0);
            root.Controls.Add(charts, 0, 2);

            lblTop5Title.AutoSize = false;
            lblTop5Title.Dock = DockStyle.Fill;
            lblTop5Title.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTop5Title.ForeColor = AppTheme.TextColor;
            lblTop5Title.Text = "Top 5 Customers by Consumption";
            lblTop5Title.TextAlign = ContentAlignment.MiddleLeft;
            root.Controls.Add(lblTop5Title, 0, 3);

            dgvTop5.Dock = DockStyle.Fill;
            dgvTop5.BackgroundColor = AppTheme.SurfaceColor;
            dgvTop5.BorderStyle = BorderStyle.FixedSingle;
            dgvTop5.GridColor = AppTheme.BorderColor;
            dgvTop5.RowHeadersVisible = false;
            dgvTop5.AllowUserToAddRows = false;
            dgvTop5.AllowUserToDeleteRows = false;
            dgvTop5.ReadOnly = true;
            dgvTop5.MultiSelect = false;
            dgvTop5.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTop5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTop5.ColumnHeadersHeight = 42;
            dgvTop5.RowTemplate.Height = 34;
            dgvTop5.EnableHeadersVisualStyles = false;
            dgvTop5.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.PrimaryColor;
            dgvTop5.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTop5.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvTop5.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvTop5.DefaultCellStyle.SelectionBackColor = Color.FromArgb(246, 232, 205);
            dgvTop5.DefaultCellStyle.SelectionForeColor = AppTheme.TextColor;
            dgvTop5.AlternatingRowsDefaultCellStyle.BackColor = AppTheme.GridRowAlt;
            root.Controls.Add(dgvTop5, 0, 4);

            var footer = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.BackgroundColor };
            btnCloseAnalytics.Text = "Close Dashboard";
            btnCloseAnalytics.Size = new Size(142, 36);
            btnCloseAnalytics.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnCloseAnalytics.Location = new Point(0, 4);
            footer.Resize += (_, _) => btnCloseAnalytics.Left = footer.ClientSize.Width - btnCloseAnalytics.Width;
            footer.Controls.Add(btnCloseAnalytics);
            root.Controls.Add(footer, 0, 5);

            Controls.Add(root);
            ResumeLayout(true);
        }

        private static void AddKpiCard(TableLayoutPanel host, System.Windows.Forms.Label valueLabel, string caption, int column)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.SurfaceColor,
                Margin = new Padding(0, 0, 14, 0),
                Padding = new Padding(14),
                BorderStyle = BorderStyle.FixedSingle
            };
            var stack = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1, BackColor = Color.Transparent };
            stack.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
            stack.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            var heading = new Label
            {
                Text = caption,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = AppTheme.MutedTextColor,
                TextAlign = ContentAlignment.MiddleLeft
            };
            valueLabel.AutoSize = false;
            valueLabel.Dock = DockStyle.Fill;
            valueLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            valueLabel.ForeColor = AppTheme.PrimaryColor;
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            stack.Controls.Add(heading, 0, 0);
            stack.Controls.Add(valueLabel, 0, 1);
            card.Controls.Add(stack);
            host.Controls.Add(card, column, 0);
        }

        private static void StylePlot(ScottPlot.WinForms.FormsPlot plot)
        {
            plot.Dock = DockStyle.Fill;
            plot.Margin = new Padding(0, 0, 14, 0);
            plot.BackColor = Color.White;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void plotPaidUnpaid_Load(object sender, EventArgs e)
        {

        }

        private void dgvTop5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmAnalytics_Load(object sender, EventArgs e)
        {
            LoadKpiSummary();
            LoadMonthlyRevenueChart();
            LoadPaidUnpaidChart();
            LoadTop5Customers();
        }

        private void LoadKpiSummary()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // 1) Total number of customers
                    string sqlCustomers = "SELECT COUNT(*) FROM Customers;";
                    using (var cmd = new MySqlCommand(sqlCustomers, conn))
                    {
                        int totalCustomers = Convert.ToInt32(cmd.ExecuteScalar());
                        lblTotalCustomers.Text = $"Total Customers: {totalCustomers}";
                    }

                    // 2) Total revenue â€” sum of all PAID bills
                    string sqlRevenue = @"SELECT IFNULL(SUM(TotalAmount), 0)
                                  FROM   Billing
                                  WHERE  Status = 'Paid';";
                    using (var cmd = new MySqlCommand(sqlRevenue, conn))
                    {
                        decimal totalRevenue = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblTotalRevenue.Text = $"Total Revenue: PHP {totalRevenue:N2}";
                    }

                    // 3) Total unpaid â€” sum of all UNPAID bills
                    string sqlUnpaid = @"SELECT IFNULL(SUM(TotalAmount), 0)
                                 FROM   Billing
                                 WHERE  Status = 'Unpaid';";
                    using (var cmd = new MySqlCommand(sqlUnpaid, conn))
                    {
                        decimal totalUnpaid = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblTotalUnpaid.Text = $"Total Unpaid: PHP {totalUnpaid:N2}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading KPI summary:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMonthlyRevenueChart()
        {
            try
            {
                var months = new List<string>();
                var amounts = new List<double>();

                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    const string sql = @"SELECT BillingMonth, SUM(TotalAmount) AS MonthlyTotal
                                         FROM Billing
                                         WHERE Status = 'Paid'
                                         GROUP BY BillingMonth
                                         ORDER BY MIN(BillingDate);";

                    using var cmd = new MySqlCommand(sql, conn);
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string rawMonth = Convert.ToString(reader["BillingMonth"]) ?? "";
                        if (DateTime.TryParse(rawMonth, out DateTime parsedMonth))
                            months.Add(parsedMonth.ToString("MMM yyyy"));
                        else
                            months.Add(rawMonth);
                        amounts.Add(Convert.ToDouble(reader["MonthlyTotal"]));
                    }
                }

                var plot = plotMonthlyRevenue.Plot;
                plot.Clear();
                plot.Title("Monthly Paid Revenue");
                plot.YLabel("Revenue (PHP)");
                plot.XLabel("Billing Month");

                if (months.Count == 0)
                {
                    plot.Add.Text("No paid billing records available", 0, 0);
                }
                else
                {
                    plot.Add.Bars(amounts.ToArray());
                    double[] positions = Enumerable.Range(0, months.Count).Select(i => (double)i).ToArray();
                    plot.Axes.Bottom.SetTicks(positions, months.ToArray());
                }

                plot.Axes.AutoScale();
                plotMonthlyRevenue.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading monthly revenue chart:\n{ex.Message}",
                    "Analytics Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPaidUnpaidChart()
        {
            try
            {
                int paidCount = 0;
                int unpaidCount = 0;

                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    const string sql = @"SELECT Status, COUNT(*) AS StatusCount
                                         FROM Billing
                                         GROUP BY Status;";

                    using var cmd = new MySqlCommand(sql, conn);
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string status = Convert.ToString(reader["Status"]) ?? "";
                        int count = Convert.ToInt32(reader["StatusCount"]);
                        if (string.Equals(status, "Paid", StringComparison.OrdinalIgnoreCase)) paidCount = count;
                        if (string.Equals(status, "Unpaid", StringComparison.OrdinalIgnoreCase)) unpaidCount = count;
                    }
                }

                var plot = plotPaidUnpaid.Plot;
                plot.Clear();
                plot.Title("Paid vs Unpaid Bills");

                double total = paidCount + unpaidCount;
                if (total <= 0)
                {
                    plot.Add.Text("No billing records available", 0, 0);
                }
                else
                {
                    // Activity 5 specifically requires a paid-vs-unpaid pie chart.
                    // The larger responsive dashboard area gives its percentage labels room.
                    var pie = plot.Add.Pie(new double[] { paidCount, unpaidCount });
                    pie.Slices[0].Label = $"Paid ({paidCount / total:P0})";
                    pie.Slices[1].Label = $"Unpaid ({unpaidCount / total:P0})";
                    pie.SliceLabelDistance = 0.65;
                }

                plot.Axes.AutoScale();
                plotPaidUnpaid.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Paid/Unpaid chart:\\n{ex.Message}",
                    "Analytics Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTop5Customers()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"SELECT c.FullName,
                                  COALESCE(SUM(b.Consumption), 0) AS TotalConsumption,
                                  COALESCE(SUM(b.TotalAmount), 0) AS TotalBilled
                           FROM   Customers c
                           LEFT JOIN Billing b ON c.CustomerID = b.CustomerID
                           GROUP  BY c.CustomerID, c.FullName
                           ORDER  BY TotalConsumption DESC, c.FullName ASC
                           LIMIT  5;";

                    using (var adapter = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvTop5.DataSource = dt;

                        if (dgvTop5.Columns["TotalBilled"] is DataGridViewColumn billedColumn)
                            billedColumn.DefaultCellStyle.Format = "\"PHP \"#,##0.00";
                        if (dgvTop5.Columns["TotalConsumption"] is DataGridViewColumn consumptionColumn)
                            consumptionColumn.DefaultCellStyle.Format = "N2";

                        if (dgvTop5.Columns.Count > 0)
                        {
                            if (dgvTop5.Columns["FullName"] is DataGridViewColumn analyticsNameHeader) analyticsNameHeader.HeaderText = "Customer Name";
                            if (dgvTop5.Columns["TotalConsumption"] is DataGridViewColumn analyticsConsumptionHeader) analyticsConsumptionHeader.HeaderText = "Total Consumption (m³)";
                            if (dgvTop5.Columns["TotalBilled"] is DataGridViewColumn analyticsBilledHeader) analyticsBilledHeader.HeaderText = "Total Billed (PHP)";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Top 5 customers:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloseAnalytics_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void plotMonthlyRevenue_Load(object sender, EventArgs e)
        {

        }
    }
}




using System.Drawing;
using System.Windows.Forms;

namespace BillingSystem.Utils
{
    /// <summary>
    /// Shared visual identity for the Billing System.
    /// A university-inspired maroon, gold, charcoal and neutral palette.
    /// </summary>
    public static class AppTheme
    {
        public static readonly Color PrimaryColor = Color.FromArgb(112, 28, 45);
        public static readonly Color PrimaryDark = Color.FromArgb(70, 20, 32);
        public static readonly Color SecondaryColor = Color.FromArgb(145, 48, 61);
        public static readonly Color AccentColor = Color.FromArgb(211, 169, 78);
        public static readonly Color DangerColor = Color.FromArgb(180, 48, 55);
        public static readonly Color SuccessColor = Color.FromArgb(39, 112, 82);
        public static readonly Color BackgroundColor = Color.FromArgb(244, 245, 247);
        public static readonly Color SurfaceColor = Color.White;
        public static readonly Color TextColor = Color.FromArgb(38, 41, 46);
        public static readonly Color MutedTextColor = Color.FromArgb(105, 111, 120);
        public static readonly Color GridRowAlt = Color.FromArgb(249, 247, 247);
        public static readonly Color ActiveRowColor = Color.FromArgb(226, 242, 234);
        public static readonly Color InactiveRowColor = Color.FromArgb(250, 230, 231);
        public static readonly Color BorderColor = Color.FromArgb(224, 226, 230);

        public static void Apply(Form form)
        {
            form.BackColor = BackgroundColor;
            form.ForeColor = TextColor;
            form.Font = new Font("Segoe UI", 9F);

            ApplyToControls(form.Controls);
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is DataGridView grid)
                {
                    grid.BackgroundColor = SurfaceColor;
                    grid.BorderStyle = BorderStyle.FixedSingle;
                    grid.GridColor = BorderColor;
                    grid.EnableHeadersVisualStyles = false;
                    grid.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
                    grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(246, 232, 205);
                    grid.DefaultCellStyle.SelectionForeColor = TextColor;
                    grid.AlternatingRowsDefaultCellStyle.BackColor = GridRowAlt;
                }
                else if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.BackColor = PrimaryColor;
                    button.ForeColor = Color.White;
                    if (button.Text.Contains("Delete", StringComparison.OrdinalIgnoreCase))
                        button.BackColor = DangerColor;
                    else if (button.Text.Contains("Save", StringComparison.OrdinalIgnoreCase) ||
                             button.Text.Contains("Add", StringComparison.OrdinalIgnoreCase))
                        button.BackColor = SuccessColor;
                }
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = SurfaceColor;
                    textBox.ForeColor = TextColor;
                }
                else if (control is Label label)
                {
                    label.ForeColor = TextColor;
                }

                if (control.HasChildren)
                    ApplyToControls(control.Controls);
            }
        }

    }
}

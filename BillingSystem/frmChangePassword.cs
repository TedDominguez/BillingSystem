using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BillingSystem.Database;
using BillingSystem.Utils;

namespace BillingSystem
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();

            txtCurrentPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = true;
            txtRetypePassword.UseSystemPasswordChar = true;

            btnSave.Click += btnSave_Click;
            btnCancel.Click += (_, _) => Close();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            if (AppSession.CurrentUserID <= 0)
            {
                MessageBox.Show(
                    "Your login session is unavailable. Please sign in again.",
                    "Change Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string retypePassword = txtRetypePassword.Text;

            if (string.IsNullOrWhiteSpace(currentPassword) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(retypePassword))
            {
                MessageBox.Show(
                    "Complete all password fields.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(newPassword, retypePassword, StringComparison.Ordinal))
            {
                MessageBox.Show(
                    "The new password and retyped password do not match.",
                    "Password Mismatch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtRetypePassword.Clear();
                txtRetypePassword.Focus();
                return;
            }

            var unmet = new List<string>();
            if (newPassword.Length < 8) unmet.Add("At least 8 characters");
            if (!HasUppercase(newPassword)) unmet.Add("At least one uppercase letter (A-Z)");
            if (!HasLowercase(newPassword)) unmet.Add("At least one lowercase letter (a-z)");
            if (!HasDigit(newPassword)) unmet.Add("At least one number (0-9)");
            if (!HasSpecialCharacter(newPassword)) unmet.Add("At least one special character");

            if (unmet.Count > 0)
            {
                MessageBox.Show(
                    "The new password must meet these requirements:\n\n• " +
                    string.Join("\n• ", unmet),
                    "Password Requirements",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            try
            {
                using var connection = DatabaseConnection.GetConnection();
                connection.Open();

                const string sql = @"
                    UPDATE Users
                    SET Password = @NewPassword
                    WHERE UserID = @UserID
                      AND Password = @CurrentPassword;";

                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@NewPassword", newPassword);
                command.Parameters.AddWithValue("@UserID", AppSession.CurrentUserID);
                command.Parameters.AddWithValue("@CurrentPassword", currentPassword);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected != 1)
                {
                    MessageBox.Show(
                        "The current password is incorrect, or the account could not be updated.",
                        "Password Not Changed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtCurrentPassword.Clear();
                    txtCurrentPassword.Focus();
                    return;
                }

                AuditLogger.Log(
                    "CHANGE_PASSWORD",
                    $"{AppSession.CurrentUsername} changed their password.");

                MessageBox.Show(
                    "Your password has been changed successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to change the password:\n" + ex.Message,
                    "Change Password Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static bool HasUppercase(string value)
        {
            foreach (char c in value)
                if (c >= 'A' && c <= 'Z') return true;
            return false;
        }

        private static bool HasLowercase(string value)
        {
            foreach (char c in value)
                if (c >= 'a' && c <= 'z') return true;
            return false;
        }

        private static bool HasDigit(string value)
        {
            foreach (char c in value)
                if (c >= '0' && c <= '9') return true;
            return false;
        }

        private static bool HasSpecialCharacter(string value)
        {
            foreach (char c in value)
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)) return true;
            return false;
        }
    }
}

using BillingSystem.Database;
using BillingSystem.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (AppSession.CurrentUserID <= 0)
            {
                MessageBox.Show(
                    "Your user session is not available. Please sign in again.",
                    "Change Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Check the current password first, as required.
                object storedPasswordValue;

                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    const string selectSql =
                        "SELECT Password FROM Users WHERE UserID = @UserID;";

                    using (var cmd = new MySqlCommand(selectSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", AppSession.CurrentUserID);
                        storedPasswordValue = cmd.ExecuteScalar();
                    }
                }

                if (storedPasswordValue == null || storedPasswordValue == DBNull.Value)
                {
                    MessageBox.Show(
                        "Could not verify the current password for this account.",
                        "Change Password",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                string storedPassword =
                    Convert.ToString(storedPasswordValue) ?? string.Empty;

                if (!string.Equals(
                        txtCurrentPassword.Text,
                        storedPassword,
                        StringComparison.Ordinal))
                {
                    MessageBox.Show(
                        "The current password is incorrect.",
                        "Change Password",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtCurrentPassword.Clear();
                    txtCurrentPassword.Focus();
                    return;
                }

                if (!string.Equals(
                        txtNewPassword.Text,
                        txtRetypePassword.Text,
                        StringComparison.Ordinal))
                {
                    MessageBox.Show(
                        "New Password and Retype Password do not match.",
                        "Change Password",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtRetypePassword.Clear();
                    txtRetypePassword.Focus();
                    return;
                }

                // Show all unmet complexity requirements in one message.
                string newPassword = txtNewPassword.Text;
                var unmetRequirements = new List<string>();

                if (newPassword.Length < 8)
                    unmetRequirements.Add("At least 8 characters");

                if (!ContainsAsciiUppercase(newPassword))
                    unmetRequirements.Add("At least one uppercase letter (A-Z)");

                if (!ContainsAsciiLowercase(newPassword))
                    unmetRequirements.Add("At least one lowercase letter (a-z)");

                if (!ContainsAsciiDigit(newPassword))
                    unmetRequirements.Add("At least one numeric digit (0-9)");

                // A special character is a non-letter/digit that is not whitespace.
                if (!ContainsSpecialCharacter(newPassword))
                    unmetRequirements.Add("At least one special character (for example: !@#$%^&*)");

                if (unmetRequirements.Count > 0)
                {
                    MessageBox.Show(
                        "The new password is missing these requirements:"
                        + Environment.NewLine
                        + "• " + string.Join(
                            Environment.NewLine + "• ",
                            unmetRequirements),
                        "Password Requirements",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    return;
                }

                // Include the current password in the UPDATE condition so the
                // password is not changed if it changed after the initial check.
                int rowsUpdated;

                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    const string updateSql = @"
                        UPDATE Users
                        SET Password = @NewPassword
                        WHERE UserID = @UserID
                        AND Password = @CurrentPassword;";

                    using (var cmd = new MySqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                        cmd.Parameters.AddWithValue("@UserID", AppSession.CurrentUserID);
                        cmd.Parameters.AddWithValue(
                            "@CurrentPassword",
                            txtCurrentPassword.Text);

                        rowsUpdated = cmd.ExecuteNonQuery();
                    }
                }

                if (rowsUpdated != 1)
                {
                    MessageBox.Show(
                        "The password was not changed. The current password may have changed; please try again.",
                        "Change Password",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    "Your password was changed successfully.",
                    "Change Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to change the password:{Environment.NewLine}{ex.Message}",
                    "Change Password Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static bool ContainsAsciiUppercase(string value)
        {
            foreach (char character in value)
            {
                if (character >= 'A' && character <= 'Z')
                    return true;
            }

            return false;
        }

        private static bool ContainsAsciiLowercase(string value)
        {
            foreach (char character in value)
            {
                if (character >= 'a' && character <= 'z')
                    return true;
            }

            return false;
        }

        private static bool ContainsAsciiDigit(string value)
        {
            foreach (char character in value)
            {
                if (character >= '0' && character <= '9')
                    return true;
            }

            return false;
        }

        private static bool ContainsSpecialCharacter(string value)
        {
            foreach (char character in value)
            {
                if (!char.IsLetterOrDigit(character) &&
                    !char.IsWhiteSpace(character))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
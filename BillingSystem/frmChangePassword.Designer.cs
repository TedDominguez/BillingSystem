namespace BillingSystem
{
    partial class frmChangePassword
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
            lblChangePassword = new Label();
            lblCurrentPassword = new Label();
            lblNewPassword = new Label();
            lblRetypePassword = new Label();
            panel1 = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            txtRetypePassword = new MaskedTextBox();
            txtNewPassword = new MaskedTextBox();
            txtCurrentPassword = new MaskedTextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblChangePassword
            // 
            lblChangePassword.AutoSize = true;
            lblChangePassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblChangePassword.Location = new Point(108, 9);
            lblChangePassword.Name = "lblChangePassword";
            lblChangePassword.Size = new Size(177, 28);
            lblChangePassword.TabIndex = 0;
            lblChangePassword.Text = "Change Password";
            // 
            // lblCurrentPassword
            // 
            lblCurrentPassword.AutoSize = true;
            lblCurrentPassword.Location = new Point(19, 33);
            lblCurrentPassword.Name = "lblCurrentPassword";
            lblCurrentPassword.Size = new Size(129, 20);
            lblCurrentPassword.TabIndex = 1;
            lblCurrentPassword.Text = "Current Password: ";
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new Point(19, 81);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(107, 20);
            lblNewPassword.TabIndex = 2;
            lblNewPassword.Text = "New Password:";
            // 
            // lblRetypePassword
            // 
            lblRetypePassword.AutoSize = true;
            lblRetypePassword.Location = new Point(19, 131);
            lblRetypePassword.Name = "lblRetypePassword";
            lblRetypePassword.Size = new Size(123, 20);
            lblRetypePassword.TabIndex = 3;
            lblRetypePassword.Text = "Retype Password:";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(txtRetypePassword);
            panel1.Controls.Add(lblCurrentPassword);
            panel1.Controls.Add(txtNewPassword);
            panel1.Controls.Add(lblNewPassword);
            panel1.Controls.Add(txtCurrentPassword);
            panel1.Controls.Add(lblRetypePassword);
            panel1.Location = new Point(37, 44);
            panel1.Name = "panel1";
            panel1.Size = new Size(319, 250);
            panel1.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(198, 194);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(19, 194);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(123, 29);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save Password";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // txtRetypePassword
            // 
            txtRetypePassword.Location = new Point(167, 128);
            txtRetypePassword.Name = "txtRetypePassword";
            txtRetypePassword.Size = new Size(125, 27);
            txtRetypePassword.TabIndex = 7;
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(167, 78);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(125, 27);
            txtNewPassword.TabIndex = 6;
            // 
            // txtCurrentPassword
            // 
            txtCurrentPassword.Location = new Point(167, 30);
            txtCurrentPassword.Name = "txtCurrentPassword";
            txtCurrentPassword.Size = new Size(125, 27);
            txtCurrentPassword.TabIndex = 5;
            // 
            // frmChangePassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 306);
            Controls.Add(panel1);
            Controls.Add(lblChangePassword);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmChangePassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmChangePassword";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblChangePassword;
        private Label lblCurrentPassword;
        private Label lblNewPassword;
        private Label lblRetypePassword;
        private Panel panel1;
        private MaskedTextBox txtCurrentPassword;
        private MaskedTextBox txtNewPassword;
        private MaskedTextBox txtRetypePassword;
        private Button btnCancel;
        private Button btnSave;
    }
}
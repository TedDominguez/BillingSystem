namespace BillingSystem
{
    partial class CustomerListForm
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
            lblFullName = new Label();
            lblAddress = new Label();
            lblContact = new Label();
            lblEmail = new Label();
            lblBalance = new Label();
            txtAddress = new TextBox();
            txtBalance = new TextBox();
            txtEmail = new TextBox();
            txtFullName = new TextBox();
            txtContact = new TextBox();
            btnSave = new Button();
            bntClear = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(133, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(157, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add New Customer";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(107, 86);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(64, 15);
            lblFullName.TabIndex = 1;
            lblFullName.Text = "Full Name:";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(107, 115);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(52, 15);
            lblAddress.TabIndex = 2;
            lblAddress.Text = "Address:";
            // 
            // lblContact
            // 
            lblContact.AutoSize = true;
            lblContact.Location = new Point(107, 143);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(99, 15);
            lblContact.TabIndex = 3;
            lblContact.Text = "Contact Number:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(107, 171);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Location = new Point(107, 198);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(83, 15);
            lblBalance.TabIndex = 5;
            lblBalance.Text = "Initial Balance:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(165, 108);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(173, 23);
            txtAddress.TabIndex = 6;
            // 
            // txtBalance
            // 
            txtBalance.Location = new Point(196, 195);
            txtBalance.Name = "txtBalance";
            txtBalance.Size = new Size(142, 23);
            txtBalance.TabIndex = 7;
            txtBalance.Text = "0.00";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(152, 166);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(186, 23);
            txtEmail.TabIndex = 8;
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(177, 79);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(161, 23);
            txtFullName.TabIndex = 9;
            // 
            // txtContact
            // 
            txtContact.Location = new Point(212, 137);
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(126, 23);
            txtContact.TabIndex = 10;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(97, 278);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // bntClear
            // 
            bntClear.Location = new Point(188, 278);
            bntClear.Name = "bntClear";
            bntClear.Size = new Size(75, 23);
            bntClear.TabIndex = 12;
            bntClear.Text = "Clear";
            bntClear.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(283, 278);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 13;
            button3.Text = "Back";
            button3.UseVisualStyleBackColor = true;
            // 
            // CustomerListForm
            // 
            AccessibleRole = AccessibleRole.TitleBar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 381);
            Controls.Add(button3);
            Controls.Add(bntClear);
            Controls.Add(btnSave);
            Controls.Add(txtContact);
            Controls.Add(txtFullName);
            Controls.Add(txtEmail);
            Controls.Add(txtBalance);
            Controls.Add(txtAddress);
            Controls.Add(lblBalance);
            Controls.Add(lblEmail);
            Controls.Add(lblContact);
            Controls.Add(lblAddress);
            Controls.Add(lblFullName);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "CustomerListForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Billing System - Add Customer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblFullName;
        private Label lblAddress;
        private Label lblContact;
        private Label lblEmail;
        private Label lblBalance;
        private TextBox txtAddress;
        private TextBox txtBalance;
        private TextBox txtEmail;
        private TextBox txtFullName;
        private TextBox txtContact;
        private Button btnSave;
        private Button bntClear;
        private Button button5;
    }
}
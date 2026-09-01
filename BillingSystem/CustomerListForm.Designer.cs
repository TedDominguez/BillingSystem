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
            lblTitle.Location = new Point(152, 28);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(195, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add New Customer";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(122, 115);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(79, 20);
            lblFullName.TabIndex = 1;
            lblFullName.Text = "Full Name:";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(122, 153);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(65, 20);
            lblAddress.TabIndex = 2;
            lblAddress.Text = "Address:";
            // 
            // lblContact
            // 
            lblContact.AutoSize = true;
            lblContact.Location = new Point(122, 191);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(121, 20);
            lblContact.TabIndex = 3;
            lblContact.Text = "Contact Number:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(122, 228);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Location = new Point(122, 264);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(105, 20);
            lblBalance.TabIndex = 5;
            lblBalance.Text = "Initial Balance:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(189, 144);
            txtAddress.Margin = new Padding(3, 4, 3, 4);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(197, 27);
            txtAddress.TabIndex = 6;
            // 
            // txtBalance
            // 
            txtBalance.Location = new Point(224, 260);
            txtBalance.Margin = new Padding(3, 4, 3, 4);
            txtBalance.Name = "txtBalance";
            txtBalance.Size = new Size(162, 27);
            txtBalance.TabIndex = 7;
            txtBalance.Text = "0.00";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(174, 221);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(212, 27);
            txtEmail.TabIndex = 8;
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(202, 105);
            txtFullName.Margin = new Padding(3, 4, 3, 4);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(183, 27);
            txtFullName.TabIndex = 9;
            // 
            // txtContact
            // 
            txtContact.Location = new Point(242, 183);
            txtContact.Margin = new Padding(3, 4, 3, 4);
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(143, 27);
            txtContact.TabIndex = 10;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(111, 371);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 31);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // bntClear
            // 
            bntClear.Location = new Point(215, 371);
            bntClear.Margin = new Padding(3, 4, 3, 4);
            bntClear.Name = "bntClear";
            bntClear.Size = new Size(86, 31);
            bntClear.TabIndex = 12;
            bntClear.Text = "Clear";
            bntClear.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(323, 371);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(86, 31);
            button3.TabIndex = 13;
            button3.Text = "Back";
            button3.UseVisualStyleBackColor = true;
            // 
            // CustomerListForm
            // 
            AccessibleRole = AccessibleRole.TitleBar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(496, 508);
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
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "AddCustomerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Billing System v1.0 - Customer List (T.D.)";
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
        private Button button3;
    }
}
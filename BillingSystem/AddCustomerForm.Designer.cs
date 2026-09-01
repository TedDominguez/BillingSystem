namespace BillingSystem
{
    partial class AddCustomerForm
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
            labellblAddress3 = new Label();
            lblContact = new Label();
            lblEmail = new Label();
            lblBalance = new Label();
            txtFullName = new TextBox();
            txtAddress = new TextBox();
            txtContact = new TextBox();
            txtEmail = new TextBox();
            txtBalance = new TextBox();
            btnSave = new Button();
            btnClear = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(122, 35);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(195, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add New Customer";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(40, 98);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(79, 20);
            lblFullName.TabIndex = 0;
            lblFullName.Text = "Full Name:";
            // 
            // labellblAddress3
            // 
            labellblAddress3.AutoSize = true;
            labellblAddress3.Location = new Point(40, 131);
            labellblAddress3.Name = "labellblAddress3";
            labellblAddress3.Size = new Size(65, 20);
            labellblAddress3.TabIndex = 0;
            labellblAddress3.Text = "Address:";
            labellblAddress3.Click += labellblAddress3_Click;
            // 
            // lblContact
            // 
            lblContact.AutoSize = true;
            lblContact.Location = new Point(40, 164);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(121, 20);
            lblContact.TabIndex = 0;
            lblContact.Text = "Contact Number:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(40, 197);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email:";
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Location = new Point(40, 230);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(105, 20);
            lblBalance.TabIndex = 0;
            lblBalance.Text = "Initial Balance:";
            lblBalance.Click += this.label6_Click;
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(173, 95);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(219, 27);
            txtFullName.TabIndex = 1;
            txtFullName.Text = "  ";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(173, 128);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(219, 27);
            txtAddress.TabIndex = 1;
            txtAddress.Text = "  ";
            // 
            // txtContact
            // 
            txtContact.Location = new Point(173, 161);
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(219, 27);
            txtContact.TabIndex = 1;
            txtContact.Text = "  ";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(173, 194);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(219, 27);
            txtEmail.TabIndex = 1;
            txtEmail.Text = "  ";
            // 
            // txtBalance
            // 
            txtBalance.Location = new Point(173, 227);
            txtBalance.Name = "txtBalance";
            txtBalance.Size = new Size(219, 27);
            txtBalance.TabIndex = 1;
            txtBalance.Text = "  0.00";
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.Location = new Point(57, 293);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClear.Location = new Point(157, 293);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += button1_Click;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBack.Location = new Point(257, 293);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 2;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // AddCustomerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 373);
            Controls.Add(btnBack);
            Controls.Add(btnClear);
            Controls.Add(btnSave);
            Controls.Add(txtBalance);
            Controls.Add(txtEmail);
            Controls.Add(txtContact);
            Controls.Add(txtAddress);
            Controls.Add(txtFullName);
            Controls.Add(lblBalance);
            Controls.Add(lblEmail);
            Controls.Add(lblContact);
            Controls.Add(labellblAddress3);
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
        private Label labellblAddress3;
        private Label lblContact;
        private Label lblEmail;
        private Label lblBalance;
        private TextBox txtFullName;
        private TextBox txtAddress;
        private TextBox txtContact;
        private TextBox txtEmail;
        private TextBox txtBalance;
        private Button btnSave;
        private Button btnClear;
        private Button btnBack;
    }
}
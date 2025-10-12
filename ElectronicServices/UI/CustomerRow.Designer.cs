namespace ElectronicServices
{
    partial class CustomerRow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            codeLabel = new Label();
            nameLabel = new Label();
            payLabel = new Label();
            takeLabel = new Label();
            resultLabel = new Label();
            customerTransactionsBtn = new Guna.UI2.WinForms.Guna2Button();
            editBtn = new Guna.UI2.WinForms.Guna2Button();
            deleteCustomerBtn = new Guna.UI2.WinForms.Guna2Button();
            customerName = new Guna.UI2.WinForms.Guna2TextBox();
            SuspendLayout();
            // 
            // codeLabel
            // 
            codeLabel.BackColor = Color.Transparent;
            codeLabel.Font = new Font("Segoe UI", 12F);
            codeLabel.Location = new Point(877, 0);
            codeLabel.Name = "codeLabel";
            codeLabel.Size = new Size(40, 40);
            codeLabel.TabIndex = 0;
            codeLabel.Text = "ك";
            codeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            nameLabel.BackColor = Color.Transparent;
            nameLabel.Font = new Font("Segoe UI", 12F);
            nameLabel.Location = new Point(489, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(382, 40);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "اسم العميل";
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // payLabel
            // 
            payLabel.BackColor = Color.Transparent;
            payLabel.Font = new Font("Segoe UI", 12F);
            payLabel.ForeColor = Color.Navy;
            payLabel.Location = new Point(383, 0);
            payLabel.Name = "payLabel";
            payLabel.Size = new Size(100, 40);
            payLabel.TabIndex = 2;
            payLabel.Text = "دفع";
            payLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // takeLabel
            // 
            takeLabel.BackColor = Color.Transparent;
            takeLabel.Font = new Font("Segoe UI", 12F);
            takeLabel.ForeColor = Color.Maroon;
            takeLabel.Location = new Point(277, 0);
            takeLabel.Name = "takeLabel";
            takeLabel.Size = new Size(100, 40);
            takeLabel.TabIndex = 3;
            takeLabel.Text = "أخذ";
            takeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // resultLabel
            // 
            resultLabel.BackColor = Color.Transparent;
            resultLabel.Font = new Font("Segoe UI", 12F);
            resultLabel.ForeColor = Color.FromArgb(0, 32, 0);
            resultLabel.Location = new Point(141, 0);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(130, 40);
            resultLabel.TabIndex = 4;
            resultLabel.Text = "الرصيد";
            resultLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // customerTransactionsBtn
            // 
            customerTransactionsBtn.BackColor = Color.Transparent;
            customerTransactionsBtn.BorderRadius = 15;
            customerTransactionsBtn.CustomizableEdges = customizableEdges1;
            customerTransactionsBtn.DisabledState.BorderColor = Color.DarkGray;
            customerTransactionsBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            customerTransactionsBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            customerTransactionsBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            customerTransactionsBtn.FillColor = Color.Transparent;
            customerTransactionsBtn.Font = new Font("Segoe UI", 9F);
            customerTransactionsBtn.ForeColor = Color.White;
            customerTransactionsBtn.Image = Properties.Resources.board;
            customerTransactionsBtn.ImageSize = new Size(40, 40);
            customerTransactionsBtn.Location = new Point(95, 0);
            customerTransactionsBtn.Name = "customerTransactionsBtn";
            customerTransactionsBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            customerTransactionsBtn.Size = new Size(40, 40);
            customerTransactionsBtn.TabIndex = 5;
            customerTransactionsBtn.SizeChanged += CustomerTransactionsBtn_SizeChanged;
            customerTransactionsBtn.Click += CustomerTransactionsBtn_Click;
            // 
            // editBtn
            // 
            editBtn.BackColor = Color.Transparent;
            editBtn.BorderRadius = 15;
            editBtn.CustomizableEdges = customizableEdges3;
            editBtn.DisabledState.BorderColor = Color.DarkGray;
            editBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            editBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            editBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            editBtn.FillColor = Color.Transparent;
            editBtn.Font = new Font("Segoe UI", 9F);
            editBtn.ForeColor = Color.White;
            editBtn.Image = Properties.Resources.editIcon;
            editBtn.ImageSize = new Size(40, 40);
            editBtn.Location = new Point(49, 0);
            editBtn.Name = "editBtn";
            editBtn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            editBtn.Size = new Size(40, 40);
            editBtn.TabIndex = 6;
            editBtn.Click += EditBtn_Click;
            // 
            // deleteCustomerBtn
            // 
            deleteCustomerBtn.BackColor = Color.Transparent;
            deleteCustomerBtn.BorderRadius = 15;
            deleteCustomerBtn.CustomizableEdges = customizableEdges5;
            deleteCustomerBtn.DisabledState.BorderColor = Color.DarkGray;
            deleteCustomerBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            deleteCustomerBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            deleteCustomerBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            deleteCustomerBtn.FillColor = Color.Transparent;
            deleteCustomerBtn.Font = new Font("Segoe UI", 9F);
            deleteCustomerBtn.ForeColor = Color.White;
            deleteCustomerBtn.Image = Properties.Resources.no_entry;
            deleteCustomerBtn.ImageSize = new Size(40, 40);
            deleteCustomerBtn.Location = new Point(3, 0);
            deleteCustomerBtn.Name = "deleteCustomerBtn";
            deleteCustomerBtn.ShadowDecoration.CustomizableEdges = customizableEdges6;
            deleteCustomerBtn.Size = new Size(40, 40);
            deleteCustomerBtn.TabIndex = 7;
            deleteCustomerBtn.Click += DeleteCustomerBtn_Click;
            // 
            // customerName
            // 
            customerName.BorderRadius = 8;
            customerName.CustomizableEdges = customizableEdges7;
            customerName.DefaultText = "";
            customerName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            customerName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            customerName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            customerName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            customerName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            customerName.Font = new Font("Segoe UI", 12F);
            customerName.ForeColor = Color.Black;
            customerName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            customerName.Location = new Point(490, 0);
            customerName.Margin = new Padding(4);
            customerName.Name = "customerName";
            customerName.PlaceholderText = "";
            customerName.RightToLeft = RightToLeft.Yes;
            customerName.SelectedText = "";
            customerName.ShadowDecoration.CustomizableEdges = customizableEdges8;
            customerName.Size = new Size(380, 40);
            customerName.TabIndex = 8;
            customerName.Visible = false;
            customerName.KeyPress += TextBox_KeyPress;
            // 
            // CustomerRow
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(180, 240, 180);
            Controls.Add(customerName);
            Controls.Add(deleteCustomerBtn);
            Controls.Add(editBtn);
            Controls.Add(customerTransactionsBtn);
            Controls.Add(resultLabel);
            Controls.Add(takeLabel);
            Controls.Add(payLabel);
            Controls.Add(nameLabel);
            Controls.Add(codeLabel);
            Name = "CustomerRow";
            Size = new Size(920, 40);
            ResumeLayout(false);
        }

        #endregion

        private Label codeLabel;
        private Label nameLabel;
        private Label payLabel;
        private Label takeLabel;
        private Label resultLabel;
        private Guna.UI2.WinForms.Guna2Button customerTransactionsBtn;
        private Guna.UI2.WinForms.Guna2Button editBtn;
        private Guna.UI2.WinForms.Guna2Button deleteCustomerBtn;
        private Guna.UI2.WinForms.Guna2TextBox customerName;
    }
}

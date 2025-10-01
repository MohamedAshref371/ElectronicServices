namespace ElectronicServices
{
    partial class WalletRow
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
            phoneNumber = new Label();
            deposit = new Label();
            withdrawal = new Label();
            balance = new Label();
            chooseBtn = new Guna.UI2.WinForms.Guna2Button();
            editBtn = new Guna.UI2.WinForms.Guna2Button();
            deleteBtn = new Guna.UI2.WinForms.Guna2Button();
            walletType = new Label();
            SuspendLayout();
            // 
            // phoneNumber
            // 
            phoneNumber.BackColor = Color.Transparent;
            phoneNumber.Font = new Font("Segoe UI", 12F);
            phoneNumber.Location = new Point(650, 0);
            phoneNumber.Name = "phoneNumber";
            phoneNumber.Size = new Size(270, 40);
            phoneNumber.TabIndex = 1;
            phoneNumber.Text = "رقم الهاتف";
            phoneNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // deposit
            // 
            deposit.BackColor = Color.Transparent;
            deposit.Font = new Font("Segoe UI", 12F);
            deposit.ForeColor = Color.Navy;
            deposit.Location = new Point(277, 0);
            deposit.Name = "deposit";
            deposit.Size = new Size(130, 40);
            deposit.TabIndex = 2;
            deposit.Text = "باقي الإيداع";
            deposit.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // withdrawal
            // 
            withdrawal.BackColor = Color.Transparent;
            withdrawal.Font = new Font("Segoe UI", 12F);
            withdrawal.ForeColor = Color.Maroon;
            withdrawal.Location = new Point(413, 0);
            withdrawal.Name = "withdrawal";
            withdrawal.Size = new Size(130, 40);
            withdrawal.TabIndex = 3;
            withdrawal.Text = "باقي السحب";
            withdrawal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // balance
            // 
            balance.BackColor = Color.Transparent;
            balance.Font = new Font("Segoe UI", 12F);
            balance.ForeColor = Color.Green;
            balance.Location = new Point(141, 0);
            balance.Name = "balance";
            balance.Size = new Size(130, 40);
            balance.TabIndex = 4;
            balance.Text = "الرصيد";
            balance.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // chooseBtn
            // 
            chooseBtn.BackColor = Color.Transparent;
            chooseBtn.BorderRadius = 15;
            chooseBtn.CustomizableEdges = customizableEdges1;
            chooseBtn.DisabledState.BorderColor = Color.DarkGray;
            chooseBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            chooseBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            chooseBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            chooseBtn.FillColor = Color.Transparent;
            chooseBtn.Font = new Font("Segoe UI", 9F);
            chooseBtn.ForeColor = Color.White;
            chooseBtn.Image = Properties.Resources.board;
            chooseBtn.ImageSize = new Size(40, 40);
            chooseBtn.Location = new Point(95, 0);
            chooseBtn.Name = "chooseBtn";
            chooseBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            chooseBtn.Size = new Size(40, 40);
            chooseBtn.TabIndex = 5;
            chooseBtn.SizeChanged += ChooseBtn_SizeChanged;
            chooseBtn.Click += ChooseBtn_Click;
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
            // deleteBtn
            // 
            deleteBtn.BackColor = Color.Transparent;
            deleteBtn.BorderRadius = 15;
            deleteBtn.CustomizableEdges = customizableEdges5;
            deleteBtn.DisabledState.BorderColor = Color.DarkGray;
            deleteBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            deleteBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            deleteBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            deleteBtn.FillColor = Color.Transparent;
            deleteBtn.Font = new Font("Segoe UI", 9F);
            deleteBtn.ForeColor = Color.White;
            deleteBtn.Image = Properties.Resources.no_entry;
            deleteBtn.ImageSize = new Size(40, 40);
            deleteBtn.Location = new Point(3, 0);
            deleteBtn.Name = "deleteBtn";
            deleteBtn.ShadowDecoration.CustomizableEdges = customizableEdges6;
            deleteBtn.Size = new Size(40, 40);
            deleteBtn.TabIndex = 7;
            deleteBtn.Click += DeleteBtn_Click;
            // 
            // walletType
            // 
            walletType.BackColor = Color.Transparent;
            walletType.Font = new Font("Segoe UI", 12F);
            walletType.ForeColor = Color.Black;
            walletType.Location = new Point(549, 0);
            walletType.Name = "walletType";
            walletType.Size = new Size(95, 40);
            walletType.TabIndex = 8;
            walletType.Text = "النوع";
            walletType.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WalletRow
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(50, 192, 192, 192);
            Controls.Add(walletType);
            Controls.Add(deleteBtn);
            Controls.Add(editBtn);
            Controls.Add(chooseBtn);
            Controls.Add(balance);
            Controls.Add(withdrawal);
            Controls.Add(deposit);
            Controls.Add(phoneNumber);
            Name = "WalletRow";
            Size = new Size(920, 40);
            ResumeLayout(false);
        }

        #endregion
        private Label phoneNumber;
        private Label deposit;
        private Label withdrawal;
        private Label balance;
        private Guna.UI2.WinForms.Guna2Button chooseBtn;
        private Guna.UI2.WinForms.Guna2Button editBtn;
        private Guna.UI2.WinForms.Guna2Button deleteBtn;
        private Label walletType;
    }
}

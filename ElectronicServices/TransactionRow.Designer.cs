namespace ElectronicServices
{
    partial class TransactionRow
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            deleteCustomerBtn = new Guna.UI2.WinForms.Guna2Button();
            customerBtn = new Guna.UI2.WinForms.Guna2Button();
            dateLabel = new Label();
            takeLabel = new Label();
            payLabel = new Label();
            nameLabel = new Label();
            editBtn = new Guna.UI2.WinForms.Guna2Button();
            infoBtn = new Guna.UI2.WinForms.Guna2Button();
            payEdit = new Guna.UI2.WinForms.Guna2NumericUpDown();
            takeEdit = new Guna.UI2.WinForms.Guna2NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)payEdit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)takeEdit).BeginInit();
            SuspendLayout();
            // 
            // deleteCustomerBtn
            // 
            deleteCustomerBtn.BackColor = Color.Transparent;
            deleteCustomerBtn.BorderRadius = 15;
            deleteCustomerBtn.CustomizableEdges = customizableEdges1;
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
            deleteCustomerBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            deleteCustomerBtn.Size = new Size(40, 40);
            deleteCustomerBtn.TabIndex = 12;
            deleteCustomerBtn.Click += DeleteCustomerBtn_Click;
            // 
            // customerBtn
            // 
            customerBtn.BackColor = Color.Transparent;
            customerBtn.BorderRadius = 15;
            customerBtn.CustomizableEdges = customizableEdges3;
            customerBtn.DisabledState.BorderColor = Color.DarkGray;
            customerBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            customerBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            customerBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            customerBtn.FillColor = Color.Transparent;
            customerBtn.Font = new Font("Segoe UI", 9F);
            customerBtn.ForeColor = Color.White;
            customerBtn.Image = Properties.Resources.board;
            customerBtn.ImageSize = new Size(40, 40);
            customerBtn.Location = new Point(49, 0);
            customerBtn.Name = "customerBtn";
            customerBtn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            customerBtn.Size = new Size(40, 40);
            customerBtn.TabIndex = 11;
            customerBtn.Click += CustomerBtn_Click;
            // 
            // dateLabel
            // 
            dateLabel.BackColor = Color.Transparent;
            dateLabel.Font = new Font("Segoe UI", 12F);
            dateLabel.Location = new Point(187, 0);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new Size(130, 40);
            dateLabel.TabIndex = 10;
            dateLabel.Text = "التاريخ";
            dateLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // takeLabel
            // 
            takeLabel.BackColor = Color.Transparent;
            takeLabel.Font = new Font("Segoe UI", 12F);
            takeLabel.Location = new Point(323, 0);
            takeLabel.Name = "takeLabel";
            takeLabel.Size = new Size(100, 40);
            takeLabel.TabIndex = 9;
            takeLabel.Text = "أخذ";
            takeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // payLabel
            // 
            payLabel.BackColor = Color.Transparent;
            payLabel.Font = new Font("Segoe UI", 12F);
            payLabel.Location = new Point(429, 0);
            payLabel.Name = "payLabel";
            payLabel.Size = new Size(100, 40);
            payLabel.TabIndex = 8;
            payLabel.Text = "دفع";
            payLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            nameLabel.BackColor = Color.Transparent;
            nameLabel.Font = new Font("Segoe UI", 12F);
            nameLabel.Location = new Point(535, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(382, 40);
            nameLabel.TabIndex = 7;
            nameLabel.Text = "اسم العميل";
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // editBtn
            // 
            editBtn.BackColor = Color.Transparent;
            editBtn.BorderRadius = 15;
            editBtn.CustomizableEdges = customizableEdges5;
            editBtn.DisabledState.BorderColor = Color.DarkGray;
            editBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            editBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            editBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            editBtn.FillColor = Color.Transparent;
            editBtn.Font = new Font("Segoe UI", 9F);
            editBtn.ForeColor = Color.White;
            editBtn.Image = Properties.Resources.editIcon;
            editBtn.ImageSize = new Size(40, 40);
            editBtn.Location = new Point(95, 0);
            editBtn.Name = "editBtn";
            editBtn.ShadowDecoration.CustomizableEdges = customizableEdges6;
            editBtn.Size = new Size(40, 40);
            editBtn.TabIndex = 13;
            editBtn.Click += EditBtn_Click;
            // 
            // infoBtn
            // 
            infoBtn.BackColor = Color.Transparent;
            infoBtn.BorderRadius = 15;
            infoBtn.CustomizableEdges = customizableEdges7;
            infoBtn.DisabledState.BorderColor = Color.DarkGray;
            infoBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            infoBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            infoBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            infoBtn.FillColor = Color.Transparent;
            infoBtn.Font = new Font("Segoe UI", 9F);
            infoBtn.ForeColor = Color.White;
            infoBtn.Image = Properties.Resources.infoIcon;
            infoBtn.ImageSize = new Size(40, 40);
            infoBtn.Location = new Point(141, 0);
            infoBtn.Name = "infoBtn";
            infoBtn.ShadowDecoration.CustomizableEdges = customizableEdges8;
            infoBtn.Size = new Size(40, 40);
            infoBtn.TabIndex = 14;
            infoBtn.Click += InfoBtn_Click;
            // 
            // payEdit
            // 
            payEdit.BackColor = Color.Transparent;
            payEdit.BorderColor = Color.Silver;
            payEdit.BorderRadius = 10;
            payEdit.CustomizableEdges = customizableEdges9;
            payEdit.DecimalPlaces = 2;
            payEdit.Font = new Font("Segoe UI", 12F);
            payEdit.Location = new Point(429, 2);
            payEdit.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            payEdit.Name = "payEdit";
            payEdit.ShadowDecoration.CustomizableEdges = customizableEdges10;
            payEdit.Size = new Size(100, 36);
            payEdit.TabIndex = 15;
            payEdit.Visible = false;
            // 
            // takeEdit
            // 
            takeEdit.BackColor = Color.Transparent;
            takeEdit.BorderColor = Color.Silver;
            takeEdit.BorderRadius = 10;
            takeEdit.CustomizableEdges = customizableEdges11;
            takeEdit.DecimalPlaces = 2;
            takeEdit.Font = new Font("Segoe UI", 12F);
            takeEdit.Location = new Point(323, 2);
            takeEdit.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            takeEdit.Name = "takeEdit";
            takeEdit.ShadowDecoration.CustomizableEdges = customizableEdges12;
            takeEdit.Size = new Size(100, 36);
            takeEdit.TabIndex = 16;
            takeEdit.UpDownButtonFillColor = Color.FromArgb(255, 148, 94);
            takeEdit.Visible = false;
            // 
            // TransactionRow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(50, 255, 255, 255);
            Controls.Add(takeEdit);
            Controls.Add(payEdit);
            Controls.Add(infoBtn);
            Controls.Add(editBtn);
            Controls.Add(deleteCustomerBtn);
            Controls.Add(customerBtn);
            Controls.Add(dateLabel);
            Controls.Add(takeLabel);
            Controls.Add(payLabel);
            Controls.Add(nameLabel);
            Name = "TransactionRow";
            Size = new Size(920, 40);
            ((System.ComponentModel.ISupportInitialize)payEdit).EndInit();
            ((System.ComponentModel.ISupportInitialize)takeEdit).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button deleteCustomerBtn;
        private Guna.UI2.WinForms.Guna2Button customerBtn;
        private Label dateLabel;
        private Label takeLabel;
        private Label payLabel;
        private Label nameLabel;
        private Guna.UI2.WinForms.Guna2Button editBtn;
        private Guna.UI2.WinForms.Guna2Button infoBtn;
        private Guna.UI2.WinForms.Guna2NumericUpDown payEdit;
        private Guna.UI2.WinForms.Guna2NumericUpDown takeEdit;
    }
}

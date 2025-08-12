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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            codeLabel = new Label();
            nameLabel = new Label();
            payLabel = new Label();
            takeLabel = new Label();
            resultLabel = new Label();
            customerTransactionsBtn = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // codeLabel
            // 
            codeLabel.BackColor = Color.Transparent;
            codeLabel.Font = new Font("Segoe UI", 12F);
            codeLabel.Location = new Point(817, 0);
            codeLabel.Name = "codeLabel";
            codeLabel.Size = new Size(80, 40);
            codeLabel.TabIndex = 0;
            codeLabel.Text = "الكود";
            codeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            nameLabel.BackColor = Color.Transparent;
            nameLabel.Font = new Font("Segoe UI", 12F);
            nameLabel.Location = new Point(397, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(414, 40);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "اسم العميل";
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // payLabel
            // 
            payLabel.BackColor = Color.Transparent;
            payLabel.Font = new Font("Segoe UI", 12F);
            payLabel.Location = new Point(291, 0);
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
            takeLabel.Location = new Point(185, 0);
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
            resultLabel.Location = new Point(49, 0);
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
            customerTransactionsBtn.CustomizableEdges = customizableEdges3;
            customerTransactionsBtn.DisabledState.BorderColor = Color.DarkGray;
            customerTransactionsBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            customerTransactionsBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            customerTransactionsBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            customerTransactionsBtn.FillColor = Color.Transparent;
            customerTransactionsBtn.Font = new Font("Segoe UI", 9F);
            customerTransactionsBtn.ForeColor = Color.White;
            customerTransactionsBtn.Image = Properties.Resources.board;
            customerTransactionsBtn.ImageSize = new Size(40, 40);
            customerTransactionsBtn.Location = new Point(3, 0);
            customerTransactionsBtn.Name = "customerTransactionsBtn";
            customerTransactionsBtn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            customerTransactionsBtn.Size = new Size(40, 40);
            customerTransactionsBtn.TabIndex = 5;
            // 
            // CustomerRow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(50, 255, 255, 255);
            Controls.Add(customerTransactionsBtn);
            Controls.Add(resultLabel);
            Controls.Add(takeLabel);
            Controls.Add(payLabel);
            Controls.Add(nameLabel);
            Controls.Add(codeLabel);
            Name = "CustomerRow";
            Size = new Size(900, 40);
            ResumeLayout(false);
        }

        #endregion

        private Label codeLabel;
        private Label nameLabel;
        private Label payLabel;
        private Label takeLabel;
        private Label resultLabel;
        private Guna.UI2.WinForms.Guna2Button customerTransactionsBtn;
    }
}

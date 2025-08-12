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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            customerTransactionsBtn = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(817, 0);
            label1.Name = "label1";
            label1.Size = new Size(80, 40);
            label1.TabIndex = 0;
            label1.Text = "الكود";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(397, 0);
            label2.Name = "label2";
            label2.Size = new Size(414, 40);
            label2.TabIndex = 1;
            label2.Text = "اسم العميل";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(291, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 40);
            label3.TabIndex = 2;
            label3.Text = "دفع (له)";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(185, 0);
            label4.Name = "label4";
            label4.Size = new Size(100, 40);
            label4.TabIndex = 3;
            label4.Text = "أخذ (عليه)";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(49, 0);
            label5.Name = "label5";
            label5.Size = new Size(130, 40);
            label5.TabIndex = 4;
            label5.Text = "النتيجة";
            label5.TextAlign = ContentAlignment.MiddleCenter;
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
            customerTransactionsBtn.ImageSize = new Size(40, 40);
            customerTransactionsBtn.Location = new Point(3, 0);
            customerTransactionsBtn.Name = "customerTransactionsBtn";
            customerTransactionsBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            customerTransactionsBtn.Size = new Size(40, 40);
            customerTransactionsBtn.TabIndex = 5;
            // 
            // CustomerRow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(200, 255, 255, 255);
            Controls.Add(customerTransactionsBtn);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "CustomerRow";
            Size = new Size(900, 40);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Guna.UI2.WinForms.Guna2Button customerTransactionsBtn;
    }
}

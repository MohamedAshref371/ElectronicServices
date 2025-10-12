namespace ElectronicServices
{
    partial class RecordRow
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
            phoneNumber = new Label();
            deposit = new Label();
            withdrawal = new Label();
            balance = new Label();
            withdRema = new Label();
            depoRema = new Label();
            date = new Label();
            SuspendLayout();
            // 
            // phoneNumber
            // 
            phoneNumber.BackColor = Color.Transparent;
            phoneNumber.Font = new Font("Segoe UI", 12F);
            phoneNumber.Location = new Point(784, 0);
            phoneNumber.Name = "phoneNumber";
            phoneNumber.Size = new Size(136, 40);
            phoneNumber.TabIndex = 1;
            phoneNumber.Text = "رقم الهاتف";
            phoneNumber.TextAlign = ContentAlignment.MiddleCenter;
            phoneNumber.Click += PhoneNumber_Click;
            // 
            // deposit
            // 
            deposit.BackColor = Color.Transparent;
            deposit.Font = new Font("Segoe UI", 12F);
            deposit.ForeColor = Color.Navy;
            deposit.Location = new Point(129, 0);
            deposit.Name = "deposit";
            deposit.Size = new Size(120, 40);
            deposit.TabIndex = 2;
            deposit.Text = "قيمة الإيداع";
            deposit.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // withdrawal
            // 
            withdrawal.BackColor = Color.Transparent;
            withdrawal.Font = new Font("Segoe UI", 12F);
            withdrawal.ForeColor = Color.Maroon;
            withdrawal.Location = new Point(255, 0);
            withdrawal.Name = "withdrawal";
            withdrawal.Size = new Size(120, 40);
            withdrawal.TabIndex = 3;
            withdrawal.Text = "قيمة السحب";
            withdrawal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // balance
            // 
            balance.BackColor = Color.Transparent;
            balance.Font = new Font("Segoe UI", 12F);
            balance.ForeColor = Color.FromArgb(0, 64, 0);
            balance.Location = new Point(3, 0);
            balance.Name = "balance";
            balance.Size = new Size(120, 40);
            balance.TabIndex = 4;
            balance.Text = "الرصيد";
            balance.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // withdRema
            // 
            withdRema.BackColor = Color.Transparent;
            withdRema.Font = new Font("Segoe UI", 12F);
            withdRema.ForeColor = Color.Maroon;
            withdRema.Location = new Point(507, 0);
            withdRema.Name = "withdRema";
            withdRema.Size = new Size(120, 40);
            withdRema.TabIndex = 6;
            withdRema.Text = "باقي السحب";
            withdRema.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // depoRema
            // 
            depoRema.BackColor = Color.Transparent;
            depoRema.Font = new Font("Segoe UI", 12F);
            depoRema.ForeColor = Color.Navy;
            depoRema.Location = new Point(381, 0);
            depoRema.Name = "depoRema";
            depoRema.Size = new Size(120, 40);
            depoRema.TabIndex = 5;
            depoRema.Text = "باقي الإيداع";
            depoRema.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // date
            // 
            date.BackColor = Color.Transparent;
            date.Font = new Font("Segoe UI", 12F);
            date.ForeColor = Color.Black;
            date.Location = new Point(633, 0);
            date.Name = "date";
            date.Size = new Size(145, 40);
            date.TabIndex = 7;
            date.Text = "التاريخ";
            date.TextAlign = ContentAlignment.MiddleCenter;
            date.DoubleClick += Date_DoubleClick;
            // 
            // RecordRow
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(202, 230, 255);
            Controls.Add(date);
            Controls.Add(withdRema);
            Controls.Add(depoRema);
            Controls.Add(balance);
            Controls.Add(withdrawal);
            Controls.Add(deposit);
            Controls.Add(phoneNumber);
            Name = "RecordRow";
            Size = new Size(920, 40);
            ResumeLayout(false);
        }

        #endregion
        private Label phoneNumber;
        private Label deposit;
        private Label withdrawal;
        private Label balance;
        private Label withdRema;
        private Label depoRema;
        private Label date;
    }
}

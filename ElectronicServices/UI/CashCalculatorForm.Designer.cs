namespace ElectronicServices
{
    partial class CashCalculatorForm
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
            one = new TextBox();
            oneLbl = new Label();
            fiveLbl = new Label();
            five = new TextBox();
            tenLbl = new Label();
            ten = new TextBox();
            twentyLbl = new Label();
            twenty = new TextBox();
            fiftyLbl = new Label();
            fifty = new TextBox();
            hundredLbl = new Label();
            hundred = new TextBox();
            twoHundredLbl = new Label();
            twoHundred = new TextBox();
            resultLbl = new Label();
            result = new TextBox();
            removeBtn = new Button();
            SuspendLayout();
            // 
            // one
            // 
            one.Font = new Font("Segoe UI", 16F);
            one.Location = new Point(12, 27);
            one.MaxLength = 9;
            one.Name = "one";
            one.Size = new Size(146, 36);
            one.TabIndex = 0;
            one.TextChanged += Number_TextChanged;
            one.KeyPress += Number_KeyPress;
            // 
            // oneLbl
            // 
            oneLbl.Font = new Font("Segoe UI", 16F);
            oneLbl.Location = new Point(186, 27);
            oneLbl.Name = "oneLbl";
            oneLbl.Size = new Size(100, 36);
            oneLbl.TabIndex = 1;
            oneLbl.Text = "1";
            oneLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fiveLbl
            // 
            fiveLbl.Font = new Font("Segoe UI", 16F);
            fiveLbl.Location = new Point(186, 71);
            fiveLbl.Name = "fiveLbl";
            fiveLbl.Size = new Size(100, 36);
            fiveLbl.TabIndex = 3;
            fiveLbl.Text = "5";
            fiveLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // five
            // 
            five.Font = new Font("Segoe UI", 16F);
            five.Location = new Point(12, 71);
            five.MaxLength = 9;
            five.Name = "five";
            five.Size = new Size(146, 36);
            five.TabIndex = 2;
            five.TextChanged += Number_TextChanged;
            five.KeyPress += Number_KeyPress;
            // 
            // tenLbl
            // 
            tenLbl.Font = new Font("Segoe UI", 16F);
            tenLbl.Location = new Point(186, 115);
            tenLbl.Name = "tenLbl";
            tenLbl.Size = new Size(100, 36);
            tenLbl.TabIndex = 5;
            tenLbl.Text = "10";
            tenLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ten
            // 
            ten.Font = new Font("Segoe UI", 16F);
            ten.Location = new Point(12, 115);
            ten.MaxLength = 9;
            ten.Name = "ten";
            ten.Size = new Size(146, 36);
            ten.TabIndex = 4;
            ten.TextChanged += Number_TextChanged;
            ten.KeyPress += Number_KeyPress;
            // 
            // twentyLbl
            // 
            twentyLbl.Font = new Font("Segoe UI", 16F);
            twentyLbl.Location = new Point(186, 159);
            twentyLbl.Name = "twentyLbl";
            twentyLbl.Size = new Size(100, 36);
            twentyLbl.TabIndex = 7;
            twentyLbl.Text = "20";
            twentyLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // twenty
            // 
            twenty.Font = new Font("Segoe UI", 16F);
            twenty.Location = new Point(12, 159);
            twenty.MaxLength = 9;
            twenty.Name = "twenty";
            twenty.Size = new Size(146, 36);
            twenty.TabIndex = 6;
            twenty.TextChanged += Number_TextChanged;
            twenty.KeyPress += Number_KeyPress;
            // 
            // fiftyLbl
            // 
            fiftyLbl.Font = new Font("Segoe UI", 16F);
            fiftyLbl.Location = new Point(186, 203);
            fiftyLbl.Name = "fiftyLbl";
            fiftyLbl.Size = new Size(100, 36);
            fiftyLbl.TabIndex = 9;
            fiftyLbl.Text = "50";
            fiftyLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fifty
            // 
            fifty.Font = new Font("Segoe UI", 16F);
            fifty.Location = new Point(12, 203);
            fifty.MaxLength = 9;
            fifty.Name = "fifty";
            fifty.Size = new Size(146, 36);
            fifty.TabIndex = 8;
            fifty.TextChanged += Number_TextChanged;
            fifty.KeyPress += Number_KeyPress;
            // 
            // hundredLbl
            // 
            hundredLbl.Font = new Font("Segoe UI", 16F);
            hundredLbl.Location = new Point(186, 247);
            hundredLbl.Name = "hundredLbl";
            hundredLbl.Size = new Size(100, 36);
            hundredLbl.TabIndex = 11;
            hundredLbl.Text = "100";
            hundredLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // hundred
            // 
            hundred.Font = new Font("Segoe UI", 16F);
            hundred.Location = new Point(12, 247);
            hundred.MaxLength = 9;
            hundred.Name = "hundred";
            hundred.Size = new Size(146, 36);
            hundred.TabIndex = 10;
            hundred.TextChanged += Number_TextChanged;
            hundred.KeyPress += Number_KeyPress;
            // 
            // twoHundredLbl
            // 
            twoHundredLbl.Font = new Font("Segoe UI", 16F);
            twoHundredLbl.Location = new Point(186, 291);
            twoHundredLbl.Name = "twoHundredLbl";
            twoHundredLbl.Size = new Size(100, 36);
            twoHundredLbl.TabIndex = 13;
            twoHundredLbl.Text = "200";
            twoHundredLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // twoHundred
            // 
            twoHundred.Font = new Font("Segoe UI", 16F);
            twoHundred.Location = new Point(12, 291);
            twoHundred.MaxLength = 9;
            twoHundred.Name = "twoHundred";
            twoHundred.Size = new Size(146, 36);
            twoHundred.TabIndex = 12;
            twoHundred.TextChanged += Number_TextChanged;
            twoHundred.KeyPress += Number_KeyPress;
            // 
            // resultLbl
            // 
            resultLbl.Font = new Font("Segoe UI", 16F);
            resultLbl.Location = new Point(186, 359);
            resultLbl.Name = "resultLbl";
            resultLbl.Size = new Size(100, 36);
            resultLbl.TabIndex = 15;
            resultLbl.Text = "المجموع";
            resultLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // result
            // 
            result.Font = new Font("Segoe UI", 16F);
            result.Location = new Point(12, 359);
            result.MaxLength = 10;
            result.Name = "result";
            result.Size = new Size(146, 36);
            result.TabIndex = 16;
            result.Text = "0";
            // 
            // removeBtn
            // 
            removeBtn.Font = new Font("Segoe UI", 12F);
            removeBtn.Location = new Point(80, 412);
            removeBtn.Name = "removeBtn";
            removeBtn.Size = new Size(115, 33);
            removeBtn.TabIndex = 17;
            removeBtn.Text = "مسح";
            removeBtn.UseVisualStyleBackColor = true;
            removeBtn.Click += RemoveBtn_Click;
            // 
            // CashCalculatorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(298, 457);
            Controls.Add(removeBtn);
            Controls.Add(result);
            Controls.Add(resultLbl);
            Controls.Add(twoHundredLbl);
            Controls.Add(twoHundred);
            Controls.Add(hundredLbl);
            Controls.Add(hundred);
            Controls.Add(fiftyLbl);
            Controls.Add(fifty);
            Controls.Add(twentyLbl);
            Controls.Add(twenty);
            Controls.Add(tenLbl);
            Controls.Add(ten);
            Controls.Add(fiveLbl);
            Controls.Add(five);
            Controls.Add(oneLbl);
            Controls.Add(one);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "CashCalculatorForm";
            Text = "حاسبة الفلوس";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox one;
        private Label oneLbl;
        private Label fiveLbl;
        private TextBox five;
        private Label tenLbl;
        private TextBox ten;
        private Label twentyLbl;
        private TextBox twenty;
        private Label fiftyLbl;
        private TextBox fifty;
        private Label hundredLbl;
        private TextBox hundred;
        private Label twoHundredLbl;
        private TextBox twoHundred;
        private Label resultLbl;
        private TextBox result;
        private Button removeBtn;
    }
}
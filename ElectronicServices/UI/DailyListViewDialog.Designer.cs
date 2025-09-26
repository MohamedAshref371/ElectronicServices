namespace ElectronicServices
{
    partial class DailyListViewDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyListViewDialog));
            listView1 = new ListView();
            dateLabel = new Label();
            datePicker = new DateTimePicker();
            saveDataBtn = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.BackColor = Color.Ivory;
            listView1.Font = new Font("Tahoma", 15.6F);
            listView1.ForeColor = Color.Black;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Location = new Point(0, 49);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.RightToLeft = RightToLeft.Yes;
            listView1.RightToLeftLayout = true;
            listView1.Size = new Size(520, 601);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.KeyPress += ListView1_KeyPress;
            // 
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Font = new Font("Tahoma", 15.6F);
            dateLabel.Location = new Point(422, 13);
            dateLabel.Name = "dateLabel";
            dateLabel.RightToLeft = RightToLeft.Yes;
            dateLabel.Size = new Size(86, 25);
            dateLabel.TabIndex = 2;
            dateLabel.Text = "التاريخ : ";
            dateLabel.DoubleClick += DateLabel_DoubleClick;
            // 
            // datePicker
            // 
            datePicker.CustomFormat = "  yyyy - MM MMMM - dd     HH : mm : ss";
            datePicker.Font = new Font("Segoe UI", 11.7F);
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.Location = new Point(92, 12);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(333, 28);
            datePicker.TabIndex = 3;
            datePicker.Value = new DateTime(2025, 9, 11, 15, 46, 5, 0);
            // 
            // saveDataBtn
            // 
            saveDataBtn.Font = new Font("Segoe UI", 11.7F);
            saveDataBtn.Location = new Point(12, 7);
            saveDataBtn.Name = "saveDataBtn";
            saveDataBtn.Size = new Size(74, 36);
            saveDataBtn.TabIndex = 4;
            saveDataBtn.Text = "حفظ";
            saveDataBtn.UseVisualStyleBackColor = true;
            saveDataBtn.Click += SaveDataBtn_Click;
            // 
            // DailyListViewDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(520, 650);
            Controls.Add(saveDataBtn);
            Controls.Add(datePicker);
            Controls.Add(dateLabel);
            Controls.Add(listView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DailyListViewDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "التقفيل اليومي";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label dateLabel;
        private DateTimePicker datePicker;
        private Button saveDataBtn;
    }
}
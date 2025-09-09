namespace ElectronicServices
{
    partial class ElecListViewDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElecListViewDialog));
            listView1 = new ListView();
            dateLabel = new Label();
            datePicker = new DateTimePicker();
            button1 = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.BackColor = Color.Ivory;
            listView1.Font = new Font("Tahoma", 12F);
            listView1.ForeColor = Color.Black;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Location = new Point(0, 38);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.RightToLeft = RightToLeft.Yes;
            listView1.RightToLeftLayout = true;
            listView1.Size = new Size(400, 462);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Font = new Font("Tahoma", 12F);
            dateLabel.Location = new Point(350, 9);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new Size(50, 19);
            dateLabel.TabIndex = 2;
            dateLabel.Text = "التاريخ";
            // 
            // datePicker
            // 
            datePicker.CustomFormat = "  yyyy - MM MMMM - dd";
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.Location = new Point(156, 7);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(188, 23);
            datePicker.TabIndex = 3;
            datePicker.Value = new DateTime(2025, 9, 9, 15, 46, 0, 0);
            // 
            // button1
            // 
            button1.Location = new Point(12, 7);
            button1.Name = "button1";
            button1.Size = new Size(93, 26);
            button1.TabIndex = 4;
            button1.Text = "إعتمد القيم";
            button1.UseVisualStyleBackColor = true;
            // 
            // ElecListViewDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(400, 500);
            Controls.Add(button1);
            Controls.Add(datePicker);
            Controls.Add(dateLabel);
            Controls.Add(listView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ElecListViewDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "مساعد الحقول";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label dateLabel;
        private DateTimePicker datePicker;
        private Button button1;
    }
}
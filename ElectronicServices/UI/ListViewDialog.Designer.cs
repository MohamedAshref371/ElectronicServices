namespace ElectronicServices
{
    partial class ListViewDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListViewDialog));
            listView1 = new ListView();
            textBox1 = new TextBox();
            label1 = new Label();
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
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 16F);
            textBox1.Location = new Point(12, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(423, 36);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += TextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 16F);
            label1.Location = new Point(444, 12);
            label1.Name = "label1";
            label1.Size = new Size(67, 27);
            label1.TabIndex = 2;
            label1.Text = "البحث";
            // 
            // ListViewDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(520, 650);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(listView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ListViewDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "مساعد الحقول";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}
namespace ElectronicServices
{
    partial class ExpenseRow
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
            title = new Label();
            amount = new Label();
            editBtn = new Guna.UI2.WinForms.Guna2Button();
            deleteBtn = new Guna.UI2.WinForms.Guna2Button();
            date = new Label();
            amountEdit = new Guna.UI2.WinForms.Guna2NumericUpDown();
            attachmentBtn = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)amountEdit).BeginInit();
            SuspendLayout();
            // 
            // title
            // 
            title.BackColor = Color.Transparent;
            title.Font = new Font("Segoe UI", 12F);
            title.Location = new Point(463, 0);
            title.Name = "title";
            title.Size = new Size(457, 40);
            title.TabIndex = 1;
            title.Text = "العنوان";
            title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // amount
            // 
            amount.BackColor = Color.Transparent;
            amount.Font = new Font("Segoe UI", 12F);
            amount.ForeColor = Color.Maroon;
            amount.Location = new Point(141, 0);
            amount.Name = "amount";
            amount.Size = new Size(130, 40);
            amount.TabIndex = 4;
            amount.Text = "المبلغ";
            amount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // editBtn
            // 
            editBtn.BackColor = Color.Transparent;
            editBtn.BorderRadius = 15;
            editBtn.CustomizableEdges = customizableEdges1;
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
            editBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            editBtn.Size = new Size(40, 40);
            editBtn.TabIndex = 6;
            editBtn.Click += EditBtn_Click;
            // 
            // deleteBtn
            // 
            deleteBtn.BackColor = Color.Transparent;
            deleteBtn.BorderRadius = 15;
            deleteBtn.CustomizableEdges = customizableEdges3;
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
            deleteBtn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            deleteBtn.Size = new Size(40, 40);
            deleteBtn.TabIndex = 7;
            deleteBtn.Click += DeleteBtn_Click;
            // 
            // date
            // 
            date.BackColor = Color.Transparent;
            date.Font = new Font("Segoe UI", 12F);
            date.ForeColor = Color.Black;
            date.Location = new Point(277, 0);
            date.Name = "date";
            date.Size = new Size(180, 40);
            date.TabIndex = 8;
            date.Text = "التاريخ";
            date.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // amountEdit
            // 
            amountEdit.BackColor = Color.Transparent;
            amountEdit.BorderColor = Color.Silver;
            amountEdit.BorderRadius = 10;
            amountEdit.CustomizableEdges = customizableEdges5;
            amountEdit.DecimalPlaces = 2;
            amountEdit.Font = new Font("Segoe UI", 12F);
            amountEdit.Location = new Point(141, 2);
            amountEdit.Maximum = new decimal(new int[] { 500000, 0, 0, 0 });
            amountEdit.Name = "amountEdit";
            amountEdit.ShadowDecoration.CustomizableEdges = customizableEdges6;
            amountEdit.Size = new Size(130, 36);
            amountEdit.TabIndex = 23;
            amountEdit.UpDownButtonFillColor = Color.FromArgb(255, 148, 94);
            amountEdit.Visible = false;
            // 
            // attachmentBtn
            // 
            attachmentBtn.BackColor = Color.Transparent;
            attachmentBtn.BorderRadius = 15;
            attachmentBtn.CustomizableEdges = customizableEdges7;
            attachmentBtn.DisabledState.BorderColor = Color.DarkGray;
            attachmentBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            attachmentBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            attachmentBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            attachmentBtn.FillColor = Color.Transparent;
            attachmentBtn.Font = new Font("Segoe UI", 9F);
            attachmentBtn.ForeColor = Color.White;
            attachmentBtn.Image = Properties.Resources.search;
            attachmentBtn.ImageSize = new Size(40, 40);
            attachmentBtn.Location = new Point(95, 0);
            attachmentBtn.Name = "attachmentBtn";
            attachmentBtn.ShadowDecoration.CustomizableEdges = customizableEdges8;
            attachmentBtn.Size = new Size(40, 40);
            attachmentBtn.TabIndex = 24;
            attachmentBtn.SizeChanged += AttachmentBtn_SizeChanged;
            attachmentBtn.MouseClick += AttachmentBtn_MouseClick;
            // 
            // ExpenseRow
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(50, 255, 255, 255);
            Controls.Add(attachmentBtn);
            Controls.Add(amountEdit);
            Controls.Add(date);
            Controls.Add(deleteBtn);
            Controls.Add(editBtn);
            Controls.Add(amount);
            Controls.Add(title);
            Name = "ExpenseRow";
            Size = new Size(920, 40);
            ((System.ComponentModel.ISupportInitialize)amountEdit).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label title;
        private Label amount;
        private Guna.UI2.WinForms.Guna2Button editBtn;
        private Guna.UI2.WinForms.Guna2Button deleteBtn;
        private Label date;
        private Guna.UI2.WinForms.Guna2NumericUpDown amountEdit;
        private Guna.UI2.WinForms.Guna2Button attachmentBtn;
    }
}

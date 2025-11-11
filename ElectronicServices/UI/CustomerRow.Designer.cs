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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            codeLabel = new Label();
            nameLabel = new Label();
            payLabel = new Label();
            takeLabel = new Label();
            resultLabel = new Label();
            transBtn = new Guna.UI2.WinForms.Guna2Button();
            editBtn = new Guna.UI2.WinForms.Guna2Button();
            deleteBtn = new Guna.UI2.WinForms.Guna2Button();
            customerName = new Guna.UI2.WinForms.Guna2TextBox();
            page = new Label();
            SuspendLayout();
            // 
            // codeLabel
            // 
            codeLabel.Font = new Font("Segoe UI", 12F);
            codeLabel.Location = new Point(877, 0);
            codeLabel.Name = "codeLabel";
            codeLabel.Size = new Size(40, 40);
            codeLabel.TabIndex = 0;
            codeLabel.Text = "ك";
            codeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            nameLabel.Font = new Font("Segoe UI", 12F);
            nameLabel.Location = new Point(489, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(382, 40);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "اسم العميل";
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // payLabel
            // 
            payLabel.Font = new Font("Segoe UI", 12F);
            payLabel.ForeColor = Color.Navy;
            payLabel.Location = new Point(383, 0);
            payLabel.Name = "payLabel";
            payLabel.Size = new Size(100, 40);
            payLabel.TabIndex = 2;
            payLabel.Text = "دفع";
            payLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // takeLabel
            // 
            takeLabel.Font = new Font("Segoe UI", 12F);
            takeLabel.ForeColor = Color.Maroon;
            takeLabel.Location = new Point(277, 0);
            takeLabel.Name = "takeLabel";
            takeLabel.Size = new Size(100, 40);
            takeLabel.TabIndex = 3;
            takeLabel.Text = "أخذ";
            takeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // resultLabel
            // 
            resultLabel.Font = new Font("Segoe UI", 12F);
            resultLabel.ForeColor = Color.FromArgb(0, 32, 0);
            resultLabel.Location = new Point(141, 0);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(130, 40);
            resultLabel.TabIndex = 4;
            resultLabel.Text = "الرصيد";
            resultLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // transBtn
            // 
            transBtn.BackColor = Color.Transparent;
            transBtn.BorderRadius = 15;
            transBtn.CustomizableEdges = customizableEdges1;
            transBtn.DisabledState.BorderColor = Color.DarkGray;
            transBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            transBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            transBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            transBtn.FillColor = Color.Transparent;
            transBtn.Font = new Font("Segoe UI", 9F);
            transBtn.ForeColor = Color.White;
            transBtn.Image = Properties.Resources.board;
            transBtn.ImageSize = new Size(40, 40);
            transBtn.Location = new Point(95, 0);
            transBtn.Name = "transBtn";
            transBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            transBtn.Size = new Size(40, 40);
            transBtn.TabIndex = 5;
            transBtn.SizeChanged += TransBtn_SizeChanged;
            transBtn.Click += TransBtn_Click;
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
            // customerName
            // 
            customerName.BorderRadius = 8;
            customerName.CustomizableEdges = customizableEdges7;
            customerName.DefaultText = "";
            customerName.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            customerName.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            customerName.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            customerName.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            customerName.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            customerName.Font = new Font("Segoe UI", 12F);
            customerName.ForeColor = Color.Black;
            customerName.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            customerName.Location = new Point(490, 0);
            customerName.Margin = new Padding(4);
            customerName.Name = "customerName";
            customerName.PlaceholderText = "";
            customerName.RightToLeft = RightToLeft.Yes;
            customerName.SelectedText = "";
            customerName.ShadowDecoration.CustomizableEdges = customizableEdges8;
            customerName.Size = new Size(380, 40);
            customerName.TabIndex = 8;
            customerName.Visible = false;
            customerName.KeyPress += TextBox_KeyPress;
            // 
            // page
            // 
            page.Font = new Font("Segoe UI", 12F);
            page.ForeColor = Color.Black;
            page.Location = new Point(0, 0);
            page.Name = "page";
            page.Size = new Size(135, 40);
            page.TabIndex = 26;
            page.TextAlign = ContentAlignment.MiddleCenter;
            page.Visible = false;
            page.MouseClick += Page_MouseClick;
            // 
            // CustomerRow
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(180, 240, 180);
            Controls.Add(customerName);
            Controls.Add(deleteBtn);
            Controls.Add(editBtn);
            Controls.Add(transBtn);
            Controls.Add(resultLabel);
            Controls.Add(takeLabel);
            Controls.Add(payLabel);
            Controls.Add(nameLabel);
            Controls.Add(codeLabel);
            Controls.Add(page);
            Name = "CustomerRow";
            Size = new Size(920, 40);
            ResumeLayout(false);
        }

        #endregion

        private Label codeLabel;
        private Label nameLabel;
        private Label payLabel;
        private Label takeLabel;
        private Label resultLabel;
        private Guna.UI2.WinForms.Guna2Button transBtn;
        private Guna.UI2.WinForms.Guna2Button editBtn;
        private Guna.UI2.WinForms.Guna2Button deleteBtn;
        private Guna.UI2.WinForms.Guna2TextBox customerName;
        private Label page;
    }
}

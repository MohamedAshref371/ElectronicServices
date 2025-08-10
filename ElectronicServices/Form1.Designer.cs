namespace ElectronicServices
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            headerPanel = new Guna.UI2.WinForms.Guna2GradientPanel();
            minimizeBtn = new Guna.UI2.WinForms.Guna2Button();
            maximizeBtn = new Guna.UI2.WinForms.Guna2Button();
            exitBtn = new Guna.UI2.WinForms.Guna2Button();
            formIcon = new Label();
            formTitle = new Label();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 20;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.ResizeForm = false;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // headerPanel
            // 
            headerPanel.BorderRadius = 10;
            headerPanel.Controls.Add(minimizeBtn);
            headerPanel.Controls.Add(maximizeBtn);
            headerPanel.Controls.Add(exitBtn);
            headerPanel.Controls.Add(formIcon);
            headerPanel.Controls.Add(formTitle);
            headerPanel.CustomizableEdges = customizableEdges15;
            headerPanel.FillColor = Color.FromArgb(192, 192, 255);
            headerPanel.FillColor2 = Color.FromArgb(128, 128, 255);
            headerPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges16;
            headerPanel.Size = new Size(950, 45);
            headerPanel.TabIndex = 0;
            // 
            // minimizeBtn
            // 
            minimizeBtn.BackColor = Color.Transparent;
            minimizeBtn.BorderColor = Color.Transparent;
            minimizeBtn.BorderRadius = 8;
            minimizeBtn.CustomizableEdges = customizableEdges9;
            minimizeBtn.DisabledState.BorderColor = Color.DarkGray;
            minimizeBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            minimizeBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            minimizeBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            minimizeBtn.FillColor = Color.Transparent;
            minimizeBtn.Font = new Font("Segoe UI", 9F);
            minimizeBtn.ForeColor = Color.Transparent;
            minimizeBtn.Image = Properties.Resources.WindowMinimize;
            minimizeBtn.ImageSize = new Size(32, 32);
            minimizeBtn.Location = new Point(88, 3);
            minimizeBtn.Name = "minimizeBtn";
            minimizeBtn.ShadowDecoration.CustomizableEdges = customizableEdges10;
            minimizeBtn.Size = new Size(32, 32);
            minimizeBtn.TabIndex = 4;
            minimizeBtn.Click += MinimizeBtn_Click;
            // 
            // maximizeBtn
            // 
            maximizeBtn.BackColor = Color.Transparent;
            maximizeBtn.BorderColor = Color.Transparent;
            maximizeBtn.BorderRadius = 8;
            maximizeBtn.CustomizableEdges = customizableEdges11;
            maximizeBtn.DisabledState.BorderColor = Color.DarkGray;
            maximizeBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            maximizeBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            maximizeBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            maximizeBtn.FillColor = Color.Transparent;
            maximizeBtn.Font = new Font("Segoe UI", 9F);
            maximizeBtn.ForeColor = Color.Transparent;
            maximizeBtn.Image = Properties.Resources.Fullscreen;
            maximizeBtn.ImageSize = new Size(32, 32);
            maximizeBtn.Location = new Point(50, 3);
            maximizeBtn.Name = "maximizeBtn";
            maximizeBtn.ShadowDecoration.CustomizableEdges = customizableEdges12;
            maximizeBtn.Size = new Size(32, 32);
            maximizeBtn.TabIndex = 3;
            maximizeBtn.Click += MaximizeBtn_Click;
            // 
            // exitBtn
            // 
            exitBtn.BackColor = Color.Transparent;
            exitBtn.BorderColor = Color.Transparent;
            exitBtn.BorderRadius = 8;
            exitBtn.CustomizableEdges = customizableEdges13;
            exitBtn.DisabledState.BorderColor = Color.DarkGray;
            exitBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            exitBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            exitBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            exitBtn.FillColor = Color.Transparent;
            exitBtn.Font = new Font("Segoe UI", 9F);
            exitBtn.ForeColor = Color.Transparent;
            exitBtn.Image = Properties.Resources.ExitToApp;
            exitBtn.ImageSize = new Size(32, 32);
            exitBtn.Location = new Point(12, 3);
            exitBtn.Name = "exitBtn";
            exitBtn.ShadowDecoration.CustomizableEdges = customizableEdges14;
            exitBtn.Size = new Size(32, 32);
            exitBtn.TabIndex = 2;
            exitBtn.Click += ExitBtn_Click;
            // 
            // formIcon
            // 
            formIcon.BackColor = Color.Transparent;
            formIcon.ForeColor = Color.Transparent;
            formIcon.Image = Properties.Resources.Icon32;
            formIcon.Location = new Point(906, 6);
            formIcon.Name = "formIcon";
            formIcon.Size = new Size(32, 32);
            formIcon.TabIndex = 1;
            // 
            // formTitle
            // 
            formTitle.AutoSize = true;
            formTitle.BackColor = Color.Transparent;
            formTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            formTitle.Location = new Point(734, 7);
            formTitle.Name = "formTitle";
            formTitle.Size = new Size(162, 32);
            formTitle.TabIndex = 0;
            formTitle.Text = "الفتح للخدمـــات";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 700);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "Form1";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2GradientPanel headerPanel;
        private Label formTitle;
        private Label formIcon;
        private Guna.UI2.WinForms.Guna2Button exitBtn;
        private Guna.UI2.WinForms.Guna2Button maximizeBtn;
        private Guna.UI2.WinForms.Guna2Button minimizeBtn;
    }
}

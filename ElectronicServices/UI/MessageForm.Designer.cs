namespace ElectronicServices
{
    partial class MessageForm
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
            yesBtn = new Button();
            noBtn = new Button();
            cancelBtn = new Button();
            text = new Label();
            iconBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)iconBox).BeginInit();
            SuspendLayout();
            // 
            // yesBtn
            // 
            yesBtn.DialogResult = DialogResult.Yes;
            yesBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            yesBtn.ForeColor = Color.Navy;
            yesBtn.ImeMode = ImeMode.NoControl;
            yesBtn.Location = new Point(291, 125);
            yesBtn.Name = "yesBtn";
            yesBtn.Size = new Size(85, 33);
            yesBtn.TabIndex = 3;
            yesBtn.Text = "نعم";
            yesBtn.UseVisualStyleBackColor = true;
            // 
            // noBtn
            // 
            noBtn.DialogResult = DialogResult.No;
            noBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            noBtn.ForeColor = Color.Green;
            noBtn.ImeMode = ImeMode.NoControl;
            noBtn.Location = new Point(200, 125);
            noBtn.Name = "noBtn";
            noBtn.Size = new Size(85, 33);
            noBtn.TabIndex = 2;
            noBtn.Text = "لا";
            noBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            cancelBtn.DialogResult = DialogResult.Cancel;
            cancelBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            cancelBtn.ForeColor = Color.Maroon;
            cancelBtn.ImeMode = ImeMode.NoControl;
            cancelBtn.Location = new Point(109, 125);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(85, 33);
            cancelBtn.TabIndex = 1;
            cancelBtn.Text = "إلغاء";
            cancelBtn.UseVisualStyleBackColor = true;
            // 
            // text
            // 
            text.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            text.ImeMode = ImeMode.NoControl;
            text.Location = new Point(12, 7);
            text.Name = "text";
            text.RightToLeft = RightToLeft.Yes;
            text.Size = new Size(360, 115);
            text.TabIndex = 0;
            text.Text = "رسالة";
            text.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // iconBox
            // 
            iconBox.BackgroundImageLayout = ImageLayout.Zoom;
            iconBox.Location = new Point(378, 35);
            iconBox.Name = "iconBox";
            iconBox.Size = new Size(64, 64);
            iconBox.TabIndex = 4;
            iconBox.TabStop = false;
            // 
            // MessageForm
            // 
            AcceptButton = yesBtn;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelBtn;
            ClientSize = new Size(454, 161);
            Controls.Add(iconBox);
            Controls.Add(yesBtn);
            Controls.Add(noBtn);
            Controls.Add(cancelBtn);
            Controls.Add(text);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MessageForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تأكيد";
            Load += MessageForm_Load;
            ((System.ComponentModel.ISupportInitialize)iconBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button yesBtn;
        private Button noBtn;
        private Button cancelBtn;
        private Label text;
        private PictureBox iconBox;
    }
}
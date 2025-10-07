using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace ElectronicServices
{
    public partial class ExpenseRow : UserControl
    {
        public ExpenseRow()
        {
            InitializeComponent();
            attachmentBtn.Visible = false;
            editBtn.Visible = false;
            deleteBtn.Visible = false;
        }
        
        private readonly ExpenseRowData data;
        public ExpenseRow(ExpenseRowData data)
        {
            InitializeComponent();
            this.data = data;
            title.Text = data.Title;
            date.Text = data.Date;
            amount.Text = data.Amount.ToString();
            if (data.Attachment == "")
                attachmentBtn.Image = null;
        }

        private void Title_DoubleClick(object sender, EventArgs e)
        {
            if (data.Comment != "")
                Form1.MessageForm(data.Comment, "ملاحظة", MessageBoxButtons.OK, MessageBoxIconV2.Information);
        }

        private void AttachmentBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (data.Attachment == "")
            {
                if (Form1.MessageForm("لا يوجد مرفق لهذا البند\nهل تريد إضافة مرفق له ؟", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIconV2.Question) != DialogResult.Yes)
                    return;
                
                data.Attachment = Program.Form.AttachmentPath();
                if (data.Attachment != "")
                {
                    if (!DatabaseHelper.EditExpense(data.Id, data.Attachment))
                    {
                        Form1.MessageForm("حدث خطأ أثناء حفظ البيانات\nيرجى المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                        data.Attachment = "";
                    }
                    else
                        attachmentBtn.Image = Properties.Resources.Folder_Explorer;
                }
                return;
            }

            if (!File.Exists(data.Attachment))
            {
                Form1.MessageForm("تعذر العثور على المرفق. قد يكون قد تم نقله أو حذفه.", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                return;
            }
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ProcessStartInfo psi = new() { FileName = data.Attachment, UseShellExecute = true };
                    Process.Start(psi);
                }
                else
                    Process.Start("explorer.exe", $"/select,\"{data.Attachment}\"");
            }
            catch (Exception ex)
            {
                Form1.MessageForm("حدث خطأ أثناء فتح المرفق:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                return;
            }
        }

        private void EditBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && amount.Visible)
            {
                if (Form1.MessageForm("هل تريد تحديث مسار المرفق لهذا البند ؟", "تنبيه", MessageBoxButtons.YesNo, MessageBoxIconV2.Question) != DialogResult.Yes)
                    return;

                string att = Program.Form.AttachmentPath();
                if (att != "")
                {
                    if (DatabaseHelper.EditExpense(data.Id, att))
                    {
                        attachmentBtn.Image = Properties.Resources.Folder_Explorer;
                        data.Attachment = att;
                    }
                    else
                        Form1.MessageForm("حدث خطأ أثناء حفظ البيانات\nيرجى المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                }
                return;
            }

            if (e.Button == MouseButtons.Right)
                return;

            if (amount.Visible)
            {
                amountEdit.Value = (decimal)data.Amount;
                amount.Visible = false;
                amountEdit.Visible = true;
                editBtn.Image = Properties.Resources.check_mark_button;
            }
            else
            {
                amount.Visible = true;
                amountEdit.Visible = false;
                editBtn.Image = Properties.Resources.editIcon;

                float val = (float)amountEdit.Value;
                if (!DatabaseHelper.EditExpense(data.Id, val))
                {
                    Form1.MessageForm("حدث خطأ أثناء حفظ البيانات\nيرجى المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                    return;
                }

                data.Amount = val;
                amount.Text = val.ToString();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Form1.MessageForm("هل أنت متأكد من حذف هذا البند ؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIconV2.Delete) != DialogResult.Yes)
                return;

            if (!DatabaseHelper.DeleteExpense(data.Id))
            {
                Form1.MessageForm("حدث خطأ أثناء حذف البند. يرجى المحاولة مرة أخرى.", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                return;
            }

            data.Id = 0;
            this.Enabled = false;
        }

        #region Border Radius
        private readonly int borderRadius = 20;

        private GraphicsPath GetRoundedPath()
        {
            GraphicsPath path = new();
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(Width - borderRadius, Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, Height - borderRadius, borderRadius, borderRadius, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.Region = new Region(GetRoundedPath());
        }
        #endregion

        private void AttachmentBtn_SizeChanged(object sender, EventArgs e)
        {
            int minSize = Math.Min(attachmentBtn.Width, attachmentBtn.Height);
            Size newSize = new(minSize, minSize);

            attachmentBtn.ImageSize = newSize;
            editBtn.ImageSize = newSize;
            deleteBtn.ImageSize = newSize;
        }
    }
}

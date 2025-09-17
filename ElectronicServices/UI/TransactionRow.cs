using System.Drawing.Drawing2D;

namespace ElectronicServices
{
    public partial class TransactionRow : UserControl
    {
        public TransactionRow()
        {
            InitializeComponent();
            deleteTransactionBtn.Visible = false;
            customerBtn.Visible = false;
            editBtn.Visible = false;
            infoBtn.Visible = false;
        }

        private TransactionRowData data;
        public TransactionRow(TransactionRowData data)
        {
            InitializeComponent();
            this.data = data;
            nameLabel.Text = data.Name;
            dateLabel.Text = data.Date[..10];
            if (data.Note != "") data.Note = "\nملاحظات : " + data.Note;
            SetTransactionRowData(data);
        }

        string result;
        private void SetTransactionRowData(TransactionRowData data)
        {
            payLabel.Text = data.Pay.ToString();
            takeLabel.Text = data.Take.ToString();

            if (data.Pay > data.Take)
            {
                result = "له ";
                result += (data.Pay - data.Take).ToString();
            }
            else if (data.Take > data.Pay)
            {
                result = "عليه ";
                result += (data.Take - data.Pay).ToString();
            }
            else
                result = "صفر";
        }

        private void InfoBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                float? before = DatabaseHelper.GetTransactionBefore(data.Id, data.CustomerId);
                if (before is null)
                    MessageBox.Show("حدث خطأ أثناء قراءة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string text;
                    if (before < 0f)
                        text = "عليه " + ((float)-before).ToString();
                    else if (before > 0f)
                        text = "له " + ((float)before).ToString();
                    else
                        text = "صفر";

                    float after = (float)before + data.Pay - data.Take;

                    string text2;
                    if (after < 0f)
                        text2 = "عليه " + ((float)-after).ToString();
                    else if (after > 0f)
                        text2 = "له " + ((float)after).ToString();
                    else
                        text2 = "صفر";

                    MessageBox.Show($"الرصيد قبل المعاملة : {text}\nالرصيد بعد المعاملة : {text2}", "معلومات المعاملة", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            else if (e.Button == MouseButtons.Left)
                MessageBox.Show($"نتيجة فرق المعاملة : {result}\n{data.Note}", "ملاحظات", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (payLabel.Visible)
            {
                payLabel.Visible = false;
                takeLabel.Visible = false;
                payEdit.Value = (decimal)data.Pay;
                takeEdit.Value = (decimal)data.Take;
                payEdit.Visible = true;
                takeEdit.Visible = true;
                editBtn.Image = Properties.Resources.check_mark_button;
            }
            else
            {
                payEdit.Visible = false;
                takeEdit.Visible = false;
                payLabel.Visible = true;
                takeLabel.Visible = true;
                editBtn.Image = Properties.Resources.editIcon;

                if (!DatabaseHelper.EditTransaction(data.Id, (float)payEdit.Value, (float)takeEdit.Value))
                {
                    MessageBox.Show("حدث خطأ أثناء حفظ البيانات\nيرجى المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                data.Pay = (float)payEdit.Value;
                data.Take = (float)takeEdit.Value;
                SetTransactionRowData(data);
            }
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
            => Program.Form.CustomerBtnClickInTransactionRow(data.Name);

        private void DeleteTransactionBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد من حذف هذه المعاملة؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (!DatabaseHelper.DeleteTransaction(data.Id))
            {
                MessageBox.Show("حدث خطأ أثناء حذف المعاملة\nيرجى المحاولة مرة أخرى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Enabled = false;
            //this.Dispose();
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

        private void InfoBtn_SizeChanged(object sender, EventArgs e)
        {
            int minSize = Math.Min(infoBtn.Width, infoBtn.Height);
            Size newSize = new(minSize, minSize);

            infoBtn.ImageSize = newSize;
            editBtn.ImageSize = newSize;
            customerBtn.ImageSize = newSize;
            deleteTransactionBtn.ImageSize = newSize;
        }
    }
}

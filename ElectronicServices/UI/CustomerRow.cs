using System.Drawing.Drawing2D;

namespace ElectronicServices
{
    public partial class CustomerRow : UserControl
    {
        public CustomerRow()
        {
            InitializeComponent();
            transBtn.Visible = false;
            editBtn.Visible = false;
            deleteBtn.Visible = false;
        }

        public int Id { get; private set; }
        float pay, take;

        public CustomerRow(CustomerRowData data)
        {
            InitializeComponent();
            SetData(data);
        }

        public void SetData(CustomerRowData data)
        {
            this.Id = data.Id;
            codeLabel.Text = data.Id.ToString();
            nameLabel.Text = data.Name;
            SetCustomerRowData(data.Pay, data.Take);
        }

        private void SetCustomerRowData(float pay, float take)
        {
            this.pay = pay;
            this.take = take;
            payLabel.Text = pay.ToString();
            takeLabel.Text = take.ToString();

            if (pay > take)
            {
                resultLabel.Text = "له ";
                resultLabel.Text += (pay - take).ToString();
            }
            else if (take > pay)
            {
                resultLabel.Text = "عليه ";
                resultLabel.Text += (take - pay).ToString();
            }
            else
                resultLabel.Text = "صفر";
        }

        public void SetPayTakePlus(float payP, float takeP)
            => SetCustomerRowData(pay + payP, take + takeP);


        private void TransBtn_Click(object sender, EventArgs e)
            => Program.Form.CustomerRowTransactions(Id);

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (nameLabel.Visible)
            {
                nameLabel.Visible = false;
                customerName.Text = nameLabel.Text;
                customerName.Visible = true;
                editBtn.Image = Properties.Resources.check_mark_button;
            }
            else
            {
                customerName.Visible = false;
                customerName.Text = customerName.Text.Trim();
                nameLabel.Visible = true;
                editBtn.Image = Properties.Resources.editIcon;

                if (nameLabel.Text == customerName.Text) return;

                int res = DatabaseHelper.SearchWithExactCustomerName(customerName.Text);
                if (res < 0)
                {
                    Form1.MessageForm("حدث خطأ أثناء قراءة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                    return;
                }
                if (res >= 1)
                {
                    Form1.MessageForm("هذا العميل موجود بالفعل", "تحذير", MessageBoxButtons.OK, MessageBoxIconV2.Warning);
                    return;
                }

                if (!DatabaseHelper.EditCustomer(Id, customerName.Text))
                {
                    Form1.MessageForm("حدث خطأ أثناء حفظ البيانات. يرجى المحاولة مرة أخرى.", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                    return;
                }

                nameLabel.Text = customerName.Text;
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            bool res = DatabaseHelper.IsThereTransactions(Id);

            if (res && Form1.MessageForm("لا يمكن حذف هذا العميل لأنه مرتبط بمعاملات\nهل تريد تصفير هذا العميل أولا؟", "تأكيد تصفير العميل", MessageBoxButtons.OKCancel, MessageBoxIconV2.Delete) != DialogResult.OK)
                return;

            if (res)
            {
                if (!DatabaseHelper.ResetCustomer(Id))
                {
                    Form1.MessageForm("حدث خطأ أثناء تصفير العميل. يرجى المحاولة مرة أخرى.", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                    return;
                }

                payLabel.Text = 0.ToString();
                takeLabel.Text = 0.ToString();
                resultLabel.Text = "صفر";
                Program.Form.DeleteTransactions(Id);
                return;
            }

            if (Form1.MessageForm("هل أنت متأكد من حذف هذا العميل؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIconV2.Delete) != DialogResult.Yes)
                return;

            if (!DatabaseHelper.DeleteCustomer(Id))
            {
                Form1.MessageForm("حدث خطأ أثناء حذف العميل. يرجى المحاولة مرة أخرى.", "خطأ", MessageBoxButtons.OK, MessageBoxIconV2.Error);
                return;
            }

            this.Enabled = false;
            Id = 0;
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

        private void TransBtn_SizeChanged(object sender, EventArgs e)
        {
            int minSize = Math.Min(transBtn.Width, transBtn.Height);
            Size newSize = new(minSize, minSize);

            transBtn.ImageSize = newSize;
            editBtn.ImageSize = newSize;
            deleteBtn.ImageSize = newSize;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'')
                e.Handled = true;
        }
    }
}

using System.Drawing.Drawing2D;

namespace ElectronicServices
{
    public partial class CustomerRow : UserControl
    {
        public CustomerRow()
        {
            InitializeComponent();
            customerTransactionsBtn.Visible = false;
            editBtn.Visible = false;
            deleteCustomerBtn.Visible = false;
        }

        public int Id { get; private set; }
        float pay, take;

        public CustomerRow(CustomerRowData data)
        {
            InitializeComponent();
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
        

        private void CustomerTransactionsBtn_Click(object sender, EventArgs e)
            => Program.Form.CustomerTransactionsBtnInCustomerRow(Id);

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
                    MessageBox.Show("حدث خطأ أثناء قراءة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (res >= 1)
                {
                    MessageBox.Show("هذا العميل موجود بالفعل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!DatabaseHelper.EditCustomer(Id, customerName.Text))
                {
                    MessageBox.Show("حدث خطأ أثناء حفظ البيانات. يرجى المحاولة مرة أخرى.");
                    return;
                }

                nameLabel.Text = customerName.Text;
            }
        }

        private void DeleteCustomerBtn_Click(object sender, EventArgs e)
        {
            bool res = DatabaseHelper.IsThereTransactions(Id);

            if (res && MessageBox.Show("لا يمكن حذف هذا العميل لأنه مرتبط بمعاملات\nهل تريد تصفير هذا العميل أولا؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (res)
            {
                if (!DatabaseHelper.ResetCustomer(Id))
                {
                    MessageBox.Show("حدث خطأ أثناء تصفير العميل. يرجى المحاولة مرة أخرى.");
                    return;
                }

                payLabel.Text = 0.ToString();
                takeLabel.Text = 0.ToString();
                resultLabel.Text = "صفر";
                Program.Form.DeleteTransactions(Id);
                return;
            }

            if (MessageBox.Show("هل أنت متأكد من حذف هذا العميل؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (!DatabaseHelper.DeleteCustomer(Id))
            {
                MessageBox.Show("حدث خطأ أثناء حذف العميل. يرجى المحاولة مرة أخرى.");
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

        private void CustomerTransactionsBtn_SizeChanged(object sender, EventArgs e)
        {
            int minSize = Math.Min(customerTransactionsBtn.Width, customerTransactionsBtn.Height);
            Size newSize = new(minSize, minSize);

            customerTransactionsBtn.ImageSize = newSize;
            editBtn.ImageSize = newSize;
            deleteCustomerBtn.ImageSize = newSize;
        }
    }
}

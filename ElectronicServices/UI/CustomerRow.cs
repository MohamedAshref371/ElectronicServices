using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private int id;
        public CustomerRow(CustomerRowData data)
        {
            InitializeComponent();
           this.id = data.Id;
            codeLabel.Text = data.Id.ToString();
            nameLabel.Text = data.Name;
            payLabel.Text = data.Pay.ToString("N2");
            takeLabel.Text = data.Take.ToString("N2");

            if (data.Pay > data.Take)
            {
                resultLabel.Text = "له ";
                resultLabel.Text += (data.Pay - data.Take).ToString("N2");
            }
            else if (data.Take > data.Pay)
            {
                resultLabel.Text = "عليه ";
                resultLabel.Text += (data.Take - data.Pay).ToString("N2");
            }
            else
                resultLabel.Text = "صفر";
        }

        private void CustomerTransactionsBtn_Click(object sender, EventArgs e)
        {

        }

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

                if (!DatabaseHelper.EditCustomer(id, customerName.Text))
                {
                    MessageBox.Show("حدث خطأ أثناء حفظ البيانات. يرجى المحاولة مرة أخرى.");
                    return;
                }

                nameLabel.Text = customerName.Text;
            }
        }

        private void DeleteCustomerBtn_Click(object sender, EventArgs e)
        {
            int res = DatabaseHelper.IsThereTransactions(id);
            if (res < 0)
            {
                MessageBox.Show("حدث خطأ أثناء قراءة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (res > 0 && MessageBox.Show("لا يمكن حذف هذا العميل لأنه مرتبط بمعاملات\nهل تريد تصفير هذا العميل أولا؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (res > 0)
            {
                if (!DatabaseHelper.ResetCustomer(id))
                {
                    MessageBox.Show("حدث خطأ أثناء تصفير العميل. يرجى المحاولة مرة أخرى.");
                    return;
                }

                payLabel.Text = 0.ToString("N2");
                takeLabel.Text = 0.ToString("N2");
                resultLabel.Text = "صفر";
                return;
            }

            if (MessageBox.Show("هل أنت متأكد من حذف هذا العميل؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (!DatabaseHelper.DeleteCustomer(id))
            {
                MessageBox.Show("حدث خطأ أثناء حذف العميل. يرجى المحاولة مرة أخرى.");
                return;
            }

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

    }
}

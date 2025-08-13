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
    public partial class TransactionRow : UserControl
    {
        public TransactionRow()
        {
            InitializeComponent();
            deleteCustomerBtn.Visible = false;
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
            dateLabel.Text = data.Date.ToString("yyyy-MM-dd");
            SetTransactionRowData(data);
        }

        string result;
        private void SetTransactionRowData(TransactionRowData data)
        {
            payLabel.Text = data.Pay.ToString("N2");
            takeLabel.Text = data.Take.ToString("N2");

            if (data.Pay > data.Take)
            {
                result = "له ";
                result += (data.Pay - data.Take).ToString("N2");
            }
            else if (data.Take > data.Pay)
            {
                result = "عليه ";
                result += (data.Take - data.Pay).ToString("N2");
            }
            else
                result = "صفر";
        }


        private void InfoBtn_Click(object sender, EventArgs e)
            => MessageBox.Show($"الرصيد : {result}\nملاحظات : {data.Note}");

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
                    MessageBox.Show("حدث خطأ أثناء حفظ البيانات. يرجى المحاولة مرة أخرى.");
                    return;
                }
                data.Pay = (float)payEdit.Value;
                data.Take = (float)takeEdit.Value;
                SetTransactionRowData(data);
            }
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {

        }

        private void DeleteCustomerBtn_Click(object sender, EventArgs e)
        {

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

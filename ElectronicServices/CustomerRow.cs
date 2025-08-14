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
            deleteCustomerBtn.Visible = false;
        }

        public CustomerRow(CustomerRowData data)
        {
            InitializeComponent();
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

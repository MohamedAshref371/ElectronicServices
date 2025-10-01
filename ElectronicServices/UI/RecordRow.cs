using System.Drawing.Drawing2D;

namespace ElectronicServices
{
    public partial class RecordRow : UserControl
    {
        public RecordRow()
        {
            InitializeComponent();
        }

        public RecordRow(RecordRowData data)
        {
            InitializeComponent();
            phoneNumber.Text = data.Phone;
            phoneNumber.Tag = data.Comment;
            date.Text = data.Date[..10];
            date.Tag = data.Date[11..];
            withdRema.Text = data.WithdrawalRemaining.ToString();
            depoRema.Text = data.DepositRemaining.ToString();
            withdrawal.Text = data.Withdrawal.ToString();
            deposit.Text = data.Deposit.ToString();
            balance.Text = data.Balance.ToString();
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

        private void PhoneNumber_Click(object sender, EventArgs e)
        {
            string comment = phoneNumber.Tag.ToString();
            if (comment == "") return;
            MessageBox.Show(phoneNumber.Tag.ToString(), "ملاحظات العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Date_Click(object sender, EventArgs e)
        {
            MessageBox.Show(date.Tag.ToString(), "وقت العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
